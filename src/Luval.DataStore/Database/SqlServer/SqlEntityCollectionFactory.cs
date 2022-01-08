using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.DataStore.Database.SqlServer
{
    /// <inheritdoc/>
    public class SqlEntityCollectionFactory : DbEntityCollectionFactory
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="connectionString">The connection string to conenct with the SQL Server database</param>
        public SqlEntityCollectionFactory(string connectionString): this(connectionString, new SqlCommandProviderFactory())
        {

        }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="connectionString">The connection string to conenct with the SQL Server database</param>
        /// <param name="commandProviderFactory">The <see cref="SqlCommandProviderFactory"/> to use</param>
        public SqlEntityCollectionFactory(string connectionString, SqlCommandProviderFactory commandProviderFactory) : this(new SqlDataStore(connectionString), commandProviderFactory)
        {

        }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="dataStore">The <see cref="SqlDataStore"/> to use</param>
        /// <param name="commandProviderFactory">The <see cref="SqlCommandProviderFactory"/> to use</param>
        public SqlEntityCollectionFactory(SqlDataStore dataStore, SqlCommandProviderFactory commandProviderFactory) : base(dataStore, commandProviderFactory)
        {
        }
    }
}
