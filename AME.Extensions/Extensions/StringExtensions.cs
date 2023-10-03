using System;
using System.Text;
using System.Threading;

namespace AME;

public class ColoredTextRegion : IDisposable
{
    private readonly string _after;
    private bool _isDisposed;

    private ColoredTextRegion(Func<string, ColoredText> colorization)
    {
        if (!StringExtensions.NoColor)
        {
            string[] parts = colorization("|").ToString().Split('|');
            Console.Write(parts[0]);
            _after = parts[1];
        }
    }

    public static IDisposable Create(Func<string, ColoredText> colorization)
    {
        return new ColoredTextRegion(colorization);
    }

    public void Dispose()
    {
        Dispose(true);
    }

    protected virtual void Dispose(bool isDisposing)
    {
        if (_isDisposed)
        {
            return;
        }

        _isDisposed = true;

        if (!StringExtensions.NoColor)
        {
            Console.Write(_after);
        }
    }
}

public class ColoredText
{
    private int _color;
    private string _message;
    private bool _bright;

    public ColoredText(string message)
    {
        _message = message;
    }

    public ColoredText Bright()
    {
        _bright = true;
        return this;
    }

    public ColoredText Red()
    {
        _color = 31;
        return this;
    }

    public ColoredText Black()
    {
        _color = 30;
        return this;
    }

    public ColoredText Green()
    {
        _color = 32;
        return this;
    }

    public ColoredText Orange()
    {
        _color = 33;
        return this;
    }

    public ColoredText Blue()
    {
        _color = 34;
        return this;
    }

    public ColoredText Purple()
    {
        _color = 35;
        return this;
    }

    public ColoredText Cyan()
    {
        _color = 36;
        return this;
    }

    public ColoredText LightGray()
    {
        _color = 37;
        return this;
    }

    public static implicit operator string(ColoredText t)
    {
        return t.ToString();
    }

    public override string ToString()
    {
        if (StringExtensions.NoColor || _color == 0)
        {
            return _message;
        }

        string colorString = _color.ToString();
        if (_bright)
        {
            colorString += "m\x1B[1";
        }

        return $"\x1B[{colorString}m{_message}\x1B[0m\x1B[39m\x1B[49m";
    }

}

public static class StringExtensions
{
    public static bool NoColor { get; set; }

    public static ColoredText Orange(this string s)
    {
        return new ColoredText(s).Orange();
    }

    public static ColoredText Black(this string s)
    {
        return new ColoredText(s).Black();
    }

    public static ColoredText Red(this string s)
    {
        return new ColoredText(s).Red();
    }

    public static ColoredText Green(this string s)
    {
        return new ColoredText(s).Green();
    }

    public static ColoredText Blue(this string s)
    {
        return new ColoredText(s).Blue();
    }

    public static ColoredText Purple(this string s)
    {
        return new ColoredText(s).Purple();
    }

    public static ColoredText Cyan(this string s)
    {
        return new ColoredText(s).Cyan();
    }

    public static ColoredText LightGray(this string s)
    {
        return new ColoredText(s).LightGray();
    }

    public static string TrimStart(this string target, string prefix)
    {
        if (target.StartsWith(prefix))
        {
            return target.Substring(prefix.Length);
        }
        return target;
    }

    public static string TrimEnd(this string target, string prefix)
    {
        if (target.EndsWith(prefix))
        {
            return target.Substring(0, target.Length - prefix.Length);
        }
        return target;
    }

    /// <summary>将大驼峰命名转为小驼峰命名</summary>
    public static string ToCamelCase(this string str)
    {
        var firstChar = str[0];

        if (firstChar == char.ToLowerInvariant(firstChar))
        {
            return str;
        }

        var name = str.ToCharArray();
        name[0] = char.ToLowerInvariant(firstChar);

        return new String(name);
    }

    /// <summary>将大驼峰命名转为蛇形命名</summary>
    public static string ToSnakeCase(this string str)
    {
        var builder = new StringBuilder();
        var name = str;
        var previousUpper = false;

        for (var i = 0; i < name.Length; i++)
        {
            var c = name[i];
            if (char.IsUpper(c))
            {
                if (i > 0 && !previousUpper)
                {
                    builder.Append("_");
                }
                builder.Append(char.ToLowerInvariant(c));
                previousUpper = true;
            }
            else
            {
                builder.Append(c);
                previousUpper = false;
            }
        }
        return builder.ToString();
    }

    public static string ToI18n(this string chn, string es, string en = null)
    {
        return Thread.CurrentThread.CurrentUICulture.Name == "es-ES" ? es : Thread.CurrentThread.CurrentUICulture.Name == "en-EN" ? (en ?? chn) : chn;
    }

    public static string Latin1ToGB2312(this string latin1String) => string.IsNullOrWhiteSpace(latin1String) ? "" : Encoding.GetEncoding("gb2312").GetString(Encoding.GetEncoding("iso-8859-1").GetBytes(latin1String));

    //public static string PadLeft(this string str, int x)
    //{
    //    return str + "".PadLeft(x, ' ');
    //}

}

public static class ConsoleExt
{
    public static void WriteLineColor(this string s, ConsoleColor foregroundColor)
    {
#if WINDOWS || Linux
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = foregroundColor;
#endif
        Console.WriteLine(s);
#if WINDOWS || Linux
       Console.ResetColor();
#endif
    }
    public static void WriteLine(this string s) => s.WriteLineColor(ConsoleColor.White);
    public static void WriteLineCyan(string s) => s.WriteLineColor(ConsoleColor.Cyan);
    public static void WriteLineGreen(string s) => s.WriteLineColor(ConsoleColor.Green);
    public static void WriteLineDarkYellow(string s) => s.WriteLineColor(ConsoleColor.DarkYellow);

}
