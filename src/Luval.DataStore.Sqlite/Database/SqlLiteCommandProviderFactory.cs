using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.DataStore.Database.Sqlite
{
    /// <inheritdoc/>
    public class SqlLiteCommandProviderFactory : AnsiSqlCommandProviderFactory
    {
        /// <inheritdoc/>
        public SqlLiteCommandProviderFactory() : base(new ReflectionDataRecordMapper())
        {

        }

        /// <inheritdoc/>
        public SqlLiteCommandProviderFactory(IDataRecordMapper dataRecordMapper) : base(dataRecordMapper)
        {

        }
    }
}
