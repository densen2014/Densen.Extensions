// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DemoShared.Pages;
public partial class Index : ComponentBase, IDisposable
{
    private CancellationTokenSource AutoRefreshCancelTokenSource { get; set; }
    bool flag;
    protected override void OnInitialized()
    {
        base.OnInitialized();
        worker();
    }

    /// <summary>
    /// Dispose 方法
    /// </summary>
    public void Dispose()
    {
        AutoRefreshCancelTokenSource = null;
    }

    public void worker()
    {
        AutoRefreshCancelTokenSource = new CancellationTokenSource();
        _ = Task.Run(async () =>
        {
            try
            {
                while (!(AutoRefreshCancelTokenSource?.IsCancellationRequested ?? true))
                {
                    flag = !flag;
                    await InvokeAsync(StateHasChanged);
                    await Task.Delay(TimeSpan.FromSeconds(5), AutoRefreshCancelTokenSource?.Token ?? new CancellationToken(true));
                }
            }
            catch (TaskCanceledException) { }
        });

    }
}
