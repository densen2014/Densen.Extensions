## Blazor 光学字符识别 (OCR) 组件

### 示例

https://www.blazor.zone/ocr

https://blazor.app1.es/ocr

## 使用方法:

1. nuget包

    ```BootstrapBlazor.OCR```

2. _Imports.razor 文件 或者页面添加 添加组件库引用

    ```@using BootstrapBlazor.Components```

3. Program.cs 文件添加

    ```
    builder.Services.AddOcrExtensions("YourAzureCvKey", "YourAzureCvEndpoint");
    ```

4. Razor页面

    Razor  
    <https://github.com/densen2014/Densen.Extensions/blob/master/Demo/DemoShared/Pages/OcrPage.razor>

    ```
    @using BootstrapBlazor.Components
    
    <OCR ShowUI="true" Debug="true" />
 
    ```


2. 更多信息请参考

    Bootstrap 风格的 Blazor UI 组件库
基于 Bootstrap 样式库精心打造，并且额外增加了 100 多种常用的组件，为您快速开发项目带来非一般的感觉

    <https://www.blazor.zone>

    <https://www.blazor.zone/ocr>

----

## Blazor OCR component

### Demo

https://www.blazor.zone/ocr

https://blazor.app1.es/ocr

## Instructions:

1. NuGet install pack 

    `BootstrapBlazor.OCR`

2. _Imports.razor or Razor page

   ```
   @using BootstrapBlazor.Components
   ```
   
3. Program.cs

    ```
    builder.Services.AddOcrExtensions("YourAzureCvKey", "YourAzureCvEndpoint");
    ```
 
4. Razor page

    Razor  
    <https://github.com/densen2014/Densen.Extensions/blob/master/Demo/DemoShared/Pages/OcrPage.razor>

    ```
    @using BootstrapBlazor.Components
    
    <OCR ShowUI="true" Debug="true" />
 
    ```
    

2.  More informations

    Bootstrap style Blazor UI component library
Based on the Bootstrap style library, it is carefully built, and 100 a variety of commonly used components have been added to bring you an extraordinary feeling for rapid development projects

    <https://www.blazor.zone>

    <https://www.blazor.zone/ocr>

