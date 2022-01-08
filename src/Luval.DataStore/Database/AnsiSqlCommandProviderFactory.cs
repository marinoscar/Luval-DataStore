using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.DataStore.Database
{
    /// <inheritdoc/>
    public class AnsiSqlCommandProviderFactory : IDataCommandProviderFactory
    {
        private readonly IDataRecordMapper _dataRecordMapper;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public AnsiSqlCommandProviderFactory() : this(new ReflectionDataRecordMapper())
        {

        }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="dataRecordMapper">The <see cref="IDataRecordMapper"/> implementation to create the commands</param>
        public AnsiSqlCommandProviderFactory(IDataRecordMapper dataRecordMapper)
        {
            _dataRecordMapper = dataRecordMapper;
        }

        /// <inheritdoc/>
        public IDataCommandProvider<TEntity> Create<TEntity>() where TEntity : class
        {
            return new AnsiSqlCommandProvider<TEntity>(_dataRecordMapper);
        }
    }
}
