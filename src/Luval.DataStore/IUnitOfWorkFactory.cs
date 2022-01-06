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
    /// <typeparam name="TEntity">The <see cref="Type"/> that represents the Data Entity</typeparam>
    public interface IUnitOfWorkFactory<TEntity> where TEntity : class
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <returns>A <see cref="IUnitOfWork{TEntity}"/> instance</returns>
        IUnitOfWork<TEntity> Create();
    }

    /// <summary>
    /// Represents an implementation of a factory to create <see cref="IUnitOfWork{TEntity}"/> instances
    /// </summary>
    public interface IUnitOfWorkFactory
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <returns>A <see cref="IUnitOfWork"/> instance</returns>
        IUnitOfWork Create();
    }
}
