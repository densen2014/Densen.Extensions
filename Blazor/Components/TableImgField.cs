using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmeBlazor.Components
{
    /// <summary>
    /// 表格图片列
    /// </summary>
    public class TableImgField
    {
        public bool RenderImgField { get; set; }
        public Type ImgFieldType { get; set; } = typeof(string);
        public string ImgField { get; set; } = "Photo";
        public string ImgColumnText { get; set; }
        public string ImgFieldTitle { get; set; }
        public string ImgFieldName { get; set; }
        public string ImgBaseUrl {  get; set;}
    }
}
