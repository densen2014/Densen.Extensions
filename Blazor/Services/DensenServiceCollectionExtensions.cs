// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using AME.CommonUtils;
using AME.Services;
using Microsoft.AspNetCore.Http;
using System.Net.Http;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Densen BootstrapBlazor 服务扩展类
    /// </summary>
    public static class DensenServiceCollectionExtensions
    {

        /// <summary>
        /// 增加 BootstrapBlazor Table 服务扩展类,<para></para>
        /// 包含BootstrapBlazor/LazyHero/WebClientInfo/BrowserService/Clipboard
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configureOptions"></param>
        /// <returns></returns>
        public static IServiceCollection AddDensenExtensions(this IServiceCollection services)
        {
            services.AddBootstrapBlazor();
            //Table附加内存数据操作服务
            services.AddTransient(typeof(LazyHeroDataService<>));
            services.AddTransient<BrowserService>();
            services.AddScoped<ClipboardService>();
            services.AddHttpContextAccessor();
            services.AddScoped<HttpContextAccessor>();
            services.AddHttpClient();
            services.AddScoped<HttpClient>();
            services.AddScoped<WebClientInfoProvider>();
            return services;
        }
    }

 
}
