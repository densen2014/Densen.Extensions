// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

namespace AME.CommonUtils;

public interface IWebClientInfoProvider
{
    string BrowserInfo { get; }
    string ClientIpAddress { get; }
    string ComputerName { get; }
}
