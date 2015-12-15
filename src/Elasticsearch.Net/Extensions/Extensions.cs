using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Elasticsearch.Net
{
	internal static class Extensions
	{
		internal static string GetStringValue(this Enum enumValue)
		{
			var knownEnum = KnownEnums.Resolve(enumValue);
			if (knownEnum != KnownEnums.UnknownEnum) return knownEnum;

			//TODO measure performance and cache 
			var type = enumValue.GetType();
			var info = type.GetField(enumValue.ToString());
			var da = (EnumMemberAttribute[])(info.GetCustomAttributes(typeof(EnumMemberAttribute), false));

			return da.Length > 0 ? da[0].Value : Enum.GetName(enumValue.GetType(), enumValue);
		}

		internal static string Utf8String(this byte[] bytes) => bytes == null ? null : Encoding.UTF8.GetString(bytes);

		internal static byte[] Utf8Bytes(this string s) => s.IsNullOrEmpty() ? null : Encoding.UTF8.GetBytes(s);

		internal static string NotNull(this string @object, string parameterName)
		{
			@object.ThrowIfNull(parameterName);
			if (string.IsNullOrWhiteSpace(@object))
				throw new ArgumentException("String argument is empty", parameterName);
			return @object;
		}

		internal static void ThrowIfEmpty<T>(this IEnumerable<T> @object, string parameterName)
		{
			@object.ThrowIfNull(parameterName);
			if (!@object.Any())
				throw new ArgumentException("Argument can not be an empty collection", parameterName);
		}
		internal static bool HasAny<T>(this IEnumerable<T> list)
		{
			return list != null && list.Any();
		}

		internal static void ThrowIfNull<T>(this T value, string name)
		{
			if (value == null)
				throw new ArgumentNullException(name);
		}
		internal static bool IsNullOrEmpty(this string value)
		{
			return string.IsNullOrEmpty(value);
		}
		
		internal static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> items, Func<T, TKey> property)
		{
			return items.GroupBy(property).Select(x => x.First());
		}
	}
}
