using Luval.DataStore.DataAnnotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Luval.DataStore.Database
{
    /// <summary>
    /// Represents the information of a sql table reference structure
    /// </summary>
    public class TableReference
    {
        /// <summary>
        /// Gets or sets the <see cref="DbColumnSchema"/> representing the source column in the table would be
        /// used as the reference value to the parent table
        /// </summary>
        public DbColumnSchema SourceColumn { get; set; }
        /// <summary>
        /// Gets or sets the <see cref="DbTableSchema"/> of the table that the value in <seealso cref="SourceColumn"/>
        /// would use to reference
        /// </summary>
        public DbTableSchema ReferenceTable { get; set; }
        /// <summary>
        /// Gets or sets the name of the key in <seealso cref="ReferenceTable"/> table, defaults to "Id"
        /// </summary>
        public string ReferenceTableKey { get; set; }

        /// <summary>
        /// Gets or sets the entity <see cref="Type"/>
        /// </summary>
        public Type EntityType { get; set; }
        /// <summary>
        /// Indicates if the reference is for a child reference
        /// </summary>
        public bool IsChild { get; set; }

        /// <summary>
        /// Creates a new instance of <see cref="TableReference"/>
        /// </summary>
        /// <param name="prop">The <see cref="PropertyInfo"/> to extract the metadata to be used to create the <see cref="TableReference"/> object</param>
        /// <returns>A new instance of <see cref="TableReference"/></returns>
        public static TableReference Create(PropertyInfo prop)
        {
            var entityType = GetReferenceTableEntityType(prop);
            return new TableReference()
            {
                ReferenceTableKey = prop.GetCustomAttribute<TableReferenceAttribute>().ReferenceTableKey,
                EntityType = entityType,
                IsChild = typeof(IEnumerable).IsAssignableFrom(prop.PropertyType),
                ReferenceTable = DbTableSchema.Create(entityType),
                SourceColumn = DbColumnSchema.Create(prop)
            };
        }
        private static Type GetReferenceTableEntityType(PropertyInfo property)
        {
            return property.PropertyType.IsGenericType ?
                property.PropertyType.GetGenericArguments()[0] :
                property.PropertyType;
        }
    }
}
