using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.DataStore.Database
{
    /// <summary>
    /// Represents a factory of <see cref="IDataCommand"/> with sql <see cref="IDataCommand"/> objects
    /// </summary>
    public interface ISqlCommandFactory
    {

        /// <summary>
        /// Converts a <see cref="IDataCommand"/> into a <see cref="IDbCommand"/> for sql operations
        /// </summary>
        /// <param name="command">An instance of the <see cref="IDataCommand"/> to transform</param>
        /// <returns>A new instance of <see cref="IDbCommand"/> with the sql instructions</returns>
        IDbCommand Create(IDataCommand command);
    }
}
