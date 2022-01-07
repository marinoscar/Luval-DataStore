using System;
using System.Collections.Generic;
using System.Text;

namespace Luval.DataStore.DataAnnotations
{
    /// <summary>
    /// Specifies a table reference
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class TableReferenceAttribute : Attribute
    {
        /// <summary>
        /// Creates a new instance of <see cref="TableReferenceAttribute"/>
        /// </summary>
        public TableReferenceAttribute() : this(null, null)
        {

        }

        /// <summary>
        /// Creates a new instance of <see cref="TableReferenceAttribute"/>
        /// </summary>
        /// <param name="referenceColumnKey">The column in the table that would be used as reference to the parent table</param>
        public TableReferenceAttribute(string referenceColumnKey) : this(referenceColumnKey, "Id")
        {

        }

        /// <summary>
        /// Creates a new instance of <see cref="TableReferenceAttribute"/>
        /// </summary>
        /// <param name="referenceColumnKey">The column name in the table that would be used as reference to the parent table</param>
        /// <param name="parentColumnKey">The column name in the parent table that would be referenced</param>
        public TableReferenceAttribute(string referenceColumnKey, string parentColumnKey)
        {
            ReferenceTableKey = referenceColumnKey;
            ParentColumnKey = parentColumnKey;
        }

        /// <summary>
        /// Gets or sets the column name to be referenced
        /// </summary>
        public string ReferenceTableKey { get; set; }
        /// <summary>
        /// Gets or sets the column name in the parent table
        /// </summary>
        public string ParentColumnKey { get; set; }

    }
}
