using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;

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

        public EventCallback<object> Callback { get; set; }

        /// <summary>
        /// 获得/设置 识别完成回调方法,返回 Model 集合
        /// </summary>
        public Func<object, Task> CallbackFunc { get; set; }
    }
     
}
