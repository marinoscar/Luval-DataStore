using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.DataStore.Entities
{
    /// <summary>
    /// Represents an entity with a Id field to identify it as primary key
    /// </summary>
    /// <typeparam name="TKey">The <see cref="Type"/> for the <see cref="IIdEntity{TKey}.Id"/> property</typeparam>
    public interface IIdEntity<TKey>
    {
        /// <summary>
        /// Gets or sets the entity Id
        /// </summary>
        public TKey Id { get; set; }
    }

    /// <summary>
    /// Represents an entity with a Id field to identify it as primary key
    /// </summary>
    public interface IStringIdEntity : IIdEntity<string>
    {

    }

    /// <summary>
    /// Represents an entity with a Id field to identify it as primary key
    /// </summary>
    public interface INumberIdEntity : IIdEntity<long>
    {

    }
}
