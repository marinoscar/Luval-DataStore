using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.DataStore
{
    /// <summary>
    /// Represents a cache implementation
    /// </summary>
    /// <typeparam name="TKey">The <see cref="Type"/> to use a key value</typeparam>
    /// <typeparam name="TEntity">The <see cref="Type"/> of entity to cache</typeparam>
    public interface ICacheProvider<TKey, TEntity> : IDisposable
    {
        /// <summary>
        /// Gets or add an <see cref="TEntity"/> to the cache store
        /// </summary>
        /// <param name="key">The <see cref="TKey"/> value to use as key</param>
        /// <param name="add">A delegate to get a new value if not present in the cache</param>
        /// <returns>An instance of the cached value</returns>
        TEntity GetOrAdd(TKey key, Func<TEntity> add);

        /// <summary>
        /// Gets or add an <see cref="TEntity"/> to the cache store
        /// </summary>
        /// <param name="key">The <see cref="TKey"/> value to use as key</param>
        /// <param name="add">A delegate to get a new value if not present in the cache</param>
        /// <returns>An <see cref="Task"/> operation with the instance of the cached value</returns>
        Task<TEntity> GetOrAddAsync(TKey key, Func<Task<TEntity>> add);
    }
}
