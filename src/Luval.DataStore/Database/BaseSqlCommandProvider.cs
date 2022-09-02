using Luval.DataStore.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Luval.DataStore.Database
{
    /// <inheritdoc/>
    public abstract class BaseSqlCommandProvider<TEntity> : IDataCommandProvider<TEntity> where TEntity : class
    {
        private readonly AnsiSqlExpressionPrinter _printer;
        private readonly DbTableSchema _schema;
        private readonly IDataRecordMapper _recordMapper;

        /// <summary>
        /// Create a new instance
        /// </summary>
        protected BaseSqlCommandProvider() : this(new ReflectionDataRecordMapper())
        {

        }

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="dataRecordMapper">The <see cref="IDataRecordMapper"/> implementation to extract data from the entities for the commands</param>
        protected BaseSqlCommandProvider(IDataRecordMapper dataRecordMapper)
        {
            _printer = new AnsiSqlExpressionPrinter();
            _schema = DbTableSchema.Create(typeof(TEntity));
            _recordMapper = dataRecordMapper;
        }

        #region Interface Implementation


        /// <inheritdoc/>
        public virtual IDataCommand GetDelete(TEntity data)
        {
            return GetDelete(GetDataRecord(data)).ToCommand();
        }

        /// <inheritdoc/>
        public virtual IDataCommand GetInsert(TEntity data)
        {
            return GetInsert(_schema, GetDataRecord(data)).ToCommand();
        }

        /// <inheritdoc/>
        public virtual IDataCommand GetUpdate(TEntity data)
        {
            return GetUpdate(GetDataRecord(data)).ToCommand();
        }

        /// <inheritdoc/>
        public virtual IDataCommand Query(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, object>> orderByExpression = null, bool descending = false)
        {
            return QueryCommand(filterExpression).ToCommand();
        }

        #endregion

        #region Sql Methods

        /// <summary>
        /// Generates a SQL insert statement
        /// </summary>
        /// <param name="schema">The <see cref="DbTableSchema"/> to use</param>
        /// <param name="record">The <see cref="IDataRecord"/> with the data for the statement</param>
        /// <param name="includeChildren">Indicates if for complex entities if should also create commands for the child entities</param>
        /// <returns>A sql statement</returns>
        protected virtual string GetInsert(DbTableSchema schema, IDataRecord record, bool includeChildren = false)
        {
            var sw = new StringWriter();
            sw.WriteLine("INSERT INTO {1} ({0}) VALUES ({2});",
                string.Join(", ", GetSqlFormattedColumnNames(schema, (i) => !i.IsIdentity)),
                GetSqlFormattedTableName(schema),
                string.Join(", ", GetSqlInserValues(schema, record)));
            if (includeChildren)
                foreach (var refTable in schema.References.Where(i => i.IsChild))
                {
                    GetCreateCommandForChildren(sw, refTable, record);
                }
            return sw.ToString();
        }

        /// <summary>
        /// Generates a SQL update statement
        /// </summary>
        /// <param name="record">The <see cref="IDataRecord"/> with the data for the statement</param>
        /// <returns>A sql statement</returns>
        protected virtual string GetUpdate(IDataRecord record)
        {
            var sw = new StringWriter();
            sw.WriteLine("UPDATE {0} SET {1} WHERE {2};", GetSqlFormattedTableName(),
                string.Join(", ", GetUpdateValueStatement(record)),
                string.Join(" AND ", GetKeyWhereStatement(record)));
            return sw.ToString();
        }

        /// <summary>
        /// Generates a SQL delete statement
        /// </summary>
        /// <param name="record">The <see cref="IDataRecord"/> with the data for the statement</param>
        /// <returns>A sql statement</returns>
        protected virtual string GetDelete(IDataRecord record)
        {
            var sw = new StringWriter();
            sw.WriteLine("DELETE FROM {0} WHERE {1};", GetSqlFormattedTableName(),
                string.Join(" AND ", GetKeyWhereStatement(record)));
            return sw.ToString();
        }

        /// <summary>
        /// Gets a sql select statement on the entity
        /// </summary>
        /// <returns>A sql statement</returns>
        protected virtual string GetReadAllCommand()
        {
            var sw = new StringWriter();
            sw.WriteLine("SELECT {0} FROM {1}",
                string.Join(", ", GetSqlFormattedColumnNames((i) => true)),
                GetSqlFormattedTableName());
            return sw.ToString();
        }

        /// <summary>
        /// Gets a sql query command on the data entity
        /// </summary>
        /// <param name="filterExpression">The expression used to filter the data</param>
        /// <param name="orderByExpression">The expression used to sort the data</param>
        /// <param name="descending">Indicates if the order should be descending, otherwhise it would be ascending</param>
        /// <returns>A sql statement</returns>
        public string QueryCommand(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, object>> orderByExpression = null, bool descending = false)
        {
            var whereStatemet = _printer.Where(filterExpression);
            var orderByStatement = default(string);
            if (orderByExpression != null) orderByStatement = string.Format(" ORDER BY {0}", _printer.OrderBy(orderByExpression, descending));
            return string.Format("{0} WHERE {1} {2}", GetReadAllCommand(), whereStatemet, orderByStatement).Trim();
        }

        #endregion

        #region Private Methods

        private IDataRecord GetDataRecord(TEntity entity)
        {
            return _recordMapper.ToDataRecord(entity);
        }

        private IEnumerable<string> GetSqlFormattedColumnNames(Func<DbColumnSchema, bool> predicate)
        { return GetSqlFormattedColumnNames(_schema, predicate); }

        private IEnumerable<string> GetUpdateValueStatement(IDataRecord record)
        {
            return GetColumnValuePair(record, i => !i.IsPrimaryKey && !i.IsIdentity).Select(i =>
            {
                if (i.Contains("IS NULL"))
                    i = i.Replace("IS NULL", "= NULL");
                return i;
            });
        }

        private IEnumerable<string> GetKeyWhereStatement(IDataRecord record)
        {
            return GetKeyWhereStatement(_schema, record);
        }


        private IEnumerable<string> GetKeyWhereStatement(DbTableSchema schema, IDataRecord record)
        {
            if (!GetColumns(schema).Any(i => i.IsPrimaryKey))
                throw new InvalidDataException("At least one primary key column is required");
            return GetColumnValuePair(schema, record, i => i.IsPrimaryKey);
        }

        private IEnumerable<string> GetColumnValuePair(IDataRecord record, Func<DbColumnSchema, bool> predicate)
        {
            return GetColumnValuePair(_schema, record, predicate);
        }

        private IEnumerable<string> GetColumnValuePair(DbTableSchema schema, IDataRecord record, Func<DbColumnSchema, bool> predicate)
        {
            return GetColumns(schema).Where(predicate)
                .Select(i =>
                {
                    var val = record[i.ColumnName];
                    var res = string.Format("{0} = {1}", GetSqlFormattedColumnName(i), val.ToSql());
                    if (val.IsNullOrDbNull()) res = string.Format("{0} IS NULL", GetSqlFormattedColumnName(i));
                    return res;
                }).ToList();
        }

        private IEnumerable<string> GetSqlInserValues(DbTableSchema schema, IDataRecord record)
        {
            return GetEntityValues(schema, record, i => !i.IsIdentity).Select(i => i.ToSql());
        }

        private IEnumerable<object> GetEntityValues(DbTableSchema schema, IDataRecord record, Func<DbColumnSchema, bool> predicate)
        {
            var res = new List<object>();
            GetColumns(schema).Where(predicate).ToList().ForEach(i => res.Add(record[i.ColumnName]));
            return res;
        }

        private void GetCreateCommandForChildren(StringWriter sw, TableReference reference, IDataRecord record)
        {
            var value = record[reference.SourceColumn.PropertyName];
            if (value.IsPrimitiveType() ||
                !(typeof(IEnumerable<IDataRecord>).IsAssignableFrom(value.GetType()) || typeof(IDataRecord).IsAssignableFrom(value.GetType()))) return;
            if (reference.IsChild)
                foreach (var item in (IEnumerable)value)
                    sw.Write(GetInsert(reference.ReferenceTable, (IDataRecord)item, true));
            else
                sw.Write(GetInsert(reference.ReferenceTable, (IDataRecord)value, true));
        }

        private string GetSqlFormattedTableName() { return GetSqlFormattedTableName(_schema); }


        protected abstract string GetSqlFormattedTableName(DbTableSchema schema);

        private IEnumerable<string> GetSqlFormattedColumnNames(DbTableSchema schema, Func<DbColumnSchema, bool> predicate)
        {
            return GetColumns(schema).Where(predicate).Select(GetSqlFormattedColumnName);
        }

        private IEnumerable<DbColumnSchema> GetColumns(DbTableSchema schema)
        {
            var parentReferences = schema.References.Where(i => !i.IsChild).ToList();
            if (parentReferences.Count <= 0) return schema.Columns;
            foreach (var parentRef in parentReferences)
            {
                if (string.IsNullOrWhiteSpace(parentRef.ReferenceTableKey))
                    parentRef.ReferenceTableKey = parentRef.ReferenceTable.TableName.Name + "Id";
                if (!schema.Columns.Any(i => i.ColumnName == parentRef.ReferenceTableKey))
                    schema.Columns.Add(new DbColumnSchema()
                    {
                        ColumnName = parentRef.ReferenceTableKey,
                        IsIdentity = false,
                        IsPrimaryKey = false
                    });
            }
            return schema.Columns;
        }

        protected abstract string GetSqlFormattedColumnName(DbColumnSchema columnSchema);

        #endregion

    }
}
