// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using System;

namespace AME.Extensions
{
    public class IsTypes
    {
        public static bool IsNumericType(Type o)
        {
            var Nullabletypeget = Nullable.GetUnderlyingType(o);
            o = Nullabletypeget ?? o;
            if (o.FullName.Contains("Enum"))
                return false;
            var typeget = Type.GetTypeCode(o);
            return typeget switch
            {
                TypeCode.Byte or TypeCode.SByte or TypeCode.UInt16 or TypeCode.UInt32 or TypeCode.UInt64 or TypeCode.Int16 or TypeCode.Int32 or TypeCode.Int64 or TypeCode.Decimal or TypeCode.Double or TypeCode.Single => true,
                _ => false,
            };
        }

        public static bool IsNumericTypeObj(object o)
        {
            var Nullabletypeget = Nullable.GetUnderlyingType(o.GetType());
            var typeget = Type.GetTypeCode(Nullabletypeget ?? o.GetType());
            return typeget switch
            {
                TypeCode.Byte or TypeCode.SByte or TypeCode.UInt16 or TypeCode.UInt32 or TypeCode.UInt64 or TypeCode.Int16 or TypeCode.Int32 or TypeCode.Int64 or TypeCode.Decimal or TypeCode.Double or TypeCode.Single => true,
                _ => false,
            };
        }

    }
}
