// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace AME.CommonUtils
{

    public class WebClientInfoProvider : IWebClientInfoProvider
    {
        public string BrowserInfo => GetBrowserInfo();
        public string ClientIpAddress => GetClientIpAddress();
        public string ComputerName => GetComputerName();

        public ILogger Logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpContext _httpContext;
        /// <summary>
        /// Creates a new  
        /// </summary>
        public WebClientInfoProvider(IHttpContextAccessor httpContextAccessor)//, ILogger logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _httpContext = httpContextAccessor.HttpContext;
            //Logger = logger;
        }
        protected virtual string GetBrowserInfo()
        {
            var httpContext = _httpContextAccessor.HttpContext ?? _httpContext;
            return httpContext?.Request?.Headers?["User-Agent"];
        }
        protected virtual string GetClientIpAddress()
        {
            try
            {
                var httpContext = _httpContextAccessor.HttpContext ?? _httpContext;

                var headers = httpContext?.Request.Headers;
                if (headers != null && headers.ContainsKey("X-Forwarded-For"))
                {
                    httpContext.Connection.RemoteIpAddress = IPAddress.Parse(headers["X-Forwarded-For"].ToString().Split(',', StringSplitOptions.RemoveEmptyEntries)[0]);
                }
                return httpContext?.Connection?.RemoteIpAddress?.ToString();
            }
            catch (Exception)
            {
                //Logger.LogWarning(ex.ToString());
            }
            return null;
        }
        protected virtual string GetComputerName()
        {
            return null; //TODO: Implement!
        }
    }
}
