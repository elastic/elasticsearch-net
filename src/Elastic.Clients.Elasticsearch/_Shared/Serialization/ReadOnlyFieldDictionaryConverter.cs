// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serialization;

internal sealed class ReadOnlyFieldDictionaryConverterAttribute : JsonConverterAttribute
{
	public ReadOnlyFieldDictionaryConverterAttribute(Type valueType) => ValueType = valueType;

	public Type ValueType { get; }

	public override JsonConverter? CreateConverter(Type typeToConvert) => (JsonConverter)Activator.CreateInstance(typeof(ReadOnlyFieldDictionaryConverter<>).MakeGenericType(ValueType));
}

internal sealed class ReadOnlyFieldDictionaryConverter<TValue> : JsonConverter<IReadOnlyDictionary<Field, TValue>>
{
	public override IReadOnlyDictionary<Field, TValue>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (!options.TryGetClientSettings(out var clientSettings))
		{
			throw new JsonException("Unable to retrieve client settings required for deserialisation.");
		}

		var initialDictionary = JsonSerializer.Deserialize<Dictionary<Field, TValue>>(ref reader, options);

		var readOnlyDictionary = new ReadOnlyFieldDictionary<TValue>(initialDictionary, clientSettings);

		return readOnlyDictionary;
	}

	public override void Write(Utf8JsonWriter writer, IReadOnlyDictionary<Field, TValue> value, JsonSerializerOptions options) => throw new NotImplementedException();
}
