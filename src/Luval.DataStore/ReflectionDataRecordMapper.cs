using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.DataStore
{
    /// <summary>
    /// Represents an implementation of <see cref="IDataRecordMapper"/> using reflection
    /// </summary>
    public class ReflectionDataRecordMapper : IDataRecordMapper
    {
        /// <inheritdoc/>
        public T FromDataRecord<T>(IDataRecord record)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public object FromDataRecord(IDataRecord record, Type entityType)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IDataRecord ToDataRecord(object entity)
        {
            throw new NotImplementedException();
        }
    }
}
