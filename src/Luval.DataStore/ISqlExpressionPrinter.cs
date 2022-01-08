using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Luval.DataStore
{
    /// <summary>
    /// Provides an implemention to parse an <see cref="Expression"/> tree into a sql string
    /// </summary>
    public interface ISqlExpressionPrinter
    {
        /// <summary>
        /// Gets a sql statement based on the expression provided
        /// </summary>
        /// <typeparam name="TEntity">The <see cref="Type"/> that represents the data entity</typeparam>
        /// <param name="filterExpression">The filter expression to use</param>
        /// <returns>A sql statement for the entity</returns>
        string Where<TEntity>(Expression<Func<TEntity, bool>> filterExpression);

        /// <summary>
        /// Gets a sql statement based on the expression provided
        /// </summary>
        /// <typeparam name="TEntity">The <see cref="Type"/> that represents the data entity</typeparam>
        /// <param name="orderByExpression">The order by expression to use</param>
        /// <param name="descending">Indicates if the order should be descending, otherwhise it would be ascending</param>
        /// <returns>A sql statement for the entity</returns>
        string OrderBy<TEntity>(Expression<Func<TEntity, object>> orderByExpression, bool descending);

        //public string ResolveOrderBy<T>(Expression<Func<T, object>> orderBy, bool descending)
        //{
        //    if (orderBy == null) return string.Empty;
        //    var expression = (UnaryExpression)orderBy.Body;
        //    var memberExpression = (MemberExpression)expression.Operand;
        //    var column = ResolveColumnName(memberExpression.Member.Name);
        //    return "ORDER BY {0} {1}".Fi(ResolveColumnName(column), descending ? "DESC" : "ASC");
        //}
    }
}
