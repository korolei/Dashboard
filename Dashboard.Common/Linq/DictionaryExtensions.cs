using System.Collections.Generic;

namespace Ofa.Common.Linq
{
    public static class DictionaryExtensions
    {
        public static bool ContainsAllKeys<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey[] keys)
        {
            IDictionary<TKey, TValue> haystack = dictionary ?? new Dictionary<TKey, TValue>();
            TKey[] needles = keys ?? new TKey[0];

            foreach (TKey key in needles)
            {
                if (!haystack.ContainsKey(key))
                    return false;
            }

            return true;
        }
    }
}
