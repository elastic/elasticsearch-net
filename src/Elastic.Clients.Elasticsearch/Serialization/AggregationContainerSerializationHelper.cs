using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using Elastic.Clients.Elasticsearch.Aggregations;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch
{
	internal static class SerializationConstants
	{
		public const byte Newline = (byte)'\n';
	}

	internal static class AggregationContainerSerializationHelper
	{
		public static AggregationContainer ReadContainer<T>(ref Utf8JsonReader reader, JsonSerializerOptions options) where T : AggregationBase
		{
			var variant = JsonSerializer.Deserialize<T?>(ref reader, options);

			var container = new AggregationContainer(variant);

			return container;
		}

		public static AggregationContainer ReadContainer<T>(string variantName, ref Utf8JsonReader reader, JsonSerializerOptions options) where T : AggregationBase
		{
			var variant = JsonSerializer.Deserialize<T?>(ref reader, options);

			//variant.Name = variantName;

			var container = new AggregationContainer(variant);

			//reader.Read();

			return container;
		}
	}
		
	internal static class UtilExtensions
	{
		private const long MillisecondsInAWeek = MillisecondsInADay * 7;
		private const long MillisecondsInADay = MillisecondsInAnHour * 24;
		private const long MillisecondsInAnHour = MillisecondsInAMinute * 60;
		private const long MillisecondsInAMinute = MillisecondsInASecond * 60;
		private const long MillisecondsInASecond = 1000;

		internal static string Utf8String(this byte[] bytes) => bytes == null ? null : Encoding.UTF8.GetString(bytes, 0, bytes.Length);

		internal static string Utf8String(this MemoryStream ms)
		{
			if (ms is null)
				return null;

			if (!ms.TryGetBuffer(out var buffer) || buffer.Array is null)
				return Encoding.UTF8.GetString(ms.ToArray());

			return Encoding.UTF8.GetString(buffer.Array, buffer.Offset, buffer.Count);
		}

		//internal static byte[] Utf8Bytes(this string s) => s.IsNullOrEmpty() ? null : Encoding.UTF8.GetBytes(s);

		//internal static void ThrowIfEmpty<T>(this IEnumerable<T> @object, string parameterName)
		//{
		//	var enumerated = @object == null ? null : (@object as T[] ?? @object.ToArray());
		//	enumerated.ThrowIfNull(parameterName);
		//	if (!enumerated!.Any())
		//		throw new ArgumentException("Argument can not be an empty collection", parameterName);
		//}

		//internal static bool HasAny<T>(this IEnumerable<T> list) => list != null && list.Any();

		//internal static bool HasAny<T>(this IEnumerable<T> list, out T[] enumerated)
		//{
		//	enumerated = list == null ? null : (list as T[] ?? list.ToArray());
		//	return enumerated.HasAny();
		//}

		//internal static Exception AsAggregateOrFirst(this IEnumerable<Exception> exceptions)
		//{
		//	var es = exceptions as Exception[] ?? exceptions?.ToArray();
		//	if (es == null || es.Length == 0)
		//		return null;

		//	return es.Length == 1 ? es[0] : new AggregateException(es);
		//}

		//internal static void ThrowIfNull<T>(this T value, string name) where T : class
		//{
		//	if (value == null)
		//		throw new ArgumentNullException(name);
		//}

		//internal static bool IsNullOrEmpty(this string value) => string.IsNullOrEmpty(value);

		//internal static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> items, Func<T, TKey> property) =>
		//	items.GroupBy(property).Select(x => x.First());

		//internal static string ToTimeUnit(this TimeSpan timeSpan)
		//{
		//	var ms = timeSpan.TotalMilliseconds;
		//	string interval;
		//	double factor;

		//	if (ms >= MillisecondsInAWeek)
		//	{
		//		factor = ms / MillisecondsInAWeek;
		//		interval = "w";
		//	}
		//	else if (ms >= MillisecondsInADay)
		//	{
		//		factor = ms / MillisecondsInADay;
		//		interval = "d";
		//	}
		//	else if (ms >= MillisecondsInAnHour)
		//	{
		//		factor = ms / MillisecondsInAnHour;
		//		interval = "h";
		//	}
		//	else if (ms >= MillisecondsInAMinute)
		//	{
		//		factor = ms / MillisecondsInAMinute;
		//		interval = "m";
		//	}
		//	else if (ms >= MillisecondsInASecond)
		//	{
		//		factor = ms / MillisecondsInASecond;
		//		interval = "s";
		//	}
		//	else
		//	{
		//		factor = ms;
		//		interval = "ms";
		//	}

		//	return factor.ToString("0.##", CultureInfo.InvariantCulture) + interval;
		//}
	}
}
