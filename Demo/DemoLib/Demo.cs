// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using FreeSql.DataAnnotations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using BootstrapBlazor.Components;

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
    public class NowInfo
    {
        [DisplayName("时间")]
        [AutoGenerateColumn(FormatString = "MM-dd HH:mm:ss")]
        public string obsTime { get; set; }
        public string temp { get; set; }
        public string feelsLike { get; set; }
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

        [DisplayName("图")]
        public string Photo { get; set; } = "https://freepos.es/uploads/demo/fage.jpg";
        public int ID { get; set; }
    }

    public class Refer
    {
        public string[] sources { get; set; }
        public string[] license { get; set; }
    }
     
     
}
