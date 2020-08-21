// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
