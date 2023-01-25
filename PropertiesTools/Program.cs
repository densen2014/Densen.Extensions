// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
var fileinfo = Directory.GetFiles(@"F:\Repos\BootstrapBlazor.PdfReader\src\BootstrapBlazor.PdfReader\wwwroot\web\locale", "*.properties", SearchOption.AllDirectories);
foreach (var item in fileinfo)
{
    File.Move(item, item.Replace(".properties", ".txt"));
}
