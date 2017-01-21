using System.Collections.Generic;

namespace Shared.Extensions
{
    public static class DictionaryExtensions
    {
        public static IDictionary<TKey, TValue> MergeLeft<TKey, TValue>(this IDictionary<TKey, TValue> first,
            IDictionary<TKey, TValue> second)
        {
            if (second == null) return first;
            foreach (var item in second)
            {
                if (!first.ContainsKey(item.Key))
                {
                    first.Add(item.Key, item.Value);
                }
            }
            return first;
        }

        public static IDictionary<TKey, TValue> MergeRight<TKey, TValue>(this IDictionary<TKey, TValue> first,
            IDictionary<TKey, TValue> second)
        {
            if (second == null) return first;
            foreach (var item in second)
            {
                first[item.Key] = item.Value;
            }
            return first;
        }

        public static void AddElementToList<TKey, TList, TItem>(this IDictionary<TKey, TList> dict, TKey key, TItem item)
            where TList : List<TItem>, new()
        {
            if (!dict.ContainsKey(key))
            {
                dict[key] = new TList();
            }
            dict[key].Add(item);
        }

        public static void AddOrIncrement<TKey> (this IDictionary<TKey,int> dict, TKey key, int value)
        {
            if (!dict.ContainsKey(key)) {
                dict[key] = 0;
            }
            dict[key] += value;
        }

        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
        {
            return dict != null && dict.ContainsKey(key) ? dict[key] : default(TValue);
        }

        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key,
            TValue defaultValue)
        {
            return dict != null && dict.ContainsKey(key) ? dict[key] : defaultValue;
        }

        public static TValue GetValueOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key,
            System.Func<TValue> getValue)
        {
            if (!dict.ContainsKey(key))
            {
                dict[key] = getValue();
            }
            return dict[key];
        }

        public static TValue GetValueOrAddNew<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
            where TValue : new()
        {
            if (!dict.ContainsKey(key))
            {
                dict[key] = new TValue();
            }
            return dict[key];
        }


        public static TValue AddAndReturnValue<TKey, TValue>(this Dictionary<TKey, TValue> dict,
            TKey key, TValue value)
        {
            if (dict != null)
            {
                dict[key] = value;
            }
            return value;
        }

        public static Dictionary<TKey, TValue> AddAndReturnDict<TKey, TValue>(this Dictionary<TKey, TValue> dict,
            TKey key, TValue value)
        {
            if (dict != null)
            {
                dict[key] = value;
            }
            return dict;
        }

        public static void RemoveAll<TKey, TValue>(this IDictionary<TKey, TValue> dict, IEnumerable<TKey> keys)
        {
            foreach (var key in keys)
            {
                dict.Remove(key);
            }
        }
    }
}