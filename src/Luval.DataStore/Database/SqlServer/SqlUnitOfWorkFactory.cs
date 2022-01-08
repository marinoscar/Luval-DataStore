using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.DataStore.Database.SqlServer
{
    public class SqlUnitOfWorkFactory : DbUnitOfWorkFactory
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="connectionString">The connection string to conenct with the SQL Server database</param>
        public SqlUnitOfWorkFactory(string connectionString) : this(new SqlEntityCollectionFactory(connectionString))
        {

        }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="collectionFactory">The <see cref="SqlEntityCollectionFactory"/> to use</param>
        public SqlUnitOfWorkFactory(SqlEntityCollectionFactory collectionFactory) : base(collectionFactory)
        {
        }
    }
}
