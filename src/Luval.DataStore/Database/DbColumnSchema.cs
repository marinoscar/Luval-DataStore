using Luval.DataStore.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Luval.DataStore.Database
{
    /// <summary>
    /// Provides an abstraction for the schema of a sql column
    /// </summary>
    public class DbColumnSchema
    {
        /// <summary>
        /// Gets or sets the name of the property on the entity
        /// </summary>
        public string PropertyName { get; set; }
        /// <summary>
        /// Gets or sets the name of the column in the database
        /// </summary>
        public string ColumnName { get; set; }
        /// <summary>
        /// Idenfies if the column and propery is a primary key
        /// </summary>
        public bool IsPrimaryKey { get; set; }
        /// <summary>
        /// Idenfies if the column and propery is an indentity column
        /// </summary>
        public bool IsIdentity { get; set; }

        /// <summary>
        /// Creates a new instance of <see cref="DbColumnSchema"/>
        /// </summary>
        /// <param name="property">A <see cref="PropertyInfo"/> with the information of the entitiy property</param>
        /// <returns></returns>
        public static DbColumnSchema Create(PropertyInfo property)
        {
            return new DbColumnSchema()
            {
                ColumnName = GetColumnName(property),
                PropertyName = property.Name,
                IsPrimaryKey = property.GetCustomAttribute<PrimaryKeyAttribute>() != null,
                IsIdentity = property.GetCustomAttribute<IdentityColumnAttribute>() != null
            };
        }

        internal static string GetColumnName(PropertyInfo property)
        {
            var att = property.GetCustomAttribute<ColumnNameAttribute>();
            if (att == null) return property.Name;
            return ((ColumnNameAttribute)att).Name;
        }
    }
}
