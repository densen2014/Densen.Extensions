// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using Microsoft.AspNetCore.Components.Routing;

namespace AmeBlazor.Components;

public class AmeMenuItem
{
    public string? Link { get; set; }
    public string? Text { get; set; }
    public NavLinkMatch Match { get; set; }
    public string? AuthorizeRoles { get; set; }
    public List<AmeMenuItem>? SubItems { get; set; }

}
