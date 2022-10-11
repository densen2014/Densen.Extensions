// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using System.Globalization;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args); //    webBuilder.UseContentRoot("D:\\T9WMS\\publish");


builder.Services.AddCors();
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor(a =>
{
    //异步调用JavaScript函数的最大等待时间
    a.JSInteropDefaultCallTimeout = TimeSpan.FromMinutes(2);
}).AddHubOptions(o =>
{
    //单个传入集线器消息的最大大小。默认 32 KB	
    o.MaximumReceiveMessageSize = null;
    //可为客户端上载流缓冲的最大项数。 如果达到此限制，则会阻止处理调用，直到服务器处理流项。
    o.StreamBufferCapacity = 20;
}); 
builder.Services.AddDensenExtensions(); 
builder.Services.AddOcrExtensions();
if (!builder.Services.Any(x => x.ServiceType == typeof(HttpClient)))
{
    builder.Services.AddSingleton<HttpClient>();
}
builder.Services.ConfigureJsonLocalizationOptions(op =>
{
    // 附加自己的 json 多语言文化资源文件 如 zh-TW.json
    op.AdditionalJsonAssemblies = new Assembly[]
    {
                typeof(DemoShared.App).Assembly, 
                typeof(BootstrapBlazor.Components.Chart).Assembly, 
    };
});
var cultureInfo = new CultureInfo("zh-CN");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseCors(options =>
{
    options.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader();
});

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.Run();
