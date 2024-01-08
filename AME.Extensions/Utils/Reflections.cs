// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AME.Extensions
{

    public class Reflections
    {

        public static object 通过反射获取设置属性值(object tmp_Class, string 属性, object 值, bool 设置 = true)
        {
            Type type = tmp_Class.GetType();
            //获取类型
            System.Reflection.PropertyInfo propertyInfo = type.GetProperty(属性);
            if (设置)
            {
                //给对应属性赋值
                propertyInfo.SetValue(tmp_Class, 值, null);
            }
            else
            {
                //获取指定名称的属性
                object value_Old = propertyInfo.GetValue(tmp_Class, null);
                //获取属性值
                //Console.WriteLine(value_Old)
                return value_Old;
            }
            object value_New = propertyInfo.GetValue(tmp_Class, null);
            //Console.WriteLine(value_New)
            return value_New;
        }


        public static void 通过反射调用方法(object tmp_Class, string 方法, object 值 = null)
        {

            try
            {
                //调用MakeGenericMethod创建具有适当类型的泛型方法
                MethodInfo generic = tmp_Class.GetType().GetMethod(方法);
                if (值 == null)
                {
                    generic.Invoke(tmp_Class, null);
                    return;
                }
                //参数
                object[] parameters1 = [值];
                //使用参数调用该方法

                generic.Invoke(tmp_Class, parameters1);
            }
            finally
            {
            }


        }



        /// <summary>  
        /// 获取一个命名空间下的所有类  
        /// </summary>
        /// <param name="amespaceName"></param>
        /// <param name="assemblyString"></param>   
        /// <returns></returns>  
        public static List<Type> GetTypes(string amespaceName = "AME.Models.Entity.", string assemblyString = null) //"AME.Models.Entity.", "AME.API"
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
    }
}
