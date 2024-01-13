// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using Microsoft.JSInterop;

namespace AmeBlazor.Components;



/// <summary>
/// 
/// </summary>
public static class NavigationManagerExtensions
{

    /// <summary>
    /// 导航或打开新页面方法
    /// </summary>
    /// <param name="JsRuntime"></param>
    /// <param name="url"></param>
    public static async void NavigateToNewTab(this IJSRuntime JsRuntime, string url)
    {
        if (JsRuntime != null)
        {
            await JsRuntime.InvokeAsync<object>("open", new object[2] { url, "_blank" });
        }
    }

    ///// <summary>
    ///// 导航或打开新页面方法
    ///// </summary>
    ///// <param name="Navigation"></param>
    ///// <param name="url"></param>
    //public static async void NavigateToNewTab(this NavigationManager Navigation, string url)
    //{
    //    var JsRuntime = BootstrapBlazor.Components.ServiceProviderHelper.ServiceProvider.GetRequiredService<JSRuntime>();
    //    if (JsRuntime != null)
    //    {
    //        await JsRuntime.InvokeAsync<object>("open", new object[2] { url, "_blank" });
    //    }
    //}
}
