using BootstrapBlazor.AzureServices;
using BootstrapBlazor.Ocr.Services;
using BootstrapBlazor.OpenAI.GPT3.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using static OpenAI.GPT3.ObjectModels.SharedModels.IOpenAiModels;

namespace AME.Controllers;
#nullable enable 

/// <summary>
/// Sample接口
/// </summary>
[ApiController]
[Route("[controller]")]
public class SampleController : ControllerBase
{

    private readonly IWebHostEnvironment environment;
    private readonly ILogger<SampleController> _logger;
    private readonly IConfiguration _config;
    private readonly OcrService ocrService;
    private readonly AiFormService aiFormService;
    private readonly TranslateService transService;
    private readonly OpenAiClientService openaiService;

    public SampleController(TranslateService transService, OcrService ocrService, AiFormService aiFormService, OpenAiClientService openaiService, IWebHostEnvironment environment, ILogger<SampleController> logger, IConfiguration config)
    {
        this.transService = transService;
        this.ocrService = ocrService;
        this.aiFormService = aiFormService;
        this.openaiService = openaiService;
        this.environment = environment;
        this._logger = logger;
        this._config = config;
    }

    string ToolsPath => Path.Combine(environment.ContentRootPath, "Files");
 

    /// <summary>
    /// json
    /// </summary>
    /// <param name="prompt"></param>
    /// <returns></returns>
    [HttpPost]
    [HttpGet]
    public IActionResult SampleJson(string prompt)
    {
        if (!string.IsNullOrWhiteSpace(prompt))
        {
            return BadRequest("关于这个问题，早有先哲以炯炯的目光洞察先机，那敢问佛教之出路在何方呢？如果拿这一问题去问古代的禅师，禅师定会说：“看脚下！”诚然，路就在脚下，那又应如何去走呢 ...");
        }
        try
        {
            string? res = @$"{{
    ""code"": ""200"",
    ""data"": ""姓名：张国立{prompt}"",
    ""msg"": ""解析成功！""
}}
";
                return Ok(res);
           
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

}
