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
        /// <param name="fileName">The connection string to conenct with the Sqlite database</param>
        public SqliteDataStore(string fileName) : this(fileName, new SqlCommandFactory())
        {

        }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="fileName">The connection string to conenct with the Sqlite database</param>
        /// <param name="sqlCommandFactory">An implementation of <see cref="ISqlCommandFactory"/> to configure the <see cref="IDbCommand"/> instances</param>
        public SqliteDataStore(string fileName, ISqlCommandFactory sqlCommandFactory) : this(fileName, sqlCommandFactory, new ReflectionDataRecordMapper())
        {

        }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="fileName">The connection string to conenct with the Sqlite database</param>
        /// <param name="sqlCommandFactory">An implementation of <see cref="ISqlCommandFactory"/> to configure the <see cref="IDbCommand"/> instances</param>
        /// <param name="dataRecordMapper">The <see cref="IDataRecordMapper"/> implementation to parse the entity data</param>
        public SqliteDataStore(string fileName, ISqlCommandFactory sqlCommandFactory, IDataRecordMapper dataRecordMapper) :
            this(() => new SQLiteConnection(GetConnectionStringFromFileName(fileName)), sqlCommandFactory, dataRecordMapper)
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
            return String.Format("URI=file:{0}", fileName);
        }
    }
}
