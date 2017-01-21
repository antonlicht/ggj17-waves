using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

namespace Shared.Extensions
{
    public static class EnumerableExtensions
    {
        public static T RandomElementAt<T>(this IEnumerable<T> enumerable, Random random = null)
        {
            return enumerable.ElementAt((random ?? new Random()).Next(enumerable.Count()));
        }

        public static bool IsNullOrEmptyEnumerable<T>(this IEnumerable<T> enumerable)
        {
            return enumerable == null || !enumerable.Any();
        }

        public static bool IsNullOrEmptyEnumerable(this IEnumerable enumerable)
        {
            return enumerable == null || !enumerable.GetEnumerator().MoveNext();
        }

        /// <summary>
        /// Wraps this object instance into an IEnumerable&lt;T&gt;
        /// consisting of a single item.
        /// </summary>
        /// <typeparam name="T"> Type of the object. </typeparam>
        /// <param name="item"> The instance that will be wrapped. </param>
        /// <returns> An IEnumerable&lt;T&gt; consisting of a single item. </returns>
        public static IEnumerable<T> Yield<T>(this T item)
        {
            if (item == null)
            {
                yield break;
            }
            yield return item;
        }

        public static IEnumerable<IEnumerable<T>> SplitInChunks<T>(this IEnumerable<T> enumerable, int chunkSize)
        {
            var loops = (int) Math.Ceiling(GetOptimizedCountIfPossible(enumerable) / (float) chunkSize);
            return Enumerable.Range(0, loops).Select(i => enumerable.Skip(i * chunkSize).Take(chunkSize));
        }

        static int GetOptimizedCountIfPossible<T>(IEnumerable<T> enumerable)
        {
            if (enumerable is IList<T>)
            {
                return ((IList<T>) enumerable).Count;
            }

            if (enumerable is T[])
            {
                return ((T[]) enumerable).Length;
            }

            return enumerable.Count();
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random rnd = null)
        {
            var random = rnd ?? new Random();
            var buffer = source.ToList();
            for (int i = 0; i < buffer.Count; i++)
            {
                int j = random.Next(i, buffer.Count);
                yield return buffer[j];

                buffer[i] = buffer[j];
            }
        }

	    public static IEnumerable<IEnumerable<T>> Transpose<T>(this IEnumerable<IEnumerable<T>> source)
	    {
		    var enumerators = source.Select(e => e.GetEnumerator()).ToArray();
		    try
		    {
			    while (enumerators.All(e => e.MoveNext()))
			    {
				    yield return enumerators.Select(e => e.Current).ToArray();
			    }
		    }
		    finally
		    {
			    Array.ForEach(enumerators, e => e.Dispose());
		    }
	    }
    }

    public static class Compare
    {
        public static IEnumerable<T> DistinctBy<T, TIdentity>(this IEnumerable<T> source,
            Func<T, TIdentity> identitySelector)
        {
            return source.Distinct(Compare.By(identitySelector));
        }

        public static IEqualityComparer<TSource> By<TSource, TIdentity>(Func<TSource, TIdentity> identitySelector)
        {
            return new DelegateComparer<TSource, TIdentity>(identitySelector);
        }

        private class DelegateComparer<T, TIdentity> : IEqualityComparer<T>
        {
            private readonly Func<T, TIdentity> identitySelector;

            public DelegateComparer(Func<T, TIdentity> identitySelector)
            {
                this.identitySelector = identitySelector;
            }

            public bool Equals(T x, T y)
            {
                return Equals(identitySelector(x), identitySelector(y));
            }

            public int GetHashCode(T obj)
            {
                return identitySelector(obj).GetHashCode();
            }
        }
    }
}