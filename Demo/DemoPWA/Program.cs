using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using DemoShared;
using System.Globalization;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System;

var cultureInfo = new CultureInfo("zh-CN");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddDensenExtensions();
await builder.Build().RunAsync();
