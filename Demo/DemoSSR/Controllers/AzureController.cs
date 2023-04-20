using BootstrapBlazor.AzureServices;
using BootstrapBlazor.Ocr.Services;
using BootstrapBlazor.OpenAI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using static OpenAI.GPT3.ObjectModels.SharedModels.IOpenAiModels;

namespace AME.Controllers;
#nullable enable 

/// <summary>
/// 工具接口
/// </summary>
[ApiController]
[Route("[controller]")]
public class AzureController : ControllerBase
{

    private readonly IWebHostEnvironment environment;
    private readonly ILogger<AzureController> _logger;
    private readonly IConfiguration _config;
    private readonly OcrService ocrService;
    private readonly AiFormService aiFormService;
    private readonly TranslateService transService;
    private readonly OpenAiClientService openaiService;

    public AzureController(TranslateService transService, OcrService ocrService, AiFormService aiFormService, OpenAiClientService openaiService, IWebHostEnvironment environment, ILogger<AzureController> logger, IConfiguration config)
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
    /// 翻译
    /// </summary>
    /// <param name="textToTranslate"></param>
    /// <param name="onlyText"></param>
    /// <returns></returns>
    [HttpPost("Translate")]
    public async Task<IActionResult> Translate(string textToTranslate = "I would really like to drive your car around the block a few times!", bool onlyText = false)
    {
        try
        {
            var res = await transService.Translate(textToTranslate);
            if (res != null)
            {
                if (!onlyText) return Ok(res);
                var res1 = new List<string>
                {
                    res[0]?.text?.UpperFirstChar()??"",
                    res[1]?.text?.UpperFirstChar()??"",
                    res[2]?.text?.UpperFirstChar()??"",
                    res[3]?.text?.UpperFirstChar()??"",
                    res[4]?.text?.UpperFirstChar()??""
                };
                return Ok(res1);
            }
            return BadRequest("Null");

        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }


    /// <summary>
    /// OCR
    /// </summary>
    /// <param name="efile">上传待识别的文件</param>
    /// <param name="url">可选: 识别URL文件, 默认 null</param>
    /// <param name="Ocr">识别文本, 默认 true</param>
    /// <param name="AnalyzeImage">分析图像, 默认 false</param>
    /// <param name="DetectObjects">检测图像中的对象, 默认 false</param>
    /// <param name="DetectDomainSpecific">检测图像中的地标或名人, 默认 false</param>
    /// <param name="Key"></param>
    /// <param name="Endpoint"></param> 
    /// <returns>水印图</returns>
    /// <remarks>存入watermark文件夹</remarks>
    [HttpPost("OCR")]
    public async Task<IActionResult> OCR(IFormFile? efile = null,
                                         string? url = null,
                                         bool Ocr = true,
                                         bool AnalyzeImage = false,
                                         bool DetectObjects = false,
                                         bool DetectDomainSpecific = false,
                                         string? Key = null,
                                         string? Endpoint = null)
    {
        try
        {
            if (null == efile && string.IsNullOrWhiteSpace(url))
            {
                _logger.LogError("file and url both null.");
                return BadRequest("file and url both null.");
            }
            if (Key != null) ocrService!.SubscriptionKey = Key;
            if (Endpoint != null) ocrService!.Endpoint = Endpoint;
            if (null != efile && efile.Length > 0)
            {

                using var stream = efile.OpenReadStream();
                await ocrService!.CopyStreamAsync(stream);
                if (DetectDomainSpecific || DetectObjects || AnalyzeImage)
                {
                    _logger.LogInformation("图像获取成功");
                }

                if (ocrService.Tempfilename == null)
                {
                    return BadRequest("Tempfilename == null");
                }

                try
                {
                    var res = new List<string>();
                    if (Ocr)
                    {
                        res.AddRange(await ocrService!.OcrLocal(ocrService.Tempfilename));
                    }
                    if (DetectDomainSpecific)
                    {
                        res.AddRange(await ocrService!.DetectDomainSpecific(localImage: ocrService.Tempfilename));
                    }
                    if (DetectObjects)
                    {
                        res.AddRange(await ocrService!.DetectObjects(localImage: ocrService.Tempfilename));
                    }
                    if (AnalyzeImage)
                    {
                        res.AddRange(await ocrService!.AnalyzeImage(localImage: ocrService.Tempfilename));
                    }
                    return Ok(res);
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                    return BadRequest(e.Message);
                }
            }
            else if (!string.IsNullOrWhiteSpace(url))
            {
                try
                {
                    var res = new List<string>();
                    if (Ocr)
                    {
                        res.AddRange(await ocrService!.StartOcr(url));
                    }
                    if (DetectDomainSpecific)
                    {
                        res.AddRange(await ocrService!.DetectDomainSpecific(url));
                    }
                    if (DetectObjects)
                    {
                        res.AddRange(await ocrService!.DetectObjects(url));
                    }
                    if (AnalyzeImage)
                    {
                        res.AddRange(await ocrService!.AnalyzeImage(url));
                    }
                    return Ok(res);
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                    return BadRequest(e.Message);
                }
            }
            return BadRequest();
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error uploading image.");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// AI表格识别
    /// </summary>
    /// <param name="efile">上传待识别的文件</param>
    /// <param name="url">可选: 识别URL文件, 默认 null</param>
    /// <param name="modelId">
    /// 用于分析输入文档的模型 ID。使用定制模型时
    /// 为了便于分析，这个参数必须是模型创建时归属于模型的ID。什么时候
    /// 使用服务的预建模型之一，必须传递受支持的预建模型 ID 之一。
    /// 可以在 <see href="https://aka.ms/azsdk/formrecognizer/models"/> 找到预构建的模型 ID。
    /// </param>
    /// <param name="Key"></param>
    /// <param name="Endpoint"></param>
    /// <returns></returns>
    [HttpPost("AiForm")]
    public async Task<IActionResult> AiForm(IFormFile? efile = null,
                                            string? url = null,
                                            string modelId = "prebuilt-receipt",
                                            string? Key = null,
                                            string? Endpoint = null)
    {
        try
        {
            if (null == efile && string.IsNullOrWhiteSpace(url))
            {
                _logger.LogError("file and url both null.");
                return BadRequest("file and url both null.");
            }
            if (Key != null) aiFormService!.SubscriptionKey = Key;
            if (Endpoint != null) aiFormService!.Endpoint = Endpoint;
            try
            {
                if (null != efile && efile.Length > 0)
                {
                    using var stream = efile.OpenReadStream();
                    var res = await aiFormService!.AnalyzeDocument(image: stream, modelId: modelId);
                    return Ok(res);
                }
                else if (!string.IsNullOrWhiteSpace(url))
                {
                    var res = await aiFormService!.AnalyzeDocument(url, modelId: modelId);
                    return Ok(res);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }

            return BadRequest();
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error uploading image.");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }


    /// <summary>
    /// OpenAI.GPT
    /// </summary>
    /// <param name="prompt"></param>
    /// <param name="model">OpenAiModel枚举: ChatGpt|Completions|DALLE, 默认 ChatGpt </param>
    /// <param name="maxTokens">完成时生成的最大令牌数。默认值为 500</param>
    /// <param name="temperature">浮点数，控制模型的输出的多样性。值越高，输出越多样化。值越低，输出越简单。默认值为 0.5</param>
    /// <returns></returns>
    [HttpPost("OpenAiGPT3")]
    [HttpGet("OpenAiGPT3")]
    public async Task<IActionResult> OpenAiGPT3(string prompt, EnumOpenAiModel? model = EnumOpenAiModel.ChatGpt, int maxTokens = 500, float temperature = 0.5f,string? key=null)
    {
        if (string.IsNullOrWhiteSpace(key) && string.IsNullOrWhiteSpace(_config["Key"]))
        {
            return BadRequest("关于这个问题，早有先哲以炯炯的目光洞察先机，那敢问佛教之出路在何方呢？如果拿这一问题去问古代的禅师，禅师定会说：“看脚下！”诚然，路就在脚下，那又应如何去走呢 ...");
        }
        else if (key!=_config["Key"])
        {
            return BadRequest("敢问施主来自何方?");
        }
        try
        {
            string? res = string.Empty;
            switch (model)
            {
                default:
                    res = await openaiService.ChatGPT(prompt, maxTokens, temperature);
                    break;
                case EnumOpenAiModel.Completions:
                    res = await openaiService.Completions(prompt, maxTokens, temperature);
                    break;
                //case EnumOpenAiModel.DALLE:
                //    res = await openaiService.DALLE_CreateImage(prompt);
                //    if (res != null)
                //    {
                //        res = $"data:image/jpg;base64,{res}";
                //    }
                //    break;
            }

            if (res != null)
            {
                return Ok(res);
            }
            else
            {
                return BadRequest("AI开小差了. 重新问点啥吧,可选模型后再问我.");
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

}

static class StringExtensions
{
    private static readonly Regex cjkCharRegex = new Regex(@"\p{IsCJKUnifiedIdeographs}");
    public static bool IsChinese(this string? str)
    {
        return str == null ? false : str.Any(z => z.IsChinese());
    }
    public static bool IsChinese(this char c)
    {
        return cjkCharRegex.IsMatch(c.ToString());
    }
    public static string? UpperFirstChar(this string? input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return null;
        }

        char[] chars = input.ToCharArray();
        chars[0] = char.ToUpper(chars[0]);
        return new string(chars);
    }
}
