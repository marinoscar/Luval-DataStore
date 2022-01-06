using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Luval.DataStore
{
    public abstract class DataStore : IDataStore
    {
        #region Methods

        /// <inheritdoc/>
        public abstract int Execute(IDataCommand command);

        /// <inheritdoc/>
        public abstract IDataReader ExecuteToDataReader(IDataCommand command);

        /// <inheritdoc/>
        public abstract IEnumerable<IDataRecord> ExecuteToDataRecord(IDataCommand command);

        /// <inheritdoc/>
        public abstract DataSet ExecuteToDataset(IDataCommand command);

        /// <inheritdoc/>
        public abstract IEnumerable<TEntity> ExecuteToEntityList<TEntity>(IDataCommand command);

        #endregion

        #region Async Methods

        /// <inheritdoc/>
        public Task<int> ExecuteAsync(IDataCommand command, CancellationToken cancelationToken)
        {
            return Task.Run(() => { return Execute(command); }, cancelationToken);
        }


        /// <inheritdoc/>
        public Task<IDataReader> ExecuteToDataReaderAsync(IDataCommand command, CancellationToken cancelationToken)
        {
            return Task.Run(() => { return ExecuteToDataReader(command); }, cancelationToken);
        }

        /// <inheritdoc/>
        public Task<IEnumerable<IDataRecord>> ExecuteToDataRecordAsync(IDataCommand command, CancellationToken cancelationToken)
        {
            return Task.Run(() => { return ExecuteToDataRecord(command); }, cancelationToken);
        }

        /// <inheritdoc/>
        public Task<IEnumerable<TEntity>> ExecuteToEntityListAsync<TEntity>(IDataCommand command, CancellationToken cancelationToken)
        {
            return Task.Run(() => { return ExecuteToEntityList<TEntity>(command); }, cancelationToken);
        }

        /// <inheritdoc/>
        public Task<DataSet> ExecuteToDatasetAsync(IDataCommand command, CancellationToken cancelationToken)
        {
            return Task.Run(() => { return ExecuteToDataset(command); }, cancelationToken);
        }

        #endregion
    }
}
