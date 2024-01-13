// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************


using AME.Services;
using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using System.Diagnostics.CodeAnalysis;
using Toolbelt.Blazor.I18nText;

namespace AmeBlazor.Components;

/// <summary>
/// AME组件基类
/// </summary>
public abstract partial class AmeBlazorComponentBase : ComponentBase, IDisposable
{
    [Inject] protected NavigationManager NavigationManager { get; set; }
    [Inject] protected I18nText I18nText { get; set; }
    [Inject] protected IConfiguration config { get; set; }
    [Inject] protected BrowserService browserService { get; set; }
    [Inject] protected ToastService ToastService { get; set; }
    [Inject] protected SwalService SwalService { get; set; }
    [Inject] protected DialogService DialogService { get; set; }


    /// <summary>
    /// 获得/设置 用户自定义属性
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public IDictionary<string, object> AdditionalAttributes { get; set; }

    /// <summary>
    /// 获得/设置 IJSRuntime 实例
    /// </summary>
    [Inject]
    [NotNull]
    protected IJSRuntime JsRuntime { get; set; }

    /// <summary>
    /// Dispose 方法
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {

    }

    /// <summary>
    /// Dispose 方法
    /// </summary>
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
    protected override void OnInitialized()
    {
        base.OnInitialized();
    }

    //导航并添加 TabItem 方法
    protected Task OnNavigation()
    {
        //NavigationManager.NavigateTo("demo", "Demo页面");
        NavigationManager.NavigateTo("demo");
        return Task.CompletedTask;
    }
    protected void GotoNewTab(string selectItem)
    {
        JsRuntime.NavigateToNewTab(selectItem);
    }
    protected void Goto(string selectItem)
    {
        NavigationManager.NavigateTo(selectItem);
    }
    protected void Goto(string selectItem, bool forceLoad = false)
    {
        NavigationManager.NavigateTo(selectItem, forceLoad);
    }

    protected string TargetDir = "";

}
