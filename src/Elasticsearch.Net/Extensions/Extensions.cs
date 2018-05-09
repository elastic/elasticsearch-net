using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Elasticsearch.Net
{
	internal static class Extensions
	{
		internal static string Utf8String(this byte[] bytes) => bytes == null ? null : Encoding.UTF8.GetString(bytes, 0, bytes.Length);

		internal static byte[] Utf8Bytes(this string s)
		{
			return s.IsNullOrEmpty() ? null : Encoding.UTF8.GetBytes(s);
		}

		internal static string ToCamelCase(this string s)
		{
			if (string.IsNullOrEmpty(s))
				return s;

			if (!char.IsUpper(s[0]))
				return s;

			string camelCase = char.ToLowerInvariant(s[0]).ToString();
			if (s.Length > 1)
				camelCase += s.Substring(1);

			return camelCase;
		}

		internal static string NotNull(this string @object, string parameterName)
		{
			@object.ThrowIfNull(parameterName);
			if (string.IsNullOrWhiteSpace(@object))
				throw new ArgumentException("String argument is empty", parameterName);
			return @object;
		}

		internal static string NotNull(this Enum @object, string parameterName)
		{
			@object.ThrowIfNull(parameterName);
			return @object.GetStringValue();
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

		internal static Exception AsAggregateOrFirst(this IEnumerable<Exception> exceptions)
		{
			var es= exceptions as Exception[] ?? exceptions?.ToArray();
			if (exceptions == null || es .Any()) return null;
			return es.Length == 1 ? es[0] : new AggregateException(es);
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

		private static readonly long _week = (long)TimeSpan.FromDays(7).TotalMilliseconds;
		private static readonly long _day = (long)TimeSpan.FromDays(1).TotalMilliseconds;
		private static readonly long _hour = (long)TimeSpan.FromHours(1).TotalMilliseconds;
		private static readonly long _minute = (long)TimeSpan.FromMinutes(1).TotalMilliseconds;
		private static readonly long _second = (long)TimeSpan.FromSeconds(1).TotalMilliseconds;

		internal static string ToTimeUnit(this TimeSpan timeSpan)
		{
			var ms = timeSpan.TotalMilliseconds;
			string interval;
			double factor = 0;

			if (ms >= _week)
			{
				factor = ms / _week;
				interval = "w";
			}
			else if (ms >= _day)
			{
				factor = ms / _day;
				interval = "d";
			}
			else if (ms >= _hour)
			{
				factor = ms / _hour;
				interval = "h";
			}
			else if (ms >= _minute)
			{
				factor = ms / _minute;
				interval = "m";
			}
			else if (ms >= _second)
			{
				factor = ms / _second;
				interval = "s";
			}
			else
			{
				factor = ms;
				interval = "ms";
			}

			return factor.ToString("0.##", CultureInfo.InvariantCulture) + interval;
		}

	}
}
