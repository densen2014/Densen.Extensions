// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using BootstrapBlazor.AzureServices;
using BootstrapBlazor.Ocr.Services;
using BootstrapBlazor.OpenAI.Services;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using System.Globalization;
using System.Reflection;
using System.Text.Encodings.Web;
#if NET7_0_OR_GREATER
using AzureOpenAIClient.Http;
#endif

//默认路径, Linux会在/root/uploads, win在 C:\Users\[username]\Documents\uploads
string UploadPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "uploads");
if (!Directory.Exists(UploadPath)) Directory.CreateDirectory(UploadPath);

var builder = WebApplication.CreateBuilder(args); //    webBuilder.UseContentRoot("D:\\T9WMS\\publish");


builder.Services.AddCors();
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor(a =>
{
    a.DetailedErrors = true;
    a.JSInteropDefaultCallTimeout = TimeSpan.FromMinutes(2);
    a.MaxBufferedUnacknowledgedRenderBatches = 20;
    a.DisconnectedCircuitRetentionPeriod = TimeSpan.FromMinutes(10);
}).AddHubOptions(o =>
{
    o.EnableDetailedErrors = true;
    //单个传入集线器消息的最大大小。默认 32 KB	
    o.MaximumReceiveMessageSize = null;
    //可为客户端上载流缓冲的最大项数。 如果达到此限制，则会阻止处理调用，直到服务器处理流项。
    o.StreamBufferCapacity = 20;
});
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

#if NET7_0_OR_GREATER
builder.Services.AddOpenAIClient(x => builder.Configuration.Bind(nameof(OpenAIClientConfiguration), x));
builder.Services.AddScoped<AzureOpenAIService>();
#endif

builder.Services.AddTransient<OcrService>();
builder.Services.AddTransient<AiFormService>();
builder.Services.AddTransient<TranslateService>();
builder.Services.AddSingleton<OpenAiClientService>();
builder.Services.AddTransient<StsService>();
builder.Services.AddStorages();

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
builder.Services.AddControllers();


var cultureInfo = new CultureInfo("zh-CN");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
//设置文件上传的大小限制 , 默认值128MB 
builder.Services.Configure<FormOptions>(o => o.MultipartBodyLengthLimit = long.MaxValue);

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

if (string.IsNullOrWhiteSpace(app.Environment.WebRootPath))
{
    app.Environment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
}
var dir = Path.Combine(app.Environment.WebRootPath, "Upload");
if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

var provider = new FileExtensionContentTypeProvider();
provider.Mappings[".properties"] = "application/octet-stream";
provider.Mappings[".apk"] = "application/octet-stream";

//opencv 模型
provider.Mappings[".caffemodel"] = "application/octet-stream";
provider.Mappings[".prototxt"] = "application/octet-stream";

provider.Mappings.Remove(".ts");
provider.Mappings.Add(".key", "text/plain");
provider.Mappings.Add(".m3u8", "application/x-mpegURL");
provider.Mappings.Add(".ts", "video/MP2T");

app.UseStaticFiles(new StaticFileOptions
{
    ContentTypeProvider = provider
});

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(dir),
    RequestPath = new PathString("/Upload"),
    ContentTypeProvider = provider
});

var opt = new DirectoryBrowserOptions
{
    FileProvider = new PhysicalFileProvider(dir),
    Formatter = new AME.HtmlDirectoryFormatterChsSorted(HtmlEncoder.Default),
    RequestPath = new PathString("/Upload")
};

app.UseDirectoryBrowser(opt);

if (Directory.Exists("C:\\Repos\\A_win7SP\\bin"))
{
    dir = "C:\\Repos\\A_win7SP\\bin";
}
else
{
    dir = Path.Combine(app.Environment.WebRootPath, "stream");
    if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
}
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(dir),
    RequestPath = new PathString("/stream"),
    ContentTypeProvider = provider,
    OnPrepareResponse = ctx =>
    {
        const int durationInSeconds = 60 * 60 * 24 * 7; //自动缓存这些文件24*7小时
        ctx.Context.Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.CacheControl] =
           "public,max-age=" + durationInSeconds;
    }
});

app.UseDirectoryBrowser(new DirectoryBrowserOptions
{
    FileProvider = new PhysicalFileProvider(dir),
    Formatter = new AME.HtmlDirectoryFormatterChsSorted(HtmlEncoder.Default),
    RequestPath = new PathString("/stream")
});

dir = Path.Combine(app.Environment.WebRootPath, "BlazorHybrid");
if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(dir),
    RequestPath = new PathString("/BlazorHybrid"),
    ContentTypeProvider = provider
});

app.UseDirectoryBrowser(new DirectoryBrowserOptions
{
    FileProvider = new PhysicalFileProvider(dir),
    Formatter = new AME.HtmlDirectoryFormatterChsSorted(HtmlEncoder.Default),
    RequestPath = new PathString("/BlazorHybrid")
});

dir = Path.Combine(app.Environment.WebRootPath, "_content", "BootstrapBlazor.ImageHelper", "models");
if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(dir), 
    ContentTypeProvider = provider,
    OnPrepareResponse = ctx =>
    {
        const int durationInSeconds = 60 * 60 * 24 * 7; //自动缓存这些文件24*7小时
        ctx.Context.Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.CacheControl] =
           "public,max-age=" + durationInSeconds;
    }
});

app.UseRouting();

app.UseCors(options =>
{
    options.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader();
});

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.MapDefaultControllerRoute();
app.Run();
