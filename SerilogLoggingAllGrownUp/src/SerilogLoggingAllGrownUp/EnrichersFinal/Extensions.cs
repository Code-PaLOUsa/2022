using System;
using System.Collections.Generic;

namespace Enrichers
{
    public static class Extensions
    {
        public static void Foreach<T>(this IEnumerable<T> elements, Action<T> action)
        {
            foreach(var element in elements)
            {
                action(element);
            }
        }
    }
}
