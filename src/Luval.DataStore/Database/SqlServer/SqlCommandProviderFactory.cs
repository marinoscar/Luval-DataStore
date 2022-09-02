using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.DataStore.Database.SqlServer
{
    /// <inheritdoc/>
    public class SqlCommandProviderFactory : BaseSqlCommandProviderFactory
    {
        /// <inheritdoc/>
        public SqlCommandProviderFactory() : this(new ReflectionDataRecordMapper())
        {

        }

        /// <inheritdoc/>
        public SqlCommandProviderFactory(IDataRecordMapper dataRecordMapper) : base(dataRecordMapper)
        {

        }

        /// <inheritdoc/>
        public override IDataCommandProvider<TEntity> Create<TEntity>() where TEntity : class
        {
            return new SqlCommandProvider<TEntity>(this.RecordMapper);
        }
    }
}
