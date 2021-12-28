using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Elastic.Clients.Elasticsearch.Aggregations;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch
{
	internal sealed class TestAttribute : Attribute { }

	internal sealed class ResolvableDictionaryFormatterFactory : JsonConverterFactory
	{
		public override bool CanConvert(Type typeToConvert) => false;

		

		public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
	}

	//internal sealed class ReadOnlyDictionaryConverter

	internal sealed class MyConverter : JsonConverterAttribute
	{
		public MyConverter(Type valueType) => ValueType = valueType;

		public Type ValueType { get; }

		public override JsonConverter? CreateConverter(Type typeToConvert) => (JsonConverter)Activator.CreateInstance(typeof(IntermediateConverter<>).MakeGenericType(ValueType));
	}

	internal sealed class IntermediateConverter<TValue> : JsonConverter<IReadOnlyDictionary<IndexName, TValue>>
	{
		public override IReadOnlyDictionary<IndexName, TValue>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var converter = options.GetConverter(typeof(ReadOnlyIndexNameDictionary<>).MakeGenericType(typeof(TValue)));

			if (converter is ReadOnlyIndexNameDictionaryConverter<TValue> specialisedConverter)
			{
				return specialisedConverter.Read(ref reader, typeToConvert, options);
			}

			return null;
		}

		public override void Write(Utf8JsonWriter writer, IReadOnlyDictionary<IndexName, TValue> value, JsonSerializerOptions options) => throw new NotImplementedException();
	}

	internal sealed class ReadOnlyIndexNameDictionaryConverterFactory : JsonConverterFactory
	{
		private readonly IElasticsearchClientSettings _settings;

		public ReadOnlyIndexNameDictionaryConverterFactory(IElasticsearchClientSettings settings) => _settings = settings;

		public override bool CanConvert(Type typeToConvert)
		{
			if (!typeToConvert.IsGenericType)
				return false;

			var canConvert = typeof(ReadOnlyIndexNameDictionary<>) == typeToConvert.GetGenericTypeDefinition();
			return canConvert;
		}

		public override JsonConverter CreateConverter(Type type, JsonSerializerOptions options)
		{
			var valueType = type.GetGenericArguments()[0];
			return (JsonConverter)Activator.CreateInstance(typeof(ReadOnlyIndexNameDictionaryConverter<>).MakeGenericType(valueType), _settings);
		}

		//private class ReadOnlyIndexNameDictionaryConverter<TValue> : JsonConverter<ReadOnlyIndexNameDictionary<TValue>>
		//{
		//	private readonly IElasticsearchClientSettings _settings;

		//	public ReadOnlyIndexNameDictionaryConverter(IElasticsearchClientSettings settings) => _settings = settings;

		//	public override ReadOnlyIndexNameDictionary<TValue>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		//	{
		//		var initialDictionary = JsonSerializer.Deserialize<Dictionary<IndexName, TValue>>(ref reader, options);

		//		var readOnlyDictionary = new ReadOnlyIndexNameDictionary<TValue>(initialDictionary, _settings);

		//		return readOnlyDictionary;

				
		//	}

		//	public override void Write(Utf8JsonWriter writer, ReadOnlyIndexNameDictionary<TValue> value, JsonSerializerOptions options) => throw new NotImplementedException();
		//}
	}

	internal sealed class ReadOnlyIndexNameDictionaryConverter<TValue> : JsonConverter<IReadOnlyDictionary<IndexName, TValue>>
	{
		private readonly IElasticsearchClientSettings _settings;

		public ReadOnlyIndexNameDictionaryConverter(IElasticsearchClientSettings settings) => _settings = settings;

		public override IReadOnlyDictionary<IndexName, TValue>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var initialDictionary = JsonSerializer.Deserialize<Dictionary<IndexName, TValue>>(ref reader, options);

			var readOnlyDictionary = new ReadOnlyIndexNameDictionary<TValue>(initialDictionary, _settings);

			return readOnlyDictionary;
		}

		public override void Write(Utf8JsonWriter writer, IReadOnlyDictionary<IndexName, TValue> value, JsonSerializerOptions options) => throw new NotImplementedException();
	}


	/// <summary>
	/// A specialised readonly dictionary for <typeparamref name="TValue"/> data, keyed by <see cref="IndexName"/>.
	/// <para>This supports inferrence enabled lookups by ensuring keys are sanitized when storing the values and when performing lookups.</para>
	/// </summary>
	/// <typeparam name="TValue"></typeparam>
	public sealed class ReadOnlyIndexNameDictionary<TValue> : IReadOnlyDictionary<IndexName, TValue>
	{
		private readonly Dictionary<IndexName, TValue> _backingDictionary;
		private readonly IElasticsearchClientSettings _settings;

		internal ReadOnlyIndexNameDictionary(Dictionary<IndexName, TValue> source, IElasticsearchClientSettings settings)
		{
			_settings = settings;

			var backingDictionary = new Dictionary<IndexName, TValue>(source.Count);

			if (source == null)
				return;

			foreach (var key in source.Keys)
				backingDictionary[Sanitize(key)] = source[key];

			_backingDictionary = backingDictionary;
		}

		private string Sanitize(IndexName key) => key?.GetString(_settings);

		public TValue this[IndexName key] => _backingDictionary.TryGetValue(Sanitize(key), out var v) ? v : default;
		public TValue this[string key] => _backingDictionary.TryGetValue(key, out var v) ? v : default;

		public IEnumerable<IndexName> Keys => _backingDictionary.Keys;
		public IEnumerable<TValue> Values => _backingDictionary.Values;
		public int Count => _backingDictionary.Count;
		public bool ContainsKey(IndexName key) => _backingDictionary.ContainsKey(Sanitize(key));
		public IEnumerator<KeyValuePair<IndexName, TValue>> GetEnumerator() => _backingDictionary.GetEnumerator();
		public bool TryGetValue(IndexName key, out TValue value) => _backingDictionary.TryGetValue(Sanitize(key), out value);
		IEnumerator IEnumerable.GetEnumerator() => _backingDictionary.GetEnumerator();
	}

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
