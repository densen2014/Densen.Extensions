using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoShared.Shared;

[JSModuleAutoLoader("./_content/DemoShared/modules/menu.js", ModuleName = "Menu", Relative = false)]
public partial class NavMenu
{
    private IEnumerable<MenuItem> Menus { get; set; } = new List<MenuItem>
    {
            new MenuItem() { Text = "首页", Url = "/"  , Match = NavLinkMatch.All},
            new MenuItem() { Text = "扫码 Barcode", Url = "/barcodescanner" },
            new MenuItem() { Text = "签名 Handwritten", Url = "/handwritten" },
            new MenuItem() { Text = "签名 SignaturePad", Url = "/signaturepad" },
            new MenuItem() { Text = "定位 Geolocation", Url = "/geolocations" },
            new MenuItem() { Text = "图片浏览 Viewer", Url = "/viewer" },
            new MenuItem() { Text = "蓝牙和打印 Bluetooth & Printer", Url = "/Bluetooth" },
            new MenuItem() { Text = "光学字符识别 OCR", Url = "/ocr" },
            new MenuItem() { Text = "AI表格识别 AI Form", Url = "/aiform" },
            new MenuItem() { Text = "文件系统 FileSystem", Url = "/filesystems" },
            new MenuItem() { Text = "PDF阅读器 PDF Reader", Url = "/pdfReaders" },
            new MenuItem() { Text = "视频播放器 Video Player", Url = "/videoPlayers" },
            new MenuItem() { Text = "屏幕键盘 OSK", Url = "/onscreenkeyboards" },
            new MenuItem() { Text = "百度地图 Baidu Map", Url = "/baidumap" },
            new MenuItem() { Text = "谷歌地图 Maps", Url = "/maps" },
            new MenuItem() { Text = "系统信息 System info", Url = "/webapis" },
            new MenuItem() { Text = "图表", Url = "/charts" },
            new MenuItem() { Text = "其他" , Items = new List<MenuItem>{
                    new MenuItem() { Text = "TableLazy", Url = "/tablelazy" },
                    new MenuItem() { Text = "Clock", Url = "/clock" },
                    new MenuItem() { Text = "IP", Url = "/ip" },
                } 
            },
    };
}