using Luval.DataStore.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.DataStore
{
    /// <summary>
    /// Provides an <see cref="IDictionary{TKey, TValue}"/> implementation of the <see cref="IDataRecord"/> interface
    /// </summary>
    public class DataRecord : IDataRecord
    {
        private readonly Dictionary<string, object> _record;

        /// <summary>
        /// Creates a new instance of <see cref="DataRecord"/>
        /// </summary>
        public DataRecord()
        {
            _record = new Dictionary<string, object>();
        }

        /// <summary>
        /// Creates a new instance of <see cref="DataRecord"/>
        /// </summary>
        /// <param name="record">A <see cref="Dictionary{String, Object}"/> to load the data from</param>
        public DataRecord(Dictionary<string, object> record)
        {
            _record = record;
        }

        /// <summary>
        /// Creates a new instance of <see cref="DataRecord"/>
        /// </summary>
        /// <param name="dataRecord">A <see cref="IDataRecord"/> to load the data from</param>
        public DataRecord(IDataRecord dataRecord) : base()
        {
            _record = new Dictionary<string, object>();
            for (int i = 0; i < dataRecord.FieldCount; i++)
            {
                _record[dataRecord.GetName(i)] = dataRecord.GetValue(i);
            }
        }

        /// <inheritdoc/>
        [NotMapped]
        public object this[int i] => GetValue(i);

        /// <inheritdoc/>
        [NotMapped]
        public object this[string name] => _record[name];

        /// <inheritdoc/>
        [NotMapped]
        public int FieldCount => _record.Keys.Count;

        /// <inheritdoc/>
        public bool GetBoolean(int i)
        {
            return Convert.ToBoolean(GetValue(i));
        }

        /// <inheritdoc/>
        public byte GetByte(int i)
        {
            return Convert.ToByte(GetValue(i));
        }

        /// <inheritdoc/>
        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            buffer = UTF8Encoding.UTF8.GetBytes(GetString(i)).Skip(bufferoffset).Take(length).ToArray();
            return buffer.LongLength;
        }

        /// <inheritdoc/>
        public char GetChar(int i)
        {
            return Convert.ToChar(GetValue(i));
        }

        /// <inheritdoc/>
        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            buffer = GetString(i).ToCharArray().Skip(bufferoffset).Take(length).ToArray();
            return buffer.LongLength;
        }

        /// <inheritdoc/>
        public IDataReader GetData(int i)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public string GetDataTypeName(int i)
        {
            return GetFieldType(i).Name;
        }

        /// <inheritdoc/>
        public DateTime GetDateTime(int i)
        {
            return Convert.ToDateTime(GetValue(i));
        }

        /// <inheritdoc/>
        public decimal GetDecimal(int i)
        {
            return Convert.ToDecimal(GetValue(i));
        }

        /// <inheritdoc/>
        public double GetDouble(int i)
        {
            return Convert.ToDouble(GetValue(i));
        }

        /// <inheritdoc/>
        public Type GetFieldType(int i)
        {
            return GetValue(i).GetType();
        }

        /// <inheritdoc/>
        public float GetFloat(int i)
        {
            return Convert.ToSingle(GetValue(i));
        }

        /// <inheritdoc/>
        public Guid GetGuid(int i)
        {
            return Guid.Parse(GetString(i));
        }

        /// <inheritdoc/>
        public short GetInt16(int i)
        {
            return Convert.ToInt16(GetValue(i));
        }

        /// <inheritdoc/>
        public int GetInt32(int i)
        {
            return Convert.ToInt32(GetValue(i));
        }

        /// <inheritdoc/>
        public long GetInt64(int i)
        {
            return Convert.ToInt64(GetValue(i));
        }

        /// <inheritdoc/>
        public string GetName(int i)
        {
            return _record.Keys.ToList()[i];
        }

        /// <inheritdoc/>
        public int GetOrdinal(string name)
        {
            return _record.Keys.ToList().IndexOf(name);
        }

        /// <inheritdoc/>
        public string GetString(int i)
        {
            return Convert.ToString(GetValue(i));
        }

        /// <inheritdoc/>
        public object GetValue(int i)
        {
            return _record[GetName(i)];
        }

        /// <inheritdoc/>
        public int GetValues(object[] values)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public bool IsDBNull(int i)
        {
            return GetValue(i).IsNullOrDbNull();
        }
    }
}
