using System;
using System.Threading;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace Tests.Framework
{
	public static class TypeExtensions
	{
#if DOTNETCORE
		internal static InterfaceMapping GetInterfaceMap(this Type t, Type interfaceType) =>
			t.GetTypeInfo().GetRuntimeInterfaceMap(interfaceType);
#endif

		internal static bool IsInterface(this Type t)
		{
#if DOTNETCORE
			return t.GetTypeInfo().IsInterface;
#else
			return t.IsInterface;
#endif
		}

		internal static bool IsGenericType(this Type type)
		{
#if DOTNETCORE
			return type.GetTypeInfo().IsGenericType;
#else
			return type.IsGenericType;
#endif
		}

		internal static bool IsValueType(this Type type)
		{
#if DOTNETCORE
			return type.GetTypeInfo().IsValueType;
#else
			return type.IsValueType;
#endif
		}

		internal static bool IsEnumType(this Type type)
		{
#if DOTNETCORE
			return type.GetTypeInfo().IsEnum;
#else
			return type.IsEnum;
#endif
		}

		internal static Assembly Assembly(this Type type)
		{
#if DOTNETCORE
			return type.GetTypeInfo().Assembly;
#else
			return type.Assembly;
#endif
		}

		internal static bool IsClass(this Type type)
		{
#if DOTNETCORE
			return type.GetTypeInfo().IsClass;
#else
			return type.IsClass;
#endif
		}

		internal static bool IsAbstract(this Type type)
		{
#if DOTNETCORE
			return type.GetTypeInfo().IsAbstract;
#else
			return type.IsAbstract;
#endif
		}

		internal static Type BaseType(this Type type)
		{
#if DOTNETCORE
			return type.GetTypeInfo().BaseType;
#else
			return type.BaseType;
#endif
		}

		internal static bool IsSealed(this Type type)
		{
#if DOTNETCORE
			return type.GetTypeInfo().IsSealed;
#else
			return type.IsSealed;
#endif
		}

		internal static bool IsVisible(this Type t)
		{
#if DOTNETCORE
			return t.GetTypeInfo().IsVisible;
#else
			return t.IsVisible;
#endif
		}

		internal static bool IsPublic(this Type t)
		{
#if DOTNETCORE
			return t.GetTypeInfo().IsPublic;
#else
			return t.IsPublic;
#endif
		}

		internal static IEnumerable<TAttribute> GetAttributes<TAttribute>(this Type t)
			where TAttribute : Attribute
		{
#if !DOTNETCORE
			var attributes = Attribute.GetCustomAttributes(t, typeof(TAttribute), true);
#else
			var attributes =  t.GetTypeInfo().GetCustomAttributes(typeof(TAttribute), true);
#endif
			return attributes.Cast<TAttribute>();
		}
	}
}
