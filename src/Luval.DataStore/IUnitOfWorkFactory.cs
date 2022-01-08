using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.DataStore
{
    /// <summary>
    /// Represents an implementation of a factory to create <see cref="IUnitOfWork{TEntity}"/> instances
    /// </summary>
    public interface IUnitOfWorkFactory
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <typeparam name="TEntity">The entity <see cref="Type"/> representing a record in the <see cref="IDataStore"/> implementation</typeparam>
        /// <returns>A <see cref="IUnitOfWork{TEntity}"/> instance</returns>
        IUnitOfWork<TEntity> Create<TEntity>()  where TEntity : class;
    }
}
