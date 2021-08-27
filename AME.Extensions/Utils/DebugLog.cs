using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace AME
{
    public class DebugLog
    {

        static internal bool ConsoleWriteLine = false;
        public static void Log(string LogStr, string tag1 = "")
        {
            if (ConsoleWriteLine) Log($"时间 {DateTime.Now.ToString()} :  {LogStr}  , Tag {tag1} ");
        }

    }
}
