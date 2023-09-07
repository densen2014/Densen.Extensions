// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************


using BootstrapBlazor.Components;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace DemoShared.Pages;

public partial class WebSerialPage
{

    private string? message;
    private string? statusmessage;
    private string? errmessage;
    private WebSerialOptions options = new WebSerialOptions() { BaudRate = 115200 };

    [NotNull]
    private IEnumerable<SelectedItem> BaudRateList { get; set; } = ListToSelectedItem();

    [DisplayName("波特率")]
    private int SelectedBaudRate { get; set; } = 115200;
    private bool Flag { get; set; } 
    private bool IsConnected { get; set; } 
    private WebSerial? WebSerial { get; set; } 

 
    private Task OnReceive(string? message)
    {
        this.message = $"{DateTime.Now:hh:mm:ss} 收到数据: {message}{Environment.NewLine}"+ this.message;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnConnect(bool flag)
    {
        this.IsConnected = flag;
        if (flag) {
            message = null;
            statusmessage = null;
            errmessage = null;
        }
        else
        {
            //Flag=false;
        }
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnLog(string message)
    {
        this.statusmessage = message;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnError(string message)
    {
        this.errmessage = message;
        StateHasChanged();
        return Task.CompletedTask;
    }

    public static IEnumerable<SelectedItem> ListToSelectedItem()
    {
        foreach (var item in WebSerialOptions.BaudRateList)
        {
            yield return new SelectedItem(item.ToString(), item.ToString());
        }
    }
    private Task OnApply()
    {
        options.BaudRate = SelectedBaudRate;
        Flag = !Flag;
        //StateHasChanged();
        return Task.CompletedTask;
    }


    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    protected IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new AttributeItem() {
            Name = "OnReceive",
            Description = "收到数据回调方法",
            Type = "Func<string, Task>?",
            ValueList = "-",
            DefaultValue = "-"
        },
        new AttributeItem() {
            Name = "OnLog",
            Description = "Log回调方法",
            Type = "Func<string, Task>?",
            ValueList = "-",
            DefaultValue = "-"
        },
        new AttributeItem() {
            Name = "OnError",
            Description = "错误回调方法",
            Type = "Func<string, Task>?",
            ValueList = "-",
            DefaultValue = "-"
        },
        new AttributeItem() {
            Name = "Element",
            Description = "UI界面元素的引用对象,为空则使用整个页面",
            Type = "ElementReference",
            ValueList = "-",
            DefaultValue = "-"
        },
        new AttributeItem() {
            Name = "ConnectBtnTitle",
            Description = "获得/设置 连接按钮文本",
            Type = "string",
            ValueList = "",
            DefaultValue = "连接"
        },
        new AttributeItem() {
            Name = "DisconnectBtnTitle",
            Description = "获得/设置 断开连接按钮文本",
            Type = "string",
            ValueList = "",
            DefaultValue = "断开连接"
        },
        new AttributeItem() {
            Name = "WriteBtnTitle",
            Description = "获得/设置 写入按钮文本",
            Type = "string",
            ValueList = "",
            DefaultValue = "写入"
        },
        new AttributeItem() {
            Name = "ShowUI",
            Description = "获得/设置 显示内置UI",
            Type = "bool",
            ValueList = "True|False",
            DefaultValue = "False"
        },
        new AttributeItem() {
            Name = "Debug",
            Description = "获得/设置 显示log",
            Type = "bool",
            ValueList = "True|False",
            DefaultValue = "False"
        },
        };


    /// <summary>s
    /// 获得WebSerialOptions属性方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetWebSerialOptionsAttributes() => new AttributeItem[]
    {
            new AttributeItem(nameof(WebSerialOptions.BaudRate),"波特率",  "9600","int"),
            new AttributeItem(nameof(WebSerialOptions.DataBits),"数据位",  "8","int","7|8"),
            new AttributeItem(nameof(WebSerialOptions.ParityType),"流控制",  "none",nameof(WebSerialFlowControlType),"none|even|odd"),
            new AttributeItem(nameof(WebSerialOptions.StopBits),"停止位",  "1","int","1|2"),
            new AttributeItem(nameof(WebSerialOptions.BufferSize),"读写缓冲区",  "255","int"),
            new AttributeItem(nameof(WebSerialOptions.FlowControlType),"校验",  "none",nameof(WebSerialParityType),"none|hardware"),
            new AttributeItem(nameof(WebSerialOptions.InputWithHex),"HEX发送",  "false","bool"),
            new AttributeItem(nameof(WebSerialOptions.OutputInHex),"HEX接收",  "false","bool"),
            new AttributeItem(nameof(WebSerialOptions.AutoConnect),"自动连接设备",  "true","bool"),
            new AttributeItem(nameof(WebSerialOptions.AutoFrameBreakType),"自动断帧方式",  "Character","AutoFrameBreakType"),
            new AttributeItem(nameof(WebSerialOptions.FrameBreakChar),"断帧字符",  "\\n","string"),
            new AttributeItem(nameof(WebSerialOptions.Break),"Break",  "false","bool"),
            new AttributeItem(nameof(WebSerialOptions.DTR),"DTR",  "false","bool"),
            new AttributeItem(nameof(WebSerialOptions.RTS),"RTS",  "false","bool"),
             new AttributeItem() {
                Name = nameof(WebSerialOptions.ConnectBtnTitle),
                Description = "获得/设置 连接按钮文本",
                Type = "string",
                ValueList = "",
                DefaultValue = "连接"
            },
            new AttributeItem() {
                Name = nameof(WebSerialOptions.DisconnectBtnTitle),
                Description = "获得/设置 断开连接按钮文本",
                Type = "string",
                ValueList = "",
                DefaultValue = "断开连接"
            },
            new AttributeItem() {
                Name = nameof(WebSerialOptions.WriteBtnTitle),
                Description = "获得/设置 写入按钮文本",
                Type = "string",
                ValueList = "",
                DefaultValue = "写入"
            },
       };
}
