// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************


using BootstrapBlazor.AzureServices;
using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using static BootstrapBlazor.AzureServices.Enums;

namespace DemoShared.Pages;

/// <summary>
/// PlayAudio 播放语音/文本转语音
/// </summary>
public partial class PlayAudioPage
{
    [NotNull]
    PlayAudio? PlayAudio { get; set; }

    [Inject]
    [NotNull]
    private StsService? StsService { get; set; }

    [NotNull]
    private IEnumerable<SelectedItem> ItemsVoiceName { get; set; } = typeof(AzureVoiceName).EnumsToSelectedItem();

    [DisplayName("语音名称")]
    private AzureVoiceName SelectedVoiceName { get; set; } = AzureVoiceName.zh_HK_HiuGaaiNeural;

    [NotNull]
    private IEnumerable<SelectedItem> ItemsVoiceStyle { get; set; } = typeof(AzureVoiceStyle).EnumsToSelectedItem();

    [DisplayName("风格")]
    private AzureVoiceStyle SelectedVoiceStyle { get; set; } = AzureVoiceStyle.calm;

    [NotNull]
    private IEnumerable<SelectedItem> ItemsSSML { get; set; } = Demos.ListToSelectedItem();

    [DisplayName("SSML内容")]
    private string ItemSSML { get; set; } = Demos.DemoSsml.First();

    [NotNull]
    [Inject]
    private IConfiguration? Config { get; set; }

    string? ErrorMessage { get; set; }

    [DisplayName("问点啥")]
    private string? InputText { get; set; } = DateTime.Now.ToString("F");

    private string? PlaceHolderText { get; set; } = "文本转换语音,回车执行.";


    private async Task OnEnterAsync(string val)
    {

        if (string.IsNullOrWhiteSpace(val))
        {
            return;
        }

        await PlayAudio.Play(val);
    }


 

    private Task OnEscAsync(string val)
    {
        InputText = string.Empty;
        return Task.CompletedTask;
    }

    private async Task OnPlay()
    {
        if (string.IsNullOrWhiteSpace(InputText))
        {
            return;
        }

        var SelectedVoice = SelectedVoiceName.GetEnumName();
        var SelectedStyle = AzureVoiceStyle.calm.ToString();
        await PlayAudio.Play(InputText, SelectedVoice, SelectedStyle);
    }

    private async Task OnPlaySSML()
    {
        if (string.IsNullOrWhiteSpace(ItemSSML))
        {
            return;
        }

        await PlayAudio.Play(ItemSSML);
    }



}

/// <summary>
/// Enum 扩展方法
/// </summary>
static class EnumExtensions
{

    /// <summary>
    /// 获取枚举的值和描述
    /// </summary>
    /// <param name="en"></param>
    /// <returns></returns>
    public static string GetEnumName(this Enum en) => en.GetDescription();

    /// <summary>
    /// 获取枚举的值和描述
    /// </summary>
    /// <param name="en"></param>
    /// <returns></returns>

    public static string GetDescription(this Enum en)
    {
        Type temType = en.GetType();
        MemberInfo[] memberInfos = temType.GetMember(en.ToString());
        if (memberInfos != null && memberInfos.Length > 0)
        {
            object[] objs = memberInfos[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (objs != null && objs.Length > 0)
            {
                return ((DescriptionAttribute)objs[0]).Description;
            }
        }
        return en.ToString();
    }

    public static string[] GetNamesFromEnums(this Type enumType)
    {
        string[] t1 = Enum.GetNames(enumType);
        return t1;
    }

    public static IEnumerable<SelectedItem> EnumsToSelectedItem(this Type enumType)
    {
        foreach (var item in Enum.GetNames(enumType))
        {
            yield return new SelectedItem(item, item.Replace("_", "-"));
        }
    }

}
