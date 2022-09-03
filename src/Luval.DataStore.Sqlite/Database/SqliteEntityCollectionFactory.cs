using Luval.DataStore.Database.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.DataStore.Database.Sqlite
{
    /// <inheritdoc/>
    public class SqliteEntityCollectionFactory : DbEntityCollectionFactory
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="dataStore">The <see cref="SqliteDataStore"/> to use</param>
        public SqliteEntityCollectionFactory(SqliteDataStore dataStore) : this(dataStore, new SqlLiteCommandProviderFactory())
        {
        }


        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="dataStore">The <see cref="SqliteDataStore"/> to use</param>
        /// <param name="commandProviderFactory">The <see cref="SqlCommandProviderFactory"/> to use</param>
        public SqliteEntityCollectionFactory(SqliteDataStore dataStore, SqlLiteCommandProviderFactory commandProviderFactory) : base(dataStore, commandProviderFactory)
        {
        }
    }
}
