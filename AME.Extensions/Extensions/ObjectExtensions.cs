using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace AME
{
    /// <summary>
    /// Object 扩展方法
    /// </summary>
    public static partial  class ObjectExtensions
    {
        /// <summary>
        /// 泛型 Clone 方法
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public static TItem Clone<TItem>(this TItem item)
        {
            var ret = item;
            if (item != null)
            {
                var type = item.GetType();
                if (typeof(ICloneable).IsAssignableFrom(type))
                {
                    var clv = type.GetMethod("Clone")?.Invoke(type, null);
                    if (clv != null)
                    {
                        ret = (TItem)clv;
                    }
                }
                if (type.IsClass)
                {
                    ret = Activator.CreateInstance<TItem>();
                    var valType = ret?.GetType();
                    if (valType != null)
                    {
                        // 20200608 tian_teng@outlook.com 支持字段和只读属性
                        type.GetFields().ToList().ForEach(f =>
                        {
                            var v = f.GetValue(item);
                            valType.GetField(f.Name)?.SetValue(ret, v);
                        });
                        type.GetProperties().ToList().ForEach(p =>
                        {
                            if (p.CanWrite)
                            {
                                var v = p.GetValue(item);
                                valType.GetProperty(p.Name)?.SetValue(ret, v);
                            }
                        });
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// 重置对象属性值到默认值方法
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        public static void Reset<TItem>(this TItem source) where TItem : class, new()
        {
            var v = new TItem();
            foreach (var pi in source.GetType().GetProperties())
            {
                pi.SetValue(source, v.GetType().GetProperty(pi.Name).GetValue(v));
            }
        }

        static JsonSerializerSettings setting2 = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.Indented
        };
        
        /// <summary>
        /// 从一个Json生成美化的String格式
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ObjectToJsonIndented(this object obj)
        {
            return JsonConvert.SerializeObject(obj, setting2);
        }

        /// <summary>
        /// 从一个对象信息生成Json串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ObjectToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// 从一个Json串生成对象信息
        /// </summary>
        /// <param name="jsonString"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static object JsonToObject(this string jsonString, object obj)
        {
            return JsonConvert.DeserializeObject(jsonString, obj.GetType());
        }

        public static string ToString2<T>(this List<T> l)
        {
            string retVal = string.Empty;
            foreach (T item in l)
                retVal += string.Format("{0}{1}", string.IsNullOrEmpty(retVal) ?
                                                            "" : ", ",
                                         item);
            return string.IsNullOrEmpty(retVal) ? "{}" : "{ " + retVal + " }";
        }

        public static int ToInt(this object obj)
        {
            try
            {
                return obj.IsNum() ? Convert.ToInt32(obj) : 0;
            }
            catch (Exception)
            {
                return obj.IsNum() ? (int) Convert.ToDouble(obj) : 0;
            }
        }
        public static string Format2Num(this object obj, int PadLeft=8, int num=2)
        { 
            return obj.IsNum() ? Convert.ToDecimal(obj).ToString($"n{num}").PadLeft(PadLeft, ' ') : "".PadLeft(PadLeft, ' ');
        } 
        public static decimal ToDecimal(this object obj)
        {
            return obj.IsNum() ? Convert.ToDecimal(obj) : 0;
        } 
        public static double ToDbl(this object obj)
        {
            return obj.IsNum() ? Convert.ToDouble(obj) : 0;
        }
        public static bool IsNum(this object t) => double.TryParse(t.ToString(), out _);

        /// <summary>
        /// 检查是否为 Number 数据类型
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool IsNumber(this Type t)
        {
            var targetType = Nullable.GetUnderlyingType(t) ?? t;
            var check = targetType == typeof(byte) ||
                targetType == typeof(sbyte) ||
                targetType == typeof(int) ||
                targetType == typeof(long) ||
                targetType == typeof(short) ||
                targetType == typeof(float) ||
                targetType == typeof(double) ||
                targetType == typeof(decimal);
            return check;
        }

        /// <summary>
        /// 检查是否为 DateTime 数据类型
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool IsDateTime(this Type t)
        {
            var targetType = Nullable.GetUnderlyingType(t) ?? t;
            var check = targetType == typeof(DateTime) ||
               targetType == typeof(DateTimeOffset);
            return check;
        }


        /// <summary>
        /// 获取属性的displayName/获取绑定字段显示名称方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Dictionary<string, decimal?> GetDisplayNameDic<T>(T t)
        {
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();
            Dictionary<string, decimal?> dic = new Dictionary<string, decimal?>();
            foreach (var p in properties)
            {
                //display名字
                var name = p.GetCustomAttribute<DisplayNameAttribute>().DisplayName;
                //对应的值
                var value = t.GetType().GetProperty(p.Name).GetValue(t, null);
                dic.Add(name, Convert.ToDecimal(value));
            }
            return dic;
        }

        /// <summary>
        /// 获取属性的displayName/获取绑定字段显示名称方法
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <returns></returns>
        public static string GetDisplayNameII<TItem>(this TItem source, string pname, string defaultValue = "")
        {
            Type type = typeof(TItem);
            PropertyInfo[] properties = type.GetProperties();
            foreach (var p in properties.Where(a => a.Name == pname))
            {
                var pAttribute = p.GetCustomAttribute<DisplayNameAttribute>();
                //display名字
                var name = p.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? defaultValue;
                return name;
            }
            return defaultValue;
        }


        /// <summary>
        /// 利用反射来判断对象是否包含某个属性
        /// </summary>
        /// <param name="instance">object</param>
        /// <param name="propertyName">需要判断的属性</param>
        /// <returns>是否包含</returns>
        /// <example>bool cc = _person.ContainProperty("cc");</example>
        public static bool ContainProperty(this object instance, string propertyName)
        {
            if (instance != null && !string.IsNullOrEmpty(propertyName))
            {
                PropertyInfo _findedPropertyInfo = instance.GetType().GetProperty(propertyName);
                return (_findedPropertyInfo != null);
            }
            return false;
        }

        /// <summary>
        /// 获取属性的主键,反射获取属性值
        /// </summary>
        /// <param name="instance">object</param>
        /// <param name="propertyName">需要判断的属性</param> 
        /// <returns>是否包含</returns>
        public static object GetIdentityKey<TItem>(this TItem instance, string propertyName)
        {
            try
            {
                if (instance != null && !string.IsNullOrEmpty(propertyName))
                {
                    var propertyInfo = instance.GetType().GetProperty(propertyName);
                    var value = propertyInfo.GetValue(instance, null);
                    return value;
                }
            }
            finally { 
            }
            return null;
        }

        /// <summary>
        /// 创建属性
        /// </summary>
        /// <param name="instance">object</param>
        /// <param name="propertyName">需要判断的属性</param>
        /// <param name="value"></param> 
        /// <returns>是否包含</returns>
        public static object FieldSetValue<TItem>(this TItem instance, string propertyName,object value)
        {

            if (instance != null && !string.IsNullOrEmpty(propertyName))
            {
                var propertyInfo = instance.GetType().GetProperty(propertyName);
                propertyInfo.SetValue(instance, value); 
                return value;
            }
            return null;
        }

        /// <summary>
        /// 获取属性
        /// </summary>
        /// <param name="instance">object</param>
        /// <param name="propertyName">需要判断的属性</param>
        /// <returns>是否包含</returns>
        public static object GetField<TItem>(this TItem instance, string propertyName)
        {

            if (instance != null && !string.IsNullOrEmpty(propertyName))
            {
                var propertyInfo = instance.GetType().GetProperty(propertyName);
                return propertyInfo;
            }
            return null;
        }


        //扩展方法必须是静态的，第一个参数必须加上this  
        public static bool IsEmail(this string _input)
        {
            return Regex.IsMatch(_input, @"^\\w+@\\w+\\.\\w+$");
        }
        //带多个参数的扩展方法  
        //在原始字符串前后加上指定的字符  
        public static string Quot(this string _input, string _quot)
        {
            return _quot + _input + _quot;
        }

        public static string AddNewLineText(this string _input, string _text, bool addNewLine = true, string _quot="" ,[CallerMemberName] string propertyName = "")
        {
            _input += $"{(addNewLine ? System.Environment.NewLine : "")}{_text}{_quot}";
            return _input;
        }


    }


    public static partial class ObjectUtils
    {
        public static void AddNewLine(ref string _input, string _text, bool addNewLine = true)
        {
            if (!string.IsNullOrEmpty(_text)) _input += $"{(addNewLine ? System.Environment.NewLine : "")}{_text}";
        }
        public static void AddNewBlankLine(ref string _input)
        {
           _input += System.Environment.NewLine;
        }
    }

    public static partial class StringExt
    {
        public static bool IsNumeric(this string text) => double.TryParse(text, out _);

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
