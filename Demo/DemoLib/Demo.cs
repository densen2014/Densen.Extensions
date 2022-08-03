﻿// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using BootstrapBlazor.Components;
using FreeSql.DataAnnotations;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AmeApi
{

    public class Rootobject
    {
        public string code { get; set; }
        public string updateTime { get; set; }
        public string fxLink { get; set; }

        [AutoGenerateColumn(Visible = false)]
        public List<NowInfo> now { get; set; }
        
        [AutoGenerateColumn(Visible = false)]
        public Refer refer { get; set; }
    }
    [AutoGenerateClass(Searchable = true, Filterable = true, Sortable = true)]
    public class NowInfo: NowInfo0
    {
        [DisplayName("时间")]
        [AutoGenerateColumn(FormatString = "MM-dd HH:mm:ss")]
        public string obsTime { get; set; }
        public string temp { get; set; }
        public string feelsLike { get; set; }
        
    }
    
    [AutoGenerateClass(Searchable = true, Filterable = true, Sortable = true)]
    public class NowInfo0
    {
        public string icon { get; set; }
        public string text { get; set; }

        [AutoGenerateColumn(Visible =false)]
        public string wind360 { get; set; }
        
        [AutoGenerateColumn(Visible = false)]
        public string windDir { get; set; }
        
        [AutoGenerateColumn(Visible = false)]
        public string windScale { get; set; }
        
        [AutoGenerateColumn(Visible = false)]
        public string windSpeed { get; set; }
        
        [AutoGenerateColumn(Visible = false)]
        public string humidity { get; set; }
        
        [AutoGenerateColumn(Visible = false)]
        public string precip { get; set; }
        
        [AutoGenerateColumn(Visible = false)]
        public string pressure { get; set; }
        
        [AutoGenerateColumn(Visible = false)]
        public string vis { get; set; }
        public string cloud { get; set; }
        [AutoGenerateColumn(Visible = false)]
        public string dew { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
#if NET5_0
        [AutoGenerateColumn(TextEllipsis = true, ShowTips = true)]
#endif
        [Required(ErrorMessage = "商品名称不能为空")]
        [JsonProperty, Column(Position = 3)]
        [DisplayName("名称")]
        public string ProductName { get; set; } = "商品名称是指为了区别于其他商品而使用的商品的称呼，可分为通用名称和特定名称。 命名的方式可以从商品功能、商品形象、商品产地、商品的象征意义等方面着手，一般以文字形式表示";

        [DisplayName("图")]
        public string Photo { get; set; } = "https://freepos.es/uploads/demo/fage.jpg";

        [DisplayName("头像")]
        public string Photo2 { get; set; } = "https://freepos.es/uploads/demo/fage.jpg";
        public int ID { get; set; }
    }

    public class Refer
    {
        public string[] sources { get; set; }
        public string[] license { get; set; }
    }
     
     
}
