using System;

namespace Shared.Extensions
{
	public static class ArrayExtensions
    {
        public static bool IsEqual<T>(this T[] a1, T[] a2)
        {
            if (a1.Length == a2.Length)
            {
                for (int i = 0; i < a1.Length; i++)
                {
                    if (!a1[i].Equals(a2[i]))
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }
    }

    public static class Array<T>
    {
        public static readonly T[] Empty = new T[0];
    }
}