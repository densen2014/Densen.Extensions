using static BootstrapBlazor.Components.UploadToBase64;

namespace DemoShared.Pages;

/// <summary>
/// UploadToBase64 上传图片
/// </summary>
public partial class UploadToBase64s
{
     
    private string? message;

    private Task OnChanged(List<ImageFile> items)
    {
        message = string.Empty;
        items.ForEach(item => message+=$"名称 {item.Name}{Environment.NewLine}类型 {item.ContentType}{Environment.NewLine}大小 {item.Size}{Environment.NewLine}DataUrl预览 {item.DataUrl?.Substring(0,50)}{Environment.NewLine}{Environment.NewLine}");
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnError(string message)
    {
        this.message = message;
        StateHasChanged();
        return Task.CompletedTask;
    }

 
    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new AttributeItem("MaxFileSize","上传文件大小限制 默认 15M",  "1024 * 1024 * 15","long"),
        new AttributeItem("Accept","接受上传的文件类型, 例如 image/* 表示只接受图片上传",  "image/*","string"),
        new AttributeItem("Label","按钮名称",  "上传图片","string"),
        new AttributeItem("OnChanged","上传回调方法,返回 ImageFile 集合",  "-","Func<List<string>, Task>"),
        new AttributeItem("OnError","错误信息回调",  "-","Func<string, Task>"),
    };
}
