// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using System;
using System.Text;

public static class GlobalExtensions
{

    /// <summary>
    /// 转格林时间，并以ISO8601格式化字符串
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public static string ToGmtISO8601(this DateTime time)
    {
        return time.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ");
    }

    /// <summary>
    /// 获取时间戳，按1970-1-1
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public static long GetTime(this DateTime time)
    {
        return (long)time.ToUniversalTime().Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
    }

    private static readonly DateTime dt19700101 = new DateTime(1970, 1, 1);
    /// <summary>
    /// 获取时间戳毫秒数，按1970-1-1
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public static long GetTimeMilliseconds(this DateTime time)
    {
        return time.ToUniversalTime().Subtract(new DateTime(1970, 1, 1)).Ticks / 1000;
    }

    /// <summary>
    /// 时间戳反转为时间，有很多中翻转方法，但是，请不要使用过字符串（string）进行操作，大家都知道字符串会很慢！
    /// </summary>
    /// <param name="TimeStamp">时间戳</param>
    /// <param name="AccurateToMilliseconds">是否精确到毫秒</param>
    /// <returns>返回一个日期时间</returns>
    public static DateTime GetTime(this long TimeStamp, bool AccurateToMilliseconds = false)
    {
#pragma warning disable CS0618 // “TimeZone”已过时:“System.TimeZone has been deprecated.  Please investigate the use of System.TimeZoneInfo instead.”
        System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
#pragma warning restore CS0618 // “TimeZone”已过时:“System.TimeZone has been deprecated.  Please investigate the use of System.TimeZoneInfo instead.”
        if (AccurateToMilliseconds)
        {
            return startTime.AddTicks(TimeStamp * 10000);
        }
        else
        {
            return startTime.AddTicks(TimeStamp * 10000000);
        }
    }
    #region ==数据转换扩展==

    /// <summary>
    /// 转换成Byte
    /// </summary>
    /// <param name="s">输入字符串</param>
    /// <returns></returns>
    public static byte ToByte(this object s)
    {
        if (s == null || s == DBNull.Value)
            return 0;

        byte.TryParse(s.ToString(), out byte result);
        return result;
    }

    /// <summary>
    /// 转换成short/Int16
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static short ToShort(this object s)
    {
        if (s == null || s == DBNull.Value)
            return 0;

        short.TryParse(s.ToString(), out short result);
        return result;
    }

    /// <summary>
    /// 转换成Int/Int32
    /// </summary>
    /// <param name="s"></param>
    /// <param name="round">是否四舍五入，默认false</param>
    /// <returns></returns>
    public static int ToInt(this object s, bool round = false)
    {
        if (s == null || s == DBNull.Value)
            return 0;

        if (s.GetType().IsEnum)
        {
            return (int)s;
        }

        if (s is bool b)
            return b ? 1 : 0;

        if (int.TryParse(s.ToString(), out int result))
            return result;

        var f = s.ToFloat();
        return round ? Convert.ToInt32(f) : (int)f;
    }

    /// <summary>
    /// 转换成Long/Int64
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static long ToLong(this object s)
    {
        if (s == null || s == DBNull.Value)
            return 0L;

        long.TryParse(s.ToString(), out long result);
        return result;
    }

    /// <summary>
    /// 转换成Float/Single
    /// </summary>
    /// <param name="s"></param>
    /// <param name="decimals">小数位数</param>
    /// <returns></returns>
    public static float ToFloat(this object s, int? decimals = null)
    {
        if (s == null || s == DBNull.Value)
            return 0f;

        float.TryParse(s.ToString(), out float result);

        if (decimals == null)
            return result;

        return (float)Math.Round(result, decimals.Value);
    }

    /// <summary>
    /// 转换成Double/Single
    /// </summary>
    /// <param name="s"></param>
    /// <param name="digits">小数位数</param>
    /// <returns></returns>
    public static double ToDouble(this object s, int? digits = null)
    {
        if (s == null || s == DBNull.Value)
            return 0d;

        double.TryParse(s.ToString(), out double result);

        if (digits == null)
            return result;

        return Math.Round(result, digits.Value);
    }

    /// <summary>
    /// 转换成Decimal
    /// </summary>
    /// <param name="s"></param>
    /// <param name="decimals">小数位数</param>
    /// <returns></returns>
    public static decimal ToDecimal(this object s, int? decimals = null)
    {
        if (s == null || s == DBNull.Value) return 0m;

        decimal.TryParse(s.ToString(), out decimal result);

        if (decimals == null)
            return result;

        return Math.Round(result, decimals.Value);
    }

    /// <summary>
    /// 转换成DateTime
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static DateTime ToDateTime(this object s)
    {
        if (s == null || s == DBNull.Value)
            return DateTime.MinValue;

        DateTime.TryParse(s.ToString(), out DateTime result);
        return result;
    }

    /// <summary>
    /// 转换成Date
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static DateTime ToDate(this object s)
    {
        return s.ToDateTime().Date;
    }

    /// <summary>
    /// 转换成Boolean
    /// </summary>
    /// <param name="s"></param>
    /// <param name="宽松策略"></param>
    /// <returns></returns>
    public static bool? ToBool(this object s, bool 宽松策略 = false)
    {
        if (s == null) return null;
        if (宽松策略)
        {
            s = s.ToString().ToLower();
            if (s.Equals(1) || s.Equals("1") || s.Equals("true") || s.Equals("是") || s.Equals("yes"))
                return true;
            if (s.Equals(0) || s.Equals("0") || s.Equals("false") || s.Equals("否") || s.Equals("no"))
                return false;
        }

        if (!bool.TryParse(s.ToString(), out bool result))
        {
            return null;
        }
        return result;
    }



    /// <summary>
    /// 泛型转换，转换失败会抛出异常
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="s"></param>
    /// <returns></returns>
    public static T To<T>(this object s)
    {
        return (T)Convert.ChangeType(s, typeof(T));
    }
    public static bool IsBool<T>(this T defaultValue)
    {
        var x = typeof(T) == typeof(bool);
        return x;
    }
    #endregion

    #region ==布尔转换==

    /// <summary>
    /// 布尔值转换为字符串1或者0
    /// </summary>
    /// <param name="b"></param>
    /// <returns></returns>
    public static string ToIntString(this bool b)
    {
        return b ? "1" : "0";
    }

    /// <summary>
    /// 布尔值转换为整数1或者0
    /// </summary>
    /// <param name="b"></param>
    /// <returns></returns>
    public static int ToInt(this bool b)
    {
        return b ? 1 : 0;
    }

    /// <summary>
    /// 布尔值转换为中文
    /// </summary>
    /// <param name="b"></param>
    /// <returns></returns>
    public static string ToZhCn(this bool b)
    {
        return b ? "是" : "否";
    }

    #endregion

    #region ==字节转换==

    /// <summary>
    /// 转换为16进制
    /// </summary>
    /// <param name="bytes"></param>
    /// <param name="lowerCase">是否小写</param>
    /// <returns></returns>
    public static string ToHex(this byte[] bytes, bool lowerCase = true)
    {
        if (bytes == null)
            return null;

        var result = new StringBuilder();
        var format = lowerCase ? "x2" : "X2";
        for (var i = 0; i < bytes.Length; i++)
        {
            result.Append(bytes[i].ToString(format));
        }

        return result.ToString();
    }

    /// <summary>
    /// 16进制转字节数组
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static byte[] HexToBytes(this string s)
    {
        if (string.IsNullOrEmpty(s))
            return null;
        var bytes = new byte[s.Length / 2];

        for (int x = 0; x < s.Length / 2; x++)
        {
            int i = (Convert.ToInt32(s.Substring(x * 2, 2), 16));
            bytes[x] = (byte)i;
        }

        return bytes;
    }

    /// <summary>
    /// 转换为Base64
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    public static string ToBase64(this byte[] bytes)
    {
        if (bytes == null)
            return null;

        return Convert.ToBase64String(bytes);
    }

    #endregion
}
