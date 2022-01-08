using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Luval.DataStore.Database
{
    /// <inheritdoc/>
    public interface IDatabaseStore : IDataStore
    {
        #region Async Methods

        /// <summary>
        /// Executes a command in the <see cref="IDataStore"/>
        /// </summary>
        /// <param name="command">The <see cref="string"/> with the instructions to execute</param>
        /// <param name="cancelationToken">A <see cref="CancellationToken"/> to use to cancel the operation</param>
        /// <returns>A <see cref="Task{TResult}"/> operation with the number of affected records </returns>
        Task<int> ExecuteAsync(string command, CancellationToken cancelationToken);

        /// <summary>
        /// Executes the command in the <see cref="IDataStore"/>
        /// </summary>
        /// <param name="command">The <see cref="string"/> with the instructions to execute</param>
        /// <param name="cancelationToken">A <see cref="CancellationToken"/> to use to cancel the operation</param>
        /// <returns>An <see cref="Task{TResult}"/> operation with the result of the command</returns>
        Task<object> ExecuteScalarAsync(string command, CancellationToken cancelationToken);

        /// <summary>
        /// Executes the command in the <see cref="IDataStore"/>
        /// </summary>
        /// <typeparam name="T">The expected data type or the result</typeparam>
        /// <param name="command">The <see cref="string"/> with the instructions to execute</param>
        /// <param name="cancelationToken">A <see cref="CancellationToken"/> to use to cancel the operation</param>
        /// <returns>An <see cref="Task{TResult}"/> operation with the result of the command</returns>
        Task<T> ExecuteScalarAsync<T>(string command, CancellationToken cancelationToken);

        /// <summary>
        /// Executes a command in the <see cref="IDataStore"/>
        /// </summary>
        /// <param name="command">The <see cref="string"/> with the instructions to execute</param>
        /// <param name="cancelationToken">A <see cref="CancellationToken"/> to use to cancel the operation</param>
        /// <returns>A <see cref="Task{TResult}"/> operation with the records from the <see cref="IDataStore"/> </returns>
        Task<IEnumerable<IDataRecord>> ExecuteToDataRecordAsync(string command, CancellationToken cancelationToken);

        /// <summary>
        /// Executes a command in the <see cref="IDataStore"/>
        /// </summary>
        /// <param name="command">The <see cref="string"/> with the instructions to execute</param>
        /// <param name="cancelationToken">A <see cref="CancellationToken"/> to use to cancel the operation</param>
        /// <returns>A <see cref="Task{IDataReader}"/> operation with the records from the <see cref="IDataStore"/> </returns>
        Task<IDataReader> ExecuteToDataReaderAsync(string command, CancellationToken cancelationToken);

        /// <summary>
        /// Executes a command in the <see cref="IDataStore"/>
        /// </summary>
        /// <param name="command">The <see cref="string"/> with the instructions to execute</param>
        /// <param name="cancelationToken">A <see cref="CancellationToken"/> to use to cancel the operation</param>
        /// <returns>A <see cref="Task{TResult}"/> operation with the data entities from the <see cref="IDataStore"/> </returns>
        Task<IEnumerable<TEntity>> ExecuteToEntityListAsync<TEntity>(string command, CancellationToken cancelationToken);

        /// <summary>
        /// Executes a command in the <see cref="IDataStore"/>
        /// </summary>
        /// <param name="command">The <see cref="string"/> with the instructions to execute</param>
        /// <param name="cancelationToken">A <see cref="CancellationToken"/> to use to cancel the operation</param>
        /// <returns>A <see cref="Task{TResult}"/> operation with the rows from the <see cref="IDataStore"/> </returns>
        Task<DataTable> ExecuteDataTableAsync(string command, CancellationToken cancelationToken);

        #endregion

        #region Methods
        /// <summary>
        /// Executes a command in the <see cref="IDataStore"/>
        /// </summary>
        /// <param name="command">The <see cref="string"/> with the instructions to execute</param>
        /// <returns>The number of affected records </returns>
        int Execute(string command);

        /// <summary>
        /// Executes the command in the <see cref="IDataStore"/>
        /// </summary>
        /// <param name="command">The <see cref="string"/> with the instructions to execute</param>
        /// <returns>An object with the result of the command</returns>
        object ExecuteScalar(string command);

        /// <summary>
        /// Executes the command in the <see cref="IDataStore"/>
        /// </summary>
        /// <typeparam name="T">The expected data type or the result</typeparam>
        /// <param name="command">The <see cref="IDataCommand"/> with the instructions to execute</param>
        /// <returns>An instance with the result of the command</returns>
        T ExecuteScalar<T>(string command);

        /// <summary>
        /// Executes a command in the <see cref="IDataStore"/>
        /// </summary>
        /// <param name="command">The <see cref="string"/> with the instructions to execute</param>
        /// <returns>A <see cref="IEnumerable{IDataRecord}"/> with the records from the <see cref="IDataStore"/> </returns>
        IEnumerable<IDataRecord> ExecuteToDataRecord(string command);

        /// <summary>
        /// Executes a command in the <see cref="IDataStore"/>
        /// </summary>
        /// <param name="command">The <see cref="string"/> with the instructions to execute</param>
        /// <returns>A <see cref="IDataReader"/> with the records from the <see cref="IDataStore"/> </returns>
        IDataReader ExecuteToDataReader(string command);

        /// <summary>
        /// Executes a command in the <see cref="IDataStore"/>
        /// </summary>
        /// <param name="command">The <see cref="string"/> with the instructions to execute</param>
        /// <returns>A <see cref="IEnumerable{T}"/> with the data entities from the <see cref="IDataStore"/> </returns>
        IEnumerable<TEntity> ExecuteToEntityList<TEntity>(string command);

        /// <summary>
        /// Executes a command in the <see cref="IDataStore"/>
        /// </summary>
        /// <param name="command">The <see cref="string"/> with the instructions to execute</param>
        /// <returns>A <see cref="DataSet"/> with the rows from the <see cref="IDataStore"/> </returns>
        DataTable ExecuteDataTable(string command);

        #endregion
    }
}
