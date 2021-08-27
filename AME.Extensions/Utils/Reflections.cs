// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using System;
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
                object[] parameters1 = new object[] { 值 };
                //使用参数调用该方法

                generic.Invoke(tmp_Class, parameters1);
            }
            finally
            {
            }


        }

 
    }
}
