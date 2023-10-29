// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

namespace DemoShared.Pages
{
    public partial class OcrPage
    {
        private List<string>? res { get; set; }
        private Task OnResult(List<string> res)
        {
            this.res = res;
            StateHasChanged();
            return Task.CompletedTask;
        }
    }
}
