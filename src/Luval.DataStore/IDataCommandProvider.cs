using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Luval.DataStore
{
    /// <summary>
    /// Represents the implementation of <see cref="IDataCommand"/> to persist data into a <see cref="IDataStore"/>
    /// </summary>
    public interface IDataCommandProvider<TEntity>
    {
        /// <summary>
        /// Gets a insert command
        /// </summary>
        /// <param name="data">Data to persist in the <see cref="IDataStore"/></param>
        /// <returns>An instance of <see cref="IDataCommand"/> with the instructions for the <see cref="IDataStore"/></returns>
        IDataCommand GetInsert(TEntity data);

        /// <summary>
        /// Gets a update command
        /// </summary>
        /// <param name="data">Data to persist in the <see cref="IDataStore"/></param>
        /// <returns>An instance of <see cref="IDataCommand"/> with the instructions for the <see cref="IDataStore"/></returns>
        IDataCommand GetUpdate(TEntity data);

        /// <summary>
        /// Gets a delete command
        /// </summary>
        /// <param name="data">Data to persist in the <see cref="IDataStore"/></param>
        /// <returns>An instance of <see cref="IDataCommand"/> with the instructions for the <see cref="IDataStore"/></returns>
        IDataCommand GetDelete(TEntity data);

        /// <summary>
        /// Gets a query command
        /// </summary>
        /// <param name="expression">The query expression to use to extract the data from the <see cref="IDataStore"/></param>
        /// <returns></returns>
        IDataCommand Query(Expression<Func<TEntity, bool>> expression);
    }

    /// <inheritdoc/>
    public interface IDataRecordCommandProvider : IDataCommandProvider<IDictionary<string, object>>
    {

    }
}
