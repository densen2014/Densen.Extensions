// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using Microsoft.Extensions.FileSystemGlobbing;
using System;
using System.Collections.Generic;
using System.IO;

namespace AME;

/// <summary>
/// Path 扩展方法
/// </summary>


public static class PathExtensions
{
    //https://docs.microsoft.com/zh-cn/dotnet/core/extensions/file-globbing


    /// <summary>
    ///文件名和目录名中的通配符 *，表示零到多个字符，不包括分隔符。 <para></para>
    ///值 说明<para></para>
    ///*.txt 具有.txt 文件扩展名的所有文件。<para></para>
    ///*.*	具有一个扩展名的所有文件。<para></para>
    ///*	顶层目录中的所有文件。<para></para>
    ///.*	以“.”开头的文件名称。<para></para>
    ///*word* 文件名中包含“word”的所有文件。<para></para>
    ///readme.* 所有带有任何文件扩展名且名为“readme”的文件。<para></para>
    ///styles/*.css	目录“styles/”中扩展名为“.css”的所有文件。<para></para>
    ///scripts/*/* “scripts/”中的或“scripts/”下一级子目录中的所有文件。<para></para>
    ///images*/*	文件夹中名称为“images”或名称以“images”开头的所有文件。<para></para>
    ///任意目录深度(/**/)。<para></para>
    ///值 描述<para></para>
    ///**/*	任何子目录中的所有文件。<para></para>
    ///dir/**/*	“dir/”下任何子目录中的所有文件。<para></para>
    ///https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.filesystemglobbing.matcher?view=dotnet-plat-ext-5.0
    /// </summary>
    /// <param name="searchDirectory">../starting-folder/</param>
    /// <param name="include">包含项 "*.txt"</param>
    /// <param name="exclude">排除项 "*.txt"</param>
    /// <param name="includeMatchers">多个包含项 new[] { "*.txt", "*.asciidoc", "*.md" }</param>
    /// <param name="excludeMatchers">多个排除项 new[] { "*.txt", "*.asciidoc", "*.md" }</param>
    /// <param name="matchfilename"></param>
    public static IEnumerable<string> GetDirFiles(
        this string searchDirectory,
        string include = null,
        string exclude = null,
        string[] includeMatchers = null,
        string[] excludeMatchers = null,
        string matchfilename = null)
    {
        Matcher matcher = new Matcher();
        if (include != null) matcher.AddInclude(include);
        if (exclude != null) matcher.AddExclude(exclude);
        if (includeMatchers != null) matcher.AddIncludePatterns(includeMatchers);
        if (excludeMatchers != null) matcher.AddExcludePatterns(excludeMatchers);
        if (string.IsNullOrEmpty(searchDirectory)) searchDirectory = ".";
        if (include == null && includeMatchers == null) matcher.AddInclude("*.*");

        IEnumerable<string> matchingFiles = matcher.GetResultsInFullPath(searchDirectory);

        return matchingFiles;
        //PatternMatchingResult result = matcher.Execute(
        //    new DirectoryInfoWrapper(
        //        new DirectoryInfo(searchDirectory))); 
    }

    /// <summary>
    /// 格式化目录名，以避免出现“Illegal characters in path”错误：
    /// </summary>
    /// <param name="pathName"></param>
    /// <param name="replaceVolumeSeparatorChar">过滤冒号</param>
    /// <returns></returns>
    public static string FormattedPathName(this string pathName,bool replaceVolumeSeparatorChar =true)
    {
        var invalidChars = Path.GetInvalidPathChars();
        var formattedPathName = string.Join("_", pathName.Split(invalidChars, StringSplitOptions.RemoveEmptyEntries));
        return replaceVolumeSeparatorChar?formattedPathName.Replace (":","_"): formattedPathName;
    }

    /// <summary>
    /// 创建目录(如果目录不存在)
    /// </summary>
    /// <param name="path">要创建的目录</param>
    public static void CreatePathIfNotExists(this string path)
    {
        var directory = Path.GetDirectoryName(path);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }


    }
}
