// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using BootstrapBlazor.Components;
using BootstrapBlazor.WebAPI.Services;
using FreeSql.DataAnnotations;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using ZXingBlazor.Components;

namespace DemoShared.Pages;

/// <summary>
/// 扫码
/// </summary>
public partial class BarcodeScannerPage: AppComponentBase
{


    /// <summary>
    /// 显示扫码界面
    /// </summary>
    bool ShowScanBarcode { get; set; } = false;

    /// <summary>
    /// 条码
    /// </summary>
    public string? BarCode { get; set; }

    public bool Pdf417 { get; set; }
    public bool DecodeContinuously { get; set; }
    public bool DecodeAllFormats { get; set; }

    private string? message;

    private Task OnError(string message)
    {
        this.message = message;
        StateHasChanged();
        return Task.CompletedTask;
    }

    bool ShowCodes;

    private async Task ScanResult(string e)
    {
        if (!DecodeContinuously)
        {
            BarCode = e;
            ShowScanBarcode = !ShowScanBarcode;
        }
        else
        {
            BarCode += e + Environment.NewLine;
            await ToastService.Information("结果", e);
        }
    }

    #region Custom

    /// <summary>
    /// 显示扫码界面Custom
    /// </summary>
    bool ShowScanBarcodeCustom { get; set; } = false;

    /// <summary>
    /// 条码
    /// </summary>
    public string? BarCodeCustom { get; set; }

    BarcodeReader? barcodeReaderCustom;

    #endregion

    BarCodes? barCodes;

    string? MessageString { get; set; } = "测试二维码 https://www.blazor.zone";

    string? Result { get; set; }

    private async Task GenQrcode()
    {
        await barCodes!.QRCodeGen(string.IsNullOrWhiteSpace(MessageString) ? "111111" : MessageString);
    }

    private async Task GenQrcodeSvg()
    {
        await barCodes!.QRCodeGenSvg(string.IsNullOrWhiteSpace(MessageString) ? "111111" : MessageString);
    }

    private Task OnResult(string message)
    {
        this.Result = message;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private async Task DecodeFromImage()
    {
        await barCodes!.DecodeFromImage();
    }

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes2() => new AttributeItem[]
    {
                new AttributeItem("OnQRCodeGen","二维码数据流回调方法/Generate QRcode callback method",  "","Func<string, Task>?"),
                new AttributeItem("OnDecodeFromImage","解码回调方法/ Decode from image callback method","","Func<string, Task>?"),
                new AttributeItem("OnError","错误信息回调/Error callback method",  "-","Func<string, Task>"),
                new AttributeItem("Options","选项/ZXingOptions",""),
                new AttributeItem("QRCodeWidth","二维码宽度/ QR Code width","300","int"),
                new AttributeItem("DecodeAllFormats","解码所有编码形式,性能较差, 开启后可用 options.formats 指定编码形式.默认为 false | Decodde All Formats, performance is poor, you can set options.formats to customize specify the encoding formats. The default is false","true","bool"),
                new AttributeItem("QRCodeGen","生成SVG二维码 / Generate SVG QR code","","Task"),
                new AttributeItem("QRCodeGenSvg","生成SVG二维码数据流文本 / Generate SVG QR code data flow text","","Task"),
                new AttributeItem("DecodeFromImage","选择图片解码 / Select picture decoding","","Task"),
                                    };

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
                new AttributeItem("ScanResult","扫码结果回调方法/Scan result callback method",  "","EventCallback<string>"),
                new AttributeItem("Close","关闭扫码框回调方法/Close scan code callback method","","EventCallback"),
                new AttributeItem("ScanBtnTitle","扫码按钮文本/Scan button title","扫码"),
                new AttributeItem("ResetBtnTitle","复位按钮文本/Reset button title","复位"),
                new AttributeItem("CloseBtnTitle","关闭按钮文本/Close button title","关闭"),
                new AttributeItem("SelectDeviceBtnTitle","选择设备按钮文本/Select device button title","选择设备"),
                new AttributeItem("OnError","错误信息回调/Error callback method",  "-","Func<string, Task>"),
                new AttributeItem("UseBuiltinDiv","使用内置DIV/Use builtin Div",  "true","Func<string, Task>"),
                new AttributeItem("Pdf417Only","只解码 Pdf417 格式 / decode only Pdf417 format",  "false","bool"),
                new AttributeItem("Decodeonce","单次|连续解码,默认单次 / Decode Once or Decode Continuously, default is Once",  "true","bool"),
                new AttributeItem("DecodeAllFormats","解码所有编码形式,性能较差, 开启后可用 options.formats 指定编码形式.默认为 false | Decodde All Formats, performance is poor, you can set options.formats to customize specify the encoding formats. The default is false","true","bool"),
                new AttributeItem("Options","选项/ZXingOptions",""),
                new AttributeItem(nameof(BarcodeReader.DeviceID),"指定摄像头设备ID",  "-","string"),
                new AttributeItem(nameof(BarcodeReader.SaveDeviceID),"保存最后使用设备ID下次自动调用",  "true","bool"),
                        };
}
