using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.DataStore
{
    /// <summary>
    /// Represents an implementation of a factory pattern to create <see cref="IDataEntityCollection{TEntity}"/> instances
    /// </summary>
    public interface IDataEntityCollectionFactory
    {
        /// <summary>
        /// Creates a <see cref="IDataEntityCollection{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">The <see cref="Type"/> that represents the data entity</typeparam>
        /// <returns></returns>
        IDataEntityCollection<TEntity> Create<TEntity>() where TEntity : class;
    }
}
