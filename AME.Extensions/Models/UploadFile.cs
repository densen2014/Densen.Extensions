// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

namespace AME.Models;

/// <summary>
/// 上传组件返回类
/// </summary>
public class UploadFile
{

    /// <summary>
    /// 文件名
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    /// 原始文件名
    /// </summary>
    public string OriginFileName { get; }

    /// <summary>
    /// 文件大小
    /// </summary>
    public long Size { get; set; }

    /// <summary>
    /// 文件上传结果 0 表示成功 非零表示失败
    /// </summary>
    public int Code { get; set; }

    /// <summary>
    /// 文件预览地址
    /// </summary>
    public string PrevUrl { get; set; }

    /// <summary>
    /// 错误信息
    /// </summary>
    public string Error { get; set; }
}
