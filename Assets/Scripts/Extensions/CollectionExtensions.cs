using System.Collections;
using System.Collections.Generic;

namespace Shared.Extensions
{
    public static class CollectionExtensions
    {
        public static void AddIfNotNull<T> (this ICollection<T> collection, T item) where T: class
        {
            if (item != null) {
                collection.Add(item);
            }
        }

        public static bool IsNullOrEmptyCollection(this ICollection collection)
        {
            return collection.Count == 0;
        }
    }
}