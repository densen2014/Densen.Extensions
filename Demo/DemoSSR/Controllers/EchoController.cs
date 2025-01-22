// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using BootstrapBlazor.AzureServices;
using BootstrapBlazor.Ocr.Services;
using BootstrapBlazor.OpenAI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AME.Controllers;
#nullable enable 
 
[ApiController]
[Route("[controller]")]
public class EchoController : ControllerBase
{
    /// <summary>
    /// Echo接口
    /// </summary>
    /// <param name="info"></param>
    /// <returns></returns>
    [HttpPost]
    [HttpGet]
    public IActionResult Echo(string info = "test")
    {
        if (string.IsNullOrWhiteSpace(info))
        {
            return BadRequest("输入不能为空");
        }

        return Ok(info);
    }

}
