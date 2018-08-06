using System;
using System.Threading;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace Tests.Framework
{
	public static class TypeExtensions
	{
		internal static InterfaceMapping GetInterfaceMap(this Type t, Type interfaceType) =>
			t.GetTypeInfo().GetRuntimeInterfaceMap(interfaceType);

		internal static bool IsInterface(this Type t)
		{
			return t.GetTypeInfo().IsInterface;
		}

		internal static bool IsGenericType(this Type type)
		{
			return type.GetTypeInfo().IsGenericType;
		}

		internal static bool IsValueType(this Type type)
		{
			return type.GetTypeInfo().IsValueType;
		}

		internal static bool IsEnumType(this Type type)
		{
			return type.GetTypeInfo().IsEnum;
		}

		internal static Assembly Assembly(this Type type)
		{
			return type.GetTypeInfo().Assembly;
		}

		internal static bool IsClass(this Type type)
		{
			return type.GetTypeInfo().IsClass;
		}

		internal static bool IsAbstract(this Type type)
		{
			return type.GetTypeInfo().IsAbstract;
		}

		internal static Type BaseType(this Type type)
		{
			return type.GetTypeInfo().BaseType;
		}

		internal static bool IsSealed(this Type type)
		{
			return type.GetTypeInfo().IsSealed;
		}

		internal static bool IsVisible(this Type t)
		{
			return t.GetTypeInfo().IsVisible;
		}

		internal static bool IsPublic(this Type t)
		{
			return t.GetTypeInfo().IsPublic;
		}

		internal static IEnumerable<TAttribute> GetAttributes<TAttribute>(this Type t)
			where TAttribute : Attribute
		{
			var attributes =  t.GetTypeInfo().GetCustomAttributes(typeof(TAttribute), true);
			return attributes.Cast<TAttribute>();
		}
		internal static IEnumerable<TAttribute> GetAttributes<TAttribute>(this MethodInfo m)
			where TAttribute : Attribute
		{
			var attributes =  m.GetCustomAttributes(typeof(TAttribute), true);
			return attributes.Cast<TAttribute>();
		}
	}
}
