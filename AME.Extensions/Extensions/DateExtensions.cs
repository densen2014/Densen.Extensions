using System;

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

        /// <summary>
        /// 日期结束范围,带自定义时间
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="obj2"></param>
        /// <returns></returns>
        public static DateTime LastSecond(this DateTime obj, DateTime obj2) => new DateTime(obj.Year, obj.Month, obj.Day, obj2.Hour, obj2.Minute, obj2.Second);

    }

}
