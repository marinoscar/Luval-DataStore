using Luval.DataStore.DataAnnotations;
using Luval.DataStore.Database;
using Luval.DataStore.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Luval.DataStore
{
    /// <summary>
    /// Represents an implementation of <see cref="IDataRecordMapper"/> using reflection
    /// </summary>
    public class ReflectionDataRecordMapper : IDataRecordMapper
    {
        private readonly ICacheProvider<FieldAndType, PropertyInfo> _entityMapCache;
        private readonly ICacheProvider<Type, EntityMetadata> _metadataCache;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public ReflectionDataRecordMapper()
        {
            _entityMapCache = new MemoryCacheStore<FieldAndType, PropertyInfo>();
            _metadataCache = new MemoryCacheStore<Type, EntityMetadata>();
        }

        /// <inheritdoc/>
        public T FromDataRecord<T>(IDataRecord record)
        {
            return (T)Convert.ChangeType(FromDataRecord(record, typeof(T)), typeof(T));
        }

        /// <inheritdoc/>
        public object FromDataRecord(IDataRecord record, Type entityType)
        {
            var entity = Activator.CreateInstance(entityType);
            for (int i = 0; i < record.FieldCount; i++)
            {
                AssignFieldValueToEntity(record.GetName(i), ref entity, record.GetValue(i));
            }
            return entity;
        }

        /// <inheritdoc/>
        public IDataRecord ToDataRecord(object entity)
        {
            return new DataRecord(ToDictionary(entity));
        }

        private void AssignFieldValueToEntity(string fieldName, ref object entity, object value)
        {
            var p = GetEntityPropertyFromFieldName(fieldName, entity.GetType());
            if (p == null) return;
            if (DBNull.Value == value || value == null) value = GetDefaultValue(p.PropertyType);
            var typeToConvert = Nullable.GetUnderlyingType(p.PropertyType) ?? p.PropertyType;
            p.SetValue(entity, TryChangeType(value, typeToConvert));
        }

        private object TryChangeType(object val, Type type)
        {
            try
            {
                val = Convert.ChangeType(val, type);
            }
            catch (InvalidCastException)
            {
                if (val != null && (typeof(Guid) == val.GetType()))
                    val = ((Guid)val).ToString();
            }
            catch (Exception)
            {
            }
            return val;
        }

        private object GetDefaultValue(Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }

        private PropertyInfo GetEntityPropertyFromFieldName(string name, Type type)
        {
            var map = new FieldAndType() { Type = type, Field = name };
            return _entityMapCache.GetOrAdd(map, () => {
                PropertyInfo property;
                property = type.GetProperty(name);
                if (property == null)
                {
                    foreach (var prop in type.GetProperties())
                    {
                        var columnName = prop.GetCustomAttribute<ColumnNameAttribute>();
                        if (columnName == null) continue;
                        if (((ColumnNameAttribute)columnName).Name == name)
                        {
                            property = prop;
                            break;
                        }
                    }
                    if (property != null && property.GetCustomAttribute<NotMappedAttribute>() != null)
                        property = null;
                }
                else
                {
                    if (property.GetCustomAttribute<NotMappedAttribute>() != null)
                        property = null;
                }
                return property;
            });
        }

        private EntityMetadata GetEntityMetadata(Type entityType)
        {
            return _metadataCache.GetOrAdd(entityType, () => {
                var metaData = new EntityMetadata(entityType);
                foreach (var property in entityType.GetProperties())
                {
                    var field = new EntityFieldMetadata
                    {
                        Property = property,
                        IsMapped = !(property.GetCustomAttribute<NotMappedAttribute>() != null),
                        IsPrimitive = ObjectExtensions.IsPrimitiveType(property.PropertyType)
                    };
                    var colAtt = property.GetCustomAttribute<ColumnNameAttribute>();
                    field.DataFieldName = colAtt != null ? colAtt.Name : property.Name;
                    if (!field.IsPrimitive && field.IsMapped)
                    {
                        field.IsList = typeof(IEnumerable).IsAssignableFrom(property.PropertyType);
                        field.TableReference = TableReference.Create(property);
                        DbTableSchema.ValidateTableRef(field.TableReference, DbTableSchema.Create(entityType));
                    }
                    metaData.Fields.Add(field);
                }
                return metaData;
            });

        }

        private Dictionary<string, object> ToDictionary(object entity)
        {
            var type = entity.GetType();
            if (typeof(IDictionary<string, object>).IsAssignableFrom(type))
                return (Dictionary<string, object>)entity;
            if (typeof(IDataRecord).IsAssignableFrom(type))
                return (Dictionary<string, object>)((IDataRecord)entity).ToDictionary();

            var metaData = GetEntityMetadata(type);
            var record = new Dictionary<string, object>();
            foreach (var field in metaData.Fields)
            {
                if (!field.IsMapped) continue;
                var value = field.Property.GetValue(entity);
                if (field.IsPrimitive)
                    record[field.DataFieldName] = value;
                else
                {
                    if (field.IsList)
                    {
                        if (!value.IsNullOrDbNull())
                        {
                            var list = new List<IDataRecord>();
                            foreach (var item in (IEnumerable)value)
                                list.Add(ToDataRecord(item));
                            record[field.DataFieldName] = list;
                        }
                    }
                    else
                    {
                        if (field.TableReference != null && !record.ContainsKey(field.TableReference.ReferenceTableKey))
                        {
                            var parentMetaData = GetEntityMetadata(field.TableReference.EntityType);
                            var parentField = parentMetaData.Fields.FirstOrDefault(i => i.DataFieldName == field.TableReference.ReferenceTable.Columns.Where(c => c.IsPrimaryKey).First().ColumnName);
                            if (parentField != null)
                                record[field.TableReference.ReferenceTableKey] = parentField.Property.GetValue(value);
                        }
                    }
                }
            }
            return record;
        }

        private class FieldAndType { public string Field { get; set; } public Type Type { get; set; } }
    }
}
