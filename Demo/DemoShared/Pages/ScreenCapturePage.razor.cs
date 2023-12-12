// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using BootstrapBlazor.Components;
using BootstrapBlazor.WebAPI.Services;
using Microsoft.AspNetCore.Components;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using ZXingBlazor.Components;

namespace DemoShared.Pages;

/// <summary>
/// Screen Capture 截屏/录像
/// </summary>
public sealed partial class ScreenCapturePage
{
    [NotNull]
    public Capture? Capture { get; set; }

    [Inject, NotNull]
    public IStorage? Storage { get; set; }

    private CaptureOptions Options { get; set; } = new CaptureOptions();
    private CaptureOptions OptionsEff { get; set; } = new CaptureOptions() { Effect = EnmuCaptureEffect.无, EffectPreview=true };
    private string? Message { get; set; }

    [NotNull]
    private Cams SelectedEnumItem { get; set; } = Cams.FHD;
    private enum Cams
    {
        VGA,
        HD,
        FHD,
        QHD,
        UHD,
    }

    public bool IsInit { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            //Cam1080 = await Storage.GetValue("Cam1080", "true") == "true";
            try
            {
                SelectedEnumItem = await Storage.GetValue("Cams", Cams.VGA);
            }
            catch (Exception)
            {

            }
            IsInit = true;
            StateHasChanged();
        }
    }

    private async Task OnSelectedChanged(IEnumerable<SelectedItem> values, Cams val)
    {
        await Storage.SetValue("Cams", val);
        //StateHasChanged();
    }

    private Task OnSelectedEffectChanged(IEnumerable<SelectedItem> values, EnmuCaptureEffect val)
    {
        OptionsEff.Effect = val;
        return Task.CompletedTask;
    }

    private async Task OnCaptureResult(Stream item)
    {
        if (EnableOCR && OCR != null) await OCR.OCRFromStream(item);
        StateHasChanged();
    }

    private async Task OnCapture(string dataurl)
    {
        if (EnableQR) await BarCodes!.DecodeFromImage(dataurl);
        StateHasChanged();
    }

    private Task OnError(string message)
    {
        Message = message;
        StateHasChanged();
        return Task.CompletedTask;
    }

    #region 附加OCR演示

    [DisplayName("启用OCR识别")]
    private bool EnableOCR { get; set; }

    private OCR? OCR { get; set; }

    private Task OnResult(List<string> res)
    {
        Message = "识别完成";
        StateHasChanged();
        return Task.CompletedTask;
    }

    #endregion

    [DisplayName("启用条码识别")]
    private bool EnableQR { get; set; }
    BarCodes? BarCodes { get; set; }
    string? Result { get; set; }
    private Task OnResult(string message)
    {
        Message = string.Empty;
        Result = message;
        StateHasChanged();
        return Task.CompletedTask;
    }

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new AttributeItem("Start","截屏",  "-","Task"),
        new AttributeItem("OnCaptureResult","截屏回调",  "-","Func<Stream, Task>"),
        new AttributeItem("OnError","错误信息回调",  "-","Func<string, Task>"),
        new AttributeItem("ShowUI","显示内置UI",  "true","bool","true|false"),
        new AttributeItem("Debug","显示log",  "false","bool","true|false"),
        new AttributeItem("Auto","自动启动摄像头预览",  "true","bool"),
        new AttributeItem("Quality","图像质量,默认为 0.8",  "0.8","double"),
        new AttributeItem("Width","图像宽度",  "640","int"),
        new AttributeItem("Height","图像高度",  "480","int"),
        new AttributeItem(nameof(Capture.SelectDeviceBtnTitle),"选择设备按钮文本",  "选择设备","string"),
        new AttributeItem(nameof(Capture.DeviceID),"指定摄像头设备ID",  "-","string"),
        new AttributeItem(nameof(Capture.SaveDeviceID),"保存最后使用设备ID下次自动调用",  "true","bool"),
    };
}
