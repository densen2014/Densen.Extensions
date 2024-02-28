// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

namespace AmeBlazor.Components;

public enum ConfigItemType
{
    Button,
    Checkbox,
    Inputbox
}


public class BigConfigItem
{
    /// <summary>
    ///  组件类型
    /// </summary>
    public ConfigItemType ItemType { get; set; } = ConfigItemType.Button;

    /// <summary>
    ///  组件 class
    /// </summary>
    public string? Class { get; set; }

    /// <summary>
    /// 组件 oi 图标,InputBox,CHeckBox无效
    /// </summary>
    public string? ClassOi { get; set; }

    /// <summary>
    /// 组件Style, Button无效
    /// </summary>
    public string? Style { get; set; }

    /// <summary>
    /// 组件显示文本
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    /// 绑定字段名称, Button无效
    /// </summary>
    public string? FiledName { get; set; }

    /// <summary>
    /// Button跳转Url,InputBox,CHeckBox无效
    /// </summary>
    public string? Url { get; set; }

    /// <summary>
    /// 按钮高度<para></para>默认120px
    /// <para></para>InputBox,CheckBox无效
    /// </summary>
    public int Height { get; set; } = 120;

    /// <summary>
    /// 按钮高度<para></para>默认200px
    /// <para></para>InputBox,CheckBox无效
    /// </summary>
    public int MaxWidth { get; set; } = 200;

    /// <summary>
    /// 授权列表
    /// </summary>
    public string[]? Roles { get; set; }
}
