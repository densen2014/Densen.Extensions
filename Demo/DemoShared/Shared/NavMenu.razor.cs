// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace DemoShared.Shared;

public partial class NavMenu
{
    private IEnumerable<MenuItem> Menus { get; set; } = new List<MenuItem>
    {
            new MenuItem() { Text = "首页", Url = "/"  , Match = NavLinkMatch.All},
            new MenuItem() { Text = "工具" ,Items= new List<MenuItem>
                {
                    new MenuItem() { Text = "签名 SignaturePad", Url = "/signaturepad" },
                    new MenuItem() { Text = "定位 Geolocation", Url = "/geolocations" },
                    new MenuItem() { Text = "屏幕键盘 OSK", Url = "/onscreenkeyboards" },
                    new MenuItem() { Text = "系统信息 System info", Url = "/webapis" },
                    new MenuItem() { Text = "签名 Handwritten", Url = "/handwritten" },
                    new MenuItem() { Text = "存取设置 Storage", Url = "/Storages" },
                    new MenuItem() { Text = "上传图片 (Base64)", Url = "/UploadToBase64s" },
                    new MenuItem() { Text = "分享按钮 Share", Url = "/ShareObject" },
                    new MenuItem() { Text = "思维导图 MindMap", Url = "/MindMaps" },
                    new MenuItem() { Text = "条码生成器 BarcodeGenerator", Url = "/barcodeGenerators" },
                    new MenuItem() { Text = "微信扫码 WxQrCode", Url = "/testcanvas" },
                }
            },
            new MenuItem() { Text = "文件" ,Items= new List<MenuItem>
                {
                    new MenuItem() { Text = "文件预览 FileViewer", Url = "/fileViewers" },
                    new MenuItem() { Text = "PDF阅读器 PDF Reader", Url = "/pdfReaders" },
                    new MenuItem() { Text = "文件", Url = "/Files" },
                    new MenuItem() { Text = "上传文件", Url = "/FileUpload" },
                    new MenuItem() { Text = "文件夹", Url = "/AppFiles" },
                    new MenuItem() { Text = "下载", Url = "/Downloads" },
                    new MenuItem() { Text = "BlazorHybrid项目下载", Url = "/BlazorHybrid" },
                    new MenuItem() { Text = "文件系统 FileSystem", Url = "/filesystems" },
                }
           },
            new MenuItem() { Text = "媒体" ,Items= new List<MenuItem>
                {
                    new MenuItem() { Text = "视频播放器 Video Player", Url = "/videoPlayers" },
                    new MenuItem() { Text = "图片浏览 Viewer", Url = "/viewer" },
                    new MenuItem() { Text = "录屏 ScreenRecord", Url = "/ScreenRecords" },
                    new MenuItem() { Text = "语音识别/合成 Speech", Url = "/Speechs" },
                    new MenuItem() { Text = "图像裁剪 ImageCropper", Url = "/ImageCroppers" },
                    new MenuItem() { Text = "图像助手 ImageHelper", Url = "/ImageHelpers" },
                }
            },
            new MenuItem() { Text = "硬件" ,Items= new List<MenuItem>
                {
                    new MenuItem() { Text = "扫码 Barcode (条码,QR码,Pdf417)", Url = "/barcodescanner" },
                    new MenuItem() { Text = "摄像头/截屏/录像 Screen Capture", Url = "/screencapture" },
                    new MenuItem() { Text = "蓝牙和打印 Bluetooth & Printer", Url = "/Bluetooth" },
                    new MenuItem() { Text = "串行设备 WebSerials", Url = "/WebSerials" },
                }
            },
            new MenuItem() { Text = "云服务" ,Items= new List<MenuItem>
                {
                    new MenuItem() { Text = "光学字符识别 OCR", Url = "/ocr" },
                    new MenuItem() { Text = "AI表格识别 AI Form", Url = "/aiform" },
                    new MenuItem() { Text = "翻译 Translate", Url = "/Translate" },
                    new MenuItem() { Text = "播放语音/文本转语音 PlayAudio", Url = "/PlayAudio" },
                    new MenuItem() { Text = "OpenAI", Url = "/OpenAI" },
                    //new MenuItem() { Text = "AzureOpenAI", Url = "/AzureOpenAI" },
                }
            },
            new MenuItem() { Text = "实验" ,Items= new List<MenuItem>
                {
                    new MenuItem() { Text = "屏幕方向", Url = "/ScreenOrientations" },
                    new MenuItem() { Text = "视频墙", Url = "/VideoWall" },
                    new MenuItem() { Text = "Iframe下载文件", Url = "/TestIframe2" },
                    new MenuItem() { Text = "JsBridge", Url = "/JsBridge" },
                    new MenuItem() { Text = "testocr", Url = "/testocr" },
                    new MenuItem() { Text = "菜哥AI", Url = "/testopenai" },
                    new MenuItem() { Text = "扫码API", Url = "/testcodes" },
                    new MenuItem() { Text = "OpenV", Url = "/testcanvas" },
                }
            },
            new MenuItem() { Text = "地图" ,Items= new List<MenuItem>
                {
                    new MenuItem() { Text = "百度地图 Baidu Map", Url = "/baidumap" },
                    new MenuItem() { Text = "谷歌地图 Maps", Url = "/maps" },
                }
            },
            new MenuItem() { Text = "图表", Url = "/charts" },
           new MenuItem() { Text = "其他" , Items = new List<MenuItem>{
                    new MenuItem() { Text = "TableLazy", Url = "/tablelazy" },
                    new MenuItem() { Text = "Clock", Url = "/clock" },
                    new MenuItem() { Text = "IP", Url = "/ip" },
                }
            },
        new MenuItem() { Text = "本站源码", Url = "https://github.com/densen2014/Densen.Extensions/blob/master/Demo/DemoShared/Pages?WT.mc_id=DT-MVP-5005078" },

    };
}
