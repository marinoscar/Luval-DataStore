using Luval.DataStore.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Luval.DataStore.DataAnnotations
{
    /// <summary>
    ///  Specifies the name of a table for an entity
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class TableNameAttribute : Attribute
    {
        /// <summary>
        /// Creates a new instance of <see cref="TableNameAttribute"/>
        /// </summary>
        /// <param name="name">The name for the table</param>
        public TableNameAttribute(string name) : this(new TableName(name))
        {

        }

        /// <summary>
        /// Creates a new instance of <see cref="TableNameAttribute"/>
        /// </summary>
        /// <param name="name">The name for the table</param>
        /// <param name="schema">The schema name</param>
        public TableNameAttribute(string name, string schema) : this(new TableName(name, schema))
        {
        }

        /// <summary>
        /// Creates a new instance of <see cref="TableNameAttribute"/>
        /// </summary>
        /// <param name="name">A <see cref="TableName"/> object with the naming information</param>
        public TableNameAttribute(TableName name)
        {
            TableName = name;
        }

        /// <summary>
        /// Gets or sets the <see cref="TableName"/> value
        /// </summary>
        public TableName TableName { get; private set; }
    }
}
