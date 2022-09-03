using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.DataStore.Database
{
    /// <inheritdoc/>
    public class AnsiSqlCommandProviderFactory : BaseSqlCommandProviderFactory
    {

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public AnsiSqlCommandProviderFactory() : this(new ReflectionDataRecordMapper())
        {

        }

        /// <summary>
        /// Creates a new instance
        /// </summary>

        public AnsiSqlCommandProviderFactory(IDataRecordMapper dataRecordMapper): base(dataRecordMapper)
        {
        }



        /// <inheritdoc/>
        public override IDataCommandProvider<TEntity> Create<TEntity>() where TEntity : class
        {
            return new AnsiSqlCommandProvider<TEntity>(this.RecordMapper);
        }
    }
}
