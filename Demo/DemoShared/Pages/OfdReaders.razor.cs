// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel;

namespace DemoShared.Pages;

public sealed partial class OfdReaders
{

    [DisplayName("文件相对路径或者URL")]
    private string FileName { get; set; } = "/_content/DemoShared/samples/sample2.ofd";

    private Task Apply()
    {
        return Task.CompletedTask;
    }

    private Task Apply1()
    {
        FileName= "/_content/DemoShared/samples/sample2.ofd";
        return Task.CompletedTask;
    }

    private Task Apply2()
    {
        FileName = "http://localhost:5000/_content/DemoShared/samples/sample.ofd";
        return Task.CompletedTask;
    }

    private Task Apply3()
    {
        FileName = "https://blazor.app1.es/_content/DemoShared/samples/sample.ofd";
        return Task.CompletedTask;
    }

    private string UploadPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "uploads");

    private string? tempfilename = null;
    private bool displayProgress;
    private string? uploadstatus;
    long maxFileSize = 1024 * 1024 * 15;

    private async Task OnChange(InputFileChangeEventArgs e)
    {
        int i = 0;
        var selectedFiles = e.GetMultipleFiles(100);
        foreach (var item in selectedFiles)
        {
            i++;
            //文件名自己处理一下
            var filename = $"{DateTime.Now:MMdd_hhmmss}_{item.Name}";
            await OnSubmit(item, filename);
            uploadstatus += Environment.NewLine + $"[{i}]: " + item.Name; 
            FileName = $"/uploads/{filename}";

        }
    }

    private async Task OnSubmit(IBrowserFile efile, string? filename = null)
    {
        if (efile == null) return;
        var tempfilename = Path.Combine(UploadPath, filename ?? efile.Name);
        await using FileStream fs = new(tempfilename, FileMode.Create);
        using var stream = efile.OpenReadStream(maxFileSize);
        displayProgress = true;
        await stream.CopyToAsync(fs);
        displayProgress = false;
        StateHasChanged();
    }
}
