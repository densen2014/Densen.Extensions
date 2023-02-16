using Microsoft.Extensions.FileSystemGlobbing;
using Microsoft.Extensions.FileSystemGlobbing.Abstractions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AME
{
    /// <summary>
    /// IEnumerable 扩展方法
    /// </summary>


    public static class IEnumerableExtensions
    {
        public static IEnumerable Append(this IEnumerable first, params object[] second)
        {
            return first.OfType<object>().Concat(second);
        }
        public static IEnumerable<T> Append<T>(this IEnumerable<T> first, params T[] second)
        {
            return first.Concat(second);
        }
        public static IEnumerable Prepend(this IEnumerable first, params object[] second)
        {
            return second.Concat(first.OfType<object>());
        }
        public static IEnumerable<T> Prepend<T>(this IEnumerable<T> first, params T[] second)
        {
            return second.Concat(first);
        }
    }

    /// <summary>
    /// Array 扩展方法
    /// </summary>


    public static class ArrayExtensions
    {
        public static object[] Append(this object[] first, params object[] second)=> second == null ? first : first.ToList().OfType<object>().Concat(second.ToList()).ToArray();

        public static T[] Append<T>(this T[] first, params T[] second) => second == null? first:first.ToList().Concat(second.ToList()).ToArray(); 
    }

}
