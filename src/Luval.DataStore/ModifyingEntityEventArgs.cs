using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.DataStore
{

    public class ModifyingEntityEventArgs<TEntity> : ModifiedEntityEventArgs<TEntity> where TEntity : class
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="entity">The <see cref="TEntity"/> data</param>
        public ModifyingEntityEventArgs(TEntity entity) : base(entity)
        {
        }

        /// <summary>
        /// Gets or sets a value indicating if the <see cref="IUnitOfWork{TEntity}"/> should cancel the operation
        /// </summary>
        public bool Cancel { get; set; }
    }

    public class ModifiedEntityEventArgs<TEntity> : EventArgs where TEntity : class
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="entity">The <see cref="TEntity"/> data</param>
        public ModifiedEntityEventArgs(TEntity entity)
        {
            Entity = entity;
        }

        /// <summary>
        /// Gets the entity that is being worked on
        /// </summary>
        public TEntity Entity { get; private set; }

    }
}
