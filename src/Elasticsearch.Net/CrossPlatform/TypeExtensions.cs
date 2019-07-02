using System;
using System.Reflection;

namespace Elasticsearch.Net.CrossPlatform
{
	internal static class DotNetCoreTypeExtensions
	{
		internal static bool IsGeneric(this Type type) => type.GetTypeInfo().IsGenericType;

		internal static Assembly Assembly(this Type type) => type.GetTypeInfo().Assembly;
	}
}
