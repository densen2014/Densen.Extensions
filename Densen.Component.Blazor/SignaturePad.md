# Blazor SignaturePad 手写签名 组件

示例:

https://blazor.app1.es/signaturepad

使用方法:

1.nuget包

```Densen.Component.Blazor 0.0.11+```

2._Imports.razor 文件 或者页面添加 添加组件库引用

```@using AmeBlazor.Components```


3.razor页面
```
    <SignaturePad OnResult="((e) =>  Result=e)" />

```
@code{

    /// <summary>
    /// 签名Base64
    /// </summary>
    public string? Result { get; set; }

}
```
