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
        public bool Render { get; set; }
        public Type FieldType { get; set; } = typeof(string);
        public string Field { get; set; } = "Photo";
        public string ColumnText { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string BaseUrl {  get; set;}
        public string Style { get; set; }
    }
}
