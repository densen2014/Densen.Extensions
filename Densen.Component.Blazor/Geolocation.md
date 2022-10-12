# Blazor Geolocation 定位/持续定位 组件

定位组件

示例:

https://blazor.app1.es/geolocations

使用方法:

1.nuget包

```Densen.Component.Blazor```

2._Imports.razor 文件 或者页面添加 添加组件库引用

```@using AmeBlazor.Components```


3.razor页面
```
<Geolocation OnResult="@OnResult" OnUpdateStatus="@OnUpdateStatus" />

<Badge Color="Color.Warning">@status</Badge>

<Table TItem="Geolocationitem" Items="Items" AutoGenerateColumns="true">
</Table>
```
```
@code{

    private string? status { get; set; }
    private Geolocationitem? geolocations { get; set; }
    private List<Geolocationitem> Items { get; set; } = new List<Geolocationitem>() { new Geolocationitem() };

    private Task OnResult(Geolocationitem geolocations)
    {
        this.geolocations = geolocations;
        Items[0] = geolocations;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnUpdateStatus(string status)
    {
        this.status = status;
        StateHasChanged();
        return Task.CompletedTask;
    } 

} 
```

源码分离到独立工程:

https://github.com/densen2014/BootstrapBlazor.Geolocation