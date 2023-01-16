// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DemoBlazorWpfApp
{
    public static class Startup
    {
        public class ConfigFake { }
        public static IServiceProvider? Services { get; private set; }
        public static IConfiguration? Config;

        public static void Init()
        {
            var host = Host.CreateDefaultBuilder()
                             .ConfigureAppConfiguration((hostingContext, config) =>
                             {
                                 config.AddUserSecrets<ConfigFake>().Build();
                                 //config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                                 Config = config.Build();
                             })
                           .ConfigureServices(WireupServices)
                           .Build();
            Services = host.Services;
        }

        private static void WireupServices(HostBuilderContext context, IServiceCollection services)
        {
            services.AddWpfBlazorWebView();
            services.AddSharedExtensions();
            services.AddOcrExtensions();
            services.AddAIFormExtensions();
#if DEBUG
            services.AddBlazorWebViewDeveloperTools();
#endif
        }
    }
}
