# Blazor Maps 谷歌地图 组件

定位组件

示例:

https://blazor.app1.es/maps

使用方法:

1.nuget包

```Densen.Component.Blazor```

2._Imports.razor 文件 或者页面添加 添加组件库引用

```@using AmeBlazor.Components```


3.razor页面
```
<Map OnError="@OnError" />

```
```
@code{

    private string message;

    private Task OnError(string message)
    {
        this.message = message;
        StateHasChanged();
        return Task.CompletedTask;
    }

} 
```