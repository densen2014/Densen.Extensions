// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using BootstrapBlazor.Components;
using System.ComponentModel;

namespace DemoShared.Pages;

public sealed partial class OfdReaders
{

    [DisplayName("文件相对路径或者URL")]
    private string FileName { get; set; } = "/_content/DemoShared/samples/sample2.ofd";

    private string FileNameStream { get; set; } = "https://blazor.app1.es/samples/sample2.ofd";
     
     
}
