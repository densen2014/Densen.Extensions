using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace AME
{
    
    /// <summary>
    /// Enum 扩展方法
    /// </summary>
    public static class EnumExtensions
    {
       
        
        /// <summary>
        /// 通过文本名获取Enum枚举Type
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pre">默认 AME.Enums+</param>
        /// <param name="assemblyString">默认空 eg: AME.API</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Type GetTypeFromEnumsName(string name, string pre = "AME.Enums+", string assemblyString = null)
        {
            Type t1;
            if (string.IsNullOrEmpty(assemblyString))
            {
                t1 = Type.GetType(pre + name, false, true);
            }
            else
            {
                //eg:assemblyString: "AME.API"
                t1 = Assembly.Load(assemblyString).GetType(pre + name, false, true);
            }
            if (t1 == null) throw new ArgumentNullException(pre + name);
            return t1;
        }

        public static string[] GetNamesFromEnums(this Type enumType)
        {
            string[] t1 = Enum.GetNames(enumType);
            return t1;
        }

        public static string[] GetNamesFromEnumsName(string name, string pre = "AME.Enums+", string assemblyString = null)
        {
            string[] t1 = Enum.GetNames(GetTypeFromEnumsName(name, pre, assemblyString));
            return t1;
        }

        public static Array GetValuesFromEnumsName(string name, string pre = "AME.Enums+", string assemblyString = null)
        {
            Array t1 = Enum.GetValues(GetTypeFromEnumsName(name, pre, assemblyString));
            return t1;
        }

        public static IEnumerable<List<string>> GetEnumValueAndDescriptionsFromEnumsName(string name, string pre = "AME.Enums+", string assemblyString = null)
        {
            return GetEnumValueAndDescriptions(GetTypeFromEnumsName(name, pre, assemblyString));
        }

        /// <summary>
        /// 获取枚举的值和描述
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public static string GetEnumName(this Enum en) => en.GetDescription();

        /// <summary>
        /// 获取枚举的值和描述
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>

        public static string GetDescription(this Enum en)
        {
            Type temType = en.GetType();
            MemberInfo[] memberInfos = temType.GetMember(en.ToString());
            if (memberInfos != null && memberInfos.Length > 0)
            {
                object[] objs = memberInfos[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (objs != null && objs.Length > 0)
                {
                    return ((DescriptionAttribute)objs[0]).Description;
                }
            }
            return en.ToString();
        }


        /// <summary>
        /// Eunm转IEnumerable,备注,value, 无备注回落为name
        /// </summary>
        /// <param name="type">Typeof(你的Enum)</param>
        /// <returns>name, Description , value</returns>
        public static IEnumerable<List<string>> GetEnumValueAndDescriptions(this Type type)
        {
            var descs = new List<List<string>>();
            var values = Enum.GetValues(type);
            foreach (int value in values)
            {
                var name = Enum.GetName(type, value);
                var field = type.GetField(name);
                var fds = field.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (fds != null && fds.Length > 0)
                {
                    foreach (DescriptionAttribute fd in fds)
                    {
                        descs.Add(new List<string>() { name, fd.Description ?? name, value.ToString() });
                    }
                }
                else
                {
                    //无DescriptionAttribute回落
                    descs.Add(new List<string>() { name, name, value.ToString() });
                }
            }
            return descs;
        }

        /// <summary>
        /// 从描述属性中获取枚举 
        /// <para>Get Enum from Description attribute</para>
        /// <para>var panda = EnumEx.GetValueFromDescription&lt;Animal&gt;("Giant Panda");</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="description"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static T GetValueFromDescription<T>(string description) where T : Enum
        {
            foreach (var field in typeof(T).GetFields())
            {
                if (Attribute.GetCustomAttribute(field,
                typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }

            throw new ArgumentException("Not found.", nameof(description));
            // Or return default(T);
        }
    }

}
