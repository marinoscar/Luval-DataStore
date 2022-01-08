using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.DataStore.Database.SqlServer
{
    /// <inheritdoc/>
    public class SqlDataStore : DbDataStore
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="connectionString">The connection string to conenct with the SQL Server database</param>
        public SqlDataStore(string connectionString) : this(connectionString, new SqlCommandFactory())
        {

        }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="connectionString">The connection string to conenct with the SQL Server database</param>
        /// <param name="sqlCommandFactory">An implementation of <see cref="ISqlCommandFactory"/> to configure the <see cref="IDbCommand"/> instances</param>
        public SqlDataStore(string connectionString, ISqlCommandFactory sqlCommandFactory) : this(connectionString, sqlCommandFactory, new ReflectionDataRecordMapper())
        {

        }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="connectionString">The connection string to conenct with the SQL Server database</param>
        /// <param name="sqlCommandFactory">An implementation of <see cref="ISqlCommandFactory"/> to configure the <see cref="IDbCommand"/> instances</param>
        /// <param name="dataRecordMapper">The <see cref="IDataRecordMapper"/> implementation to parse the entity data</param>
        public SqlDataStore(string connectionString, ISqlCommandFactory sqlCommandFactory, IDataRecordMapper dataRecordMapper) :
            this(() => new SqlConnection(connectionString), sqlCommandFactory, dataRecordMapper)
        {

        }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="connectionFactory">The delegate with a function that creates a new instance of <see cref="IDbConnection"/></param>
        /// <param name="sqlCommandFactory">An implementation of <see cref="ISqlCommandFactory"/> to configure the <see cref="IDbCommand"/> instances</param>
        /// <param name="dataRecordMapper">The <see cref="IDataRecordMapper"/> implementation to parse the entity data</param>
        public SqlDataStore(Func<IDbConnection> connectionFactory, ISqlCommandFactory sqlCommandFactory, IDataRecordMapper dataRecordMapper) : base(connectionFactory, sqlCommandFactory, dataRecordMapper)
        {
        }
    }
}
