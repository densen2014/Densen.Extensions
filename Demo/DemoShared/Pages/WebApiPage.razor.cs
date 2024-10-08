﻿// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using BootstrapBlazor.Components;

namespace DemoShared.Pages;

/// <summary>
/// WebApi 浏览器api
/// </summary>
public sealed partial class WebApiPage
{

    private List<BatteryStatus> batteryStatus { get; set; } = new List<BatteryStatus>() { new BatteryStatus() };
    private List<NetworkInfoStatus> networkInfoStatus { get; set; } = new List<NetworkInfoStatus>() { new NetworkInfoStatus() };
    private List<UAInfo> UAInfos { get; set; } = new List<UAInfo>() { new UAInfo() };
    private string? message;

    private Task OnBatteryResult(BatteryStatus item)
    {
        batteryStatus[0] = item;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnNetworkInfoResult(NetworkInfoStatus item)
    {
        networkInfoStatus[0] = item;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnUserAgentResult(UAInfo item)
    {
        UAInfos[0] = item;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnError(string message)
    {
        this.message = message;
        StateHasChanged();
        return Task.CompletedTask;
    }



    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
            new AttributeItem("GetBattery","获取电量",  "-","void"),
            new AttributeItem("GetNetworkInfo","获取网络信息",  "-","void"),
            new AttributeItem("OnBatteryResult","获取电量回调",  "-","Func<BatteryStatus, Task>"),
            new AttributeItem("OnNetworkInfoResult","获取网络信息回调",  "-","Func<NetworkInfoStatus, Task>"),
            new AttributeItem("OnUserAgentResult","获取浏览器信息回调",  "-","Func<UAInfo, Task>"),
            new AttributeItem("OnError","错误信息回调",  "-","Func<string, Task>"),
    };
}
