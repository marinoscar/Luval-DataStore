using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.DataStore.DataAnnotations
{
    /// <summary>
    /// Specifies that a column is an identity auto increment
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class IdentityColumnAttribute : Attribute
    {
    }
}
