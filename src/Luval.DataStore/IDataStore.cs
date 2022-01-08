using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Luval.DataStore
{
    /// <summary>
    /// Represents the methods to query and persist data on a data store
    /// </summary>
    public interface IDataStore
    {
        #region Async Methods

        /// <summary>
        /// Executes a command in the <see cref="IDataStore"/>
        /// </summary>
        /// <param name="command">The <see cref="IDataCommand"/> with the instructions to execute</param>
        /// <param name="cancelationToken">A <see cref="CancellationToken"/> to use to cancel the operation</param>
        /// <returns>A <see cref="Task{TResult}"/> operation with the number of affected records </returns>
        Task<int> ExecuteAsync(IDataCommand command, CancellationToken cancelationToken);

        /// <summary>
        /// Executes the command in the <see cref="IDataStore"/>
        /// </summary>
        /// <param name="command">The <see cref="IDataCommand"/> with the instructions to execute</param>
        /// <param name="cancelationToken">A <see cref="CancellationToken"/> to use to cancel the operation</param>
        /// <returns>An <see cref="Task{TResult}"/> operation with the result of the command</returns>
        Task<object> ExecuteScalarAsync(IDataCommand command, CancellationToken cancelationToken);

        /// <summary>
        /// Executes the command in the <see cref="IDataStore"/>
        /// </summary>
        /// <typeparam name="T">The expected data type or the result</typeparam>
        /// <param name="command">The <see cref="IDataCommand"/> with the instructions to execute</param>
        /// <param name="cancelationToken">A <see cref="CancellationToken"/> to use to cancel the operation</param>
        /// <returns>An <see cref="Task{TResult}"/> operation with the result of the command</returns>
        Task<T> ExecuteScalarAsync<T>(IDataCommand command, CancellationToken cancelationToken);

        /// <summary>
        /// Executes a command in the <see cref="IDataStore"/>
        /// </summary>
        /// <param name="command">The <see cref="IDataCommand"/> with the instructions to execute</param>
        /// <param name="cancelationToken">A <see cref="CancellationToken"/> to use to cancel the operation</param>
        /// <returns>A <see cref="Task{TResult}"/> operation with the records from the <see cref="IDataStore"/> </returns>
        Task<IEnumerable<IDataRecord>> ExecuteToDataRecordAsync(IDataCommand command, CancellationToken cancelationToken);

        /// <summary>
        /// Executes a command in the <see cref="IDataStore"/>
        /// </summary>
        /// <param name="command">The <see cref="IDataCommand"/> with the instructions to execute</param>
        /// <param name="cancelationToken">A <see cref="CancellationToken"/> to use to cancel the operation</param>
        /// <returns>A <see cref="Task{TResult}"/> operation with the records from the <see cref="IDataStore"/> </returns>
        Task<IDataReader> ExecuteToDataReaderAsync(IDataCommand command, CancellationToken cancelationToken);

        /// <summary>
        /// Executes a command in the <see cref="IDataStore"/>
        /// </summary>
        /// <param name="command">The <see cref="IDataCommand"/> with the instructions to execute</param>
        /// <param name="cancelationToken">A <see cref="CancellationToken"/> to use to cancel the operation</param>
        /// <returns>A <see cref="Task{TResult}"/> operation with the data entities from the <see cref="IDataStore"/> </returns>
        Task<IEnumerable<TEntity>> ExecuteToEntityListAsync<TEntity>(IDataCommand command, CancellationToken cancelationToken);

        /// <summary>
        /// Executes a command in the <see cref="IDataStore"/>
        /// </summary>
        /// <param name="command">The <see cref="IDataCommand"/> with the instructions to execute</param>
        /// <param name="cancelationToken">A <see cref="CancellationToken"/> to use to cancel the operation</param>
        /// <returns>A <see cref="Task{TResult}"/> operation with the rows from the <see cref="IDataStore"/> </returns>
        Task<DataTable> ExecuteDataTableAsync(IDataCommand command, CancellationToken cancelationToken);

        #endregion

        #region Methods
        /// <summary>
        /// Executes a command in the <see cref="IDataStore"/>
        /// </summary>
        /// <param name="command">The <see cref="IDataCommand"/> with the instructions to execute</param>
        /// <returns>The number of affected records </returns>
        int Execute(IDataCommand command);

        /// <summary>
        /// Executes the command in the <see cref="IDataStore"/>
        /// </summary>
        /// <param name="command">The <see cref="IDataCommand"/> with the instructions to execute</param>
        /// <returns>An object with the result of the command</returns>
        object ExecuteScalar(IDataCommand command);

        /// <summary>
        /// Executes the command in the <see cref="IDataStore"/>
        /// </summary>
        /// <typeparam name="T">The expected data type or the result</typeparam>
        /// <param name="command">The <see cref="IDataCommand"/> with the instructions to execute</param>
        /// <returns>An instance with the result of the command</returns>
        T ExecuteScalar<T>(IDataCommand command);

        /// <summary>
        /// Executes a command in the <see cref="IDataStore"/>
        /// </summary>
        /// <param name="command">The <see cref="IDataCommand"/> with the instructions to execute</param>
        /// <returns>A <see cref="IEnumerable{IDataRecord}"/> with the records from the <see cref="IDataStore"/> </returns>
        IEnumerable<IDataRecord> ExecuteToDataRecord(IDataCommand command);

        /// <summary>
        /// Executes a command in the <see cref="IDataStore"/>
        /// </summary>
        /// <param name="command">The <see cref="IDataCommand"/> with the instructions to execute</param>
        /// <returns>A <see cref="IDataReader"/> with the records from the <see cref="IDataStore"/> </returns>
        IDataReader ExecuteToDataReader(IDataCommand command);

        /// <summary>
        /// Executes a command in the <see cref="IDataStore"/>
        /// </summary>
        /// <param name="command">The <see cref="IDataCommand"/> with the instructions to execute</param>
        /// <returns>A <see cref="IEnumerable{T}"/> with the data entities from the <see cref="IDataStore"/> </returns>
        IEnumerable<TEntity> ExecuteToEntityList<TEntity>(IDataCommand command);

        /// <summary>
        /// Executes a command in the <see cref="IDataStore"/>
        /// </summary>
        /// <param name="command">The <see cref="IDataCommand"/> with the instructions to execute</param>
        /// <returns>A <see cref="DataSet"/> with the rows from the <see cref="IDataStore"/> </returns>
        DataTable ExecuteDataTable(IDataCommand command); 
        #endregion
    }
}
