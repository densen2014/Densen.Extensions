// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");


var basepaths = new List<string>()
{
    @"C:\Repos\BootstrapBlazor.PdfReader\src\BootstrapBlazor.PdfReader\wwwroot\",
    @"C:\Repos\BootstrapBlazor.PdfReader\src\BootstrapBlazor.PdfReader\wwwroot\2.6.347\",
    @"C:\Repos\BootstrapBlazor.PdfReader\src\BootstrapBlazor.PdfReader\wwwroot\compat\"
};
foreach (var basepath in basepaths)
{

    var extlist = new Dictionary<string, string>() {
    { ".properties", @"web\locale\locale.txt" },
    { ".pfb", @"build\pdf.worker.js" },
    { ".bcmap", @"build\pdf.js,build\pdf.js.map" },
};

    foreach (var ext in extlist)
    {
        var fileinfo = Directory.GetFiles(basepath, $"*{ext.Key}", SearchOption.AllDirectories);
        foreach (var item2 in fileinfo)
        {
            File.Move(item2, item2.Replace(ext.Key, ".txt"));
        }

        var items = ext.Value.Split(',');
        foreach (var item in items)
        {
            var locale_txt = File.ReadAllText(basepath + item).Replace(ext.Key, ".txt");
            File.WriteAllText(basepath + item, locale_txt);
        }

    }

    var viewer_txt = File.ReadAllText(basepath + @"web\viewer.html").Replace("locale.properties", "locale.txt");
    File.WriteAllText(basepath + @"web\viewer.html", viewer_txt);
}
