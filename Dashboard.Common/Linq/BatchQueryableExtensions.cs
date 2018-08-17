using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Ofa.Common.Linq;

namespace Dashboard.Common.Linq
{
    public static class BatchQueryableExtensions
    {
        private const int MAX_DB_CONTAINS_SIZE = 2000;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        public static IEnumerable<T> InRange<T, TValue>(this IQueryable<T> source, Expression<Func<T, TValue>> selector, IEnumerable<TValue> values)
        {
            return InRange(source, selector, MAX_DB_CONTAINS_SIZE, values);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        public static IEnumerable<T> InRange<T, TValue>(this IQueryable<T> source, Expression<Func<T, TValue>> selector, int blockSize, IEnumerable<TValue> values)
        {
            MethodInfo containsMethod = null;

            foreach (MethodInfo tmp in typeof(Enumerable).GetMethods(BindingFlags.Public | BindingFlags.Static))
                if (tmp.Name == "Contains" && tmp.IsGenericMethodDefinition && tmp.GetParameters().Length == 2)
                {
                    containsMethod = tmp.MakeGenericMethod(typeof(TValue));
                    break;
                }

            if (containsMethod == null)
                throw new InvalidOperationException("Unable to locate Contains method");

            foreach (TValue[] block in values.InSetsOf(blockSize))
            {
                var row = Expression.Parameter(typeof(T), "row");
                var member = Expression.Invoke(selector, row);
                var keys = Expression.Constant(block, typeof(TValue[]));
                var predicate = Expression.Call(containsMethod, keys, member);
                var lambda = Expression.Lambda<Func<T, bool>>(predicate, row);

                foreach (T record in source.Where(lambda))
                    yield return record;
            }
        }
    }
}
