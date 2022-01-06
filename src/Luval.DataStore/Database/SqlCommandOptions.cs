using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data;

namespace Luval.DataStore.Database
{
    /// <summary>
    /// Provides the options for a <see cref="IDbCommand"/> instance
    /// </summary>
    public class SqlCommandOptions
    {
        /// <summary>
        /// Gets or sets the wait time (in seconds) before terminating the attempt to execute
        /// a command and generating an error.
        /// </summary>
        public int? CommandTimeout { get; set; }
        /// <summary>
        ///Indicates or specifies how the <seealso cref="IDbCommand.CommandText"/> property is interpreted.
        /// </summary>
        public CommandType CommandType { get; set; }
        /// <summary>
        /// Gets or sets the Gets the <see cref="IDataParameterCollection"/> with the parameters for the SQL statement or stored procedure.
        /// </summary>
        public IEnumerable<IDbDataParameter> Parameters { get; set; }
    }
}
