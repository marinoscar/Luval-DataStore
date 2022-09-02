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
    public class AnsiSqlCommandProvider<TEntity> : BaseSqlCommandProvider<TEntity> where TEntity : class
    {

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public AnsiSqlCommandProvider() : this(new ReflectionDataRecordMapper())
        {

        }

        /// <inheritdoc/>
        public AnsiSqlCommandProvider(IDataRecordMapper dataRecordMapper) : base(dataRecordMapper)
        {

        }

        protected override string GetSqlFormattedColumnName(DbColumnSchema columnSchema)
        {
            return columnSchema.ColumnName;
        }

        protected override string GetSqlFormattedTableName(DbTableSchema schema)
        {
            return schema.TableName.Name;
        }
    }
}
