using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Elasticsearch.Net
{
	internal static class DotNetCoreTypeExtensions
	{
		internal static bool IsGeneric(this Type type)
		{
			return type.GetTypeInfo().IsGenericType;
		}

		internal static bool AssignableFrom(this Type type, Type from)
		{
			return type.GetTypeInfo().IsAssignableFrom(from.GetTypeInfo());
		}

		internal static Assembly Assembly(this Type type)
		{
			return type.GetTypeInfo().Assembly;
		}

		internal static bool IsValue(this Type type)
		{
			return type.GetTypeInfo().IsValueType;
		}

		internal static TypeCode GetTypeCode(this Type type)
		{
			if (type == null)
				return TypeCode.Empty;
			else if (type == typeof(bool))
				return TypeCode.Boolean;
			else if (type == typeof(char))
				return TypeCode.Char;
			else if (type == typeof(sbyte))
				return TypeCode.SByte;
			else if (type == typeof(byte))
				return TypeCode.Byte;
			else if (type == typeof(short))
				return TypeCode.Int16;
			else if (type == typeof(ushort))
				return TypeCode.UInt16;
			else if (type == typeof(int))
				return TypeCode.Int32;
			else if (type == typeof(uint))
				return TypeCode.UInt32;
			else if (type == typeof(long))
				return TypeCode.Int64;
			else if (type == typeof(ulong))
				return TypeCode.UInt64;
			else if (type == typeof(float))
				return TypeCode.Single;
			else if (type == typeof(double))
				return TypeCode.Double;
			else if (type == typeof(decimal))
				return TypeCode.Decimal;
			else if (type == typeof(System.DateTime))
				return TypeCode.DateTime;
			else if (type == typeof(string))
				return TypeCode.String;
			else if (type.GetTypeInfo().IsEnum)
				return GetTypeCode(Enum.GetUnderlyingType(type));
			else
				return TypeCode.Object;
		}
	}
}
