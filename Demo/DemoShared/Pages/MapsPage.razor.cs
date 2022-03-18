// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using AmeBlazor.Components;

namespace DemoShared.Pages;

/// <summary>
/// 谷歌地图 Maps
/// </summary>
public sealed partial class MapsPage
{

    private string message;


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
            new AttributeItem("Style","地图大小",  "height:700px;width:100%;","string"),
            new AttributeItem("Init","初始化",  "-","Task<bool>"),
            new AttributeItem("OnError","错误信息回调",  "-","Func<string, Task>"),
    };
}
