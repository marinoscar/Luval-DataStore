using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.DataStore.DataAnnotations
{
    /// <summary>
    /// Specificies the name of a column
    /// </summary>
    public abstract class NameBaseAttribute : Attribute
    {
        /// <summary>
        /// Creates a new instance of <see cref="NameBaseAttribute"/>
        /// </summary>
        /// <param name="name">The column name</param>
        public NameBaseAttribute(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Gets or sets the column name
        /// </summary>
        public string Name { get; set; }
    }
}
