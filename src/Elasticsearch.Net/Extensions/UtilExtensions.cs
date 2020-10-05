// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Elasticsearch.Net.Extensions
{
	internal static class UtilExtensions
	{
		internal static string Utf8String(this byte[] bytes) => bytes == null ? null : Encoding.UTF8.GetString(bytes, 0, bytes.Length);

		internal static string Utf8String(this MemoryStream ms)
		{
			if (ms is null)
				return null;

			if (!ms.TryGetBuffer(out var buffer) || buffer.Array is null)
				return Encoding.UTF8.GetString(ms.ToArray());

			return Encoding.UTF8.GetString(buffer.Array, buffer.Offset, buffer.Count);
		}

		internal static byte[] Utf8Bytes(this string s) => s.IsNullOrEmpty() ? null : Encoding.UTF8.GetBytes(s);

		internal static void ThrowIfEmpty<T>(this IEnumerable<T> @object, string parameterName)
		{
			var enumerated = @object == null ? null : (@object as T[] ?? @object.ToArray());
			enumerated.ThrowIfNull(parameterName);
			if (!enumerated!.Any())
				throw new ArgumentException("Argument can not be an empty collection", parameterName);
		}

		internal static bool HasAny<T>(this IEnumerable<T> list) => list != null && list.Any();

		internal static bool HasAny<T>(this IEnumerable<T> list, out T[] enumerated)
		{
			enumerated = list == null ? null : (list as T[] ?? list.ToArray());
			return enumerated.HasAny();
		}

		internal static void ThrowIfNull<T>(this T value, string name) where T : class
		{
			if (value == null)
				throw new ArgumentNullException(name);
		}

		internal static bool IsNullOrEmpty(this string value) => string.IsNullOrEmpty(value);

		internal static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> items, Func<T, TKey> property) =>
			items.GroupBy(property).Select(x => x.First());

	}
}
