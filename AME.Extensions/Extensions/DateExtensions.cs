using System;
using System.Globalization;

namespace AME
{
    /// <summary>
    /// Date 扩展方法
    /// </summary>


    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime dateTime)
        {
            int dayOfWeek = (int)dateTime.DayOfWeek;
            dayOfWeek = dayOfWeek == 0 ? 7 : dayOfWeek;
            DateTime startOfWeek = dateTime.AddDays(1 - dayOfWeek);

            return startOfWeek;
        }

        // returns the number of milliseconds since Jan 1, 1970 (useful for converting C# dates to JS dates)
        public static double ToUnixTimeStamp(this DateTime dateTime)
        {
            return dateTime
               .Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc))
               .TotalMilliseconds;
        }

        public static string ToStringDate(this DateTime obj)=> obj.ToString("yyyy-MM-dd");
        public static string ToStringTime(this DateTime obj)=> obj.ToString("HH:mm:ss");
        public static string ToStringLong(this DateTime obj)=> obj.ToString("yyyy-MM-dd HH:mm:ss");
        public static DateTime FirstDay(this DateTime obj)=> new DateTime(obj.Year, obj.Month, 1, 0, 0, 0);
        
        public static DateTime LastDay(this DateTime obj)=>  obj.FirstDay().AddMonths(1).AddDays(-1).LastSecond();
        public static DateTime LastDayThisYear(this DateTime obj)=> new DateTime(obj.Date.Year, 12, 31);

        /// <summary>
        /// 日期开始范围
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DateTime FirstSecond(this DateTime obj) => new DateTime(obj.Year, obj.Month, obj.Day, 0, 0, 0);
        public static string FirstSecondString(this DateTime obj) => FirstSecond(obj).ToString("yyyy-MM-dd HH:mm:ss");

        /// <summary>
        /// 日期开始范围,带自定义时间
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="obj2"></param>
        /// <returns></returns>
        public static DateTime FirstSecond(this DateTime obj, DateTime obj2) => new DateTime(obj.Year, obj.Month, obj.Day, obj2.Hour , obj2.Minute , obj2.Second);

        /// <summary>
        /// 日期结束范围 23:59:59
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DateTime LastSecond(this DateTime obj) => new DateTime(obj.Year, obj.Month, obj.Day, 23, 59, 59);
        public static string LastSecondString(this DateTime obj) => LastSecond(obj).ToString("yyyy-MM-dd HH:mm:ss");

        /// <summary>
        /// 日期结束范围,带自定义时间
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="obj2"></param>
        /// <returns></returns>
        public static DateTime LastSecond(this DateTime obj, DateTime obj2) => new DateTime(obj.Year, obj.Month, obj.Day, obj2.Hour, obj2.Minute, obj2.Second);

        /// <summary>
        /// 将日期转换成yyyy-MM-dd HH:mm:ss字符串
        /// </summary>
        public static string TransformDataLong(this DateTime? dateTime)
        {
            string result = "";
            if (dateTime.HasValue)
            {
                result = dateTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
            }
            return result;
        }

        /// <summary>
        /// 将日期转换成yyyy-MM-dd字符串
        /// </summary>
        public static string TransformDataShort(this DateTime? dateTime)
        {
            string result = "";
            if (dateTime.HasValue)
            {
                result = dateTime.Value.ToString("yyyy-MM-dd");
            }
            return result;
        }

        /// <summary>
        /// 将日期转换成decimal
        /// </summary>
        public static decimal TransDateTimeToDecimal(this DateTime date)
        {
            decimal ret = 0;
            ret = Convert.ToDecimal(date.ToString("yyyyMMddHHmmss"));
            return ret;
        }

        /// <summary>
        ///  字符串日期转DateTime
        /// </summary>
        public static DateTime TransStrToDateTime(this string strDateTime)
        {
            DateTime now;
            string[] format = new string[]
            {
            "yyyyMMddHHmmss", "yyyy-MM-dd HH:mm:ss", "yyyy年MM月dd日 HH时mm分ss秒",
            "yyyyMdHHmmss","yyyy年M月d日 H时mm分ss秒", "yyyy.M.d H:mm:ss", "yyyy.MM.dd HH:mm:ss","yyyy-MM-dd","yyyyMMdd"
            ,"yyyy/MM/dd","yyyy/M/d","ddMMyyyyHHmmss","dd/MM/yyyy","dd-MM-yyyy","dd-MM-yyyy HH:mm:ss","dd.MM.yyyy HH:mm:ss","dd/MM/yyyy "
            };
            if (DateTime.TryParseExact(strDateTime, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out now))
            {
                return now;
            }
            return DateTime.MinValue;
        }



        /// <summary>
        /// 将decimal转换成日期格式
        /// </summary>
        /// <param name="date">yyyyMMddHHmmss</param>
        /// <returns>yyyy-MM-dd HH:mm:ss</returns>
        public static string TransDecimalToDateTime(this string date)
        {
            DateTimeFormatInfo dtfi = new CultureInfo("zh-CN", false).DateTimeFormat;
            DateTime dateTime = DateTime.Now;
            DateTime.TryParseExact(date, "yyyyMMddHHmmss", dtfi, DateTimeStyles.None, out dateTime);
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss"); ;
        }
    }

}
