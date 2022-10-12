## Blazor Bluetooth & Printer 蓝牙和打印 组件

#### 1. 蓝牙打印机 Printer  
#### 2. 蓝牙心率带  Heart Rate 
#### 3. 蓝牙设备电量 Battery Level

### 示例

https://www.blazor.zone/bluetooth

https://blazor.app1.es/bluetooth

## 使用方法:

1. nuget包

    ```BootstrapBlazor.Bluetooth```

2. _Imports.razor 文件 或者页面添加 添加组件库引用

    ```@using BootstrapBlazor.Components```


3. Razor页面

    蓝牙打印机 BT Printer  
    <https://github.com/densen2014/Densen.Extensions/blob/master/Demo/DemoShared/Pages/BtPrinterPage.razor>

    ```
    @using BootstrapBlazor.Components
    
    <Printer OnResult="OnResult" ShowUI="true" Debug="true" />
 
    ```

    蓝牙心率带  
    <https://github.com/densen2014/Densen.Extensions/blob/master/Demo/DemoShared/Pages/BtHeartratePage.razor>

    ```
    @using BootstrapBlazor.Components
    
    <button class="btn btn-outline-secondary" @onclick="GetHeartrate ">查询心率</button>
    <button class="btn btn-outline-secondary" @onclick="StopHeartrate ">停止读取</button>
    <Heartrate @ref="heartrate" OnUpdateValue="OnUpdateValue" />
    <h2 style="color:red" data-action="heartrate"/>
 
    @code{
        Heartrate heartrate { get; set; } = new Heartrate();
        private int? value;
        
        private Task OnUpdateValue(int value)
        {
            this.value = value;
            StateHasChanged();
            return Task.CompletedTask;
        }
    }
    ```

    蓝牙设备电量  
    <https://github.com/densen2014/Densen.Extensions/blob/master/Demo/DemoShared/Pages/BtBatteryLevelPage.razor>
    ```
    @using BootstrapBlazor.Components
    
    <button class="btn btn-outline-secondary" @onclick="GetBatteryLevel ">查询电量</button>
    <BatteryLevel @ref="batteryLevel" OnUpdateValue="OnUpdateValue" />
    <pre>@message</pre>

    @code{
        Heartrate heartrate { get; set; } = new Heartrate();
        private int? value;
        
        private Task OnUpdateValue(decimal value)
        {
            this.value = value;
            this.statusmessage = $"设备电量{value}%";
            StateHasChanged();
            return Task.CompletedTask;
        }
    }

4. 更多信息请参考

    Bootstrap 风格的 Blazor UI 组件库
基于 Bootstrap 样式库精心打造，并且额外增加了 100 多种常用的组件，为您快速开发项目带来非一般的感觉

    <https://www.blazor.zone>

    <https://www.blazor.zone/bluetooth>

----

## Blazor Printer component

#### 1. Printer  
#### 2. Heart Rate 
#### 3. Battery Level

### Demo

https://www.blazor.zone/bluetooth

https://blazor.app1.es/bluetooth

## Instructions:

1. NuGet install pack 

    `BootstrapBlazor.Bluetooth`

2. _Imports.razor or Razor page

   ```
   @using BootstrapBlazor.Components
   ```
3. Razor page

    BT Printer  
    <https://github.com/densen2014/Densen.Extensions/blob/master/Demo/DemoShared/Pages/BtPrinterPage.razor>

    ```
    @using BootstrapBlazor.Components
    
    <Printer OnResult="OnResult" ShowUI="true" Debug="true" />
 
    ```

    Heart rate  
    
    <https://github.com/densen2014/Densen.Extensions/blob/master/Demo/DemoShared/Pages/BtHeartratePage.razor>

    ```
    @using BootstrapBlazor.Components
    
    <button class="btn btn-outline-secondary" @onclick="GetHeartrate ">查询心率</button>
    <button class="btn btn-outline-secondary" @onclick="StopHeartrate ">停止读取</button>
    <Heartrate @ref="heartrate" OnUpdateValue="OnUpdateValue" />
    <h2 style="color:red" data-action="heartrate"/>
 
    @code{
        Heartrate heartrate { get; set; } = new Heartrate();
        private int? value;
        
        private Task OnUpdateValue(int value)
        {
            this.value = value;
            StateHasChanged();
            return Task.CompletedTask;
        }
    }
    ```

    Battery Level  
    <https://github.com/densen2014/Densen.Extensions/blob/master/Demo/DemoShared/Pages/BtBatteryLevelPage.razor>
    ```
    @using BootstrapBlazor.Components
    
    <button class="btn btn-outline-secondary" @onclick="GetBatteryLevel ">查询电量</button>
    <BatteryLevel @ref="batteryLevel" OnUpdateValue="OnUpdateValue" />
    <pre>@message</pre>

    @code{
        Heartrate heartrate { get; set; } = new Heartrate();
        private int? value;
        
        private Task OnUpdateValue(decimal value)
        {
            this.value = value;
            this.statusmessage = $"设备电量{value}%";
            StateHasChanged();
            return Task.CompletedTask;
        }
    }

4.  More informations

    Bootstrap style Blazor UI component library
Based on the Bootstrap style library, it is carefully built, and 100 a variety of commonly used components have been added to bring you an extraordinary feeling for rapid development projects

    <https://www.blazor.zone>

    <https://www.blazor.zone/bluetooth>

