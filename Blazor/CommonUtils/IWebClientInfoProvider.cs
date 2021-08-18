using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AME.CommonUtils
{
    public interface IWebClientInfoProvider
    {
        string BrowserInfo { get; }
        string ClientIpAddress { get; }
        string ComputerName { get; }
    }

}
