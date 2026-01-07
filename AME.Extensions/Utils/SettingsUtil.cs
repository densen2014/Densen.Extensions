// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AME;

public partial class SettingsUtil
{

    /// <summary>
    /// 从指定路径的 JSON 文件加载 T 类型的配置对象。如果文件不存在则新建一个实例被创建并可选择保存到磁盘。
    /// </summary>
    /// <remarks>如果文件存在但为空或包含无效 JSON，则返回 T 的新实例。该方法尝试在反序列化之前验证并清理 JSON 文件。如果保存时文件不存在为 true 且文件不存在，则将新实例保存到指定路径。</remarks>
    /// <typeparam name="T">要加载的配置对象的类型。必须有一个无参数构造函数。</typeparam>
    /// <param name="filePath">要加载的 JSON 配置文件的路径。</param>
    /// <param name="saveWhenFileNoExist">true 将配置的新实例保存到指定文件（如果不存在, 默认为 true。</param>
    /// <returns>从 JSON 文件加载类型 T 的实例，如果文件不存在或为空，则为新实例。</returns>
    public static T LoadJsonConfig<T>(string filePath, bool saveWhenFileNoExist = true) where T : new()
    {
        if (!File.Exists(filePath))
        {
            var instance = new T();
            if (saveWhenFileNoExist)
            {
                SaveJsonConfig(filePath, instance);
            }
            return instance;
        }
        else
        {
            ValidateAndCleanJsonFile(filePath);

            string TestFileStream = File.ReadAllText(filePath);
            try
            {
                if (string.IsNullOrWhiteSpace(TestFileStream) == false)
                {
                    var instance = JsonConvert.DeserializeObject<T>(TestFileStream);//反序列化
                    if (instance != null)
                    {
                        return instance;
                    }
                    else
                    {
                        return new T();
                    }
                }
            }
            finally
            {
            }
            return new T();
        }

    }

    public static void SaveJsonConfig<T>(string filePath, T config)
    {
        if (config != null)
        {
            string json = JsonConvert.SerializeObject(config, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
    }

    /// <summary>
    /// 读取配置文件内容，只保留第一个有效的JSON对象（或数组），并自动清理多余内容。这样可以防止JsonConvert.DeserializeObject因多余内容报错
    /// </summary>
    /// <param name="filePath"></param>
    public static void ValidateAndCleanJsonFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return;
        }

        string content = File.ReadAllText(filePath);
        content = content.Trim();

        // 尝试只提取第一个完整的JSON对象
        int openBraces = 0;
        int closeBraces = 0;
        int endIndex = -1;
        for (int i = 0; i < content.Length; i++)
        {
            if (content[i] == '{')
            {
                openBraces++;
            }

            if (content[i] == '}')
            {
                closeBraces++;
            }

            if (openBraces > 0 && openBraces == closeBraces)
            {
                endIndex = i + 1;
                break;
            }
        }

        if (endIndex > 0)
        {
            string json = content.Substring(0, endIndex);
            try
            {
                // 验证JSON有效性
                JObject.Parse(json);
                // 如果后面还有多余内容，进行清理
                if (endIndex < content.Length)
                {
                    File.WriteAllText(filePath, json);
                }
            }
            catch (JsonReaderException)
            {
                // JSON无效，可选择备份原文件并清空
                File.Copy(filePath, filePath + ".bak", true);
                File.WriteAllText(filePath, "{}");
            }
        }
        else
        {
            // 没有找到完整的JSON对象，备份并清空
            File.Copy(filePath, filePath + ".bak", true);
            File.WriteAllText(filePath, "{}");
        }
    }


    #region System.Text.Json 版本
    /// <summary>
    /// 从指定路径的 JSON 文件加载 T 类型的配置对象。如果文件不存在则新建一个实例被创建并可选择保存到磁盘。
    /// </summary>
    /// <remarks>如果文件存在但为空或包含无效 JSON，则返回 T 的新实例。该方法尝试在反序列化之前验证并清理 JSON 文件。如果保存时文件不存在为 true 且文件不存在，则将新实例保存到指定路径。</remarks>
    /// <typeparam name="T">要加载的配置对象的类型。必须有一个无参数构造函数。</typeparam>
    /// <param name="filePath">要加载的 JSON 配置文件的路径。</param>
    /// <param name="saveWhenFileNoExist">true 将配置的新实例保存到指定文件（如果不存在, 默认为 true。</param>
    /// <returns>从 JSON 文件加载类型 T 的实例，如果文件不存在或为空，则为新实例。</returns>
    public static T LoadTextJsonConfig<T>(string filePath, bool saveWhenFileNoExist = true) where T : new()
    {
        if (!File.Exists(filePath))
        {
            var instance = new T();
            if (saveWhenFileNoExist)
            {
                SaveTextJsonConfig(filePath, instance);
            }
            return instance;
        }
        else
        {
            ValidateAndCleanJsonFile(filePath);

            string TestFileStream = File.ReadAllText(filePath);
            try
            {
                if (string.IsNullOrWhiteSpace(TestFileStream) == false)
                {
                    var instance = System.Text.Json.JsonSerializer.Deserialize<T>(TestFileStream);//反序列化
                    if (instance != null)
                    {
                        return instance;
                    }
                    else
                    {
                        return new T();
                    }
                }
            }
            finally
            {
            }
            return new T();
        }

    }

    public static void SaveTextJsonConfig<T>(string filePath, T config, JsonSerializerOptions options=null)
    {
        if (config != null)
        {
            string json = System.Text.Json.JsonSerializer.Serialize(config, options??optionsTextJson);
            File.WriteAllText(filePath, json);
        }
    }

    public static JsonSerializerOptions optionsTextJson = new JsonSerializerOptions()
    {
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
    };

    #endregion

}
