// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
var basepath = @"F:\Repos\BootstrapBlazor.PdfReader\src\BootstrapBlazor.PdfReader\wwwroot\compat\web\";
var fileinfo = Directory.GetFiles(basepath + "locale", "*.properties", SearchOption.AllDirectories);
foreach (var item in fileinfo)
{
    File.Move(item, item.Replace(".properties", ".txt"));
}
var locale_txt = File.ReadAllText(basepath + @"locale\locale.txt").Replace(".properties", ".txt");
File.WriteAllText(basepath + @"locale\locale.txt", locale_txt);

locale_txt = File.ReadAllText(basepath + @"viewer.html").Replace("locale.properties", "locale.txt");
File.WriteAllText(basepath + @"viewer.html", locale_txt);