using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.DataStore.Extensions
{
    /// <summary>
    /// Provides extensions to the <see cref="Byte"/> class
    /// </summary>
    public static class ByteExtensions
    {
        static ByteExtensions()
        {
            var hexValues = new string[256];

            for (var i = 0; i < hexValues.Length; i++)
            {
                hexValues[i] = "{0:x2}".Fi(i);
            }

            HexValues = new ReadOnlyCollection<string>(hexValues);
        }

        public static string ToHex(this byte b)
        {
            return HexValues[b];
        }

        public static readonly ReadOnlyCollection<string> HexValues;
    }
}
