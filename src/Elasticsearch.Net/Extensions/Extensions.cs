using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Elasticsearch.Net
{
	internal static class Extensions
	{
		private const long MillisecondsInAWeek = MillisecondsInADay * 7;
		private const long MillisecondsInADay = MillisecondsInAnHour * 24;
		private const long MillisecondsInAnHour = MillisecondsInAMinute * 60;
		private const long MillisecondsInAMinute = MillisecondsInASecond * 60;
		private const long MillisecondsInASecond = 1000;

		internal static string Utf8String(this byte[] bytes) => bytes == null ? null : Encoding.UTF8.GetString(bytes);
		internal static string Utf8String(this MemoryStream ms)
		{
			if (ms is null)
				return null;

			if (!ms.TryGetBuffer(out ArraySegment<byte> buffer) || buffer.Array is null)
				return Encoding.UTF8.GetString(ms.ToArray());

			return Encoding.UTF8.GetString(buffer.Array, buffer.Offset, buffer.Count);
		}

		internal static char[] Utf8CharArray(this MemoryStream ms)
		{
			if (ms is null)
				return null;

			if (!ms.TryGetBuffer(out ArraySegment<byte> buffer) || buffer.Array is null)
				return Encoding.UTF8.GetChars(ms.ToArray());

			return Encoding.UTF8.GetChars(buffer.Array, buffer.Offset, buffer.Count);
		}

		internal static byte[] Utf8Bytes(this string s) => s.IsNullOrEmpty() ? null : Encoding.UTF8.GetBytes(s);

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

		internal static bool HasAny<T>(this IEnumerable<T> list) => list != null && list.Any();

		internal static Exception AsAggregateOrFirst(this IEnumerable<Exception> exceptions)
		{
			var es = exceptions as Exception[] ?? exceptions?.ToArray();
			if (es == null || es.Length == 0) return null;

			return es.Length == 1 ? es[0] : new AggregateException(es);
		}

		internal static void ThrowIfNull<T>(this T value, string name)
		{
			if (value == null)
				throw new ArgumentNullException(name);
		}

		internal static bool IsNullOrEmpty(this string value) => string.IsNullOrEmpty(value);

		internal static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> items, Func<T, TKey> property) =>
			items.GroupBy(property).Select(x => x.First());

		internal static string ToTimeUnit(this TimeSpan timeSpan)
		{
			var ms = timeSpan.TotalMilliseconds;
			string interval;
			double factor = 0;

			if (ms >= MillisecondsInAWeek)
			{
				factor = ms / MillisecondsInAWeek;
				interval = "w";
			}
			else if (ms >= MillisecondsInADay)
			{
				factor = ms / MillisecondsInADay;
				interval = "d";
			}
			else if (ms >= MillisecondsInAnHour)
			{
				factor = ms / MillisecondsInAnHour;
				interval = "h";
			}
			else if (ms >= MillisecondsInAMinute)
			{
				factor = ms / MillisecondsInAMinute;
				interval = "m";
			}
			else if (ms >= MillisecondsInASecond)
			{
				factor = ms / MillisecondsInASecond;
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
