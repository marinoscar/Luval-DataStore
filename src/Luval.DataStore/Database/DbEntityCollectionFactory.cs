using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.DataStore.Database
{
    /// <inheritdoc/>
    public class DbEntityCollectionFactory : IDataEntityCollectionFactory
    {

        private readonly IDataStore _dataStore;
        private readonly IDataCommandProviderFactory _commandProviderFactory;

        /// <summary>
        /// Creates a new instance of <see cref="DbEntityCollectionFactory"/>
        /// </summary>
        /// <param name="dataStore">The target instance <see cref="IDataStore"/></param>
        /// <param name="commandProviderFactory">The <see cref="IDataCommandProviderFactory"/> to use</param>
        public DbEntityCollectionFactory(IDataStore dataStore, IDataCommandProviderFactory commandProviderFactory)
        {
            _dataStore = dataStore;
            _commandProviderFactory = commandProviderFactory;
        }

        /// <inheritdoc/>
        public IDataEntityCollection<TEntity> Create<TEntity>() where TEntity : class
        {
            return new EntityCollection<TEntity>(_dataStore, _commandProviderFactory.Create<TEntity>());
        }
    }
}
