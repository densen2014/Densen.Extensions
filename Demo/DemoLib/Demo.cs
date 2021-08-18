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
        public List<NowInfo> now { get; set; }
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
        public string wind360 { get; set; }
        public string windDir { get; set; }
        public string windScale { get; set; }
        public string windSpeed { get; set; }
        public string humidity { get; set; }
        public string precip { get; set; }
        public string pressure { get; set; }
        public string vis { get; set; }
        public string cloud { get; set; }
        public string dew { get; set; }
    }

    public class Refer
    {
        public string[] sources { get; set; }
        public string[] license { get; set; }
    }
     
     
}
