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
        public string CommandText { get; private set; }
        public SqlDataCommand(string sqlCommand)
        {
            CommandText = sqlCommand;
        }
        public object Get()
        {
            return CommandText;
        }
    }
}
