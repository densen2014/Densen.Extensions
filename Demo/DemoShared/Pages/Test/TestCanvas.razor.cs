﻿// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using AME;
using AmeBlazor.Components;
using BootstrapBlazor.AzureServices;
using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;
using System.Diagnostics.CodeAnalysis;
using UAParser;

namespace DemoShared.Pages;

public partial class TestCanvas
{
    private IJSObjectReference? module;
    private DotNetObjectReference<TestCanvas>? Instance { get; set; }
    private ElementReference Element { get; set; }

    /// <summary>
    /// 获得/设置 错误回调方法
    /// </summary>
    [Parameter]
    public Func<string, Task>? OnError { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        try
        {
            if (firstRender)
            {
                module = await JS!.InvokeAsync<IJSObjectReference>("import", "./_content/DemoShared/Pages/Test/TestCanvas.razor.js" + "?v=" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version);
                Instance = DotNetObjectReference.Create(this);
                await module!.InvokeVoidAsync("init", Instance, Element);
             }
        }
        catch (Exception e)
        {
            if (OnError != null) await OnError.Invoke(e.Message);
        }
    }

    /// <summary>
    /// 错误回调方法
    /// </summary>
    /// <param name="error"></param>
    /// <returns></returns>
    [JSInvokable]
    public async Task GetError(string error)
    {
        if (OnError != null) await OnError.Invoke(error);
    }

    //async ValueTask IAsyncDisposable.DisposeAsync()
    //{
    //    //await module!.InvokeVoidAsync("destroy");
    //    Instance?.Dispose();
    //    if (module is not null)
    //    {
    //        await module.DisposeAsync();
    //    }
    //}
}