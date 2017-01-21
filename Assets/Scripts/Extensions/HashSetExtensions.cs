using System.Collections.Generic;
using System.Linq;

namespace Shared.Extensions
{
    public static class HashSetExtensions
    {
        /// <summary>
        /// Adds a range of items to an HashSet. Return the items that could not be added.
        /// </summary>
        /// <param name="hashSet"></param>
        /// <param name="toAdd"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> AddRange<T>(this HashSet<T> hashSet, IEnumerable<T> toAdd)
        {
            return toAdd.Where(i => !hashSet.Add(i));
        }
    }
}