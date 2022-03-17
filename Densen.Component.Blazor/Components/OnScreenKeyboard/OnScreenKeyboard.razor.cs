// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;

namespace AmeBlazor.Components;

/// <summary>
/// WebApi 组件基类
/// </summary>
public partial class OnScreenKeyboard : IAsyncDisposable
{
    [Inject] IJSRuntime? JS { get; set; }
    private IJSObjectReference? module;
    private DotNetObjectReference<OnScreenKeyboard>? InstanceWebApi { get; set; }

    /// <summary>
    /// 获得/设置 组件class名称
    /// </summary>
    [Parameter]
    public string ClassName { get; set; } = "js-virtual-keyboard";

    /// <summary>
    /// 获得/设置 键盘语言布局
    /// </summary>
    [Parameter]
    public KeyboardKeysType? KeyboardKeys { get; set; } = KeyboardKeysType.english;

    /// <summary>
    /// 获得/设置 键盘类型
    /// </summary>
    [Parameter]
    public KeyboardType Keyboard { get; set; } = KeyboardType.all;

    /// <summary>
    /// 获得/设置 对齐
    /// </summary>
    [Parameter]
    public KeyboardPlacement Placement { get; set; } = KeyboardPlacement.bottom;

    /// <summary>
    /// 获得/设置 对齐
    /// </summary>
    [Parameter]
    public string Placeholder { get; set; } = "";

    /// <summary>
    /// 获得/设置 对齐
    /// </summary>
    [Parameter]
    public bool Specialcharacters { get; set; } = true;

    /// <summary>
    /// 获得/设置 配置
    /// </summary>
    [Parameter]
    public KeyboardOption? Option { get; set; } = new KeyboardOption();

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        try
        {
            if (firstRender)
            { 
                module = await JS!.InvokeAsync<IJSObjectReference>("import", "./_content/Densen.Component.Blazor/lib/kioskboard/kioskboards.js");
                InstanceWebApi = DotNetObjectReference.Create(this);
                await module.InvokeVoidAsync("addScript", "./_content/Densen.Component.Blazor/lib/kioskboard/kioskboard-aio-2.1.0.min.js");
                await Task.Delay(200);
                Option??= new KeyboardOption();
                if (KeyboardKeys != null) Option.KeyboardKeysType = KeyboardKeys!.Value;
                await module.InvokeVoidAsync("init", ClassName, Option); 
            }
        }
        catch (Exception e)
        {
            if (OnError != null) await OnError.Invoke(e.Message);
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
        try
        {
            await module!.InvokeVoidAsync("GetBattery", InstanceWebApi);
        }
        catch (Exception e)
        {
            if (OnError != null) await OnError.Invoke(e.Message);
        }
    }

    /// <summary>
    /// 获得/设置 错误回调方法
    /// </summary>
    [Parameter]
    public Func<string, Task>? OnError { get; set; }

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
        try
        {
            if (OnBatteryResult != null) await OnBatteryResult.Invoke(batteryStatus);
        }
        catch (Exception e)
        {
            if (OnError != null) await OnError.Invoke(e.Message);
        }
    }


    /// <summary>
    /// 获取网络信息
    /// </summary>
    public virtual async Task GetNetworkInfo()
    {
        try
        {
            await module!.InvokeVoidAsync("GetNetworkInfo", InstanceWebApi);
        }
        catch (Exception e)
        {
            if (OnError != null) await OnError.Invoke(e.Message);
        }
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
        try
        {
            if (OnNetworkInfoResult != null) await OnNetworkInfoResult.Invoke(networkInfoStatus);
        }
        catch (Exception e)
        {
            if (OnError != null) await OnError.Invoke(e.Message);
        }
    }


}
