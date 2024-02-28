// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************


using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

namespace AmeBlazor.Components;

/// <summary>
/// AME组件基类
/// </summary>
public abstract partial class AmeBlazorComponentBase : ComponentBase, IDisposable
{

    [Parameter]
    public Dictionary<string, BootstrapDynamicComponent>? TabPages { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
        {
            return;
        }

        await base.OnAfterRenderAsync(firstRender);

        //if (TabPages != null)
        //{
        //    TabPages.ToList().ForEach(item => TabSetMenu.AddTab(new Dictionary<string, object>
        //    {
        //        [nameof(TabItem.Text)] = item.Key,
        //        [nameof(TabItem.ChildContent)] = item.Value.Render()
        //    }));
        //    TabSetMenu.ActiveTab(TabSetMenu.Items.FirstOrDefault());
        //}
    }

    #region 懒加载Tab控件
    /// <summary>
    /// 懒加载Tab控件 <para></para>
    /// 使用方法: <para></para>
    /// &lt;Tab @ref="TabSetMenu" OnClickTab="@OnClickTabItem" /&gt; 
    /// </summary>
    [NotNull]
    protected Tab? TabSetMenu { get; set; }

    #region Tab控件

    //新功能 IsLazyLoadTabItem 已经启用, 这个方法就过时了. 

    //        /// <summary>
    //        /// Tab控件点击懒加载TabItem
    //        /// </summary>
    //        /// <param name="item"></param>
    //        /// <returns></returns>
    //        protected Task OnClickTabItem(TabItem item)
    //        {
    //            if (item.ChildContent == null)
    //            {
    //#pragma warning disable BL0005
    //                item.ChildContent = (Pages.Where(a => a.Key == item.Text).FirstOrDefault().Value).Render();
    //#pragma warning restore BL0005
    //            }
    //            return Task.CompletedTask;
    //        }

    /// <summary>
    /// Tab控件页面Pages字典
    /// <para></para>
    /// 1. OnAfterRenderAsync 里加入
    /// <para></para>
    ///  Pages = new Dictionary &lt;string, BootstrapDynamicComponent&gt; 
    /// </summary>
    protected Dictionary<string, BootstrapDynamicComponent> Pages =
        new Dictionary<string, BootstrapDynamicComponent>
        {
            { "类别",BootstrapDynamicComponent.CreateComponent<BootstrapBlazor.Components.Button>() },
        };

    /// <summary>
    /// Tab控件初始化TabSet
    /// <para></para>
    /// 2. OnAfterRenderAsync 里加入 InitTabSet(); 
    /// </summary>
    /// <param name="AllPages">显示所有页面,默认 true</param>
    /// <param name="PagesTake">非显示所有页面情况下,定义显示几个页面</param>
    protected void InitTabSet(bool AllPages = true, int PagesTake = 11)
    {
        //var flag = false;
        foreach (var item in AllPages ? Pages : Pages.Take(PagesTake))
        {
            TabSetMenu.AddTab(new Dictionary<string, object?>
            {
                [nameof(TabItem.Text)] = item.Key,
                //[nameof(TabItem.ChildContent)] = flag ? null : item.Value.Render()
                [nameof(TabItem.ChildContent)] = item.Value.Render()
            });
            //flag = true;
        }

        TabSetMenu.ActiveTab(TabSetMenu.Items.First());
    }

    #endregion


    #region Menu控件

    /// <summary>
    /// Menu控件MenuItem集合,点击后动态生成TabItem <para></para>
    /// 使用方法: <para></para>
    /// &lt;Menu Items="@GetSideMenuItems()" IsVertical="true" OnClick="@OnClickMenuItem" /&gt; 
    /// </summary>
    /// <returns></returns>
    protected IEnumerable<MenuItem> GetSideMenuItems()
    {
        var menu = new List<MenuItem>();
        foreach (var item in Pages)
        {
            menu.Add(new MenuItem() { Text = item.Key });
        }
        return menu;
    }

    /// <summary>
    /// 点击Menu
    /// </summary>
    /// <param name="item">点击的MenuItem</param>
    /// <returns></returns>
    protected Task OnClickMenuItem(MenuItem item)
    {
        if (TabSetMenu != null)
        {
            var text = item.Text;
            var tabItem = TabSetMenu.Items.FirstOrDefault(i => i.Text == text);
            if (tabItem == null)
            {
                AddTabItem(text ?? "");
            }
            else
            {
                TabSetMenu.ActiveTab(tabItem);
            }
        }
        return Task.CompletedTask;
    }

    /// <summary>
    /// 点击Menu动态生成TabItem
    /// </summary>
    /// <param name="text"></param>
    protected void AddTabItem(string text)
    {
        var component = (Pages.Where(a => a.Key == text).FirstOrDefault().Value);
        var item = new TabItem();
        var parameters = new Dictionary<string, object?>
        {
            [nameof(TabItem.Text)] = text,
            [nameof(TabItem.IsActive)] = true,
            [nameof(TabItem.ChildContent)] = component.Render()
            //[nameof(TabItem.ChildContent)] = new RenderFragment(builder =>
            //{
            //    var index = 0;
            //    builder.OpenElement(index++, "div");
            //    builder.AddContent(index++, $"我是新建的 Tab 名称是 {text}");
            //    builder.CloseElement();
            //})

        };

        TabSetMenu.AddTab(parameters);
    }

    #endregion

    #endregion


}
