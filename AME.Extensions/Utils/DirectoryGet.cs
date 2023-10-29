// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace DirectoryGet;

public class Library : IEnumerable<string>
{
    public IEnumerator<string> GetEnumerator()
    {
        yield return $"{Environment.CurrentDirectory}";
        yield return $"{Directory.GetCurrentDirectory()}";
        yield return $"{GetType().Assembly.Location}";
        yield return $"{Process.GetCurrentProcess().MainModule.FileName}";
        yield return $"{AppDomain.CurrentDomain.BaseDirectory}";
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
