// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serialization;

internal sealed class SingleOrManyDictionaryMarker<TDictionary, TKey, TCollection, TElement>
	where TDictionary : class, IEnumerable<KeyValuePair<TKey, TCollection>>
	where TCollection : class, IEnumerable<TElement>
{
	static SingleOrManyDictionaryMarker()
	{
		DynamicallyAccessed.PublicConstructors(typeof(SingleOrManyDictionaryMarkerConverter<TDictionary, TKey, TCollection, TElement>));
	}
}

internal sealed class SingleOrManyDictionaryMarkerConverter<TDictionary, TKey, TCollection, TElement> :
	JsonConverter<SingleOrManyDictionaryMarker<TDictionary, TKey, TCollection, TElement>>,
	IMarkerTypeConverter
	where TDictionary : class, IEnumerable<KeyValuePair<TKey, TCollection>>
	where TCollection : class, IEnumerable<TElement>
{
	public JsonConverter WrappedConverter { get; } = new SingleOrManyDictionaryConverter<TDictionary, TKey, TCollection, TElement>();

	public override SingleOrManyDictionaryMarker<TDictionary, TKey, TCollection, TElement>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		throw new NotImplementedException();
	}

	public override void Write(Utf8JsonWriter writer, SingleOrManyDictionaryMarker<TDictionary, TKey, TCollection, TElement> value, JsonSerializerOptions options)
	{
		throw new NotImplementedException();
	}
}

internal sealed class SingleOrManyDictionaryMarkerConverterFactory :
	JsonConverterFactory
{
	public override bool CanConvert(Type typeToConvert)
	{
		return typeToConvert.IsGenericType &&
			   typeToConvert.GetGenericTypeDefinition() == typeof(SingleOrManyDictionaryMarker<,,,>);
	}

	public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
	{
		var args = typeToConvert.GetGenericArguments();

#pragma warning disable IL3050 // SingleOrManyDictionaryMarker<,,,> static constructor roots SingleOrManyDictionaryMarkerConverter<,,,>.

		var converter = (JsonConverter)Activator.CreateInstance(
			typeof(SingleOrManyDictionaryMarkerConverter<,,,>).MakeGenericType(args[0], args[1], args[2], args[3]),
			BindingFlags.Instance | BindingFlags.Public,
			binder: null,
			args: null,
			culture: null)!;

#pragma warning restore IL3050

		return converter;
	}
}

// TODO: For this code to work well with AOT, we have to make sure that `JsonSerializable` attributes
//       for `TCollection`, `TElement` (and potentially `List<TElement>`) are generated.

internal sealed class SingleOrManyDictionaryConverter<TDictionary, TKey, TCollection, TElement> :
	JsonConverter<TDictionary>
	where TDictionary : class, IEnumerable<KeyValuePair<TKey, TCollection>>
	where TCollection : class, IEnumerable<TElement>
{
	public override TDictionary? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		reader.ValidateToken(JsonTokenType.StartObject);

		var result = new Dictionary<TKey, TCollection?>();

		while (reader.Read() && reader.TokenType is JsonTokenType.PropertyName)
		{
			var key = reader.ReadPropertyName<TKey>(options);
			reader.Read();
			var value = reader.ReadValue<TCollection?>(options, typeof(SingleOrManyMarker<TCollection, TElement>));

			result.Add(key, value);
		}

		reader.ValidateToken(JsonTokenType.EndObject);

		return result as TDictionary;
	}

	public override void Write(Utf8JsonWriter writer, TDictionary value, JsonSerializerOptions options)
	{
		writer.WriteStartObject();

		foreach (var pair in value)
		{
			writer.WriteProperty(options, pair.Key!, pair.Value, null, typeof(SingleOrManyMarker<TCollection, TElement>));
		}

		writer.WriteEndObject();
	}
}
