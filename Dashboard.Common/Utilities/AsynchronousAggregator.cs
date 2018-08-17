using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.Common.Utilities
{
    public static class AsynchronousAggregator
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public static ICollection<TResult> QueryAsync<TArg, TResult>(TArg arg, Action<Exception> onError, params Func<TArg, Collection<TResult>>[] queries)
        {
            List<TResult> returnList = new List<TResult>();
            onError = onError ?? delegate(Exception e) { };

            Parallel.For<List<TResult>>(0,
                queries.Count(),
                () => new List<TResult>(),
                delegate(int i, ParallelLoopState state, List<TResult> subtotal)
                {
                    try
                    {
                        subtotal.AddRange(queries.ElementAt(i)(arg));
                    }
                    catch (Exception e)
                    {
                        onError(e);
                    }
                    return subtotal;
                },
                delegate(List<TResult> subtotal)
                {
                    returnList.AddRange(subtotal);
                });

            return returnList;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public static ICollection<TResult> QueryAsync<TResult>(Action<Exception> onError, params Func<Collection<TResult>>[] queries)
        {
            List<TResult> returnList = new List<TResult>();
            onError = onError ?? delegate(Exception e) { };

            Parallel.For<List<TResult>>(0,
                queries.Count(),
                () => new List<TResult>(),
                delegate(int i, ParallelLoopState state, List<TResult> subtotal)
                {
                    try
                    {
                        subtotal.AddRange(queries.ElementAt(i)());
                    }
                    catch (Exception e)
                    {
                        onError(e);
                    }
                    return subtotal;
                },
                delegate(List<TResult> subtotal)
                {
                    returnList.AddRange(subtotal);
                });

            return returnList;
        }
    }
}
