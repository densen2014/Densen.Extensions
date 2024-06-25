// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

namespace AME.Models;


public class CheckNewResponse
{
    /// <summary>
    /// 文件名,带相对路径
    /// </summary>
    public string Filename { get; set; }

    /// <summary>
    /// 文件大小
    /// </summary>
    public string Size { get; set; }

    /// <summary>
    /// 建立时间
    /// </summary>
    public string CreationTime { get; set; }

    /// <summary>
    /// 刷新时间
    /// </summary>
    public string LasFetch { get; set; }
}
