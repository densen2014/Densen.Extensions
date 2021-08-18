using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace AME.Services
{

    public class BrowserService
    {
        private readonly IJSRuntime _js;

        public BrowserService(IJSRuntime js)
        {
            _js = js;
        }

        public async Task<BrowserDimension> GetDimensions()
        {
            return await _js.InvokeAsync<BrowserDimension>("toolsFunctions.getDimensions");
        }

        public async void Log(object msg)
        {
            try
            {
                await _js.InvokeAsync<object>("console.log", msg);
            }
            catch
            {
            }
        }

        public async Task SetTitle(string pageName)
        {
            try
            {
                await _js.InvokeAsync<string>("JsFunctions.setDocumentTitle", pageName);
            }
            catch
            {
            }
        }


        //滚动加载
        public bool IsLoading { get; set; } = false;
        public bool StopLoading = false;

        public async Task InitScrollListListenerAsync()
        {
            try
            {
                await _js.InvokeVoidAsync("ScrollList.Init", "list-end", DotNetObjectReference.Create(this));
            }
            catch
            {
            }
        }
        public async Task StopScrollListListenerAsync()
        {
            try
            {
                await _js.InvokeVoidAsync("ScrollList.RemoveListener");
            }
            catch
            {
            }
        }
        public void RemoveScrollListListenerAsync()
        {
            try
            {

                _js.InvokeVoidAsync("ScrollList.RemoveListener");
            }
            catch
            {
            }
        }

        public async Task StopListener()
        {
            StopLoading = true;
            IsLoading = false;
            await StopScrollListListenerAsync();
        }
        [JSInvokable]
        public void LoadMore()
        {
            OnBoilerEventLog(false);
        }
        public delegate void SyncLogHandler(bool firstRender);
        public event SyncLogHandler SyncEventLog;
        protected void OnBoilerEventLog(bool firstRender)
        {
            SyncEventLog?.Invoke(firstRender);
        }

        public async void ScrollTop()
        {
            try
            {
                await _js.InvokeAsync<object>("scroll", new object [] { 0,0});
            }
            catch
            {
            }
        }

        public async void ScrollElementTop()
        {
            try
            {
                await _js.InvokeVoidAsync("toolsFunctions.scrollelementtop");
            }
            catch
            {
            }
        }
    }

    public class BrowserDimension
    {
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
