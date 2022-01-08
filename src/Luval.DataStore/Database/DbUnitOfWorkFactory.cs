using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.DataStore.Database
{
    /// <inheritdoc/>
    public class DbUnitOfWorkFactory: IUnitOfWorkFactory
    {

        private readonly IDataEntityCollectionFactory _entityCollectionFactory;

        /// <summary>
        /// Creates a new instance of <see cref="IUnitOfWork{TEntity}"/>
        /// </summary>
        /// <param name="collectionFactory">The <see cref="IDataEntityCollectionFactory"/> implementation to create the required instances</param>
        public DbUnitOfWorkFactory(IDataEntityCollectionFactory collectionFactory)
        {
            _entityCollectionFactory = collectionFactory;
        }

        /// <inheritdoc/>
        public IUnitOfWork<TEntity> Create<TEntity>() where TEntity : class
        {
            return new UnitOfWork<TEntity>(_entityCollectionFactory.Create<TEntity>());
        }
    }
}
