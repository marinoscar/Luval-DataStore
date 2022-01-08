using Luval.DataStore.DataAnnotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Luval.DataStore.Database
{
    /// <summary>
    /// Provides an abstraction for the schema of a sql table
    /// </summary>
    public class DbTableSchema
    {

        private static readonly ICacheProvider<Type, DbTableSchema> _cache = new MemoryCacheStore<Type, DbTableSchema>();

        /// <summary>
        /// Gets or sets the <see cref="TableName"/> for the <see cref="DbTableSchema"/>
        /// </summary>
        public TableName TableName { get; set; }
        /// <summary>
        /// Gets or sets the <see cref="Type"/> for the entity
        /// </summary>
        public Type EntityType { get; set; }
        /// <summary>
        /// Gets or sets a <see cref="List{T}"/> of <see cref="DbColumnSchema"/>
        /// </summary>
        public List<DbColumnSchema> Columns { get; set; }

        /// <summary>
        /// Gets or sets a <see cref="List{T}"/> of <see cref="TableReference"/>
        /// </summary>
        public List<TableReference> References { get; set; }

        /// <summary>
        /// Creates a new instance of <see cref="DbTableSchema"/>
        /// </summary>
        /// <param name="type">The entity <see cref="Type"/> to use to create the schema metadata</param>
        /// <returns>A new instance of <see cref="DbTableSchema"/></returns>
        public static DbTableSchema Create(Type type)
        {
            return _cache.GetOrAdd(type, () =>
            {
                var columns = new List<DbColumnSchema>();
                var refs = new List<TableReference>();
                var res = new DbTableSchema() { TableName = GetTableName(type), Columns = columns, References = refs, EntityType = type };

                foreach (var prop in type.GetProperties())
                {
                    if (prop.GetCustomAttribute<NotMappedAttribute>() != null) continue;
                    if (prop.GetCustomAttribute<TableReferenceAttribute>() != null)
                    {
                        var tableRef = TableReference.Create(prop);
                        ValidateTableRef(tableRef, res);
                        refs.Add(tableRef);
                        continue;
                    }
                    columns.Add(DbColumnSchema.Create(prop));
                }
                if (!res.Columns.Any(i => i.IsPrimaryKey) && res.Columns.Any(i => i.ColumnName == "Id"))
                    res.Columns.Single(i => i.ColumnName == "Id").IsPrimaryKey = true;

                return res;
            });
        }

        /// <summary>
        /// Validates the references and provide the default values if required
        /// </summary>
        /// <param name="tableReference">The <see cref="TableReference"/> to validate</param>
        /// <param name="parent">The reference parent <see cref="DbTableSchema"/></param>
        public static void ValidateTableRef(TableReference tableReference, DbTableSchema parent)
        {
            if (!string.IsNullOrWhiteSpace(tableReference.ReferenceTableKey)) return;

            tableReference.ReferenceTableKey = tableReference.IsChild ?
                string.Format("{0}Id", parent.TableName.Name) :
                string.Format("{0}Id", tableReference.ReferenceTable.TableName.Name);
        }



        /// <summary>
        /// Gets the <see cref="TableName"/> from the entity <see cref="Type"/> metadata
        /// </summary>
        /// <param name="type">The entity <see cref="Type"/> to extract the metadata</param>
        /// <returns>An instance of <see cref="TableName"/></returns>
        public static TableName GetTableName(Type type)
        {
            var att = type.GetCustomAttribute<TableNameAttribute>();
            if (att == null) return new TableName(type.Name);
            return att.TableName;
        }

    }
}
