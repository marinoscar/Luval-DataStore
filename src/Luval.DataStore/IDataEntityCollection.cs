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
    public interface IDataEntityCollection : ICollection
    {

        /// <summary>
        /// Insert the entity to the collection, it verifies the status and attach it
        /// </summary>
        /// <param name="entity">The entity data</param>
        void Insert(IDataRecord entity);

        /// <summary>
        /// Update the entity to the collection, it verifies the status and attach it
        /// </summary>
        /// <param name="entity">The entity data</param>
        void Update(IDataRecord entity);

        /// <summary>
        /// Delete the entity to the collection, it verifies the status and attach it
        /// </summary>
        /// <param name="entity">The entity data</param>
        void Delete(IDataRecord entity);

        /// <summary>
        /// Queries the data store
        /// </summary>
        /// <param name="command">The command to execute</param>
        /// <returns>A <see cref="IEnumerable{IDataRecord}"/> with the data</returns>
        IEnumerable<IDataRecord> Query(IDataCommand command);

        /// <summary>
        /// Queries the data store
        /// </summary>
        /// <param name="command">The command to execute</param>
        /// <returns>A <see cref="IEnumerable{IDataRecord}"/> with the data</returns>
        Task<IEnumerable<IDataRecord>> QueryAsync(IDataCommand command, CancellationToken cancellationToken);
    }


    /// <summary>
    /// Represents the implementation of a collection of Data Entities
    /// </summary>
    public interface IDataEntityCollection<TEntity> : ICollection where TEntity : class
    {
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
        /// <param name="command">The command to execute</param>
        /// <returns>A <see cref="IQueryable{TEntity}"/> with the data</returns>
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> whereExpression);

        /// <summary>
        /// Queries the data store
        /// </summary>
        /// <param name="command">The command to execute</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to use to cancel the operation</param>
        /// <returns>A <see cref="Task{IQueryable{TEntity}}"/> with the operation with the data</returns>
        IQueryable<TEntity> QueryAsync(Expression<Func<TEntity, bool>> whereExpression, CancellationToken cancellationToken);

        /// <summary>
        /// Queries the data store
        /// </summary>
        /// <param name="command">The command to execute</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to use to cancel the operation</param>
        /// <returns>A <see cref="Task{IEnumerable{TEntity}}"/> with the operation with the data</returns>
        Task<IEnumerable<TEntity>> QueryAsync(IDataCommand command, CancellationToken cancellationToken);
    }
}
