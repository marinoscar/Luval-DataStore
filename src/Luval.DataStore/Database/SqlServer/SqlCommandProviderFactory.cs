using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.DataStore.Database.SqlServer
{
    /// <inheritdoc/>
    public class SqlCommandProviderFactory : AnsiSqlCommandProviderFactory
    {
        /// <inheritdoc/>
        public SqlCommandProviderFactory() : base(new ReflectionDataRecordMapper())
        {

        }

        /// <inheritdoc/>
        public SqlCommandProviderFactory(IDataRecordMapper dataRecordMapper) : base(dataRecordMapper)
        {

        }
    }
}
