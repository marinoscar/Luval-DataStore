using Luval.DataStore.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Luval.DataStore.Extensions
{
    /// <summary>
    /// Provides extension methods for the <see cref="MemberInfo"/> class
    /// </summary>
    public static class MemberInfoExtensions
    {
        /// <summary>
        /// Gets the annotated name of the member
        /// </summary>
        /// <param name="info">The <see cref="MemberInfo"/> to use</param>
        /// <returns>The <see cref="MemberInfo"/> annoted name</returns>
        public static string GetName(this MemberInfo info)
        {
            if (info.CustomAttributes == null || !info.CustomAttributes.Any()) return info.Name;
            var ca = info.GetCustomAttributes().FirstOrDefault(i => i.GetType() == typeof(ColumnNameAttribute));
            if (ca != null) return ((ColumnNameAttribute)ca).Name;
            var ta = info.GetCustomAttributes().FirstOrDefault(i => i.GetType() == typeof(TableNameAttribute));
            if (ca != null) return ((TableNameAttribute)ta).TableName.GetFullTableName();
            return info.Name;
        }
    }
}
