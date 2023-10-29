// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

//using System.Data.SQLite;
using Microsoft.AspNetCore.Components.Web;
using System.Globalization;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// 服务扩展类
/// </summary>
public static class SharedServiceCollectionExtensions
{

    /// <summary>
    /// 服务扩展类,<para></para>
    /// 包含各平台差异实现
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddSharedExtensions(this IServiceCollection services)
    {
        var cultureInfo = new CultureInfo("zh-CN");
        CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
        CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

        services.AddDensenExtensions();

        //据说已经修复
        //2022/8/11 测试fsql是不是这个问题
        services.AddSingleton<IErrorBoundaryLogger, MyErrorBoundaryLogger>();
        return services;
    }

}
