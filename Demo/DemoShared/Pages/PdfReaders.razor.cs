﻿using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using System.ComponentModel;

namespace DemoShared.Pages;

public sealed partial class PdfReaders
{
    private EnumZoomMode Zoom { get; set; } = EnumZoomMode.PageHeight;

    private EnumPageMode Pagemode { get; set; } = EnumPageMode.None;

    [DisplayName("搜索")]
    private string Search { get; set; } = "Performance";

    private int Page { get; set; } = 3;

    PdfReader pdfReader { get; set; }

    PdfReader pdfReader2 { get; set; }

    [DisplayName("文件相对路径或者URL")]
    private string Filename1 { get; set; } = "/_content/DemoShared/samples/sample.pdf";

    [DisplayName("文件相对路径或者URL")]
    private string Filename { get; set; } = "/_content/DemoShared/samples/sample2.pdf";

    private string FilenameStream { get; set; } = "https://blazor.app1.es/_content/DemoShared/samples/sample.pdf";

    [DisplayName("流模式")]
    private bool StreamMode { get; set; }

    private bool Debug { get; set; }

    [DisplayName("禁用复制/打印/下载")]
    public bool ReadOnly { get; set; } = true;

    private async Task Apply()
    {
        await pdfReader.Refresh();
    }

    private async Task ApplyZoom()
    {
        Zoom = Zoom switch
        {
            EnumZoomMode.Auto => EnumZoomMode.PageFit,
            EnumZoomMode.PageFit => EnumZoomMode.PageWidth,
            EnumZoomMode.PageWidth => EnumZoomMode.PageHeight,
            EnumZoomMode.PageHeight => EnumZoomMode.Zoom75,
            EnumZoomMode.Zoom75 => EnumZoomMode.Zoom50,
            EnumZoomMode.Zoom50 => EnumZoomMode.Zoom25,
            _ => EnumZoomMode.Auto
        };
        await pdfReader2?.Refresh();
    }

    private async Task ApplyPagemode()
    {
        Pagemode = Pagemode switch
        {
            EnumPageMode.Thumbs => EnumPageMode.Outline,
            EnumPageMode.Outline => EnumPageMode.Attachments,
            EnumPageMode.Attachments => EnumPageMode.Layers,
            EnumPageMode.Layers => EnumPageMode.None,
            _ => EnumPageMode.Thumbs
        };
        await pdfReader2?.Refresh(Search, Page, Pagemode, Zoom, ReadOnly);
    }
    private async Task ApplyPage()
    {
        Search = null;
        await pdfReader2?.Refresh(Search, Page, Pagemode, Zoom, ReadOnly);
    }
    private async Task ApplyPagePrevious()
    {
        Page--;
        Search = null;
        await pdfReader2?.Refresh(Search, Page, Pagemode, Zoom, ReadOnly);
    }
    private async Task ApplyPageNext()
    {
        Page++;
        Search = null;
        await pdfReader2?.Refresh(Search, Page, Pagemode, Zoom, ReadOnly);
    }
    private async Task ApplySearch()
    {
        await pdfReader2?.Refresh(Search, Page, Pagemode, Zoom, ReadOnly);
    }
}
