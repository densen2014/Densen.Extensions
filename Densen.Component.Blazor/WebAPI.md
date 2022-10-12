# Blazor WebAPI 组件 (电池信息/网络信息)

1. 电池信息类
2. 网络信息类


示例:

https://blazor.app1.es/WebAPI

使用方法:

1.nuget包

```BootstrapBlazor.WebAPI```

2._Imports.razor 文件 或者页面添加 添加组件库引用

```@using BootstrapBlazor.Components```


3.razor页面
```
<WebApi OnError="@OnError" />

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