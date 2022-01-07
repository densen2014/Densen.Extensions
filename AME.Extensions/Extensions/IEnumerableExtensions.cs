using Microsoft.Extensions.FileSystemGlobbing;
using Microsoft.Extensions.FileSystemGlobbing.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;

namespace AME
{
    /// <summary>
    /// IEnumerable 扩展方法
    /// </summary>


    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> Add<T>(this IEnumerable<T> e, T value)
        {
            foreach (var cur in e)
            {
                yield return cur;
            }
            yield return value;
        }

    }

}
