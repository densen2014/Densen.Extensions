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
            new AttributeItem("GoogleKey", @"为空则在 IConfiguration 服务获取 ""GoogleKey"" , 默认在 appsettings.json 文件配置",""),
            new AttributeItem("Style","地图大小",  "height:700px;width:100%;","string"),
            new AttributeItem("Init","初始化",  "-","Task<bool>"),
            new AttributeItem("OnError","错误信息回调",  "-","Func<string, Task>"),
    };
}
