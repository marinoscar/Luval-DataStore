using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.DataStore.Database
{
    /// <summary>
    /// Represents a Sql database implementation of a <see cref="IDataCommand"/>
    /// </summary>
    public class SqlDataCommand : IDataCommand
    {
        /// <summary>
        /// Gets the Sql command text
        /// </summary>
        public string CommandText { get; private set; }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="sqlCommand">A <see cref="string"/> with the sql command</param>
        public SqlDataCommand(string sqlCommand)
        {
            CommandText = sqlCommand;
        }

        /// <inheritdoc/>
        public object Get()
        {
            return CommandText;
        }
    }
}
