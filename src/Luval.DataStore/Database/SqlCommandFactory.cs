using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.DataStore.Database
{
    /// <inheritdoc/>
    public class SqlCommandFactory : ISqlCommandFactory
    {
        public virtual SqlCommandOptions Options { get; private set; }

        /// <summary>
        /// Creates a new instance of <see cref="SqlCommandFactory"/>
        /// </summary>
        public SqlCommandFactory() : this(new SqlCommandOptions() { CommandType = CommandType.Text })
        {

        }

        /// <summary>
        /// Creates a new instance of <see cref="SqlCommandFactory"/>
        /// </summary>
        /// <param name="commandTimeout">The wait time (in seconds) before terminating the attempt to execute</param>
        public SqlCommandFactory(int commandTimeout): this(new SqlCommandOptions() { CommandTimeout = commandTimeout, CommandType = CommandType.Text })
        {

        }

        /// <summary>
        /// Creates a new instance of <see cref="SqlCommandFactory"/>
        /// </summary>
        /// <param name="options">The options for the <see cref="IDbCommand"/></param>
        public SqlCommandFactory(SqlCommandOptions options)
        {
            Options = options ?? throw new ArgumentNullException(nameof(options));
        }

        /// <inheritdoc/>
        public IDbCommand Create(IDataCommand command, IDbConnection connection)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));
            var sqlCmd = Convert.ToString(command.Get());
            var cmd = connection.CreateCommand();
            PrepareCommand(cmd, sqlCmd);
            return cmd;
        }

        private void PrepareCommand(IDbCommand cmd, string query)
        {
            cmd.CommandText = query;
            cmd.CommandType = Options.CommandType;
            if (Options.Parameters != null && Options.Parameters.Any())
                foreach (var param in Options.Parameters)
                {
                    cmd.Parameters.Add(param);
                }
            if (Options.CommandTimeout != null) cmd.CommandTimeout = Options.CommandTimeout.Value;
            else cmd.CommandTimeout = cmd.Connection.ConnectionTimeout;
        }
    }
}
