using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.DataStore.Extensions
{
    /// <summary>
    /// Provides extensions to the <see cref="IDataRecord"/> interface
    /// </summary>
    public static class IDataRecordExtensions
    {
        /// <summary>
        /// Creates a <see cref="IDictionary{TKey, TValue}"/> from a <see cref="IDataRecord"/> implementation
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public static IDictionary<string, object> ToDictionary(this IDataRecord record)
        {
            var dic = new Dictionary<string, object>();
            for (int i = 0; i < record.FieldCount; i++)
            {
                dic[record.GetName(i)] = record.GetValue(i);
            }
            return dic;
        }
    }
}
