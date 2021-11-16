// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AmeBlazor.Components
{
    public partial class ImgColumn : ComponentBase 
    {
        [Parameter] public string Url { get; set; }
        [Parameter] public string BaseUrl { get; set; } = "";
        [Parameter] public string Name { get; set; } = "Name";
        [Parameter] public string Title { get; set; } = "Title";
        [Parameter] public string Style { get; set; } = "max-height:50px;max-width:50px;";

        protected override void OnInitialized()
        {
            base.OnInitialized();
        } 
         
    }
}
