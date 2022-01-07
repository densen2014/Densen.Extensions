using System;
using System.Collections.Generic;
using System.Text;

namespace AME.Extensions
{
	public class IsTypes
    {
		public static bool IsNumericType(Type o)
		{
			var Nullabletypeget = Nullable.GetUnderlyingType(o);
			o = Nullabletypeget ?? o;
			if (o.FullName.Contains ("Enum")) 
				return false;
			var typeget = Type.GetTypeCode(o);
			switch (typeget)
			{
				case TypeCode.Byte:
				case TypeCode.SByte:
				case TypeCode.UInt16:
				case TypeCode.UInt32:
				case TypeCode.UInt64:
				case TypeCode.Int16:
				case TypeCode.Int32:
				case TypeCode.Int64:
				case TypeCode.Decimal:
				case TypeCode.Double:
				case TypeCode.Single:
					return true;
				default:
					return false;
			}

		}

		public static bool IsNumericTypeObj(object o)
		{
			var Nullabletypeget = Nullable.GetUnderlyingType(o.GetType());
			var typeget = Type.GetTypeCode(Nullabletypeget??o.GetType());
			switch (typeget)
			{
				case TypeCode.Byte:
				case TypeCode.SByte:
				case TypeCode.UInt16:
				case TypeCode.UInt32:
				case TypeCode.UInt64:
				case TypeCode.Int16:
				case TypeCode.Int32:
				case TypeCode.Int64:
				case TypeCode.Decimal:
				case TypeCode.Double:
				case TypeCode.Single:
					return true;
				default:
					return false;
			}

		}

	}
}
