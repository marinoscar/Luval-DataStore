using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data;
using System.Threading;
using Luval.DataStore.Extensions;

namespace Luval.DataStore.Database
{
    /// <summary>
    /// Provides an implementation of <see cref="IDataStore"/> for relational databases using
    /// implementations of the <see cref="IDbConnection"/> interface
    /// </summary>
    public class DbDataStore : DataStore, IDatabaseStore
    {

        #region Variable Declaration

        private readonly Func<IDbConnection> _factory;
        private readonly ISqlCommandFactory _sqlCmdFactory;
        private readonly IDataRecordMapper _dataRecordMapper;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of <see cref="DbDataStore"/>
        /// </summary>
        /// <param name="connectionFactory"></param>
        public DbDataStore(Func<IDbConnection> connectionFactory, ISqlCommandFactory sqlCommandFactory, IDataRecordMapper dataRecordMapper)
        {
            _factory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
            _sqlCmdFactory = sqlCommandFactory ?? throw new ArgumentNullException(nameof(sqlCommandFactory));
            _dataRecordMapper = dataRecordMapper ?? throw new ArgumentNullException(nameof(dataRecordMapper));
        }

        #endregion

        #region IDataStore Implementation

        /// <inheritdoc/>
        public override int Execute(IDataCommand command)
        {
            var rows = 0;
            UsingCommand(command, (cmd) =>
            {
                rows = cmd.ExecuteNonQuery();
            });
            return rows;
        }

        /// <inheritdoc/>
        public override object ExecuteScalar(IDataCommand command)
        {
            object res = null;
            UsingCommand(command, (cmd) => {
                res = cmd.ExecuteScalar();
            });
            return res;
        }

        /// <inheritdoc/>
        public override T ExecuteScalar<T>(IDataCommand command)
        {
            var res = ExecuteScalar(command);
            return (T)Convert.ChangeType(res, typeof(T));
        }

        /// <inheritdoc/>
        public override IDataReader ExecuteToDataReader(IDataCommand command)
        {
            IDataReader dataReader = null;
            UsingCommand(command, (cmd) =>
            {
                dataReader = cmd.ExecuteReader();
            });
            return dataReader;
        }

        /// <inheritdoc/>
        public override IEnumerable<IDataRecord> ExecuteToDataRecord(IDataCommand command)
        {
            var data = new List<IDataRecord>();
            UsingReader(command, (r) => { data.Add(r); });
            return data;
        }

        /// <inheritdoc/>
        public override DataTable ExecuteDataTable(IDataCommand command)
        {
            DataTable dt = null;
            UsingReader(command, (r) => {
                if (dt == null) dt = CreateTableSchema(r);
                dt.Rows.Add(CreateFromRecord(r, dt));
            });
            return dt;
        }

        /// <inheritdoc/>
        public override IEnumerable<TEntity> ExecuteToEntityList<TEntity>(IDataCommand command)
        {
            var data = new List<TEntity>();
            UsingReader(command, (r) => {
                data.Add(_dataRecordMapper.FromDataRecord<TEntity>(r));
            });
            return data;
        }

        #endregion

        #region IDatabaseStore Implementation

        /// <inheritdoc/>
        public Task<int> ExecuteAsync(string command, CancellationToken cancelationToken)
        {
            return ExecuteAsync(command.ToCommand(), cancelationToken);
        }

        /// <inheritdoc/>
        public Task<object> ExecuteScalarAsync(string command, CancellationToken cancelationToken)
        {
            return ExecuteScalarAsync(command.ToCommand(), cancelationToken);
        }

        /// <inheritdoc/>
        public Task<T> ExecuteScalarAsync<T>(string command, CancellationToken cancelationToken)
        {
            return ExecuteScalarAsync<T>(command.ToCommand(), cancelationToken);
        }

        /// <inheritdoc/>
        public Task<IEnumerable<IDataRecord>> ExecuteToDataRecordAsync(string command, CancellationToken cancelationToken)
        {
            return ExecuteToDataRecordAsync(command.ToCommand(), cancelationToken);
        }

        /// <inheritdoc/>
        public Task<IDataReader> ExecuteToDataReaderAsync(string command, CancellationToken cancelationToken)
        {
            return ExecuteToDataReaderAsync(command.ToCommand(), cancelationToken);
        }

        /// <inheritdoc/>
        public Task<IEnumerable<TEntity>> ExecuteToEntityListAsync<TEntity>(string command, CancellationToken cancelationToken)
        {
            return ExecuteToEntityListAsync<TEntity>(command.ToCommand(), cancelationToken);
        }

        /// <inheritdoc/>
        public Task<DataTable> ExecuteDataTableAsync(string command, CancellationToken cancelationToken)
        {
            return ExecuteDataTableAsync(command.ToCommand(), cancelationToken);
        }

        /// <inheritdoc/>
        public int Execute(string command)
        {
            return Execute(command.ToCommand());
        }

        /// <inheritdoc/>
        public object ExecuteScalar(string command)
        {
            return ExecuteScalar(command.ToCommand());
        }

        /// <inheritdoc/>
        public T ExecuteScalar<T>(string command)
        {
            return ExecuteScalar<T>(command.ToCommand());
        }

        /// <inheritdoc/>
        public IEnumerable<IDataRecord> ExecuteToDataRecord(string command)
        {
            return ExecuteToDataRecord(command.ToCommand());
        }

        /// <inheritdoc/>
        public IDataReader ExecuteToDataReader(string command)
        {
            return ExecuteToDataReader(command.ToCommand());
        }

        /// <inheritdoc/>
        public IEnumerable<TEntity> ExecuteToEntityList<TEntity>(string command)
        {
            return ExecuteToEntityList<TEntity>(command.ToCommand());
        }

        /// <inheritdoc/>
        public DataTable ExecuteDataTable(string command)
        {
            return ExecuteDataTable(command.ToCommand());
        }

        #endregion

        #region Private Methods

        private void OpenConnection(IDbConnection conn)
        {
            try
            {
                if (conn.State == ConnectionState.Closed) conn.Open();
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Unable to open the connection", ex);
            }
        }

        private void CloseConnection(IDbConnection conn)
        {
            try
            {
                if (conn != null && conn.State != ConnectionState.Closed) conn.Close();
            }
            catch (Exception ex)
            {

                throw new DatabaseException("Unable to close the connection", ex);
            }
        }

        private void WorkTransaction(IDbTransaction tran, Action action)
        {
            if (tran == null || tran.Connection == null || tran.Connection.State == ConnectionState.Closed) return;
            action();
        }

        private void UsingCommand(IDataCommand command, Action<IDbCommand> runCommand)
        {
            try
            {
                using (var conn = _factory())
                {
                    OpenConnection(conn);
                    using (var cmd = _sqlCmdFactory.Create(command, conn))
                    {
                        using (var tran = conn.BeginTransaction(_sqlCmdFactory.Options.IsolationLevel))
                        {
                            try
                            {
                                runCommand(cmd);
                                WorkTransaction(tran, () => { tran.Commit(); });
                            }
                            catch (Exception ex)
                            {
                                WorkTransaction(tran, () => { tran.Rollback(); });
                                throw new DatabaseException(string.Format("Failed to run command: {0}", cmd.CommandText), ex);
                            }
                        }
                    }
                    CloseConnection(conn);
                }
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Command failed", ex);
            }
        }

        private void UsingReader(IDataCommand command, Action<IDataRecord> runCommand)
        {
            UsingCommand(command, (cmd) =>
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        runCommand(new DataRecord(reader));
                    }
                }
            });
        }

        private DataTable CreateTableSchema(IDataRecord record)
        {
            var dt = new DataTable();
            for (int i = 0; i < record.FieldCount; i++)
            {
                dt.Columns.Add(record.GetName(i), record.GetFieldType(i));
            }
            return dt;
        }

        private DataRow CreateFromRecord(IDataRecord record, DataTable dt)
        {
            var row = dt.NewRow();
            for (int i = 0; i < record.FieldCount; i++)
                row[record.GetName(i)] = record.GetValue(i);
            return row;
        }

        #endregion
    }
}
