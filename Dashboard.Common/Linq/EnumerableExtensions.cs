using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Dashboard.Common.Utilities;

namespace Dashboard.Common.Linq
{
    public static class EnumerableExtensions
    {
        public static int IndexOf(this string[] haystack, string needle, StringComparison comparer)
        {
            ParameterValidator.AssertIsNotNull("haystack", haystack);

            int indexOf = -1;

            for (int i = 0; i < haystack.Length; i++)
            {
                if (String.Equals(haystack[i], needle, comparer))
                {
                    indexOf = i;
                    break;
                }
            }

            return indexOf;
        }

        public static IEnumerable<TTarget> SafeCast<TTarget>(this IEnumerable source)
        {
            return source == null ? null : source.Cast<TTarget>();
        }
    }
}
