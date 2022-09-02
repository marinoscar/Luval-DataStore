using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.DataStore.Database
{
    /// <inheritdoc/>
    public abstract class BaseSqlCommandProviderFactory : IDataCommandProviderFactory
    {
        /// <summary>
        /// Gets the <see cref="IDataRecordMapper"/>
        /// </summary>
        protected IDataRecordMapper RecordMapper { get; private set; }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="recordMapper">The <see cref="IDataRecordMapper"/> to use</param>
        public BaseSqlCommandProviderFactory(IDataRecordMapper recordMapper)
        {
            RecordMapper = recordMapper;    
        }

        /// <inheritdoc/>
        public abstract IDataCommandProvider<TEntity> Create<TEntity>() where TEntity : class;
    }
}
