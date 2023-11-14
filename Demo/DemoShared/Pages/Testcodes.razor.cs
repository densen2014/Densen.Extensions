// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using BootstrapBlazor.Components;
using BootstrapBlazor.WebAPI.Services;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using ZXingBlazor.Components;
using static BootstrapBlazor.Components.UploadToBase64;

namespace DemoShared.Pages;

/// <summary>
/// 
/// </summary>
public partial class Testcodes
{
    BarcodeReader? barcodeReaderCustom;

    [Inject, NotNull]
    public IStorage? Storage { get; set; }

    /// <summary>
    /// 显示扫码界面Custom
    /// </summary>
    bool CheckAPI { get; set; } = true;

    bool IsBusy { get; set; }

    public string? BarCodesCustom { get; set; }

    private async Task ScanResult2(string e)
    {
        BarCode = e;
        if (BarCodesCustom?.Length > 60)
        {
            BarCodesCustom = string.Empty;
        }
        BarCodesCustom = $"{DateTime.Now:mm:ss} {e}{Environment.NewLine}{BarCodesCustom}";

        if (CheckAPI && !IsBusy)
        {
            IsBusy = true;
            _ = Task.Run(async () =>
            {
                await Task.Delay(1000);
                IsBusy = false;
            });
            await OnAPI();
        }
    }

    BarCodes? barCodes;

    private async Task OnScan()
    {
        CheckAPI = !CheckAPI;
        if (CheckAPI)
            await barcodeReaderCustom!.Start();
        else
            await barcodeReaderCustom!.Stop();
    }

    private async Task OnChanged(List<ImageFile> items)
    {
        await barcodeReaderCustom!.Stop();
        CheckAPI = false;
        BarCodesCustom = string.Empty;
        items.ForEach(item => BarCodesCustom = $"{DateTime.Now:mm:ss} {item.ContentType} size {item.Size/1024:n2}kb {Environment.NewLine}{BarCodesCustom}");
        StateHasChanged();
        await barCodes!.DecodeFromImage(items[0].DataUrl!);
    }

    private async Task DecodeResult(string e)
    {
        BarCode = e;
        if (BarCodesCustom?.Length > 60)
        {
            BarCodesCustom = string.Empty;
        }
        BarCodesCustom = $"{DateTime.Now:mm:ss} {e}{Environment.NewLine}{BarCodesCustom}";

        IsBusy = true;
        _ = Task.Run(async () =>
        {
            await Task.Delay(1000);
            IsBusy = false;
        });
        await OnAPI();

    }
    private Task OnError(string message)
    {
        BarCodesCustom = $"{DateTime.Now:mm:ss} 错误: {message}{Environment.NewLine}{BarCodesCustom}";
        StateHasChanged();
        return Task.CompletedTask;
    }


    /// <summary>
    /// 条码
    /// </summary>
    public string? BarCode { get; set; }

    [Inject] HttpClient? httpClient { get; set; }

    List<Prod>? prods { get; set; }

    private Task OnEsc()
    {
        BarCode = string.Empty;
        StateHasChanged();
        return Task.CompletedTask;
    }


    private Task OnAPI()
    {

        try
        {
            var json = httpClient!.GetStringAsync($"https://api6012.app1.es/ibProduct/GetProductsV2?ibnumber={ibnumber}&pagesize=5&keyword={BarCode}");
            var result = JsonSerializer.Deserialize<Rootobject>(json.Result);
            if (result != null)
            {
                prods = result?.data?.prods?.ToList();
                StateHasChanged();
            }
        }
        catch
        {
        }
        return Task.CompletedTask;
    }



    public class Rootobject
    {
        public Data? data { get; set; }
        public int code { get; set; }
        public string? message { get; set; }
    }

    public class Data
    {
        public int pagesize { get; set; }
        public int pagenumber { get; set; }
        public int pagecount { get; set; }
        public int count { get; set; }
        public Prod[]? prods { get; set; }
    }

    public class Prod
    {
        public string? BarCode { get; set; }
        public string? UserCode { get; set; }
        public string? ProductName { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? UnitPrice2 { get; set; }
        public decimal? Discount { get; set; }
        public string? Image { get; set; }
        public string? Remark { get; set; }
    }

    private string? ibnumber;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            try
            {
                ibnumber = await Storage.GetValue("testcode", ibnumber);
            }
            catch (Exception)
            {

            }
        }
    }
    private async Task OnChangedCode()
    {
        await Storage.SetValue("testcode", ibnumber); 
    }
}
