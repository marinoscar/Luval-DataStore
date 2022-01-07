using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.DataStore.DataAnnotations
{
    /// <summary>
    /// Specifies the name of a column for an entity
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnNameAttribute : NameBaseAttribute
    {
        /// <summary>
        /// Creates a new instance of <see cref="ColumnNameAttribute"/>
        /// </summary>
        /// <param name="name"></param>
        public ColumnNameAttribute(string name) : base(name)
        {
        }
    }
}
