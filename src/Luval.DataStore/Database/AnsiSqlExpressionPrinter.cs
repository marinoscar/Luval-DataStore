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
    public class AnsiSqlExpressionPrinter : ExpressionVisitor, ISqlExpressionPrinter
    {
        private StringBuilder sb;

        /// <inheritdoc/>
        public virtual string Where<T>(Expression<Func<T, bool>> expression)
        {
            this.sb = new StringBuilder();
            this.Visit(expression);
            return sb.ToString();

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

        private static Expression StripQuotes(Expression e)
        {
            while (e.NodeType == ExpressionType.Quote)
            {
                e = ((UnaryExpression)e).Operand;
            }
            return e;
        }

        /// <inheritdoc/>
        protected override Expression VisitMethodCall(MethodCallExpression m)
        {
            if (m.Method.DeclaringType == typeof(Queryable) && m.Method.Name == "Where")
            {
                this.Visit(m.Arguments[0]);
                LambdaExpression lambda = (LambdaExpression)StripQuotes(m.Arguments[1]);
                this.Visit(lambda.Body);
                return m;
            }

            throw new NotSupportedException(string.Format("The method '{0}' is not supported", m.Method.Name));
        }

        /// <inheritdoc/>
        protected override Expression VisitUnary(UnaryExpression u)
        {
            switch (u.NodeType)
            {
                case ExpressionType.Not:
                    sb.Append(" NOT ");
                    this.Visit(u.Operand);
                    break;
                case ExpressionType.Convert:
                    this.Visit(u.Operand);
                    break;
                default:
                    throw new NotSupportedException(string.Format("The unary operator '{0}' is not supported", u.NodeType));
            }
            return u;
        }


        /// <inheritdoc/>
        protected override Expression VisitBinary(BinaryExpression b)
        {
            sb.Append("(");
            this.Visit(b.Left);

            switch (b.NodeType)
            {
                case ExpressionType.And:
                    sb.Append(" AND ");
                    break;

                case ExpressionType.AndAlso:
                    sb.Append(" AND ");
                    break;

                case ExpressionType.Or:
                    sb.Append(" OR ");
                    break;

                case ExpressionType.OrElse:
                    sb.Append(" OR ");
                    break;

                case ExpressionType.Equal:
                    if (IsNullConstant(b.Right))
                    {
                        sb.Append(" IS ");
                    }
                    else
                    {
                        sb.Append(" = ");
                    }
                    break;

                case ExpressionType.NotEqual:
                    if (IsNullConstant(b.Right))
                    {
                        sb.Append(" IS NOT ");
                    }
                    else
                    {
                        sb.Append(" <> ");
                    }
                    break;

                case ExpressionType.LessThan:
                    sb.Append(" < ");
                    break;

                case ExpressionType.LessThanOrEqual:
                    sb.Append(" <= ");
                    break;

                case ExpressionType.GreaterThan:
                    sb.Append(" > ");
                    break;

                case ExpressionType.GreaterThanOrEqual:
                    sb.Append(" >= ");
                    break;

                default:
                    throw new NotSupportedException(string.Format("The binary operator '{0}' is not supported", b.NodeType));

            }

            this.Visit(b.Right);
            sb.Append(")");
            return b;
        }
        /// <inheritdoc/>
        protected override Expression VisitConstant(ConstantExpression c)
        {
            IQueryable q = c.Value as IQueryable;

            if (q == null && c.Value == null)
            {
                sb.Append("NULL");
            }
            else if (q == null)
            {
                sb.Append(c.Value.ToSql());
            }

            return c;
        }
        /// <inheritdoc/>
        protected override Expression VisitMember(MemberExpression m)
        {
            if (m.Expression != null && m.Expression.NodeType == ExpressionType.Parameter)
            {
                sb.Append(m.Member.GetName());
                return m;
            }
            throw new NotSupportedException(string.Format("The member '{0}' is not supported", m.Member.Name));
        }
        /// <inheritdoc/>
        protected bool IsNullConstant(Expression exp)
        {
            return (exp.NodeType == ExpressionType.Constant && ((ConstantExpression)exp).Value == null);
        }
    }
}
