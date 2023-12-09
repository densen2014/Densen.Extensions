// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Diagnostics.CodeAnalysis;

namespace DemoShared.Pages;

public partial class JsBridge : IAsyncDisposable
{
    private string? message;
    private bool BridgeEnabled;

    [Inject, NotNull]
    private IJSRuntime? JSRuntime { get; set; }

    [Inject, NotNull]
    private ToastService? ToastService { get; set; }

    private IJSObjectReference? Module { get; set; }

    private async Task GetMacAdress()
    {
        message = await Module!.InvokeAsync<string>("GetMacAdress");
        await ToastService.Information("JS方式 macAdress", message);

        message = await JSRuntime.InvokeAsync<string>("eval", $"localStorage.getItem('macAdress');");
        await ToastService.Information("eval macAdress", message);

        message = await JSRuntime.InvokeAsync<string>("eval", "bridge.Func('测试')");
        await ToastService.Information("eval bridge.Func", message);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        try
        {
            if (firstRender)
            {
                BridgeEnabled = await JSRuntime.InvokeAsync<bool>("eval", $"typeof bridge != 'undefined'");

                message = await JSRuntime.InvokeAsync<string>("eval", $"localStorage.getItem('macAdress');");

                Module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/DemoShared/Pages/JsBridge.razor.js" + "?v=" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version);
            }
        }
        catch (Exception e)
        {
            message = e.Message;
        }
        StateHasChanged();
    }


    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        if (Module is not null)
        {
            await Module.DisposeAsync();
        }
    }

}
