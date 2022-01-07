using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Luval.DataStore
{


    /// <inheritdoc/>
    public class UnitOfWork<TEntity> : IUnitOfWork<TEntity> where TEntity : class
    {

        /// <summary>
        /// Creates a new instance of <see cref="UnitOfWork"/>
        /// </summary>
        /// <param name="dataEntityCollection">The <see cref="IDataEntityCollection{TEntity}"/> implementation to use</param>
        public UnitOfWork(IDataEntityCollection<TEntity> dataEntityCollection)
        {
            if(dataEntityCollection == null) throw new ArgumentNullException(nameof(dataEntityCollection));

            Entities = dataEntityCollection;
            CommandProvider = dataEntityCollection.CommandProvider;
            DataStore = dataEntityCollection.DataStore; 
        }

        /// <inheritdoc/>
        public IDataEntityCollection<TEntity> Entities { get; private set; }

        /// <summary>
        /// Gets the instance of the <see cref="IDataStore"/> that would be used to persist the data
        /// </summary>
        public IDataStore DataStore { get; private set; }

        /// <summary>
        /// Gets the <see cref="IDataRecordCommandProvider"/> to persist data into the <see cref="IDataStore"/>
        /// </summary>
        public IDataCommandProvider<TEntity> CommandProvider { get; private set; }

        /// <inheritdoc/>
        public virtual int SaveChanges()
        {
            var res = 0;
            try
            {
                foreach (var insertEnt in Entities.Inserted)
                {
                    res += DataStore.Execute(CommandProvider.GetInsert(insertEnt));
                }
                foreach (var insertEnt in Entities.Updated)
                {
                    res += DataStore.Execute(CommandProvider.GetUpdate(insertEnt));
                }
                foreach (var insertEnt in Entities.Deleted)
                {
                    res += DataStore.Execute(CommandProvider.GetDelete(insertEnt));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to save the changes in the data store", ex);
            }
            return res;
        }

        /// <inheritdoc/>
        public virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return Task.Run(() => { return SaveChanges(); }, cancellationToken);
        }
    }

    public class UnitOfWork : UnitOfWork<IDictionary<string, object>>
    {
        /// <summary>
        /// Creates a new instance of <see cref="UnitOfWork"/>
        /// </summary>
        /// <param name="dataEntityCollection">The <see cref="IDataRecordEntityCollection"/> implementation to use</param>
        public UnitOfWork(IDataRecordEntityCollection dataEntityCollection): base(dataEntityCollection)
        {

        }
    }


}
