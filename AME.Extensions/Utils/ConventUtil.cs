// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace AME;

public partial class ConventUtil
{
    /// <summary>
    /// 对象转换成JSON
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static string ObjectToJSON2<T>(T obj)
    {
        // Framework 2.0 不支持
        DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
        string result = string.Empty;
        using (MemoryStream ms = new MemoryStream())
        {
            serializer.WriteObject(ms, obj);
            ms.Position = 0;

            using (StreamReader read = new StreamReader(ms))
            {
                result = read.ReadToEnd();
            }
        }
        return result;
    }

    /// <summary>
    /// Json转换成对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="jsonText"></param>
    /// <returns></returns>
    public static T JsonToObject2<T>(string jsonText)
    {
        // Framework 2.0 不支持
        DataContractJsonSerializer s = new DataContractJsonSerializer(typeof(T));
        MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonText));
        T obj = (T)s.ReadObject(ms);
        ms.Dispose();
        return obj;


    }

    public static void TaskMethod2(object dd, bool ThreadInfo = true)
    {

        Console.WriteLine(dd);
        if (ThreadInfo == false) return;
        Console.WriteLine("Task id :{0},Thread :{1}", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
        Console.WriteLine("Is pool thread :{0}", Thread.CurrentThread.IsThreadPoolThread);
        Console.WriteLine("Is background thread:{0}", Thread.CurrentThread.IsBackground);
        Console.WriteLine("");
    }

    // 从一个对象信息生成Json串
    public static string ObjectToJson(object obj)
    {
        return JsonConvert.SerializeObject(obj);
    }
    // 从一个Json串生成对象信息
    public static object JsonToObject(string jsonString, object obj)
    {
        return JsonConvert.DeserializeObject(jsonString, obj.GetType());
    }
    public static T JsonToObjectT<T>(string jsonString)
    {
        return JsonConvert.DeserializeObject<T>(jsonString);
    }

    public static IEnumerable<string> GetEnumDescriptions(Enum value)
    {
        var descs = new List<string>();
        var type = value.GetType();
        var name = Enum.GetName(type, value);
        var field = type.GetField(name);
        var fds = field.GetCustomAttributes(typeof(DescriptionAttribute), true);
        foreach (DescriptionAttribute fd in fds)
        {
            descs.Add(fd.Description);
        }
        return descs;
    }

    public static bool IsNumeric(object anyObject)
    {
        try
        {
            if (anyObject is null or DBNull) return false;
            string anyString = anyObject.ToString();
            if (anyString.Length > 0)
            {
                double dummyOut = new double();
                System.Globalization.CultureInfo cultureInfo =
                    new System.Globalization.CultureInfo("en-US", true);

                return double.TryParse(anyString, System.Globalization.NumberStyles.Any,
                    cultureInfo.NumberFormat, out dummyOut);
            }
            else
            {
                return false;
            }
        }
        catch (Exception)
        {
            return false;
        }

    }

    public static string passwordhash(string password)
    {
        string result;
        using (var cryptoProvider = System.Security.Cryptography.SHA1.Create())
        {
            byte[] passwordHash = cryptoProvider.ComputeHash(Encoding.UTF8.GetBytes(password));
            result = "new byte[] { " +
                String.Join(",", passwordHash.Select(x => "0x" + x.ToString("x2")).ToArray())
                 + " } ";
        }
        return result;
    }


    /// <summary>
    /// 转换List到DataTable
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    public static DataTable GenericToDataTable<T>(IList<T> list)
    {
        var json = JsonConvert.SerializeObject(list);
        DataTable dt = (DataTable)JsonConvert.DeserializeObject(json, (typeof(DataTable)));
        return dt;
    }

    /// <summary>
    /// 转换DataTable到List
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    public static IList<T> DataTableToGeneric<T>(DataTable dt)
    {
        var json = JsonConvert.SerializeObject(dt);
        IList<T> list = JsonConvert.DeserializeObject<IList<T>>(json);
        return list;
    }


    /// <summary>
    /// 从 DataUrl 转换为 Stream
    /// <para>Convert from a DataUrl to an Stream</para>
    /// </summary>
    /// <param name="base64encodedstring"></param>
    /// <returns></returns>
    public static Stream DataUrl2Stream(string base64encodedstring)
    {
        var base64Data = Regex.Match(base64encodedstring, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
        var bytes = Convert.FromBase64String(base64Data);
        var stream = new MemoryStream(bytes);
        return stream;
    }

    /// <summary>
    /// 从 DataUrl 转换为 Byte[]
    /// <para>Convert from a DataUrl to an Stream</para>
    /// </summary>
    /// <param name="base64encodedstring"></param>
    /// <returns></returns>
    public static byte[] DataUrl2Bytes(string base64encodedstring)
    {
        var base64Data = Regex.Match(base64encodedstring, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
        var bytes = Convert.FromBase64String(base64Data);
        return bytes;
    }


    /// <summary>
    /// 从 base64 转换为 Stream
    /// <para>Convert from a base64 to an Stream</para>
    /// </summary>
    /// <param name="base64encodedstring"></param>
    /// <returns></returns>
    public static Stream Base642Stream(string base64encodedstring)
    {
        var bytes = Convert.FromBase64String(base64encodedstring);
        var stream = new MemoryStream(bytes);
        return stream;
    }


}
