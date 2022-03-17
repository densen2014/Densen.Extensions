// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using AmeBlazor.Components;

namespace DemoShared.Pages;

/// <summary>
/// 百度地图 Baidu Map
/// </summary>
public sealed partial class BaiduMapPage
{

    private string message;

    private Task OnBatteryResult(BatteryStatus item)
    {
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
            new AttributeItem("OnError","错误信息回调",  "-","Func<string, Task>"),
    };
}
