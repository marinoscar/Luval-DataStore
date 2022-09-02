using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.DataStore.Database
{
    /// <inheritdoc/>
    public class AnsiSqlCommandProviderFactory : BaseSqlCommandProviderFactory
    {
        private readonly IDataRecordMapper _dataRecordMapper;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public AnsiSqlCommandProviderFactory() : base(new ReflectionDataRecordMapper())
        {

        }

        /// <inheritdoc/>
        public override IDataCommandProvider<TEntity> Create<TEntity>() where TEntity : class
        {
            return new AnsiSqlCommandProvider<TEntity>(_dataRecordMapper);
        }
    }
}
