using System;
using System.Collections.Generic;
using System.Linq;

namespace DC.Infrastructure.Extensions
{
    public static class IEnumerableExtensions
    {
        public static bool HasItems<T>(this IEnumerable<T> lst)
        {
            return lst != null && lst.Count() > 0;
        }
    }
}