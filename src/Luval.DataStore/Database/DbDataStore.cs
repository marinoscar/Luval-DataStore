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
    /// Provides an implementation of <see cref="IDataStore"/> for relational databases using
    /// implementations of the <see cref="IDbConnection"/> interface
    /// </summary>
    public class DbDataStore : DataStore
    {

        #region Variable Declaration
        
        private readonly Func<IDbConnection> _factory;
        

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of <see cref="DbDataStore"/>
        /// </summary>
        /// <param name="connectionFactory"></param>
        public DbDataStore(Func<IDbConnection> connectionFactory)
        {
            _factory = connectionFactory;
        }

        #endregion

        #region Interface Implementation

        /// <inheritdoc/>
        public override int Execute(IDataCommand command)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override IDataReader ExecuteToDataReader(IDataCommand command)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override IEnumerable<IDataRecord> ExecuteToDataRecord(IDataCommand command)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override DataSet ExecuteToDataset(IDataCommand command)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override IEnumerable<TEntity> ExecuteToEntityList<TEntity>(IDataCommand command)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Database Methods

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

        #endregion

        /// <summary>
        /// Encapsulates the use of a <see cref="IDbConnection"/> object
        /// </summary>
        /// <param name="doSomething">Action that would use the <see cref="IDbConnection"/> object</param>
        public void WithConnection(Action<IDbConnection> doSomething)
        {
            using (var conn = _factory())
            {
                if (conn == null) throw new ArgumentNullException(nameof(conn), "Connection is not properly provided");
                doSomething(conn);
            }
        }

        /// <summary>
        /// Encapsulates the use of a <see cref="IDbTransaction"/> object
        /// </summary>
        /// <param name="doSomething">Action that would use the <see cref="IDbTransaction"/> object</param>
        public void WithTransaction(Action<IDbTransaction> doSomething)
        {
            WithConnection((conn) =>
            {
                try
                {
                    OpenConnection(conn);
                    using (var tran = conn.BeginTransaction())
                    {
                        try
                        {
                            doSomething(tran);
                            WorkTransaction(tran, () => { tran.Commit(); });
                        }
                        catch (Exception ex)
                        {
                            WorkTransaction(tran, () => { tran.Rollback(); });

                            if (!typeof(DatabaseException).IsAssignableFrom(ex.GetType()))
                                throw new DatabaseException("Failed to complete the transaction", ex);
                            throw;
                        }
                    }
                    CloseConnection(conn);
                }
                catch (Exception ex)
                {
                    if (!typeof(DatabaseException).IsAssignableFrom(ex.GetType()))
                        throw new DatabaseException("Failed to begin or rollback the transaction", ex);
                    throw;
                }
            });
        }

        /// <summary>
        /// Encapsulates the use of a <see cref="IDbCommand"/> object
        /// </summary>
        /// <param name="doSomething">Action that would use the <see cref="IDbCommand"/> object</param>
        public void WithCommand(Action<IDbCommand> doSomething)
        {

            WithTransaction((tran) =>
            {
                using (var cmd = tran.Connection.CreateCommand())
                {
                    cmd.Transaction = tran;
                    try
                    {
                        doSomething(cmd);
                    }
                    catch (Exception ex)
                    {
                        throw new DatabaseException("Unable to execute sql command.",
                            new DatabaseException(string.Format("COMMAND FAILURE: {0}", cmd.CommandText), ex));
                    }
                }
            });
        }

        /// <summary>
        /// Encapsulates the reading of a <see cref="IDataReader"/> and its <see cref="IDataRecord"/>
        /// </summary>
        /// <param name="cmd">The <see cref="IDbCommand"/> to use for the <see cref="IDataReader"/></param>
        /// <param name="behavior">The <see cref="CommandBehavior"/> to use</param>
        /// <param name="doSomething">Action that would be used for the <see cref="IDataRecord"/></param>
        public void WhileReading(IDbCommand cmd, CommandBehavior behavior, Action<IDataRecord> doSomething)
        {
            using (var reader = cmd.ExecuteReader(behavior))
            {
                while (reader.Read())
                {
                    doSomething(reader);
                }
            }
        }

        #endregion
    }
}
