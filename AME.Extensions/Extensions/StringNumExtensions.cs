using System;

namespace AME
{
    public static partial class StringExtensions
    {

        /// <summary>
        /// String转Decimal
        /// </summary>
        /// <param name="t"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this string t, decimal defaultValue = 0m)
        {
            try
            {
                var x = t.IsNumeric() ? Convert.ToDecimal(t) : defaultValue;
                return x;
            }
            catch
            {
            }
            return defaultValue;
        }
        public static double ToDouble(this string t, double defaultValue = 0d)
        {
            try
            {
                var x = t.IsNumeric() ? Convert.ToDouble(t) : defaultValue;
                return x;
            }
            catch
            {
            }
            return defaultValue;
        }
    }
}