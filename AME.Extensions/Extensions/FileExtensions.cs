﻿// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Extensions;


/// <summary>
/// System.IO 扩展方法
/// </summary>
public static class FileExtensions
{
    public static async void test()
    {
#if NET48 || NETSTANDARD2_0
        await WriteAllTextAsync("testFileAsync.txt", "111111111");
        await ReadAllTextAsync("testFileAsync.txt");
#else
        await System.IO.File.WriteAllTextAsync("testFileAsync.txt", "22222");
        await System.IO.File.ReadAllTextAsync("testFileAsync.txt");
#endif

    }
#if NET48 || NETSTANDARD2_0
    /// <summary>
    /// 以异步形式创建一个新文件，在其中写入指定的字符串，然后关闭该文件。 如果目标文件已存在，则覆盖该文件。
    /// </summary>
    /// <param name="path"></param>
    /// <param name="content"></param>
    /// <param name="delay"></param>
    /// <returns></returns>
    public static async Task WriteAllTextAsync(string path, string content, int delay = 0)
    {
        if (delay > 0)
        {
            await Task.Delay(delay);
        }
        try
        {
            using (var sw = new StreamWriter(path))
            {
                await sw.WriteAsync(content);
            }
        }
        catch  
        {
            await Task.Delay(100);
            using (var sw = new StreamWriter(path))
            {
                await sw.WriteAsync(content);
            }
        }
    }

    /// <summary>
    /// 以异步形式打开一个文本文件，读取文件中的所有文本，然后关闭此文件。
    /// </summary>
    /// <param name="Path"></param>
    /// <returns></returns> 
    public static async Task<string> ReadAllTextAsync(string Path)
    {
        var stringBuilder = new StringBuilder();
        using (var fileStream = System.IO.File.OpenRead(Path))
        using (var streamReader = new StreamReader(fileStream))
        {
            string line = await streamReader.ReadLineAsync();
            while (line != null)
            {
                stringBuilder.AppendLine(line);
                line = await streamReader.ReadLineAsync();
            }
            return stringBuilder.ToString();
        }
    }
#endif

    private const double KBCount = 1024;
    private const double MBCount = KBCount * 1024;
    private const double GBCount = MBCount * 1024;
    private const double TBCount = GBCount * 1024;

    /// <summary>
    /// 得到适应的大小
    /// </summary>
    /// <param name="sizes"></param>
    /// <param name="roundCount"></param> 
    /// <returns>string</returns>
    public static string GetAutoSizeString(this long sizes, int roundCount = 2)
    {
        var size = (double)sizes;
        if (KBCount > size)
        {
            return $"{Math.Round(size, roundCount):N2}B";
        }
        else if (MBCount > size)
        {
            return $"{Math.Round(size / KBCount, roundCount):N2}KB";
        }
        else if (GBCount > size)
        {
            return $"{Math.Round(size / MBCount, roundCount):N2}MB";
        }
        else if (TBCount > size)
        {
            return $"{Math.Round(size / GBCount, roundCount):N2}GB";
        }
        else
        {
            return $"{Math.Round(size / TBCount, roundCount):N2}TB";
        }
    }


    /// <summary>
    /// 格式化文件名，以避免出现“Illegal characters in path”错误：
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static string FormattedFileName(this string fileName)
    {
        var invalidChars = Path.GetInvalidFileNameChars();
        var formattedFileName = string.Join("_", fileName.Split(invalidChars, StringSplitOptions.RemoveEmptyEntries));
        return formattedFileName;
    }


}
