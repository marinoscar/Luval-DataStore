using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.DataStore
{
    /// <summary>
    /// Represents the instruction to run a command on a datastore
    /// </summary>
    public interface IDataCommand
    {
        /// <summary>
        /// Gets the command for the data store
        /// </summary>
        /// <returns>An object containing the command</returns>
        object Get();
    }
}
