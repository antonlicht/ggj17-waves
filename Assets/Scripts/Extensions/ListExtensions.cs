using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

namespace Shared.Extensions
{
    public static class ListExtensions
    {
        public static void Shuffle<T>(this IList<T> list, Random random = null)
        {
            int n = list.Count;
            if (random == null)
            {
                random = new Random();
            }
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static T RandomElement<T>(this IList<T> list, bool removeFromList = false, Random random = null)
        {
            if (list == null || list.Count == 0)
            {
                return default(T);
            }
            if (random == null)
            {
                random = new Random();
            }
            var rnd = random.Next(list.Count);
            var result = list[rnd];
            if (removeFromList)
            {
                list.RemoveAt(rnd);
            }
            return result;
        }

        public static T RemoveAndGetAt<T>(this IList<T> list, int index = 0)
        {
            var result = list[index];
            list.RemoveAt(index);
            return result;
        }

        public static T RemoveAndGetLast<T>(this IList<T> list)
        {
            return list.RemoveAndGetAt(list.Count - 1);
        }

        public static List<string> ConvertToList(this string str, char delimiter = ',', bool removeWhiteSpace = true,
            bool includeEmptyEntries = true)
        {
            var newString = str;
            if (removeWhiteSpace)
            {
                newString = str.Replace(" ", "");
            }

            if (string.IsNullOrEmpty(newString))
            {
                return new List<string>();
            }

            if (includeEmptyEntries)
            {
                return new List<string>(newString.Split(delimiter));
            }
            else
            {
                return
                    new List<string>(newString.Split(delimiter)).Where(stringValue => !string.IsNullOrEmpty(stringValue))
                        .ToList();
            }
        }

        public static List<int> ConvertToIntList(this string str, char delimiter = ',')
        {
            return str.ConvertToList(delimiter, true, false).ConvertAll(stringValue => Convert.ToInt32(stringValue));
        }

        public static T AddIfNotAlreadyPresent<T>(this List<T> itemList, T itemToAdd, bool allowNull = false)
        {
            if (itemToAdd != null || allowNull)
            {
                if (!itemList.Contains(itemToAdd))
                {
                    itemList.Add(itemToAdd);
                }
            }
            return itemToAdd;
        }

        public static List<T> AddRangeElementsIfNotAlreadyPresent<T>(this List<T> itemList, IEnumerable<T> elementsToAdd,
            bool allowNull = false)
        {
            if (!elementsToAdd.IsNullOrEmptyEnumerable())
            {
                foreach (var item in elementsToAdd)
                {
                    itemList.AddIfNotAlreadyPresent(item, allowNull);
                }
            }
            return itemList;
        }

        public static List<T> AddElements<T>(this List<T> itemList, params T[] itemsToAdd)
        {
            itemList.AddRange(itemsToAdd);
            return itemList;
        }

        public static void PopulateWithNew<T>(this IList<T> list, int count = 0) where T : new()
        {
            if (list == null)
            {
                throw new NullReferenceException();
            }

            list.Clear();
            for (var i = 0; i < count; i++)
            {
                list.Add(new T());
            }
        }

        public static void CopyWithCapacityChange<T>(this IList<T> list, int count = 0) where T : new()
        {
            if (list == null)
            {
                throw new NullReferenceException();
            }

            var tempCopyList = new List<T>(list);
            list.Clear();

            for (int i = 0; i < count; i++)
            {
                if (i < tempCopyList.Count)
                {
                    list.Add(tempCopyList[i]);
                }
                else
                {
                    list.Add(new T());
                }
            }
        }

        public static T ElementAtOrLastElement<T>(this List<T> list, int index)
        {
            if (list.IsNullOrEmptyCollection())
            {
                return default(T);
            }

            var id = Math.Min(list.Count - 1, index);
            return list[id];
        }

        public static T ElementAtOrDefault<T>(this List<T> list, int index)
        {
            return index > list.Count - 1 ? default(T) : list[index];
        }

        public static List<T> Clone<T>(this List<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T) item.Clone()).ToList();
        }
    }
}