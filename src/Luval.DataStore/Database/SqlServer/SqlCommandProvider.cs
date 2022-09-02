using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.DataStore.Database.SqlServer
{
    /// <inheritdoc/>
    public class SqlCommandProvider<TEntity> : BaseSqlCommandProvider<TEntity> where TEntity : class
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        public SqlCommandProvider() : this(new ReflectionDataRecordMapper())
        {

        }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public SqlCommandProvider(IDataRecordMapper dataRecordMapper) : base(dataRecordMapper)
        {

        }

        protected override string GetSqlFormattedColumnName(DbColumnSchema columnSchema)
        {
            return string.Format("[{0}]", columnSchema.ColumnName);
        }

        protected override string GetSqlFormattedTableName(DbTableSchema schema)
        {
            return schema.TableName.GetFullTableName();
        }
    }
}
