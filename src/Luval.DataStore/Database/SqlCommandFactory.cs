using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.DataStore.Database
{
    public class SqlCommandFactory : ISqlCommandFactory
    {
        protected virtual IDbConnection Connection { get; private set; }
        protected virtual SqlCommandOptions Options { get; private set; }

        /// <summary>
        /// Creates a new instance of <see cref="SqlCommandFactory"/>
        /// </summary>
        /// <param name="connection">The <see cref="IDbConnection"/> to leverage to create <see cref="IDbCommand"/></param>
        public SqlCommandFactory(IDbConnection connection) : this(connection, new SqlCommandOptions() { CommandType = CommandType.Text })
        {

        }

        /// <summary>
        /// Creates a new instance of <see cref="SqlCommandFactory"/>
        /// </summary>
        /// <param name="connection">The <see cref="IDbConnection"/> to leverage to create <see cref="IDbCommand"/></param>
        /// <param name="commandTimeout">The wait time (in seconds) before terminating the attempt to execute</param>
        public SqlCommandFactory(IDbConnection connection, int commandTimeout): this(connection, new SqlCommandOptions() { CommandTimeout = commandTimeout, CommandType = CommandType.Text })
        {

        }

        /// <summary>
        /// Creates a new instance of <see cref="SqlCommandFactory"/>
        /// </summary>
        /// <param name="connection">The <see cref="IDbConnection"/> to leverage to create <see cref="IDbCommand"/></param>
        /// <param name="options">The options for the <see cref="IDbCommand"/></param>
        public SqlCommandFactory(IDbConnection connection, SqlCommandOptions options)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));
            if (options == null) throw new ArgumentNullException(nameof(options));
            Connection = connection;
            Options = options;
        }

        /// <inheritdoc/>
        public IDbCommand Create(IDataCommand command)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));
            var sqlCmd = Convert.ToString(command.Get());
            var cmd = Connection.CreateCommand();
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
