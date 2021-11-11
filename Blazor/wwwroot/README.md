 Program.cs 加入

 builder.Services.AddDensenExtensions();
if (!builder.Services.Any(x => x.ServiceType == typeof(HttpClient)))
{
    builder.Services.AddSingleton<HttpClient>();
}
