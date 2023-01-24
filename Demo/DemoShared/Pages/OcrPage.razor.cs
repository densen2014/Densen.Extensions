namespace DemoShared.Pages
{
    public partial class OcrPage
    {
        List<string>? res { get; set; }
        private Task OnResult(List<string> res)
        {
            this.res = res;
            StateHasChanged();
            return Task.CompletedTask;
        }
    }
}
