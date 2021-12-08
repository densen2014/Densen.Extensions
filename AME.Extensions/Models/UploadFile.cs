// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AME.Models
{
    //     上传组件返回类
    public class UploadFile
    {

        //
        // 摘要:
        //     获得/设置 文件名
        public string FileName { get; set; }
        //
        // 摘要:
        //     获得/设置 原始文件名
        public string OriginFileName { get; }
        //
        // 摘要:
        //     获得/设置 文件大小
        public long Size { get; set; }
        //
        // 摘要:
        //     获得/设置 文件上传结果 0 表示成功 非零表示失败
        public int Code { get; set; }
        //
        // 摘要:
        //     获得/设置 文件预览地址
        public string PrevUrl { get; set; }
        //
        // 摘要:
        //     获得/设置 错误信息
        public string Error { get; set; }
    }

}
