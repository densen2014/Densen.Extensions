﻿// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using System.Globalization;
using System.Text;
using System.Text.Encodings.Web;

namespace AME;

// 参考 https://github.com/dotnet/aspnetcore/blob/19d2f6124f5d04859e350d1f5a01e994e14ef1ce/src/Middleware/StaticFiles/src/HtmlDirectoryFormatter.cs#L20

/// <summary>
/// 中文界面按修改时间逆序排序<para></para>
/// Generates an HTML view title chinese for a directory with soft by LastModified.
/// </summary>
public class HtmlDirectoryFormatterChsSorted : IDirectoryFormatter
{
    private const string TextHtmlUtf8 = "text/html; charset=utf-8";

    private readonly HtmlEncoder _htmlEncoder;

    /// <summary>
    /// Constructs the <see cref="HtmlDirectoryFormatterChsSorted"/>.
    /// </summary>
    /// <param name="encoder">The character encoding representation to use.</param>
    public HtmlDirectoryFormatterChsSorted(HtmlEncoder encoder)
    {
        if (encoder == null)
        {
            throw new ArgumentNullException(nameof(encoder));
        }
        _htmlEncoder = encoder;
    }

    public static class Resources
    {
        public static string HtmlDir_IndexOf = "目录";
        public static string HtmlDir_TableSummary = "TableSummary";
        public static string HtmlDir_Name = "文件名";
        public static string HtmlDir_Size = "大小";
        public static string HtmlDir_Modified = "修改";
        public static string HtmlDir_LastModified = "最后修改";
    }

    /// <summary>
    /// Generates an HTML view for a directory.
    /// </summary>
    public virtual Task GenerateContentAsync(HttpContext context, IEnumerable<IFileInfo> contents)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (contents == null)
        {
            throw new ArgumentNullException(nameof(contents));
        }

        context.Response.ContentType = TextHtmlUtf8;

        if (HttpMethods.IsHead(context.Request.Method))
        {
            // HEAD, no response body
            return Task.CompletedTask;
        }

        PathString requestPath = context.Request.PathBase + context.Request.Path;

        var builder = new StringBuilder();

        builder.AppendFormat(
@"<!DOCTYPE html>
<html lang=""{0}"">", CultureInfo.CurrentUICulture.TwoLetterISOLanguageName);

        builder.AppendFormat(@"
<head>
  <title>{0} {1}</title>", HtmlEncode(Resources.HtmlDir_IndexOf), HtmlEncode(requestPath.Value));

        builder.Append(@"
  <style>
    body {
        font-family: ""Segoe UI"", ""Segoe WP"", ""Helvetica Neue"", 'RobotoRegular', sans-serif;
        font-size: 14px;}
    header h1 {
        font-family: ""Segoe UI Light"", ""Helvetica Neue"", 'RobotoLight', ""Segoe UI"", ""Segoe WP"", sans-serif;
        font-size: 28px;
        font-weight: 100;
        margin-top: 5px;
        margin-bottom: 0px;}
    #index {
        border-collapse: separate;
        border-spacing: 0;
        margin: 0 0 20px; }
    #index th {
        vertical-align: bottom;
        padding: 10px 5px 5px 5px;
        font-weight: 400;
        color: #a0a0a0;
        text-align: center; }
    #index td { padding: 3px 10px; }
    #index th, #index td {
        border-right: 1px #ddd solid;
        border-bottom: 1px #ddd solid;
        border-left: 1px transparent solid;
        border-top: 1px transparent solid;
        box-sizing: border-box; }
    #index th:last-child, #index td:last-child {
        border-right: 1px transparent solid; }
    #index td.length, td.modified { text-align:right; }
    a { color:#1ba1e2;text-decoration:none; }
    a:hover { color:#13709e;text-decoration:underline; }
  </style>
</head>
<body>
  <section id=""main"">");
        builder.AppendFormat(@"
    <header><h1>{0} <a href=""/"">/</a>", HtmlEncode(Resources.HtmlDir_IndexOf));

        string cumulativePath = "/";
        foreach (var segment in requestPath.Value.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries))
        {
            cumulativePath = cumulativePath + segment + "/";
            builder.AppendFormat(@"<a href=""{0}"">{1}/</a>",
                HtmlEncode(cumulativePath), HtmlEncode(segment));
        }

        builder.AppendFormat(CultureInfo.CurrentUICulture,
@"</h1></header>
    <table id=""index"" summary=""{0}"">
    <thead>
      <tr><th abbr=""{1}"">{1}</th><th abbr=""{2}"">{2}</th><th abbr=""{3}"">{4}</th></tr>
    </thead>
    <tbody>",
        HtmlEncode(Resources.HtmlDir_TableSummary),
        HtmlEncode(Resources.HtmlDir_Name),
        HtmlEncode(Resources.HtmlDir_Size),
        HtmlEncode(Resources.HtmlDir_Modified),
        HtmlEncode(Resources.HtmlDir_LastModified));

        contents = contents
                    .Where(x => !x.Name.Contains("_content") && !x.Name.Equals("favicon.ico") && !x.Name.Equals("private"))
                    .OrderBy(content => content.IsDirectory)
                    .ThenByDescending(content => content.LastModified);

        foreach (var subdir in contents.Where(info => info.IsDirectory))
        {
            // Collect directory metadata in a try...catch in case the file is deleted while we're getting the data.
            // The metadata is retrieved prior to calling AppendFormat so if it throws, we won't have written a row
            // to the table.
            try
            {
                builder.AppendFormat(@"
      <tr class=""directory"">
        <td class=""name""><a href=""./{0}/"">{0}/</a></td>
        <td></td>
        <td class=""modified"">{1}</td>
      </tr>",
                    HtmlEncode(subdir.Name),
                    HtmlEncode(subdir.LastModified.ToString(CultureInfo.CurrentCulture)));
            }
            catch (DirectoryNotFoundException)
            {
                // The physical DirectoryInfo class doesn't appear to throw for either
                // of Name or LastWriteTimeUtc (which backs LastModified in the physical provider)
                // if the directory doesn't exist. However, we don't know what other providers might do.

                // Just skip this directory. It was deleted while we were enumerating.
            }
            catch (FileNotFoundException)
            {
                // The physical DirectoryInfo class doesn't appear to throw for either
                // of Name or LastWriteTimeUtc (which backs LastModified in the physical provider)
                // if the directory doesn't exist. However, we don't know what other providers might do.

                // Just skip this directory. It was deleted while we were enumerating.
            }
        }

        foreach (var file in contents.Where(info => !info.IsDirectory))
        {
            // Collect file metadata in a try...catch in case the file is deleted while we're getting the data.
            // The metadata is retrieved prior to calling AppendFormat so if it throws, we won't have written a row
            // to the table.
            try
            {
                builder.AppendFormat(@"
      <tr class=""file"">
        <td class=""name""><a href=""./{0}"">{0}</a></td>
        <td class=""length"">{1}</td>
        <td class=""modified"">{2}</td>
      </tr>",
                    HtmlEncode(file.Name),
                    HtmlEncode(AutoSize.GetAutoSizeString(file.Length)),
                    //HtmlEncode(file.Length.ToString("n0", CultureInfo.CurrentCulture)),
                    HtmlEncode(file.LastModified.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CurrentCulture)));
            }
            catch (DirectoryNotFoundException)
            {
                // There doesn't appear to be a case where DirectoryNotFound is thrown in the physical provider,
                // but we don't know what other providers might do.

                // Just skip this file. It was deleted while we were enumerating.
            }
            catch (FileNotFoundException)
            {
                // Just skip this file. It was deleted while we were enumerating.
            }
        }

        builder.Append(@"
    </tbody>
    </table>
  </section>
</body>
</html>");
        string data = builder.ToString();
        byte[] bytes = Encoding.UTF8.GetBytes(data);
        context.Response.ContentLength = bytes.Length;
        return context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
    }

    private string HtmlEncode(string body)
    {
        return _htmlEncoder.Encode(body);
    }
}

// 参考 <PackageReference Include="SortedHtmlDirectoryFormatter" Version="0.1.8" />
public class SortedHtmlDirectoryFormatter : HtmlDirectoryFormatterChsSorted, IDirectoryFormatter
{
    /// <summary>
    /// Constructs the <see cref="SortedHtmlDirectoryFormatter"/>.
    /// </summary>
    /// <param name="encoder">The character encoding representation to use.</param>
    public SortedHtmlDirectoryFormatter(HtmlEncoder encoder)
        : base(encoder)
    { }

    /// <summary>
    /// Constructs the <see cref="SortedHtmlDirectoryFormatter"/> with default encoder
    /// </summary>
    public SortedHtmlDirectoryFormatter() : base(HtmlEncoder.Default)
    { }

    public override Task GenerateContentAsync(HttpContext context, IEnumerable<IFileInfo> contents)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (contents == null)
        {
            throw new ArgumentNullException(nameof(contents));
        }

        var sortedContents = contents
            .OrderBy(content => content.IsDirectory)
            .ThenByDescending(content => content.LastModified);
        return base.GenerateContentAsync(context, sortedContents);
    }
}
public class ListDirectoryFormatter : IDirectoryFormatter
{
    public async Task GenerateContentAsync(HttpContext context, IEnumerable<IFileInfo> contents)
    {
        contents = contents.OrderByDescending(a => a.LastModified);
        context.Response.ContentType = "text/html";
        await context.Response.WriteAsync($"<html><head><title></title><body><ul>");
        foreach (var file in contents.OrderByDescending(a => a.LastModified))
        {
            string href = $"{context.Request.Path.Value.TrimEnd('/')}/{file.Name}";
            await context.Response.WriteAsync($"<li><a href='{href}'>{file.Name} - {file.Length} - {file.LastModified}</a></li>");
        }
        await context.Response.WriteAsync("</ul></body></html>");
    }
}

public class AutoSize
{
    private const double KBCount = 1024;
    private const double MBCount = KBCount * 1024;
    private const double GBCount = MBCount * 1024;
    private const double TBCount = GBCount * 1024;

    /// <summary>
    /// 得到适应的大小
    /// </summary>
    /// <param name="size"></param>
    /// <param name="roundCount"></param> 
    /// <returns>string</returns>
    public static string GetAutoSizeString(double size, int roundCount = 2)
    {
        if (KBCount > size)
        {
            return Math.Round(size, roundCount) + "B";
        }
        else if (MBCount > size)
        {
            return Math.Round(size / KBCount, roundCount) + "KB";
        }
        else if (GBCount > size)
        {
            return Math.Round(size / MBCount, roundCount) + "MB";
        }
        else if (TBCount > size)
        {
            return Math.Round(size / GBCount, roundCount) + "GB";
        }
        else
        {
            return Math.Round(size / TBCount, roundCount) + "TB";
        }
    }
}
