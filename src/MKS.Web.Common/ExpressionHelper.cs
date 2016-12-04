using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MKS.Web.Common
{
    public static class ExpressionHelper
    {
        /// <summary>
        /// Create member accessor lambda by property name.
        /// </summary>
        /// <returns>
        /// lambda:    t => t.propertyName
        /// </returns>
        public static Expression<Func<T, object>> CreateMemberGetter<T>(string propertyName)
        {
            var param = Expression.Parameter(typeof(T), "t");

            return Expression.Lambda<Func<T, object>>(
                Expression.Convert(
                    Expression.PropertyOrField(param, propertyName),
                    typeof(object)
                ),
                param
            );
        }

        /// <summary>
        /// Extract property name from member accessor.
        /// E.g. GetMemberName(t => t.Name) returns "Name".
        /// t => t.Subobject.Prop return "Subobject.Prop"
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="memberAccessor"></param>
        /// <returns></returns>
        public static string GetMemberName<T, TProperty>(Expression<Func<T, TProperty>> memberAccessor)
        {
            MemberExpression memberExp;
            if (!TryFindMemberExpression(memberAccessor.Body, out memberExp))
                return string.Empty;

            var memberNames = new Stack<string>();
            do
            {
                memberNames.Push(memberExp.Member.Name);
            }
            while (TryFindMemberExpression(memberExp.Expression, out memberExp));

            return string.Join(".", memberNames.ToArray());
        }

        private static bool TryFindMemberExpression(Expression exp, out MemberExpression memberExp)
        {
            memberExp = exp as MemberExpression;
            if (memberExp != null)
            {
                // heyo! that was easy enough
                return true;
            }

            // if the compiler created an automatic conversion,
            // it'll look something like...
            // obj => Convert(obj.Property) [e.g., int -> object]
            // OR:
            // obj => ConvertChecked(obj.Property) [e.g., int -> long]
            // ...which are the cases checked in IsConversion
            if (IsConversion(exp) && exp is UnaryExpression)
            {
                memberExp = ((UnaryExpression)exp).Operand as MemberExpression;
                if (memberExp != null)
                {
                    return true;
                }
            }

            return false;
        }

        private static bool IsConversion(Expression exp)
        {
            return (
                exp.NodeType == ExpressionType.Convert ||
                exp.NodeType == ExpressionType.ConvertChecked
            );
        }
    }
}
