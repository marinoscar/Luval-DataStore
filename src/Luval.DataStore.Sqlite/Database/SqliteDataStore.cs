using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Luval.DataStore.Database.Sqlite
{
    /// <inheritdoc/>
    public class SqliteDataStore : DbDataStore
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="connectionString">The connection string to conenct with the Sqlite database</param>
        /// <param name="parseViaFramework">Flag to indicate if the connection should parse via framework</param>
        public SqliteDataStore(string connectionString, bool parseViaFramework = false) : this(connectionString, new SqlCommandFactory(), parseViaFramework)
        {

        }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="connectionString">The connection string to conenct with the Sqlite database</param>
        /// <param name="sqlCommandFactory">An implementation of <see cref="ISqlCommandFactory"/> to configure the <see cref="IDbCommand"/> instances</param>
        /// <param name="parseViaFramework">Flag to indicate if the connection should parse via framework</param>
        public SqliteDataStore(string connectionString, ISqlCommandFactory sqlCommandFactory, bool parseViaFramework = false) : this(connectionString, sqlCommandFactory, new ReflectionDataRecordMapper(), parseViaFramework)
        {

        }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="connectionString">The connection string to conenct with the Sqlite database</param>
        /// <param name="sqlCommandFactory">An implementation of <see cref="ISqlCommandFactory"/> to configure the <see cref="IDbCommand"/> instances</param>
        /// <param name="dataRecordMapper">The <see cref="IDataRecordMapper"/> implementation to parse the entity data</param>
        /// <param name="parseViaFramework">Flag to indicate if the connection should parse via framework</param>
        public SqliteDataStore(string connectionString, ISqlCommandFactory sqlCommandFactory, IDataRecordMapper dataRecordMapper, bool parseViaFramework = false) :
            this(() => new SQLiteConnection(connectionString, parseViaFramework), sqlCommandFactory, dataRecordMapper)
        {

        }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="databaseFile">The <see cref="FileInfo"/> with the database location</param>
        /// <param name="parseViaFramework">Flag to indicate if the connection should parse via framework</param>
        public SqliteDataStore(FileInfo databaseFile, bool parseViaFramework = false) :
            this(databaseFile, new SqlCommandFactory(), parseViaFramework)
        {

        }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="databaseFile">The <see cref="FileInfo"/> with the database location</param>
        /// <param name="sqlCommandFactory">An implementation of <see cref="ISqlCommandFactory"/> to configure the <see cref="IDbCommand"/> instances</param>
        /// <param name="parseViaFramework">Flag to indicate if the connection should parse via framework</param>
        public SqliteDataStore(FileInfo databaseFile, ISqlCommandFactory sqlCommandFactory, bool parseViaFramework = false) :
            this(databaseFile, sqlCommandFactory, new ReflectionDataRecordMapper(), parseViaFramework)
        {

        }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="databaseFile">The <see cref="FileInfo"/> with the database location</param>
        /// <param name="sqlCommandFactory">An implementation of <see cref="ISqlCommandFactory"/> to configure the <see cref="IDbCommand"/> instances</param>
        /// <param name="parseViaFramework">Flag to indicate if the connection should parse via framework</param>
        public SqliteDataStore(FileInfo databaseFile, ISqlCommandFactory sqlCommandFactory, IDataRecordMapper dataRecordMapper, bool parseViaFramework = false) :
            this(GetConnectionStringFromFileName(databaseFile.FullName), sqlCommandFactory, dataRecordMapper, parseViaFramework)
        {

        }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="connectionFactory">The delegate with a function that creates a new instance of <see cref="IDbConnection"/></param>
        /// <param name="sqlCommandFactory">An implementation of <see cref="ISqlCommandFactory"/> to configure the <see cref="IDbCommand"/> instances</param>
        /// <param name="dataRecordMapper">The <see cref="IDataRecordMapper"/> implementation to parse the entity data</param>
        public SqliteDataStore(Func<IDbConnection> connectionFactory, ISqlCommandFactory sqlCommandFactory, IDataRecordMapper dataRecordMapper) : base(connectionFactory, sqlCommandFactory, dataRecordMapper)
        {
        }

        private static string GetConnectionStringFromFileName(string fileName)
        {
            return String.Format(@"Data Source={0};Version=3;", fileName);
        }
    }
}
