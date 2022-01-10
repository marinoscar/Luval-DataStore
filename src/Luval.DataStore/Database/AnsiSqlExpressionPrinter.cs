using Luval.DataStore.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Luval.DataStore.Database
{
    /// <summary>
    /// Provides an implementation to create Sql statements from Lambda expressions
    /// </summary>
    public class AnsiSqlExpressionPrinter : ISqlExpressionPrinter
    {
        /// <inheritdoc/>
        public virtual string Where<T>(Expression<Func<T, bool>> expression)
        {
            return ResolveExpression(expression.Body, typeof(T));

        }

        /// <inheritdoc/>
        public virtual string OrderBy<TEntity>(Expression<Func<TEntity, object>> orderByExpression, bool descending)
        {
            if (orderByExpression == null) return string.Empty;
            var expression = (UnaryExpression)orderByExpression.Body;
            var memberExpression = (MemberExpression)expression.Operand;
            var column = memberExpression.Member.GetName();
            return "ORDER BY {0} {1}".Fi(column, descending ? "DESC" : "ASC");
        }

        #region Expresion Methods

        private string ResolveBinaryExpression(Expression expression, Type modelType)
        {
            var localExpression = (BinaryExpression)expression;
            var left = ResolveExpression(localExpression.Left, modelType);
            var right = ResolveExpression(localExpression.Right, modelType);
            var oper = ResolveExpressionNodeType(localExpression.NodeType);
            return string.Format("({0} {1} {2})", left, oper, right);
        }

        private static bool IsConstantExpression(Type type)
        {
            return typeof(ConstantExpression) == type || type.IsSubclassOf(typeof(ConstantExpression));
        }

        private string ResolveExpression(Expression expression, Type modelType)
        {
            var type = expression.GetType();
            if (typeof(MemberExpression) == type || type.IsSubclassOf(typeof(MemberExpression)))
                return ResolveMemberExpression((MemberExpression)expression, modelType);
            if (IsConstantExpression(type))
                return ResolveConstantExpression(expression);
            if (typeof(MethodCallExpression) == type || type.IsSubclassOf(typeof(MethodCallExpression)))
                return ResolveMethodExpression(expression);
            if (typeof(BinaryExpression) == type || type.IsSubclassOf(typeof(BinaryExpression)))
                return ResolveBinaryExpression(expression, modelType);
            throw new ArgumentException(string.Format("Expression type {0} is not supported", type));
        }

        private string ResolveMethodExpression(Expression expression)
        {
            var localExpression = (MethodCallExpression)expression;
            var memberExpression = (MemberExpression)localExpression.Object;
            object value = null;
            if (memberExpression.Expression != null)
                value = GetConstantValueFromExpression(memberExpression.Expression);
            else
            {
                throw new InvalidOperationException("Expression not supported {0}".Fi(expression));
            }
            return Convert.ToString(localExpression.Method.Invoke(value, null)).ToSql();
        }

        private string ResolveMemberExpression(MemberExpression expression, Type modelType)
        {
            if (expression.Expression != null && expression.Expression.NodeType == ExpressionType.Parameter ||
                expression.Expression.NodeType == ExpressionType.Convert)
            {
                return expression.Member.GetName();
            }
            var member = Expression.Convert(expression, typeof(object));
            var lambda = Expression.Lambda<Func<object>>(member);
            var getter = lambda.Compile();
            return getter().ToSql();
        }

        private string ResolveConstantExpression(Expression expression)
        {
            return GetConstantValueFromExpression(expression).ToSql();
        }

        private object GetConstantValueFromExpression(Expression expression)
        {
            var localExpression = (ConstantExpression)expression;
            var member = Expression.Convert(localExpression, typeof(object));
            var lambda = Expression.Lambda<Func<object>>(member);
            var getter = lambda.Compile();
            return getter();
        }

        private string ResolveExpressionNodeType(ExpressionType nodeType)
        {
            switch (nodeType)
            {
                case ExpressionType.Equal:
                    return "=";
                case ExpressionType.GreaterThan:
                    return ">";
                case ExpressionType.GreaterThanOrEqual:
                    return ">=";
                case ExpressionType.LessThan:
                    return "<";
                case ExpressionType.LessThanOrEqual:
                    return "<=";
                case ExpressionType.AndAlso:
                    return "And";
                case ExpressionType.And:
                    return "And";
                case ExpressionType.OrElse:
                    return "Or";
                case ExpressionType.Or:
                    return "Or";
                case ExpressionType.NotEqual:
                    return "<>";
            }
            throw new ArgumentException(string.Format("ExpressionType {0} not supported", nodeType));
        }

        #endregion
    }
}
