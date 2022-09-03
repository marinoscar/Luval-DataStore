using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.DataStore.Database.Sqlite
{
    /// <inheritdoc/>
    public class SqliteUnitOfWorkFactory : DbUnitOfWorkFactory
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="dataStore">The <see cref="SqliteDataStore"/> to use</param>
        public SqliteUnitOfWorkFactory(SqliteDataStore dataStore) : this(new SqliteEntityCollectionFactory(dataStore))
        {

        }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="collectionFactory">The <see cref="SqliteEntityCollectionFactory"/> to use</param>
        public SqliteUnitOfWorkFactory(SqliteEntityCollectionFactory collectionFactory) : base(collectionFactory)
        {
        }
    }
}
