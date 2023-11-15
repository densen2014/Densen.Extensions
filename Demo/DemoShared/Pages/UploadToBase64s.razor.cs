// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

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
        items.ForEach(item => message += $"名称 {item.Name}{Environment.NewLine}类型 {item.ContentType}{Environment.NewLine}大小 {(item.Size/1024):n2}kb{Environment.NewLine}DataUrl预览 {item.DataUrl?.Substring(0, 50)}{Environment.NewLine}{Environment.NewLine}");
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
        new AttributeItem("Accept","接受上传的文件类型, 例如 image/* 表示只接受图片上传",  "image/*","string"),
        new AttributeItem("Capture","在移动设备上可使用相机拍照",  "false","bool"),
        new AttributeItem("CaptureUser","在移动设备上使用前置相机拍照",  "false","bool"),
        new AttributeItem("OnChanged","上传回调方法,返回 ImageFile 集合",  "-","Func<List<ImageFile>, Task>"),
        new AttributeItem("OnError","错误信息回调",  "-","Func<string, Task>"),
        new AttributeItem("Multiple","文件多选,反之为单选",  "true","bool"),
        new AttributeItem("ImageOnly","只接受图片上传",  "true","bool"),
        new AttributeItem("MaxFileSize","上传文件大小限制 默认 15M",  "1024 * 1024 * 15","long"),
        new AttributeItem("UploadButtonText","按钮名称",  "上传图片","string"),
        new AttributeItem("Style","用户自定义样式",  "-","string"),
    };

    private IEnumerable<AttributeItem> GetAttributes2() => new AttributeItem[]
    {
        new AttributeItem("Name","文件名称",  "-","string"),
        new AttributeItem("ContentType","文件类型",  "-","string"),
        new AttributeItem("DataUrl","Base64",  "-","string"),
        new AttributeItem("Size","文件大小",  "-","long"),
    };
}
