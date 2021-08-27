using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel;

namespace AME
{
    public class ConventUtil
    {
        /// <summary>
        /// 对象转换成JSON
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ObjectToJSON2<T>(T obj)
        {
            // Framework 2.0 不支持
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            string result = string.Empty;
            using (MemoryStream ms = new MemoryStream())
            {
                serializer.WriteObject(ms, obj);
                ms.Position = 0;

                using (StreamReader read = new StreamReader(ms))
                {
                    result = read.ReadToEnd();
                }
            }
            return result;
        }

        /// <summary>
        /// Json转换成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonText"></param>
        /// <returns></returns>
        public static T JsonToObject2<T>(string jsonText)
        {
            // Framework 2.0 不支持
            DataContractJsonSerializer s = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonText));
            T obj = (T)s.ReadObject(ms);
            ms.Dispose();
            return obj;


        }

        public static void TaskMethod2(object dd, bool ThreadInfo = true)
        {

            Console.WriteLine(dd);
            if (ThreadInfo == false) return;
            Console.WriteLine("Task id :{0},Thread :{1}", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("Is pool thread :{0}", Thread.CurrentThread.IsThreadPoolThread);
            Console.WriteLine("Is background thread:{0}", Thread.CurrentThread.IsBackground);
            Console.WriteLine("");
        }

        // 从一个对象信息生成Json串
        public static string ObjectToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
        // 从一个Json串生成对象信息
        public static object JsonToObject(string jsonString, object obj)
        {
            return JsonConvert.DeserializeObject(jsonString, obj.GetType());
        }

        public static IEnumerable<string> GetEnumDescriptions(Enum value)
        {
            var descs = new List<string>();
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            var field = type.GetField(name);
            var fds = field.GetCustomAttributes(typeof(DescriptionAttribute), true);
            foreach (DescriptionAttribute fd in fds)
            {
                descs.Add(fd.Description);
            }
            return descs;
        }

        /// <summary>
        /// Eunm转IEnumerable,备注也一起获取
        /// </summary>
        /// <param name="type">Typeof(你的Enum)</param>
        /// <returns></returns>
        public static IEnumerable<List<string>> GetEnumValueAndDescriptions(Type type)
        {
            var descs = new List<List<string>>();
            var names = Enum.GetNames(type);
            foreach (var name in names)
            {
                var field = type.GetField(name);
                var fds = field.GetCustomAttributes(typeof(DescriptionAttribute), true);
                foreach (DescriptionAttribute fd in fds)
                {
                    descs.Add(new List<string>() { name, fd.Description ?? name });
                }
            }
            return descs;
        }

        /// <summary>  
        /// 获取一个命名空间下的所有类  
        /// </summary>
        /// <param name="amespaceName"></param>
        /// <param name="assemblyString"></param>   
        /// <returns></returns>  
        public static List<Type> GetTypes(string amespaceName = "AME.Models.Entity.",string assemblyString=null)
        {
            List<Type> lt = new List<Type>();
            try
            {
                var lists = assemblyString == null ?
                    System.Reflection.Assembly.GetExecutingAssembly().GetTypes().Where(a => a.IsClass == true && a.FullName.StartsWith(amespaceName)).ToList() :
                    System.Reflection.Assembly.Load(assemblyString).GetTypes().Where(a => a.IsClass == true && a.FullName.StartsWith(amespaceName)).ToList();
                lt.AddRange(lists);
            }
            catch { }
            return lt;
        }

        public static void LoadInfoByAssembly()
        {
            var query = from t in System.Reflection.Assembly.Load("AME.Models.Entity").GetTypes()
                        where t.IsClass && t.Namespace.Equals("AssemblyDemo.Business.Security", StringComparison.InvariantCultureIgnoreCase)
                        select t;

            query.ToList().ForEach(x =>
            {
                Console.WriteLine("类命名:{0}", x.Name);
                foreach (var field in x.GetFields())
                {
                    Console.WriteLine("字段名称:{0},字段值:{1}", field.Name, field.GetValue(field.Name));
                }
                Console.WriteLine("========================================\n");

            });
        }
        public static bool IsNumeric(object anyObject)
        {
            try
            {
                if (anyObject == null || anyObject is DBNull) return false;
                string anyString = anyObject.ToString();
                if (anyString.Length > 0)
                {
                    double dummyOut = new double();
                    System.Globalization.CultureInfo cultureInfo =
                        new System.Globalization.CultureInfo("en-US", true);

                    return double.TryParse(anyString, System.Globalization.NumberStyles.Any,
                        cultureInfo.NumberFormat, out dummyOut);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

        }

        public static string passwordhash(string password)
        {
            string result;
            using (var cryptoProvider = System.Security.Cryptography.SHA1.Create())
            {
                byte[] passwordHash = cryptoProvider.ComputeHash(Encoding.UTF8.GetBytes(password));
                result = "new byte[] { " +
                    String.Join(",", passwordHash.Select(x => "0x" + x.ToString("x2")).ToArray())
                     + " } ";
            }
            return result;
        }


    }
}
