// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using System;
using System.Linq;

namespace AME.Base;
#nullable enable
#pragma warning disable CA1416 // #warning 指令


public class GeneralResponse
{
    public int Code { get; set; }
    public string? Message { get; set; }
}
