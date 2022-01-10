using Luval.DataStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Luval.DataStore.Extensions
{
    /// <summary>
    /// Provides extensions for the <see cref="IUnitOfWork{TEntity}"/> implementations
    /// </summary>
    public static class UnitOfWorkExtensions
    {

        #region Standard CRUD Operations

        /// <summary>
        /// Adds the entity to the <see cref="IDataEntityCollection{TEntity}"/> and saves to the target <see cref="IDataStore"/>
        /// </summary>
        /// <typeparam name="TEntity">The data entity <see cref="Type"/></typeparam>
        /// <param name="uow">Unit of Work to use</param>
        /// <param name="entity">The entity data to persist</param>
        /// <returns>Number of affected records</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static int AddAndSave<TEntity>(this IUnitOfWork<TEntity> uow, TEntity entity) where TEntity : class
        {
            if (uow == null) throw new ArgumentNullException(nameof(uow));
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            uow.Entities.Insert(entity);
            return uow.SaveChanges();
        }

        /// <summary>
        /// Adds the entity to the <see cref="IDataEntityCollection{TEntity}"/> and saves to the target <see cref="IDataStore"/>
        /// </summary>
        /// <typeparam name="TEntity">The data entity <see cref="Type"/></typeparam>
        /// <param name="uow">Unit of Work to use</param>
        /// <param name="entity">The entity data to persist</param>
        /// <param name="cancellationToken">Operation cancellation notification token</param>
        /// <returns>A <see cref="Task{TResult}"/> with the affected records</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Task<int> AddAndSaveAsync<TEntity>(this IUnitOfWork<TEntity> uow, TEntity entity, CancellationToken cancellationToken) where TEntity : class
        {
            if (uow == null) throw new ArgumentNullException(nameof(uow));
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            uow.Entities.Insert(entity);
            return uow.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Updates the entity to the <see cref="IDataEntityCollection{TEntity}"/> and saves to the target <see cref="IDataStore"/>
        /// </summary>
        /// <typeparam name="TEntity">The data entity <see cref="Type"/></typeparam>
        /// <param name="uow">Unit of Work to use</param>
        /// <param name="entity">The entity data to persist</param>
        /// <returns>Number of affected records</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static int UpdateAndSave<TEntity>(this IUnitOfWork<TEntity> uow, TEntity entity) where TEntity : class
        {
            if (uow == null) throw new ArgumentNullException(nameof(uow));
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            uow.Entities.Update(entity);
            return uow.SaveChanges();
        }

        /// <summary>
        /// Updates the entity to the <see cref="IDataEntityCollection{TEntity}"/> and saves to the target <see cref="IDataStore"/>
        /// </summary>
        /// <typeparam name="TEntity">The data entity <see cref="Type"/></typeparam>
        /// <param name="uow">Unit of Work to use</param>
        /// <param name="entity">The entity data to persist</param>
        /// <param name="cancellationToken">Operation cancellation notification token</param>
        /// <returns>A <see cref="Task{TResult}"/> with the affected records</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Task<int> UpdateAndSaveAsync<TEntity>(this IUnitOfWork<TEntity> uow, TEntity entity, CancellationToken cancellationToken) where TEntity : class
        {
            if (uow == null) throw new ArgumentNullException(nameof(uow));
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            uow.Entities.Update(entity);
            return uow.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Deletes the entity from the <see cref="IDataEntityCollection{TEntity}"/> and saves to the target <see cref="IDataStore"/>
        /// </summary>
        /// <typeparam name="TEntity">The data entity <see cref="Type"/></typeparam>
        /// <param name="uow">Unit of Work to use</param>
        /// <param name="entity">The entity data to delete</param>
        /// <returns>Number of affected records</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static int DeleteAndSave<TEntity>(this IUnitOfWork<TEntity> uow, TEntity entity) where TEntity : class
        {
            if (uow == null) throw new ArgumentNullException(nameof(uow));
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            uow.Entities.Delete(entity);
            return uow.SaveChanges();
        }

        /// <summary>
        /// Deletes the entity from the <see cref="IDataEntityCollection{TEntity}"/> and saves to the target <see cref="IDataStore"/>
        /// </summary>
        /// <typeparam name="TEntity">The data entity <see cref="Type"/></typeparam>
        /// <param name="uow">Unit of Work to use</param>
        /// <param name="entity">The entity data to delete</param>
        /// <param name="cancellationToken">Operation cancellation notification token</param>
        /// <returns>A <see cref="Task{TResult}"/> with the affected records</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Task<int> DeleteAndSaveAsync<TEntity>(this IUnitOfWork<TEntity> uow, TEntity entity, CancellationToken cancellationToken) where TEntity : class
        {
            if (uow == null) throw new ArgumentNullException(nameof(uow));
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            uow.Entities.Delete(entity);
            return uow.SaveChangesAsync(cancellationToken);
        }

        #endregion

        #region Upsert Operations

        #region Standard Upset

        /// <summary>
        /// Adds or updates the entity to the <see cref="IDataEntityCollection{TEntity}"/> and saves to the target <see cref="IDataStore"/>
        /// </summary>
        /// <typeparam name="TEntity">The data entity <see cref="Type"/></typeparam>
        /// <param name="uow">Unit of Work to use</param>
        /// <param name="entity">The entity data to persist</param>
        /// <param name="filterExpression">Expression used to identify if the record already exists in the <see cref="IDataStore"/></param>
        /// <returns>Number of affected records</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public static int AddOrUpdateAndSave<TEntity>(this IUnitOfWork<TEntity> uow, TEntity entity, Expression<Func<TEntity, bool>> filterExpression) where TEntity : class
        {
            return AddOrUpdateAndSaveAsync(uow, new[] { entity }, filterExpression, CancellationToken.None).Result;
        }

        /// <summary>
        /// Adds or updates the entity to the <see cref="IDataEntityCollection{TEntity}"/> and saves to the target <see cref="IDataStore"/>
        /// </summary>
        /// <typeparam name="TEntity">The data entity <see cref="Type"/></typeparam>
        /// <param name="uow">Unit of Work to use</param>
        /// <param name="entities">The collection of entities to persist</param>
        /// <param name="filterExpression">Expression used to identify if the record already exists in the <see cref="IDataStore"/></param>
        /// <returns>Number of affected records</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public static int AddOrUpdateAndSave<TEntity>(this IUnitOfWork<TEntity> uow, IEnumerable<TEntity> entities, Expression<Func<TEntity, bool>> filterExpression) where TEntity : class
        {
            return AddOrUpdateAndSaveAsync(uow, entities, filterExpression, CancellationToken.None).Result;
        }

        /// <summary>
        /// Adds or updates the entity to the <see cref="IDataEntityCollection{TEntity}"/> and saves to the target <see cref="IDataStore"/>
        /// </summary>
        /// <typeparam name="TEntity">The data entity <see cref="Type"/></typeparam>
        /// <param name="uow">Unit of Work to use</param>
        /// <param name="entity">The entity data to persist</param>
        /// <param name="filterExpression">Expression used to identify if the record already exists in the <see cref="IDataStore"/></param>
        /// <param name="cancellationToken">Operation cancellation notification token</param>
        /// <returns>A <see cref="Task{TResult}"/> with the affected records</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public static Task<int> AddOrUpdateAndSaveAsync<TEntity>(this IUnitOfWork<TEntity> uow, TEntity entity, Expression<Func<TEntity, bool>> filterExpression, CancellationToken cancellationToken) where TEntity : class
        {
            return AddOrUpdateAndSaveAsync(uow, new[] { entity }, filterExpression, cancellationToken);
        }

        /// <summary>
        /// Adds or updates the entity to the <see cref="IDataEntityCollection{TEntity}"/> and saves to the target <see cref="IDataStore"/>
        /// </summary>
        /// <typeparam name="TEntity">The data entity <see cref="Type"/></typeparam>
        /// <param name="uow">Unit of Work to use</param>
        /// <param name="entities">The collection of entities to persist</param>
        /// <param name="filterExpression">Expression used to identify if the record already exists in the <see cref="IDataStore"/></param>
        /// <param name="cancellationToken">Operation cancellation notification token</param>
        /// <returns>A <see cref="Task{TResult}"/> with the affected records</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public static async Task<int> AddOrUpdateAndSaveAsync<TEntity>(this IUnitOfWork<TEntity> uow, IEnumerable<TEntity> entities, Expression<Func<TEntity, bool>> filterExpression, CancellationToken cancellationToken) where TEntity : class
        {
            if (uow == null) throw new ArgumentNullException(nameof(uow));
            if (entities == null) throw new ArgumentNullException(nameof(entities));

            foreach (var entity in entities)
            {
                var entries = await uow.Entities.QueryAsync(filterExpression, null, null, cancellationToken);
                if (entries != null && entries.Any())
                {
                    if (entries.Count() > 1) throw new InvalidOperationException(string.Format("{0} contains more than one record", nameof(filterExpression)));
                    uow.Entities.Update(entity);
                }
                else
                {
                    uow.Entities.Insert(entity);
                }
            }

            return await uow.SaveChangesAsync(cancellationToken);
        }
        #endregion

        /// <summary>
        /// Adds or updates the audit based entity to the <see cref="IDataEntityCollection{TEntity}"/> and saves to the target <see cref="IDataStore"/>
        /// </summary>
        /// <typeparam name="TEntity">The data entity <see cref="Type"/></typeparam>
        /// <typeparam name="TKey">The type Id propery of the entity</typeparam>
        /// <param name="uow">Unit of Work to use</param>
        /// <param name="entity">The entity data to persist</param>
        /// <param name="userId">The user Id that is affecting the entity</param>
        /// <param name="filterExpression">Expression used to identify if the record already exists in the <see cref="IDataStore"/></param>
        /// <returns>Number of affected records</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public static int AddOrUpdateAndSave<TEntity, TKey>(this IUnitOfWork<TEntity> uow, TEntity entity, string userId, Expression<Func<TEntity, bool>> filterExpression) where TEntity : class, IIdAuditedEntity<TKey>
        {
            return AddOrUpdateAndSave<TEntity, TKey>(uow, new[] { entity }, userId, filterExpression);
        }

        /// <summary>
        /// Adds or updates the audit based entity to the <see cref="IDataEntityCollection{TEntity}"/> and saves to the target <see cref="IDataStore"/>
        /// </summary>
        /// <typeparam name="TEntity">The data entity <see cref="Type"/></typeparam>
        /// <typeparam name="TKey">The type Id propery of the entity</typeparam>
        /// <param name="uow">Unit of Work to use</param>
        /// <param name="entities">The collection of entities to persist</param>
        /// <param name="userId">The user Id that is affecting the entity</param>
        /// <param name="filterExpression">Expression used to identify if the record already exists in the <see cref="IDataStore"/></param>
        /// <returns>Number of affected records</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public static int AddOrUpdateAndSave<TEntity, TKey>(this IUnitOfWork<TEntity> uow, IEnumerable<TEntity> entities, string userId, Expression<Func<TEntity, bool>> filterExpression) where TEntity : class, IIdAuditedEntity<TKey>
        {
            return AddOrUpdateAndSaveAsync<TEntity, TKey>(uow, entities, userId, filterExpression, CancellationToken.None).Result;
        }

        /// <summary>
        /// Adds or updates the audit based entity to the <see cref="IDataEntityCollection{TEntity}"/> and saves to the target <see cref="IDataStore"/>
        /// </summary>
        /// <typeparam name="TEntity">The data entity <see cref="Type"/></typeparam>
        /// <typeparam name="TKey">The type Id propery of the entity</typeparam>
        /// <param name="uow">Unit of Work to use</param>
        /// <param name="entity">The entity data to persist</param>
        /// <param name="filterExpression">Expression used to identify if the record already exists in the <see cref="IDataStore"/></param>
        /// <param name="userId">The user Id that is affecting the entity</param>
        /// <param name="cancellationToken">Operation cancellation notification token</param>
        /// <returns>A <see cref="Task{TResult}"/> with the affected records</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public static Task<int> AddOrUpdateAndSaveAsync<TEntity, TKey>(this IUnitOfWork<TEntity> uow, TEntity entity, string userId, Expression<Func<TEntity, bool>> filterExpression, CancellationToken cancellationToken) where TEntity : class, IIdAuditedEntity<TKey>
        {
            return AddOrUpdateAndSaveAsync<TEntity, TKey>(uow, new[] { entity }, userId, filterExpression, cancellationToken);
        }

        /// <summary>
        /// Adds or updates the audit based entity to the <see cref="IDataEntityCollection{TEntity}"/> and saves to the target <see cref="IDataStore"/>
        /// </summary>
        /// <typeparam name="TEntity">The data entity <see cref="Type"/></typeparam>
        /// <typeparam name="TKey">The type Id propery of the entity</typeparam>
        /// <param name="uow">Unit of Work to use</param>
        /// <param name="entities">The collection of entities to persist</param>
        /// <param name="filterExpression">Expression used to identify if the record already exists in the <see cref="IDataStore"/></param>
        /// <param name="cancellationToken">Operation cancellation notification token</param>
        /// <param name="userId">The user Id that is affecting the entity</param>
        /// <returns>A <see cref="Task{TResult}"/> with the affected records</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public static async Task<int> AddOrUpdateAndSaveAsync<TEntity, TKey>(this IUnitOfWork<TEntity> uow, IEnumerable<TEntity> entities, string userId, Expression<Func<TEntity, bool>> filterExpression, CancellationToken cancellationToken) where TEntity : class, IIdAuditedEntity<TKey>
        {
            if (uow == null) throw new ArgumentNullException(nameof(uow));
            if (entities == null) throw new ArgumentNullException(nameof(entities));

            foreach (var entity in entities)
            {
                var entries = await uow.Entities.QueryAsync(filterExpression, null, null, cancellationToken);
                if (entries != null && entries.Any())
                {
                    if (entries.Count() > 1) throw new InvalidOperationException(string.Format("{0} contains more than one record", nameof(filterExpression)));
                    var originalEntry = entries.First();
                    entity.Id = originalEntry.Id;
                    entity.CreatedByUserId = originalEntry.CreatedByUserId;
                    entity.UtcCreatedOn = originalEntry.UtcCreatedOn;
                    entity.UtcUpdatedOn = DateTime.UtcNow;
                    entity.UpdatedByUserId = userId;

                    uow.Entities.Update(entity);
                }
                else
                {
                    entity.UtcCreatedOn = DateTime.UtcNow;
                    entity.CreatedByUserId = userId;
                    entity.UtcUpdatedOn = DateTime.UtcNow;
                    entity.UpdatedByUserId = userId;

                    uow.Entities.Insert(entity);
                }
            }

            return await uow.SaveChangesAsync(cancellationToken);
        }

        #endregion
    }
}
