// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************


using AME.Services;
using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Toolbelt.Blazor.I18nText;

namespace AmeBlazor.Components
{
    /// <summary>
    /// AME组件基类
    /// </summary>
    public abstract class AmeBlazorComponentBase : ComponentBase, IDisposable
    {
        [Inject] protected NavigationManager NavigationManager { get; set; }
        [Inject] protected I18nText I18nText { get; set; }
        [Inject] protected IConfiguration config { get; set; }
        [Inject] protected BrowserService browserService { get; set; }
        [Inject] protected ToastService ToastService { get; set; }
        [Inject] protected SwalService SwalService { get; set; }
        
        protected bool enableHangFire;

        /// <summary>
        /// 获得/设置 用户自定义属性
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object> AdditionalAttributes { get; set; }

        /// <summary>
        /// 获得/设置 IJSRuntime 实例
        /// </summary>
        [Inject]
        [NotNull]
        protected IJSRuntime JsRuntime { get; set; }

        /// <summary>
        /// Dispose 方法
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {

        }

        /// <summary>
        /// Dispose 方法
        /// </summary>
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        protected override void OnInitialized()
        {
            base.OnInitialized();
            enableHangFire = config["EnableHangFireLinux"].Equals("True") || (config["EnableHangFire"].Equals("True") && RuntimeInformation.IsOSPlatform(OSPlatform.Windows));
        }

        //导航并添加 TabItem 方法
        protected Task OnNavigation()
        {
            //NavigationManager.NavigateTo("demo", "Demo页面");
            NavigationManager.NavigateTo("demo");
            return Task.CompletedTask;
        }
        protected void GotoNewTab(string selectItem)
        {
            JsRuntime.NavigateToNewTab(selectItem);
        }
        protected void Goto(string selectItem)
        {
            NavigationManager.NavigateTo(selectItem);
        }
        protected void Goto(string selectItem, bool forceLoad = false)
        {
            NavigationManager.NavigateTo(selectItem, forceLoad);
        }

        protected string TargetDir = "";

        #region 懒加载Tab控件
        /// <summary>
        /// 懒加载Tab控件 <para></para>
        /// 使用方法: <para></para>
        /// &lt;Tab @ref="TabSetMenu" OnClickTab="@OnClickTabItem" /&gt; 
        /// </summary>
        protected Tab TabSetMenu { get; set; }

        #region Tab控件
        /// <summary>
        /// Tab控件点击懒加载TabItem
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected Task OnClickTabItem(TabItem item)
        {
            if (item.ChildContent == null)
            {
                item.ChildContent = (Pages.Where(a => a.Key == item.Text).FirstOrDefault().Value).Render();
            }
            return Task.CompletedTask;
        }

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
            var flag = false;
            foreach (var item in AllPages ? Pages : Pages.Take(PagesTake))
            {
                TabSetMenu.AddTab(new Dictionary<string, object>
                {
                    [nameof(TabItem.Text)] = item.Key,
                    [nameof(TabItem.ChildContent)] = flag ? null : item.Value.Render()
                });
                flag = true;
            }

            TabSetMenu.ActiveTab(TabSetMenu.Items.FirstOrDefault());
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
                if (tabItem == null) AddTabItem(text ?? "");
                else TabSetMenu.ActiveTab(tabItem);
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
            var parameters = new Dictionary<string, object>
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

            if (TabSetMenu != null) TabSetMenu.AddTab(parameters);
        }

        #endregion

        #endregion

    }
}
