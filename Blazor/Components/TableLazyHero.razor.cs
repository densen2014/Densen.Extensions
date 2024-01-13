// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using AME;
using AME.Services;
using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using static AME.EnumsExtensions;

namespace AmeBlazor.Components;


/// <summary>
/// TableLazy 组件,使用注入LazyHeroDataService服务维护内存数据
/// </summary>
/// <typeparam name="TItem">主表类名</typeparam> 
public partial class TableLazyHero<TItem> : BootstrapComponentBase
    where TItem : class, new()
{

    public Table<TItem> mainTable;

    [Inject] private LazyHeroDataService<TItem> LazyHeroDataService { get; set; }
    [Inject] protected ToastService ToastService { get; set; }

    /// <summary>
    /// 获得/设置 IJSRuntime 实例
    /// </summary>
    [Inject]
    [NotNull]
    protected IJSRuntime JsRuntime { get; set; }

    /// <summary>
    /// 动态附加查询条件, 详表关联字段名称, 当Field!=FieldD才需要设置
    /// </summary>
    [Parameter] public string FieldD { get; set; } = null;

    /// <summary>
    /// 获得/设置 显示导入,执行添加工具栏按钮
    /// </summary>
    [Parameter] public ShowToolbarType ShowToolbarType { get; set; } = ShowToolbarType.无;
    protected Modal ExtraLargeModal { get; set; }
    public TItem SelectOneItem { get; set; }

    [Parameter] public EventCallback<string> Excel导入 { get; set; }
    [Parameter] public string Excel导入文本 { get; set; } = "Excel导入";
    [Parameter] public EventCallback<string> 导入 { get; set; }
    [Parameter] public string 导入文本 { get; set; } = "导入";
    [Parameter] public EventCallback<string> 导入II { get; set; }
    [Parameter] public string 导入II文本 { get; set; } = "导入II";
    [Parameter] public EventCallback<string> 执行添加 { get; set; }
    [Parameter] public string 执行添加文本 { get; set; } = "执行添加";
    [Parameter] public EventCallback<IEnumerable<TItem>> 导出 { get; set; }
    [Parameter] public string 导出文本 { get; set; } = "导出";
    [Parameter] public EventCallback<string> 刷新 { get; set; }
    [Parameter] public string 刷新文本 { get; set; } = "刷新";
    [Parameter] public string 打印文本 { get; set; } = "打印";
    [Parameter] public string 查询文本 { get; set; } = "查询";
    [Parameter] public string 新窗口打开文字 { get; set; } = "新窗口打开";
    [Parameter] public string 新窗口打开Url { get; set; }
    [Parameter] public int Footercolspan1 { get; set; } = 3;
    [Parameter] public int Footercolspan2 { get; set; }
    [Parameter] public int Footercolspan3 { get; set; }
    [Parameter] public int FootercolspanTotal { get; set; } = 2;
    [Parameter] public string FooterText { get; set; }
    [Parameter] public string FooterText2 { get; set; }
    [Parameter] public string FooterText3 { get; set; }
    [Parameter] public string FooterTotal { get; set; }
    [Parameter] public bool ShowDateTimeRange { get; set; }
    [Parameter] public bool DateTimeRangeDefaultMotherly { get; set; }
    /// <summary>
    /// 动态附加查询条件, 主键字段类型
    /// </summary>
    [Parameter] public Type FieldType { get; set; } = typeof(int);
    [Parameter] public string Field { get; set; } = "ID";

#nullable enable 
    /// <summary>
    /// 图片列参数,图片列参数集合优先
    /// </summary>
    [Parameter] public TableImgField? TableImgField { get; set; }

    /// <summary>
    /// 图片列参数集合,优先读取
    /// </summary>
    [Parameter] public List<TableImgField>? TableImgFields { get; set; }

    /// <summary>
    /// 获得/设置 是否显示导出按钮 默认为 false 显示
    /// </summary>
    [Parameter]
    public bool ShowExportButton { get; set; }

    /// <summary>
    /// 获得/设置 导出按钮图标 默认为 fa-solid fa-download
    /// </summary>
    [Parameter]
    public string? ExportButtonIcon { get; set; }

    /// <summary>
    /// 获得/设置 导出按钮异步回调方法
    /// </summary>
    [Parameter]
    public Func<ITableExportDataContext<TItem>, Task<bool>>? OnExportAsync { get; set; }

#nullable disable

    /// <summary>
    /// 使用日期范围
    /// </summary>
    public bool EnableDateTimeRange { get; set; } = true;
    public string SearchText { get; set; }
    [Parameter] public EventCallback<string> 查询 { get; set; }
    [Parameter] public EventCallback<string> 多点数据 { get; set; }
    [Parameter]
    public List<SelectedItem> 多点数据Items { get; set; } = [new SelectedItem { Text = "本机", Value = "", Active = true }];


    /// <summary>
    /// 标题
    /// </summary>
    [Parameter] public string Title { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        LazyHeroDataService.Items = Items;
        LazyHeroDataService.Field = Field;
    }
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
        if (!firstRender)
        {
            return;
        }

        InitDateTimeRange();
    }


    /// <summary>
    /// 重新加载数据
    /// </summary>
    /// <param name="items"></param>
    /// <returns></returns>
    public async Task Load(List<TItem> items)
    {
        //if (!string.IsNullOrEmpty(FooterTotal)) await InvokeAsync(StateHasChanged);
        Items = items;
        LazyHeroDataService.Items = items;
        await mainTable.QueryAsync();
    }

    private async Task ImportExcel()
    {
        if (!Items.Any())
        {
            ToastService?.Error("提示", "数据为空!");
            return;
        }
        var option = new ToastOption()
        {
            Category = ToastCategory.Information,
            Title = "提示",
            Content = "导入文件中,请稍等片刻...",
            IsAutoHide = false
        };
        // 弹出 Toast
        await ToastService.Show(option);
        await Task.Delay(100);


        // 开启后台进程进行数据处理
        if (!Excel导入.HasDelegate)
        {
            await MockDownLoadAsync();
        }
        else
        {
            await Excel导入.InvokeAsync("");
        }

        // 关闭 option 相关联的弹窗
        option.Close();

        // 弹窗告知下载完毕
        await ToastService.Show(new ToastOption()
        {
            Category = ToastCategory.Success,
            Title = "提示",
            Content = "操作成功,请检查数据",
        });

        await mainTable.QueryAsync();
    }

    private async Task ImportItems()
    {
        if (!导入.HasDelegate)
        {
            ToastService?.Error("提示", "操作过程为空!");
            return;
        }
        if (!Items.Any())
        {
            ToastService?.Error("提示", "数据为空!");
            return;
        }
        var option = new ToastOption()
        {
            Category = ToastCategory.Information,
            Title = "提示",
            Content = "导入文件中,请稍等片刻...",
            IsAutoHide = false
        };
        // 弹出 Toast
        await ToastService.Show(option);
        await Task.Delay(100);


        // 开启后台进程进行数据处理
        await 导入.InvokeAsync("");

        // 关闭 option 相关联的弹窗
        option.Close();

        // 弹窗告知下载完毕
        await ToastService.Show(new ToastOption()
        {
            Category = ToastCategory.Success,
            Title = "提示",
            Content = "操作成功,请检查数据",
        });

        await mainTable.QueryAsync();
    }
    private async Task ImportItemsII()
    {
        if (!导入II.HasDelegate)
        {
            ToastService?.Error("提示", "操作过程为空!");
            return;
        }
        if (!Items.Any())
        {
            ToastService?.Error("提示", "数据为空!");
            return;
        }
        var option = new ToastOption()
        {
            Category = ToastCategory.Information,
            Title = "提示",
            Content = "导入文件中,请稍等片刻...",
            IsAutoHide = false
        };
        // 弹出 Toast
        await ToastService.Show(option);
        await Task.Delay(100);


        // 开启后台进程进行数据处理
        await 导入II.InvokeAsync("");

        // 关闭 option 相关联的弹窗
        option.Close();

        // 弹窗告知下载完毕
        await ToastService.Show(new ToastOption()
        {
            Category = ToastCategory.Success,
            Title = "提示",
            Content = "操作成功,请检查数据",
        });

        await mainTable.QueryAsync();
    }
    private async Task 执行添加Cmd()
    {
        var option = new ToastOption()
        {
            Category = ToastCategory.Information,
            Title = "提示",
            Content = "执行添加中,请稍等片刻...",
            IsAutoHide = false
        };
        // 弹出 Toast
        await ToastService.Show(option);
        await Task.Delay(100);


        // 开启后台进程进行数据处理
        if (!执行添加.HasDelegate)
        {
            await MockDownLoadAsync();
        }
        else
        {
            await 执行添加.InvokeAsync("");
        }

        // 关闭 option 相关联的弹窗
        option.Close();

        // 弹窗告知下载完毕
        await ToastService.Show(new ToastOption()
        {
            Category = ToastCategory.Success,
            Title = "提示",
            Content = "操作成功,请检查数据",
        });

    }
    private async Task ExportExcel(IEnumerable<TItem> item)
    {
        var option = new ToastOption()
        {
            Category = ToastCategory.Information,
            Title = "提示",
            Content = "导出正在执行,请稍等片刻...",
            IsAutoHide = false
        };
        // 弹出 Toast
        await ToastService.Show(option);
        await Task.Delay(100);


        // 开启后台进程进行数据处理
        if (!导出.HasDelegate)
        {
            await MockDownLoadAsync();
        }
        else
        {
            await 导出.InvokeAsync(item);
        }

        // 关闭 option 相关联的弹窗
        option.Close();

        // 弹窗告知下载完毕
        await ToastService.Show(new ToastOption()
        {
            Category = ToastCategory.Success,
            Title = "提示",
            Content = "操作成功,请检查数据",
        });

    }
    public Task PrintPreview(IEnumerable<TItem> item)
    {
        JsRuntime.InvokeAsync<object>(
        "toolsFunctions.printpreview", 100
        );
        return Task.CompletedTask;
    }

    private async Task Reset()
    {
        if (刷新.HasDelegate)
        {
            await 刷新.InvokeAsync("");
            await mainTable.QueryAsync();
        }
    }
    private async Task 查询Click()
    {
        if (查询.HasDelegate)
        {
            await 查询.InvokeAsync("");
            await mainTable.QueryAsync();
        }
    }

    private async Task MockDownLoadAsync()
    {
        // 此处模拟打包下载数据耗时 5 秒
        await Task.Delay(TimeSpan.FromSeconds(5));
    }

    private async Task Select多点数据Item(SelectedItem e)
    {
        if (多点数据.HasDelegate)
        {
            await 多点数据.InvokeAsync(e.Value);
        }
    }

    #region 动态生成控件

    /// <summary>
    /// 动态获取表达式
    /// </summary>
    /// <param name="model"></param>
    /// <param name="field">列名,默认"ID"</param>
    /// <param name="fieldType">列类型,默认typeof(int)</param>
    /// <returns></returns>
    private object GetExpression(object model, string field = "ID", Type fieldType = null)
    {
        // ValueExpression
        var body = Expression.Property(Expression.Constant(model), typeof(TItem), field);
        var tDelegate = typeof(Func<>).MakeGenericType(fieldType ?? typeof(int));
        var valueExpression = Expression.Lambda(tDelegate, body);
        return valueExpression;
    }


    /// <summary>
    /// 动态生成控件 TableColumn 图片列
    /// </summary>
    /// <param name="model"></param>
    /// <param name="tableImgField"></param>
    /// <returns></returns>
    private RenderFragment RenderTableImgColumn(TItem model, TableImgField tableImgField = null) => builder =>
    {
        tableImgField = tableImgField ?? TableImgField;
        var fieldExpresson = GetExpression(model, tableImgField.Field, tableImgField.FieldType);
        builder.OpenComponent(0, typeof(TableColumn<,>).MakeGenericType(typeof(TItem), tableImgField.FieldType));
        builder.AddAttribute(1, "FieldExpression", fieldExpresson);
        //builder.AddAttribute(2, "Width", 200);
        builder.AddAttribute(3, "Template", new RenderFragment<TableColumnContext<TItem, string>>(context => buttonBuilder =>
        {
            buttonBuilder.OpenComponent<ImgColumn>(0);
            buttonBuilder.AddAttribute(1, nameof(ImgColumn.Title), tableImgField.Title);
            buttonBuilder.AddAttribute(2, nameof(ImgColumn.Name), tableImgField.Name);
            var value = (context.Row).GetIdentityKey(tableImgField.Field);
            buttonBuilder.AddAttribute(3, nameof(ImgColumn.Url), value);
            buttonBuilder.AddAttribute(4, nameof(ImgColumn.BaseUrl), tableImgField.BaseUrl);
            if (!string.IsNullOrEmpty(tableImgField.Style))
            {
                buttonBuilder.AddAttribute(5, nameof(ImgColumn.Style), tableImgField.Style);
            }

            buttonBuilder.CloseComponent();
        }));
        if (!string.IsNullOrEmpty(tableImgField.ColumnText))
        {
            builder.AddAttribute(4, "Text", tableImgField.ColumnText);
        }

        builder.CloseComponent();
    };

    #endregion

    #region 日期选择控件
    public DateTimeRangeValue DateTimeRange { get; set; } = new DateTimeRangeValue();

    /// <summary>
    /// 初始化日期选择控件
    /// </summary>
    private void InitDateTimeRange()
    {
        if (ShowDateTimeRange)
        {
            if (DateTimeRangeDefaultMotherly)
            {
                DateTimeRange.Start = DateTime.Today.FirstDay();
                DateTimeRange.End = DateTime.Today.LastDay();
            }
            else
            {
                DateTimeRange.Start = DateTime.Today.FirstSecond();
                DateTimeRange.End = DateTime.Today.LastSecond();
            }
            StateHasChanged();
        }
    }

    /// <summary>
    /// 日期选择控件确认按钮事件
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    private async Task OnConfirm(DateTimeRangeValue value)
    {
        DateTimeRange.Start = value.Start.FirstSecond();
        DateTimeRange.End = value.End.Year == 1 ? value.Start.LastSecond() : value.End.LastSecond();
        await Reset();
    }

    /// <summary>
    /// 日期选择控件清除按钮事件
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    private async Task OnClear(DateTimeRangeValue value)
    {
        DateTimeRange.Start = DateTime.Today.FirstSecond();
        DateTimeRange.End = DateTime.Today.LastSecond();
        await Reset();
    }
    private Task 新窗口打开()
    {
        if (string.IsNullOrEmpty(新窗口打开Url))
        {
            ToastService?.Error("提示", "Url为空!");
            return Task.CompletedTask;
        }
        JsRuntime.NavigateToNewTab(新窗口打开Url);
        return Task.CompletedTask;
    }
    private Task IsExcelToggle()
    {
        IsExcel = !IsExcel;
        StateHasChanged();
        return Task.CompletedTask;
    }

    #endregion

    #region 继承bbtable的设置
    /// <summary>
    /// 获得/设置 是否斑马线样式 默认为 true
    /// </summary>
    [Parameter]
    public bool IsStriped { get; set; } = true;


    /// <summary>
    /// 获得/设置 是否带边框样式 默认为 true
    /// </summary>
    [Parameter]
    public bool IsBordered { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否自动刷新表格 默认为 false
    /// </summary>
    [Parameter]
    public bool IsAutoRefresh { get; set; }

    /// <summary>
    /// 获得/设置 自动刷新时间间隔 默认 2000 毫秒
    /// </summary>
    [Parameter]
    public int AutoRefreshInterval { get; set; } = 2000;

    /// <summary>
    /// 获得/设置 点击行即选中本行 默认为 false
    /// </summary>
    [Parameter]
    public bool ClickToSelect { get; set; }

    /// <summary>
    /// 获得/设置 单选模式下双击即编辑本行 默认为 true
    /// </summary>
    [Parameter]
    public bool DoubleClickToEdit { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否显示工具栏 默认 true 显示
    /// </summary>
    [Parameter]
    public bool ShowToolbar { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否显示扩展按钮 默认为 true 显示
    /// </summary>
    [Parameter]
    public bool ShowExtendButtons { get; set; } = false;

    /// <summary>
    /// 获得/设置 是否分页 默认为 true
    /// </summary>
    [Parameter]
    public bool IsPagination { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否为多选模式 默认为 false 不显示
    /// </summary>
    [Parameter] public bool IsMultipleSelect { get; set; } = false;

    /// <summary>
    /// 获得/设置 是否显示默认添加/编辑/删除按钮 默认为 false 不显示
    /// </summary>
    [Parameter] public bool ShowDefaultButtons { get; set; } = false;

    /// <summary>
    /// 获得/设置 是否显示搜索框 默认为 true 显示搜索框
    /// </summary>
    [Parameter]
    public bool ShowSearch { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否显示加载骨架屏 默认 false 不显示
    /// </summary>
    [Parameter]
    public bool ShowSkeleton { get; set; }

    /// <summary>
    /// 获得/设置 是否显示每行的明细行展开图标
    /// </summary>
    [Parameter]
    public Func<TItem, bool> ShowDetailRow { get; set; } = _ => false;

    /// <summary>
    /// 获得/设置 组件编辑模式 默认为弹窗编辑行数据 PopupEditForm
    /// </summary>
    [Parameter]
    public EditMode EditMode { get; set; }

    /// <summary>
    /// 获得/设置 数据集合
    /// </summary>
    [Parameter]
    public List<TItem> Items { get; set; } = [];

    /// <summary>
    /// 获取/设置 表格 thead 样式 <see cref="TableHeaderStyle"/>，默认<see cref="TableHeaderStyle.Light"/>
    /// </summary>
    [Parameter]
    public TableHeaderStyle HeaderStyle { get; set; } = TableHeaderStyle.Light;

    /// <summary>
    /// 获得/设置 表格组件大小 默认为 Compact 紧凑模式
    /// </summary>
    [Parameter]
    public TableSize TableSize { get; set; } = TableSize.Compact;

    /// <summary>
    /// 获得/设置 是否允许列宽度调整 默认 false 固定表头时此属性生效
    /// </summary>
    [Parameter]
    public bool AllowResizing { get; set; }

    /// <summary>
    /// 获得/设置 是否显示表脚 默认为 false
    /// </summary>
    [Parameter]
    public bool ShowFooter { get; set; }


    /// <summary>
    /// 获得/设置 是否显示列选择下拉框 默认为 false 不显示
    /// </summary>
    [Parameter]
    public bool ShowColumnList { get; set; }


    /// <summary>
    /// 获得/设置 是否显示刷新按钮 默认为 true
    /// </summary>
    [Parameter]
    public bool ShowRefresh { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否显示行号列 默认为 false
    /// </summary>
    [Parameter]
    public bool ShowLineNo { get; set; }


    /// <summary>
    /// 获得/设置 每页显示数据数量的外部数据源
    /// </summary>
    [Parameter]
    public IEnumerable<int> PageItemsSource { get; set; } = new int[] { 20, 50, 100, 200, 500, 1000 };

    /// <summary>
    /// 获得/设置 组件布局方式 默认为 Auto
    /// </summary>
    [Parameter]
    public TableRenderMode RenderMode { get; set; } = TableRenderMode.Table;

    /// <summary>
    /// 获得/设置 组件是否采用 Tracking 模式对编辑项进行直接更新 默认 false
    /// </summary>
    [Parameter]
    public bool IsTracking { get; set; }

    /// <summary>
    /// 获得/设置 组件工作模式为 Excel 模式 默认 false
    /// </summary>
    [Parameter]
    public bool IsExcel { get; set; }
    /// <summary>
    /// 获得/设置 Table 高度 默认为 null
    /// </summary>
    /// <remarks>开启固定表头功能时生效 <see cref="IsFixedHeader"/></remarks>

    [Parameter]
    public int? Height { get; set; }

    /// <summary>
    /// 获得/设置 固定表头 默认 false
    /// </summary>
    /// <remarks>固定表头时设置 <see cref="Height"/> 即可出现滚动条，未设置时尝试自适应</remarks>
    [Parameter]
    public bool IsFixedHeader { get; set; }

    #endregion
}
