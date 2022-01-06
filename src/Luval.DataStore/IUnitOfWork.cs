using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Luval.DataStore
{
    /// <summary>
    /// Represents an implementation of the Unit of Work pattern
    /// </summary>
    public interface IUnitOfWork
    {

        /// <summary>
        /// Gets the <see cref="IDataEntityCollection"/> with the entities
        /// </summary>
        public IDataEntityCollection Entities { get; }

        /// <summary>
        /// Persists the changes in the target data store
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to use to cancel the operation</param>
        /// <returns>A <see cref="Task{TResult}"/> operation with the number of affected records</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Persists the changes in the target data store
        /// </summary>
        /// <returns>The number of affected records</returns>
        int SaveChanges();
    }

    /// <summary>
    /// Represents an implementation of the Unit of Work pattern
    /// </summary>
    public interface IUnitOfWork<TEntity> where TEntity : class
    {

        /// <summary>
        /// Gets the <see cref="IDataEntityCollection"/> with the entities
        /// </summary>
        public IDataEntityCollection<TEntity> Entities { get; }

        /// <summary>
        /// Persists the changes in the target data store
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to use to cancel the operation</param>
        /// <returns>A <see cref="Task{TResult}"/> operation with the number of affected records</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Persists the changes in the target data store
        /// </summary>
        /// <returns>The number of affected records</returns>
        int SaveChanges();
    }
}
