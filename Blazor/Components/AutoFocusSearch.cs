// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 自动获得焦点的搜索框
    /// </summary>
    public class AutoFocusSearch : Search, IDisposable
    {
        /// <summary>
        /// 获得/设置 自动获得焦点时间间隔 默认 5000 毫秒
        /// </summary>
        [Parameter]
        public int FocusInterval { get; set; } = 5000;

#nullable enable
        private CancellationTokenSource? CancellationToken { get; set; }
#nullable disable

        /// <summary>
        /// OnAfterRender
        /// </summary>
        /// <param name="firstRender"></param>
        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                CancellationToken ??= new CancellationTokenSource();
                Task.Run(async () =>
                {
                    while (!CancellationToken.IsCancellationRequested)
                    {
                        await base.OnAfterRenderAsync(true);
                        await Task.Delay(FocusInterval, CancellationToken.Token);
                    }
                });
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                CancellationToken?.Cancel();
                CancellationToken?.Dispose();
                CancellationToken = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
