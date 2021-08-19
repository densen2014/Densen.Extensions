// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AmeBlazor.Components
{
    public partial class TimerAme : ComponentBase, IDisposable
    {
        private CancellationTokenSource AutoRefreshCancelTokenSource { get; set; }

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
                        await InvokeAsync(StateHasChanged);
                        await Task.Delay(TimeSpan.FromSeconds(1), AutoRefreshCancelTokenSource?.Token ?? new CancellationToken(true));
                    }
                }
                catch (TaskCanceledException) { }
            });

        }
    }
}
