# Blazor Viewerjs 组件

封装Viewer.js库

示例:

https://test1.app1.es/

使用方法:

1.nuget包

```Densen.Component.Blazor```

2._Imports.razor 文件 或者页面添加 添加组件库引用

```@using AmeBlazor.Components```


3.razor页面

Demo

```<Viewerjs />```

单图片+简化工具条

```<Viewerjs Src="@_srcPhoto" Width="600px" Height="300px" toolbarlite="true" />```

多图片+简化工具条

```<Viewerjs Images='new List<string>() { "photo-2.jpg","photo-1.jpg","photo-3.jpg"}' toolbarlite="true" />```

多图片

```<Viewerjs Images='new List<string>() { "photo-2.jpg","photo-1.jpg","photo-3.jpg"}' />```

