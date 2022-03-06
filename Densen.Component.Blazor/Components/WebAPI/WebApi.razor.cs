// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Diagnostics.CodeAnalysis;

namespace AmeBlazor.Components;

/// <summary>
/// WebApi 组件基类
/// </summary>
public partial class WebApi : IAsyncDisposable
{
    [Inject] IJSRuntime? JS { get; set; }
    private IJSObjectReference? module;
    private DotNetObjectReference<WebApi>? InstanceWebApi { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            module = await JS!.InvokeAsync<IJSObjectReference>("import", "./_content/Densen.Component.Blazor/lib/webapi/webapi.js");
            InstanceWebApi = DotNetObjectReference.Create(this);
            if (OnBatteryResult != null) await GetBattery();
            if (OnNetworkInfoResult != null) await GetNetworkInfo();
        }
    }

    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        if (module is not null)
        {
            await module.DisposeAsync();
        }
    }

    /// <summary>
    /// 获取电量
    /// </summary>
    public virtual async Task GetBattery()
    {
        await module!.InvokeVoidAsync("GetBattery", InstanceWebApi);
    }

    /// <summary>
    /// 获得/设置 电池信息回调方法
    /// </summary>
    [Parameter]
    public Func<BatteryStatus, Task>? OnBatteryResult { get; set; }

    /// <summary>
    /// 获取电池信息完成回调方法
    /// </summary>
    /// <param name="batteryStatus"></param>
    /// <returns></returns>
    [JSInvokable]
    public async Task GetBatteryResult(BatteryStatus batteryStatus)
    {
        if (OnBatteryResult != null) await OnBatteryResult.Invoke(batteryStatus);
    }


    /// <summary>
    /// 获取网络信息
    /// </summary>
    public virtual async Task GetNetworkInfo()
    {
        await module!.InvokeVoidAsync("GetNetworkInfo", InstanceWebApi);
    }

    /// <summary>
    /// 获得/设置 网络信息回调方法
    /// </summary>
    [Parameter]
    public Func<NetworkInfoStatus, Task>? OnNetworkInfoResult { get; set; }

    /// <summary>
    /// 获取网络信息完成回调方法
    /// </summary>
    /// <param name="NetworkInfoStatus"></param>
    /// <returns></returns>
    [JSInvokable]
    public async Task GetNetworkInfoResult(NetworkInfoStatus networkInfoStatus)
    {
        if (OnNetworkInfoResult != null) await OnNetworkInfoResult.Invoke(networkInfoStatus);
    }


}
