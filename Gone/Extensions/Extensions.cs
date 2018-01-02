using System;
using System.Collections.Generic;

namespace Gone.Extensions
{
    public static class Extensions
    {
        public static IEnumerable<T> Nplicate<T>(this IEnumerable<T> source, int factor)
        {
            foreach (var element in source)
            {
                for (int i = 0; i <= factor; i++)
                {
                    yield return element;
                }
            }
        }
    }
}
