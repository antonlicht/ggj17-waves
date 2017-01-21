using System;
using System.Collections.Generic;
using System.Collections;

namespace Shared.Extensions
{
    public static class EnumeratorExtensions
    {
        public static IEnumerator ContinueWith(this IEnumerator head, Func<IEnumerator> next)
        {
            while (head.MoveNext())
            {
                yield return head.Current;
            }

            var tail = next();
            while (tail.MoveNext())
            {
                yield return tail.Current;
            }
        }

        public static IEnumerator ContinueWith<TOut>(this IEnumerator head, Func<TOut> next)
        {
            while (head.MoveNext())
            {
                yield return head.Current;
            }

            yield return next();
        }

        /// <summary>
        /// Wraps this object instance into an IEnumerator
        /// consisting of a single item.
        /// </summary>
        /// <typeparam name="T"> Type of the object. </typeparam>
        /// <param name="item"> The instance that will be wrapped. </param>
        /// <returns> An IEnumerable&lt;T&gt; consisting of a single item. </returns>
        public static IEnumerator<T> YieldAsEnumerator<T>(this T item)
        {
            yield return item;
        }
    }
}