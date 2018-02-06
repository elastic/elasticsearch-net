using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Nest
{
	internal static class DotNetCoreTypeExtensions
	{
		internal static bool IsGeneric(this Type type)
		{
			return type.GetTypeInfo().IsGenericType;
		}

		internal static Assembly Assembly(this Type type)
		{
			return type.GetTypeInfo().Assembly;
		}

		internal static bool IsGenericDictionary(this Type type)
		{
			return type.GetInterfaces().Any(t =>
				t.IsGeneric() && (
				t.GetGenericTypeDefinition() == typeof(IDictionary<,>) ||
				t.GetGenericTypeDefinition() == typeof(IReadOnlyDictionary<,>)));
		}

		internal static bool TryGetGenericDictionaryArguments(this Type type, out Type[] genericArguments)
		{
			var genericDictionary = type.GetInterfaces().FirstOrDefault(t =>
				t.IsGeneric() && (
				t.GetGenericTypeDefinition() == typeof(IDictionary<,>) ||
				t.GetGenericTypeDefinition() == typeof(IReadOnlyDictionary<,>)));

			if (genericDictionary == null)
			{
				genericArguments = new Type[0];
				return false;
			}

			genericArguments = genericDictionary.GetGenericArguments();
			return true;
		}

		internal static bool IsValue(this Type type)
		{
			return type.GetTypeInfo().IsValueType;
		}

		internal static bool IsClass(this Type type)
		{
			return type.GetTypeInfo().IsClass;
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

		internal static bool IsAssignableFrom(this Type t, Type other) => t.GetTypeInfo().IsAssignableFrom(other.GetTypeInfo());

		internal static bool IsEnumType(this Type type)
		{
			return type.GetTypeInfo().IsEnum;
		}

		internal static IEnumerable<Type> GetInterfaces(this Type type)
		{
			return type.GetTypeInfo().ImplementedInterfaces;
		}

		internal static IEnumerable<TAttribute> GetAttributes<TAttribute>(this Type t)
			where TAttribute : Attribute
		{
			var attributes =  t.GetTypeInfo().GetCustomAttributes(typeof(TAttribute), true);
			return attributes.Cast<TAttribute>();
		}
	}
}
