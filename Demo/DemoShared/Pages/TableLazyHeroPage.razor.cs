// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using AME.CommonUtils;
using AmeApi;
using AmeBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace DemoShared.Pages;

public partial class TableLazyHeroPage
{
    [Inject][NotNull] protected HttpClient? Http { get; set; }
    [Inject][NotNull] protected ILogger<Index>? Logger { get; set; }
    [Inject][NotNull] protected WebClientInfoProvider? WebClientInfo { get; set; }

    private Rootobject? mineStatus;
    private List<Rootobject>? mineStatuss;
    private List<NowInfo>? mines; 
    TableLazyHero<NowInfo>? list1 { get; set; }

    private CancellationTokenSource? AutoRefreshCancelTokenSource { get; set; }
 

    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender) worker();
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
                    await 刷新();
                    await InvokeAsync(() => { StateHasChanged(); list1?.Load(mines); });//两个语句前后有次序关系,StateHasChanged()在后会导致等待延时
                    await Task.Delay(TimeSpan.FromSeconds(5), AutoRefreshCancelTokenSource?.Token ?? new CancellationToken(true));
                }
            }
            catch
            {
            }
        });

    }

    private async Task 刷新() {
        Logger.LogInformation($"{DateTime.Now.ToString("HH:mm:ss")} 读API");
        var res = await Http!.GetStringAsync("https://git.app1.es/Densen.Extensions.BootstrapBlazor.json");
        Logger.LogInformation(res.Length.ToString());
        mineStatus = JsonConvert.DeserializeObject<Rootobject>(res);
        mineStatuss = new List<Rootobject> { mineStatus! };
        mines = mineStatus!.now ;
        for (int i = 0; i < mines.Count-1; i++)
        {
            mines[i].Photo = $"https://freepos.es/uploads/demo/Product/{i}.jpg";
        }
    }
    private async Task 刷新数据()
    {
        await 刷新();
        await list1!.Load(mines);
    }


}


