// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using AME.Base;
using System;
using System.Linq;

namespace AME.Util;
#nullable enable
#pragma warning disable CA1416 // #warning 指令

public class PrinterError: GeneralResponse
{
    public string? Printer { get; set; }

    public static PrinterError GetMessage(string message, bool isH5 = true)
    {
        var error = new PrinterError();
        if (message.Contains("code="))
        {
            //ERR:code={ex.HResult},message={ex.Message},printer={ipadress1}
            var errs = message.TrimStart(' ').Replace("ERR:", "").Split(',');
            try
            {
                error.Message = errs.First(a => a.StartsWith("message=")).Split('=')[1];
            }
            catch { }
            try
            {
                error.Printer = errs.First(a => a.StartsWith("printer=")).Split('=')[1];
            }
            catch { }
            try
            {
                var _code = errs.First(a => a.StartsWith("code=")).Split('=')[1];
                error.Code = Convert.ToInt32(_code);
                error.Message = error.Code switch
                {
                    //IP地址无法连通
                    10001 => $"打印机网线未连接.\n({error.Message})",
                    -2147467259 => $"打印机网线未连接.\n({error.Message})",
                    _ => $"{error.Message}\n{error.Printer}\n{error.Code}",
                };
            }
            catch { }

            error.Message ??= message;

            if (isH5)
            {
                error.Message = error.Message.Replace("\n", "<br/>");
            }
        }
        return error;
    }
}
