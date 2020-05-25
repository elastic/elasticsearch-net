// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Tests.Framework.Extensions
{
	public static class TypeExtensions
	{
		internal static InterfaceMapping GetInterfaceMap(this Type t, Type interfaceType) =>
			t.GetTypeInfo().GetRuntimeInterfaceMap(interfaceType);

		internal static bool IsInterface(this Type t) => t.GetTypeInfo().IsInterface;

		internal static bool IsGenericType(this Type type) => type.GetTypeInfo().IsGenericType;

		internal static bool IsValueType(this Type type) => type.GetTypeInfo().IsValueType;

		internal static bool IsEnumType(this Type type) => type.GetTypeInfo().IsEnum;

		internal static Assembly Assembly(this Type type) => type.GetTypeInfo().Assembly;

		internal static bool IsClass(this Type type) => type.GetTypeInfo().IsClass;

		internal static bool IsAbstract(this Type type) => type.GetTypeInfo().IsAbstract;

		internal static Type BaseType(this Type type) => type.GetTypeInfo().BaseType;

		internal static bool IsSealed(this Type type) => type.GetTypeInfo().IsSealed;

		internal static bool IsVisible(this Type t) => t.GetTypeInfo().IsVisible;

		internal static bool IsPublic(this Type t) => t.GetTypeInfo().IsPublic;

		internal static IEnumerable<TAttribute> GetAttributes<TAttribute>(this Type t)
			where TAttribute : Attribute
		{
			var attributes = t.GetTypeInfo().GetCustomAttributes(typeof(TAttribute), true);
			return attributes.Cast<TAttribute>();
		}

		internal static IEnumerable<TAttribute> GetAttributes<TAttribute>(this MethodInfo m)
			where TAttribute : Attribute
		{
			var attributes = m.GetCustomAttributes(typeof(TAttribute), true);
			return attributes.Cast<TAttribute>();
		}
	}
}
