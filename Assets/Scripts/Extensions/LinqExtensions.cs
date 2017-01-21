using System.Collections.Generic;
using System;

namespace Shared.Extensions
{
    static class LinqExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> ie, Action<T> action)
        {
            foreach (var i in ie)
            {
                action(i);
            }
        }
    }
}
