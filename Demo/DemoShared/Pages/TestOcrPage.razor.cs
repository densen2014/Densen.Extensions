// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using AME;
using AmeBlazor.Components;
using BootstrapBlazor.AzureServices;
using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

namespace DemoShared.Pages;

public partial class TestOcrPage
{
    [Inject, NotNull]
    protected ToastService? ToastService { get; set; }

    [NotNull]
    [Inject]
    private TranslateService? TranslateService { get; set; }

    private List<string>? res { get; set; }
    private string? status { get; set; }

    private string? resfull { get; set; }

    private TableLazyHero<交通银行>? list1 { get; set; }
    private List<交通银行>? mines;

    private async Task 翻译()
    {
        if (mines != null)
        {
            foreach (var item in mines)
            {
                item.E收付 = await OnTranslate(item.E收付);
                item.D交易类型 = await OnTranslate(item.D交易类型);
                item.J交易地点 = await OnTranslate(item.J交易地点);
                item.I对方户名 = await OnTranslate(item.I对方户名);
                item.K摘要 = await OnTranslate(item.K摘要);
            }
            await list1!.Load(mines);
        }
    }

    private string route = "/translate?api-version=3.0&to=es";
    private async Task<string?> OnTranslate(string? val)
    {
        if (string.IsNullOrWhiteSpace(val))
        {
            return val;
        }

        status = "翻译中...";
        var res = await TranslateService.Translate(val, route);

        return res?.First()?.text ?? val;
    }

    private async Task 处理()
    {
        if (string.IsNullOrWhiteSpace(resfull))
        {
            return;
        }

        mines = new List<交通银行>();
        var flag = false;
        var line = resfull.Replace("\r\n", "\n").Split(new char[] { '\t', '\n' });
        for (var i = 0; i < line.Length; i++)
        {
            try
            {
                if (line[i] == "交易地点")
                {
                    flag = true;
                }
                else if (flag && !string.IsNullOrWhiteSpace(line[i]))
                {
                    var itemTemp = new List<string>();

                    for (var j = i; j < line.Length; j++)
                    {
                        if (Loop提取(ref itemTemp, line[j]))
                        {
                            var item = new 交通银行
                            {
                                A序号 = itemTemp[0]
                            };
                            if (itemTemp.Count > 1) item.B交易日期 = itemTemp[1];
                            if (item.B交易日期 != null)
                            {
                                try
                                {
                                    item.B交易日期ES = DateTime.ParseExact(item.B交易日期, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                                }
                                catch
                                {
                                }
                            }
                            if (itemTemp.Count > 2) item.C交易时间 = itemTemp[2];
                            if (itemTemp.Count > 3) item.D交易类型 = itemTemp[3];
                            if (itemTemp.Count > 4) item.E收付 = itemTemp[4];
                            if (itemTemp.Count > 5) item.F交易金额 = itemTemp[5];
                            if (itemTemp.Count > 6) item.G余额 = itemTemp[6];
                            if (itemTemp.Count > 7) item.H对方账户 = itemTemp[7];
                            if (itemTemp.Count > 8) item.I对方户名 = itemTemp[8];
                            if (itemTemp.Count > 9) item.J交易地点 = itemTemp[9];
                            item.K摘要 = itemTemp.LastOrDefault();
                            mines.Add(item);
                            itemTemp = new List<string>();
                        }
                    }

                    if (mines != null) await list1!.Load(mines);

                    break;

                    //if (t0.Length == 2)
                    //{
                    //    item.A序号 = t0[0];
                    //    item.B交易日期 = t0[1];
                    //    i++;
                    //}
                    //else
                    //{
                    //    item.A序号 = line[i];
                    //    i++;
                    //    item.B交易日期 = line[i];
                    //    i++;
                    //}

                    //var t1 = line[i].Split(' ');
                    //if (line[i].Split(' ').Length == 4)
                    //{
                    //    item.C交易时间 = t1[0];
                    //    item.D交易类型 = t1[1];
                    //    item.E收付 = t1[2];
                    //    item.F交易金额 = t1[3];
                    //    i++;
                    //}
                    //else if (line[i].Split(' ').Length == 3)
                    //{
                    //    item.C交易时间 = t1[0];
                    //    item.D交易类型 = t1[1];
                    //    item.E收付 = t1[2];
                    //    i++;
                    //    item.F交易金额 = line[i];
                    //    i++;
                    //}
                    //else if (line[i].Split(' ').Length == 2)
                    //{
                    //    item.C交易时间 = t1[0];
                    //    item.D交易类型 = t1[1];
                    //    i++;
                    //    var t2 = line[i].Split(' ');
                    //    if (line[i].Split(' ').Length == 2)
                    //    {
                    //        item.E收付 = t2[0];
                    //        item.F交易金额 = t2[1];
                    //        i++;
                    //    }
                    //    else
                    //    {
                    //        item.E收付 = line[i];
                    //        i++;
                    //        item.F交易金额 = line[i];
                    //        i++;
                    //    }
                    //}
                    //else
                    //{
                    //    item.C交易时间 = line[i];
                    //    i++;
                    //    item.D交易类型 = line[i];
                    //    i++;
                    //    item.E收付 = line[i];
                    //    i++;
                    //    item.F交易金额 = line[i];
                    //    i++;
                    //}
                    //item.G余额 = line[i];
                    //i++;
                    //item.H对方账户 = line[i];
                    //i++;
                    //item.I对方户名 = line[i];
                    //i++;
                    //if (line[i].StartsWith("Abstra"))
                    //{
                    //    item.K摘要 = line[i];
                    //}
                    //else
                    //{
                    //    item.J交易地点 = line[i];
                    //    i++;
                    //    item.K摘要 = line[i];
                    //}
                    //if (item.B交易日期 != null)
                    //{
                    //    try
                    //    {
                    //        item.B交易日期ES = DateTime.ParseExact(item.B交易日期, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                    //    }
                    //    catch  
                    //    { 
                    //    }
                    //}
                    //mines.Add(item);
                }
            }
            catch (Exception e)
            {
                await ToastService.Error(e.Message);
            }
        }
        //if (mines != null) await list1!.Load(mines);
    }

    private static bool Loop提取(ref List<string> itemTemp, string item)
    {
        if (item.Contains(' ') && !item.StartsWith("Abstra") && itemTemp.Count != 7)
        {
            //非备注继续切分
            var tt0 = item.Split(' ');
            for (int k = 0; k < tt0.Length; k++)
            {
                Loop提取(ref itemTemp, tt0[k]);
            }
        }
        else
        {
            if ((itemTemp.Count == 0 && item.IsNum()) || itemTemp.Count > 0)
            {
                itemTemp.Add(item);
            }
            if (item.StartsWith("Abstra"))
                return true;
        }
        return false;
    }

    private async Task OnResult(List<string> res)
    {
        this.res = res;
        resfull = string.Join("\n", res);
        await 处理();
        StateHasChanged();
    }
    private async Task OnStatus(string message)
    {
        status = message;
        StateHasChanged();
    }

    [AutoGenerateClass(Searchable = true, Filterable = true, Sortable = true)]
    public class 交通银行
    {
        public string? A序号 { get; set; }

        [AutoGenerateColumn(FormatString = "dd/MM/yyyy", Visible = false)]
        public string? B交易日期 { get; set; }

        [AutoGenerateColumn(FormatString = "dd/MM/yyyy")]
        public DateTime? B交易日期ES { get; set; }

        [AutoGenerateColumn(FormatString = "HH:mm:ss")]
        public string? C交易时间 { get; set; }

        public string? D交易类型 { get; set; }

        public string? E收付 { get; set; }

        [AutoGenerateColumn(FormatString = "n2")]
        public string? F交易金额 { get; set; }

        [AutoGenerateColumn(FormatString = "n2")]
        public string? G余额 { get; set; }

        public string? H对方账户 { get; set; }

        public string? I对方户名 { get; set; }

        public string? J交易地点 { get; set; }

        [AutoGenerateColumn(TextEllipsis = true, ShowTips = true)]
        public string? K摘要 { get; set; }

    }

}
