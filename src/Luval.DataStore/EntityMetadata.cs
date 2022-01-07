using Luval.DataStore.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Luval.DataStore
{
    /// <summary>
    /// Represents the metadata of an <see cref="object"/>
    /// </summary>
    public class EntityMetadata
    {
        /// <summary>
        /// Creates a new instance of <see cref="EntityMetadata"/>
        /// </summary>
        /// <param name="entityType">The entity <see cref="Type"/></param>
        public EntityMetadata(Type entityType) : this(entityType, new EntityFieldMetadata[] { })
        {

        }

        /// <summary>
        /// Creates a new instance of <see cref="EntityMetadata"/>
        /// </summary>
        /// <param name="entityType">The entity <see cref="Type"/></param>
        /// <param name="entityFields">The <see cref="EntityFieldMetadata"/> to use</param>
        public EntityMetadata(Type entityType, IEnumerable<EntityFieldMetadata> entityFields)
        {
            EntityType = entityType;
            Fields = new List<EntityFieldMetadata>(entityFields);
        }
        /// <summary>
        /// Gets the entity <see cref="Type"/>
        /// </summary>
        public Type EntityType { get; private set; }
        /// <summary>
        /// Gets the list of fields
        /// </summary>
        public List<EntityFieldMetadata> Fields { get; private set; }
    }

    /// <summary>
    /// Represents the information for a entity field
    /// </summary>
    public class EntityFieldMetadata
    {
        /// <summary>
        /// Gets or sets the <see cref="PropertyInfo"/>
        /// </summary>
        public PropertyInfo Property { get; set; }
        /// <summary>
        /// Gets or sets a value indicating if the field is mapped
        /// </summary>
        public bool IsMapped { get; set; }
        /// <summary>
        /// Gets or sets the target data store field name
        /// </summary>
        public string DataFieldName { get; set; }
        /// <summary>
        /// Gets or sets a value indicating if it is a primitive type
        /// </summary>
        public bool IsPrimitive { get; set; }
        /// <summary>
        /// Gets or sets a value indiciating if the field is a list
        /// </summary>
        public bool IsList { get; set; }

        /// <summary>
        /// Gets or sets <see cref="TableReference"/> for the field
        /// </summary>
        public TableReference TableReference { get; set; }
    }

}
