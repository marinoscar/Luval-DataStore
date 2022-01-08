using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Luval.DataStore
{


    /// <inheritdoc/>
    public class UnitOfWork<TEntity> : IUnitOfWork<TEntity> where TEntity : class
    {

        /// <summary>
        /// Creates a new instance of <see cref="UnitOfWork"/>
        /// </summary>
        /// <param name="dataEntityCollection">The <see cref="IDataEntityCollection{TEntity}"/> implementation to use</param>
        public UnitOfWork(IDataEntityCollection<TEntity> dataEntityCollection)
        {
            if(dataEntityCollection == null) throw new ArgumentNullException(nameof(dataEntityCollection));

            Entities = dataEntityCollection;
            CommandProvider = dataEntityCollection.CommandProvider;
            DataStore = dataEntityCollection.DataStore; 
        }

        /// <inheritdoc/>
        public IDataEntityCollection<TEntity> Entities { get; private set; }

        /// <summary>
        /// Gets the instance of the <see cref="IDataStore"/> that would be used to persist the data
        /// </summary>
        public IDataStore DataStore { get; private set; }

        /// <summary>
        /// Gets the <see cref="IDataRecordCommandProvider"/> to persist data into the <see cref="IDataStore"/>
        /// </summary>
        public IDataCommandProvider<TEntity> CommandProvider { get; private set; }


        /// <inheritdoc/>
        public event EventHandler<ModifyingEntityEventArgs<TEntity>> InsertingEntity;

        /// <inheritdoc/>
        public event EventHandler<ModifyingEntityEventArgs<TEntity>> UpdatingEntity;

        /// <inheritdoc/>
        public event EventHandler<ModifyingEntityEventArgs<TEntity>> DeletingEntity;

        /// <inheritdoc/>
        public event EventHandler<ModifiedEntityEventArgs<TEntity>> EntityInserted;

        /// <inheritdoc/>
        public event EventHandler<ModifiedEntityEventArgs<TEntity>> EntityUpdated;

        /// <inheritdoc/>
        public event EventHandler<ModifiedEntityEventArgs<TEntity>> EntityDeleted;


        /// <inheritdoc/>
        public virtual int SaveChanges()
        {
            var res = 0;
            try
            {
                foreach (var insertEnt in Entities.Inserted)
                {
                    var arg = OnInserting(insertEnt);
                    if (arg != null && arg.Cancel) continue;
                    res += DataStore.Execute(CommandProvider.GetInsert(insertEnt));
                    OnEntityInserted(insertEnt);
                }
                foreach (var updateEnt in Entities.Updated)
                {
                    var arg = OnUpdating(updateEnt);
                    if (arg != null && arg.Cancel) continue;
                    res += DataStore.Execute(CommandProvider.GetUpdate(updateEnt));
                    OnEntityUpdated(updateEnt);
                }
                foreach (var deleteEnt in Entities.Deleted)
                {
                    var arg = OnDeleting(deleteEnt);
                    if (arg != null && arg.Cancel) continue;
                    res += DataStore.Execute(CommandProvider.GetDelete(deleteEnt));
                    OnEntityDeleted(deleteEnt);
                }
                Entities.Clear(); // clears the entities
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to save the changes in the data store", ex);
            }
            return res;
        }

        /// <inheritdoc/>
        public virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return Task.Run(() => { return SaveChanges(); }, cancellationToken);
        }

        /// <summary>
        /// Invokes the <seealso cref="IUnitOfWork{TEntity}.InsertingEntity"/> event
        /// </summary>
        /// <param name="entity">The entity to insert</param>
        /// <returns>The instance of the <see cref="ModifyingEntityEventArgs{TEntity}"/> returned by the event</returns>
        protected virtual ModifyingEntityEventArgs<TEntity> OnInserting(TEntity entity)
        {
            return TriggerEvent(entity, InsertingEntity);
        }

        /// <summary>
        /// Invokes the <seealso cref="IUnitOfWork{TEntity}.UpdatingEntity"/> event
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <returns>The instance of the <see cref="ModifyingEntityEventArgs{TEntity}"/> returned by the event</returns>
        protected virtual ModifyingEntityEventArgs<TEntity> OnUpdating(TEntity entity)
        {
            return TriggerEvent(entity, UpdatingEntity);
        }

        /// <summary>
        /// Invokes the <seealso cref="IUnitOfWork{TEntity}.DeletingEntity"/> event
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <returns>The instance of the <see cref="ModifyingEntityEventArgs{TEntity}"/> returned by the event</returns>
        protected virtual ModifyingEntityEventArgs<TEntity> OnDeleting(TEntity entity)
        {
            return TriggerEvent(entity, DeletingEntity);
        }

        /// <summary>
        /// Invokes the <see cref="IUnitOfWork{TEntity}.EntityInserted"/> event
        /// </summary>
        /// <param name="entity">The affected entity</param>
        protected virtual void OnEntityInserted(TEntity entity)
        {
            TriggerEvent(entity, EntityInserted);
        }

        /// <summary>
        /// Invokes the <see cref="IUnitOfWork{TEntity}.EntityUpdated"/> event
        /// </summary>
        /// <param name="entity">The affected entity</param>
        protected virtual void OnEntityUpdated(TEntity entity)
        {
            TriggerEvent(entity, EntityUpdated);
        }

        /// <summary>
        /// Invokes the <see cref="IUnitOfWork{TEntity}.EntityDeleted"/> event
        /// </summary>
        /// <param name="entity">The affected entity</param>
        protected virtual void OnEntityDeleted(TEntity entity)
        {
            TriggerEvent(entity, EntityDeleted);
        }

        private ModifyingEntityEventArgs<TEntity> TriggerEvent(TEntity entity, EventHandler<ModifyingEntityEventArgs<TEntity>> eventHandler)
        {
            var args = new ModifyingEntityEventArgs<TEntity>(entity);
            eventHandler?.Invoke(this, args);
            return args;
        }

        private void TriggerEvent(TEntity entity, EventHandler<ModifiedEntityEventArgs<TEntity>> eventHandler)
        {
            eventHandler?.Invoke(this, new ModifiedEntityEventArgs<TEntity>(entity));
        }
    }

    public class UnitOfWork : UnitOfWork<IDictionary<string, object>>
    {
        /// <summary>
        /// Creates a new instance of <see cref="UnitOfWork"/>
        /// </summary>
        /// <param name="dataEntityCollection">The <see cref="IDataRecordEntityCollection"/> implementation to use</param>
        public UnitOfWork(IDataRecordEntityCollection dataEntityCollection): base(dataEntityCollection)
        {

        }
    }


}
