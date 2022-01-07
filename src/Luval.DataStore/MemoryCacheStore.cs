using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.DataStore
{
    /// <summary>
    /// Application memory cache implementation
    /// </summary>
    /// <typeparam name="TKey">The <see cref="Type"/> to use a key value</typeparam>
    /// <typeparam name="TEntity">The <see cref="Type"/> of entity to cache</typeparam>
    public class MemoryCacheStore<TKey, TEntity> : ICacheProvider<TKey, TEntity>
    {
        private ConcurrentDictionary<TKey, TEntity> _store;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public MemoryCacheStore()
        {
            _store = new ConcurrentDictionary<TKey, TEntity>();
        }

        /// <inheritdoc/>
        public TEntity GetOrAdd(TKey key, Func<TEntity> add)
        {
            if (_store.ContainsKey(key)) return _store[key];
            _store[key] = add();
            return _store[key];
        }

        /// <inheritdoc/>
        public Task<TEntity> GetOrAddAsync(TKey key, Func<Task<TEntity>> add)
        {
            return Task.Run(async () => {
                if (_store.ContainsKey(key)) return _store[key];
                _store[key] = await add();
                return _store[key];
            });
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            _store.Clear();
            _store = null;
        }
    }
}
