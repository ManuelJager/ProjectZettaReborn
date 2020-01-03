using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zetta.Extensions
{
    public static class EnumerableExtensions
    {
        public static int GetHashCodeAggregate<T>(this IEnumerable<T> source)
        {
            return source.GetHashCodeAggregate(17);
        }

        public static int GetHashCodeAggregate<T>(this IEnumerable<T> source, int hash)
        {
            unchecked
            {
                foreach (var item in source)
                {
                    hash = hash * 31 + item.GetHashCode();
                }
            }
            return hash;
        }
    }
}
