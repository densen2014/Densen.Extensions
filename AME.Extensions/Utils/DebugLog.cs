// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using System;

namespace AME
{
    public class DebugLog
    {

        internal static bool ConsoleWriteLine = false;
        public static void Log(string LogStr, string tag1 = "")
        {
            if (ConsoleWriteLine) Log($"时间 {DateTime.Now} :  {LogStr}  , Tag {tag1} ");
        }

    }
}
