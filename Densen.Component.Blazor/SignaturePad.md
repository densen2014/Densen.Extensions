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
```
    <SignaturePad OnResult="((e) =>  Result=e)" BtnCssClass="btn btn-outline-success" />
```
```
    <SignaturePad OnResult="((e) =>  Result=e)"
                  SignAboveLabel="Sign above"
                  UndoBtnTitle="Undo"
                  SaveBase64BtnTitle="OK"
                  ChangeColorBtnTitle="Change color"
                  ClearBtnTitle="Clear" />
```

```
@code{

    /// <summary>
    /// 签名Base64
    /// </summary>
    public string? Result { get; set; }

}
```

源码分离到独立工程:

https://github.com/densen2014/BootstrapBlazor.SignaturePad