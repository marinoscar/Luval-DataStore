using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Luval.DataStore
{
    /// <summary>
    /// Represents the implementation of a collection of Data Entities
    /// </summary>
    /// <typeparam name="TEntity">The <see cref="Type"/> that represents the data entity</typeparam>
    public interface IDataEntityCollection<TEntity> : ICollection, IDisposable where TEntity : class
    {
        /// <summary>
        /// Gets the <see cref="IDataStore"/> implementation to use
        /// </summary>
        IDataStore DataStore { get; }

        /// <summary>
        /// Gets the <see cref="IDataCommandProvider{TEntity}"/> implementation to use
        /// </summary>
        IDataCommandProvider<TEntity> CommandProvider { get; }

        /// <summary>
        /// Insert the entity to the collection, it verifies the status and attach it
        /// </summary>
        /// <param name="entity">The entity data</param>
        void Insert(TEntity entity);

        /// <summary>
        /// Update the entity to the collection, it verifies the status and attach it
        /// </summary>
        /// <param name="entity">The entity data</param>
        void Update(TEntity entity);

        /// <summary>
        /// Delete the entity to the collection, it verifies the status and attach it
        /// </summary>
        /// <param name="entity">The entity data</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Queries the data store
        /// </summary>
        /// <param name="command">The command to execute</param>
        /// <returns>A <see cref="IEnumerable{TEntity}"/> with the data</returns>
        IEnumerable<TEntity> Query(IDataCommand command);

        /// <summary>
        /// Queries the data store
        /// </summary>
        /// <param name="filterExpression">The expression to use to filter the data</param>
        /// <param name="orderByExpression">The expression to sort the result set, null by default</param>
        /// <param name="descending">Indetifies if in case the <paramref name="orderByExpression"/> is provided if the order is descending, otherwise is ascending</param>
        /// <returns>A <see cref="IEnumerable{TEntity}"/> with the data</returns>
        IEnumerable<TEntity> Query(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, object>> orderByExpression = null, bool descending = false);

        /// <summary>
        /// Queries the data store
        /// </summary>
        /// <param name="filterExpression">The expression to use to filter the data</param>
        /// <param name="orderByExpression">The expression to sort the result set, null by default</param>
        /// <param name="descending">Indetifies if in case the <paramref name="orderByExpression"/> is provided if the order is descending, otherwise is ascending</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to use to cancel the operation</param>
        /// <returns>A <see cref="Task{TResult}"/> with the operation with the data</returns>
        Task<IEnumerable<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, object>> orderByExpression, bool descending, CancellationToken cancellationToken);

        /// <summary>
        /// Queries the data store
        /// </summary>
        /// <param name="command">The command to execute</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to use to cancel the operation</param>
        /// <returns>A <see cref="Task{TResult}"/> with the operation with the data</returns>
        Task<IEnumerable<TEntity>> QueryAsync(IDataCommand command, CancellationToken cancellationToken);

        /// <summary>
        /// Gets the entities that have been inserted in memory to the collection
        /// </summary>
        IEnumerable<TEntity> Inserted { get; }
        /// <summary>
        /// Gets the entities that have been updated in memory to the collection
        /// </summary>
        IEnumerable<TEntity> Updated { get; }
        /// <summary>
        /// Gets the entities that have been deleted in memory to the collection
        /// </summary>
        IEnumerable<TEntity> Deleted { get; }

        /// <summary>
        /// Clears all the entities in the collection
        /// </summary>
        void Clear();
    }
}
