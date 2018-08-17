using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System;

namespace Ofa.Common.Linq
{
    public static class GenericEnumerableExtensions
    {
        public static IEnumerable<T[]> InSetsOf<T>(
               this IEnumerable<T> source, int setSize)
        {
            List<T> list = new List<T>(setSize);

            foreach (T item in source)
            {
                list.Add(item);
                if (list.Count == setSize)
                {
                    yield return list.ToArray();
                    list.Clear();
                }
            }

            if (list.Any())
                yield return list.ToArray();
        }

        public static void AddRange<T>(this ICollection<T> source, IEnumerable<T> items)
        {
            foreach (T item in items)
                source.Add(item);
        }

        public static void RemoveAll<T>(this ICollection<T> source, Predicate<T> match)
        {
            if (source == null)
                return;

            for (int i = source.Count - 1; i >= 0; i--)
            {
                T elementAt = source.ElementAt(i);
                if (match(elementAt))
                    source.Remove(elementAt);
            }
        }

        public static void RemoveAt<T>(this ICollection<T> source, int index)
        {
            if (source == null)
                return;

            source.Remove(source.ElementAt(index));
        }

        public static Collection<T> ToCollection<T>(this IEnumerable<T> source)
        {
            Collection<T> result = new Collection<T>();

            foreach (T item in source)
                result.Add(item);

            return result;
        }

        public static bool Same<T>(this IEnumerable<T> source, IEnumerable<T> items)
        {
            return source.Count() == items.Count() && !source.Except(items).Any();
        }
    }
}
