using System;
using System.Collections.Generic;
using System.Text;

namespace Luval.DataStore.Database
{
    /// <summary>
    /// Represents a sql table name
    /// </summary>
    public class TableName
    {
        /// <summary>
        /// Creates a new instance of <see cref="TableName"/>
        /// </summary>
        public TableName()
        {
        }

        /// <summary>
        /// Creates a new instance of <see cref="TableName"/>
        /// </summary>
        /// <param name="name">The name for the table</param>
        public TableName(string name) : this (name, "dbo")
        {

        }

        /// <summary>
        /// Creates a new instance of <see cref="TableName"/>
        /// </summary>
        /// <param name="name">The name for the table</param>
        /// <param name="schema">The schema name for the table</param>
        public TableName(string name, string schema)
        {
            Name = name;
            Schema = schema;
        }

        /// <summary>
        /// Gets or sets the table name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the schema name
        /// </summary>
        public string Schema { get; set; }

        /// <summary>
        /// Gets a <see cref="string"/> with the full name for the table
        /// </summary>
        /// <returns>A <see cref="string"/> with the full name for the table</returns>
        public string GetFullTableName()
        {
            if (string.IsNullOrWhiteSpace(Schema)) return "[" + Name + "]";
            return string.Format("[{0}].[{1}]", Schema, Name);
        }
    }
}
