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
    /// <inheritdoc/>
    public class EntityCollection<TEntity> : IDataEntityCollection<TEntity> where TEntity : class
    {

        private IList<Entry> _internal;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="dataStore">The <see cref="IDataStore"/> implementation to use</param>
        /// <param name="commandProvider">The <see cref="IDataCommandProvider{TEntity}"/> implementation to use</param>
        public EntityCollection(IDataStore dataStore, IDataCommandProvider<TEntity> commandProvider)
        {
            DataStore = dataStore ?? throw new ArgumentNullException(nameof(dataStore));
            CommandProvider = commandProvider ?? throw new ArgumentNullException(nameof(commandProvider));

            _internal = new List<Entry>();
        }

        #region IDataEntityCollection Methods

        /// <inheritdoc/>
        public IDataStore DataStore { get; private set; }

        /// <inheritdoc/>
        public IDataCommandProvider<TEntity> CommandProvider { get; private set; }

        /// <inheritdoc/>
        public IEnumerable<TEntity> Inserted => _internal.Where(i => i.Status == Status.Inserted).Select(i => i.Entity);

        /// <inheritdoc/>
        public IEnumerable<TEntity> Updated => _internal.Where(i => i.Status == Status.Updated).Select(i => i.Entity);

        /// <inheritdoc/>
        public IEnumerable<TEntity> Deleted => _internal.Where(i => i.Status == Status.Deleted).Select(i => i.Entity);

        /// <inheritdoc/>
        public void Delete(TEntity entity)
        {
            Add(entity, Status.Deleted);
        }

        /// <inheritdoc/>
        public void Update(TEntity entity)
        {
            Add(entity, Status.Updated);
        }

        /// <inheritdoc/>
        public void Insert(TEntity entity)
        {
            Add(entity, Status.Inserted);
        }

        /// <inheritdoc/>
        public IEnumerable<IDataRecord> Query(IDataCommand command)
        {
            return DataStore.ExecuteToDataRecord(command);
        }

        /// <inheritdoc/>
        public IEnumerable<TEntity> Query(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, object>> orderByExpression, bool? descending)
        {
            return DataStore.ExecuteToEntityList<TEntity>(CommandProvider.Query(filterExpression, orderByExpression, descending?? false));
        }

        /// <inheritdoc/>
        public Task<IEnumerable<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, object>> orderByExpression, bool? descending, CancellationToken cancellationToken)
        {
            return DataStore.ExecuteToEntityListAsync<TEntity>(CommandProvider.Query(filterExpression, orderByExpression, descending?? false), cancellationToken);
        }

        /// <inheritdoc/>
        public Task<IEnumerable<IDataRecord>> QueryAsync(IDataCommand command, CancellationToken cancellationToken)
        {
            return Task.Run(() => Query(command), cancellationToken);
        }

        /// <inheritdoc/>
        public void Clear()
        {
            _internal.Clear();
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Clear();
            _internal = null;
        }

        #endregion

        #region ICollection Methods

        /// <inheritdoc/>
        public IEnumerator GetEnumerator()
        {
            return _internal.Select(i => i.Entity).GetEnumerator();
        }

        /// <inheritdoc/>
        public int Count => _internal.Count;

        /// <inheritdoc/>
        public bool IsSynchronized => false;

        /// <inheritdoc/>
        public object SyncRoot => null;

        /// <inheritdoc/>
        [Obsolete("Not implemented")]
        public void CopyTo(Array array, int index)
        {

        }

        #endregion

        #region Private Items

        private void Add(TEntity entity, Status status)
        {
            _internal.Add(new Entry() { Status = status, Entity = entity });
        }

        private enum Status { Inserted, Updated, Deleted }
        private class Entry { public Status Status { get; set; } public TEntity Entity { get; set; } }

        #endregion
    }
}
