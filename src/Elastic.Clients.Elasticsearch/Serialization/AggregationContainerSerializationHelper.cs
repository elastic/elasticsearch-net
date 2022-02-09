// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

	internal sealed class ReadOnlyIndexNameDictionaryConverter : JsonConverterAttribute
	{
		public ReadOnlyIndexNameDictionaryConverter(Type valueType) => ValueType = valueType;

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


	internal sealed class SourceConverterAttribute : JsonConverterAttribute
	{
		public override JsonConverter? CreateConverter(Type typeToConvert) => (JsonConverter)Activator.CreateInstance(typeof(IntermediateSourceConverter<>).MakeGenericType(typeToConvert));
	}

	internal sealed class IntermediateSourceConverter<T> : JsonConverter<T>
	{
		public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var converter = options.GetConverter(typeof(SourceMarker<>).MakeGenericType(typeToConvert));

			if (converter is SourceConverter<T> sourceConverter)
			{
				var source = sourceConverter.Read(ref reader, typeToConvert, options);
				return source.Source;
			}

			return default;
		}

		public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
		{
			var converter = options.GetConverter(typeof(SourceMarker<>).MakeGenericType(typeof(T)));

			if (converter is SourceConverter<T> sourceConverter)
			{
				sourceConverter.Write(writer, new SourceMarker<T> { Source = value }, options);
			}
		}
	}

	internal sealed class SourceConverterFactory : JsonConverterFactory
	{
		private readonly IElasticsearchClientSettings _settings;

		public SourceConverterFactory(IElasticsearchClientSettings settings) => _settings = settings;

		public override bool CanConvert(Type typeToConvert)
		{
			if (!typeToConvert.IsGenericType)
				return false;

			var canConvert = typeof(SourceMarker<>) == typeToConvert.GetGenericTypeDefinition();
			return canConvert;
		}

		public override JsonConverter CreateConverter(Type type, JsonSerializerOptions options)
		{
			var valueType = type.GetGenericArguments()[0];
			return (JsonConverter)Activator.CreateInstance(typeof(SourceConverter<>).MakeGenericType(valueType), _settings);
		}
	}

	internal interface ISourceMarker<T> { }

	internal sealed class SourceMarker<T>
	{
		public T Source { get; set; }
	}

	internal sealed class SourceConverter<T> : JsonConverter<SourceMarker<T>>
	{
		private readonly IElasticsearchClientSettings _settings;

		public SourceConverter(IElasticsearchClientSettings settings) => _settings = settings;

		public override SourceMarker<T>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => new()
		{
			Source = SourceSerialisation.Deserialize<T>(ref reader, _settings)
		};

		public override void Write(Utf8JsonWriter writer, SourceMarker<T> value, JsonSerializerOptions options) => SourceSerialisation.Serialize<T>(value.Source, writer, _settings);
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
	public struct ReadOnlyIndexNameDictionary<TValue> : IReadOnlyDictionary<IndexName, TValue>
	{
		private readonly Dictionary<IndexName, TValue> _backingDictionary;
		private readonly IElasticsearchClientSettings? _settings;

		public ReadOnlyIndexNameDictionary()
		{
			_backingDictionary = new Dictionary<IndexName,TValue>(0);
			_settings = null;
		}

		internal ReadOnlyIndexNameDictionary(Dictionary<IndexName, TValue> source, IElasticsearchClientSettings settings)
		{
			_settings = settings;

			// This is an "optimised version" which doesn't cause a second dictionary to be allocated.
			// Since we expect this to be used only for deserialisation, the keys received will already have been strings,
			// so no further sanitisation is required.

			//var backingDictionary = new Dictionary<IndexName, TValue>(source.Count);

			if (source == null)
			{
				_backingDictionary = new Dictionary<IndexName, TValue>(0);
				return;
			}

			//foreach (var key in source.Keys)
			//	backingDictionary[Sanitize(key)] = source[key];

			_backingDictionary = source;
		}

		private string Sanitize(IndexName key) => _settings is not null ? key?.GetString(_settings) : string.Empty;

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
