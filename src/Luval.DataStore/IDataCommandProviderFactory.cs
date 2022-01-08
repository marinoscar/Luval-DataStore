using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.DataStore
{
    public interface IDataCommandProviderFactory
    {
        /// <summary>
        /// Creates a new instance of <see cref="IDataCommandProvider{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">The entity <see cref="Type"/> representing a record in the <see cref="IDataStore"/> implementation</typeparam>
        /// <returns>A <see cref="IDataCommandProvider{TEntity}"/> isntance</returns>
        IDataCommandProvider<TEntity> Create<TEntity>() where TEntity : class;
    }
}
