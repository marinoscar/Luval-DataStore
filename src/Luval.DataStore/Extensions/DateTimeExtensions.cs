using Luval.DataStore.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.DataStore.Extensions
{
    /// <summary>
    /// Provides extension methods for the <see cref="DateTime"/> struct
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Formats the <see cref="DateTime"/> into a string
        /// </summary>
        /// <param name="d">The <see cref="DateTime"/> instance</param>
        /// <returns>Formated <see cref="DateTime"/> string</returns>
        public static string ToSqliteString(this DateTime d)
        {
            return SqlFormatter.FormatDateTimeAsString(d);
        }

        /// <summary>
        /// Formats the <see cref="DateTime"/> into a string
        /// </summary>
        /// <param name="d">The <see cref="DateTime"/> instance</param>
        /// <returns>Formated <see cref="DateTime"/> string</returns>
        public static string ToSqliteString(this DateTime? d)
        {
            if (d == null) return null;
            return SqlFormatter.FormatDateTimeAsString(d.Value);
        }


        /// <summary>
        /// Provides the number of elapsed seconds between the current time and the root value
        /// </summary>
        /// <param name="d">The datatime value</param>
        /// <param name="root">The datetime root</param>
        /// <returns>The number of elapsed seconds</returns>
        public static long? ToElapsedSeconds(this DateTime? d, DateTime root)
        {
            if (d == null) return null;
            return ToElapsedSeconds(d.Value, root);
        }

        /// <summary>
        /// Provides the number of elapsed seconds between the current time and the root value
        /// </summary>
        /// <param name="d">The datatime value</param>
        /// <param name="root">The datetime root</param>
        /// <returns>The number of elapsed seconds</returns>
        public static long ToElapsedSeconds(this DateTime d, DateTime root)
        {
            return Convert.ToInt64(d.Subtract(root).TotalSeconds);
        }

        /// <summary>
        /// Provides the number of elapsed seconds between the current time and Jan 1st 1983
        /// </summary>
        /// <param name="d">The datatime value</param>
        /// <returns>The number of elapsed seconds</returns>
        public static long? ToElapsedSeconds(this DateTime? d)
        {
            return ToElapsedSeconds(d, new DateTime(1983, 1, 1));
        }

        /// <summary>
        /// Provides the number of elapsed seconds between the current time and Jan 1st 1983
        /// </summary>
        /// <param name="d">The datatime value</param>
        /// <returns>The number of elapsed seconds</returns>
        public static long ToElapsedSeconds(this DateTime d)
        {
            return ToElapsedSeconds(d, new DateTime(1983, 1, 1));
        }
    }
}
