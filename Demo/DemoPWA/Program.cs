using DemoShared;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Globalization;
using System.Reflection;
using static System.Net.WebRequestMethods;

var cultureInfo = new CultureInfo("zh-CN");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddDensenExtensions();
builder.Services.ConfigureJsonLocalizationOptions(op =>
{
    // 忽略文化信息丢失日志
    op.IgnoreLocalizerMissing = true;

    // 附加自己的 json 多语言文化资源文件 如 zh-TW.json
    op.AdditionalJsonAssemblies = new Assembly[]
    {
                //typeof(BootstrapBlazor.Shared.App).Assembly,
                typeof(BootstrapBlazor.Components.Chart).Assembly,
                //typeof(BootstrapBlazor.Components.SignaturePad).Assembly
    };
});
builder.Services.AddOcrExtensions(builder.Configuration["AzureCvKey"], builder.Configuration["AzureCvUrl"]);
builder.Services.AddAIFormExtensions(builder.Configuration["AzureAiFormKey"], builder.Configuration["AzureAiFormUrl"]);
await builder.Build().RunAsync();
