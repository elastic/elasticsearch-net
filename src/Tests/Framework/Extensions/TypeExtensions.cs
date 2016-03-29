using System;
using System.Threading;
using System.Reflection;
using System.Collections.Generic;

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
		
		internal static bool IsGeneric(this Type type)
		{
#if DOTNETCORE
			return type.GetTypeInfo().IsGenericType;
#else
			return type.IsGenericType;
#endif
		}

		internal static bool IsAbstractClass(this Type type)
		{
#if DOTNETCORE
			return type.GetTypeInfo().IsAbstract;
#else
			return type.IsAbstract;
#endif
		}

		internal static bool IsValue(this Type type)
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



	}
}