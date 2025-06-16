// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serialization;

internal sealed class KeyValuePairConverterFactory : JsonConverterFactory
{
	public override bool CanConvert(Type typeToConvert) =>
		typeToConvert.IsGenericType &&
		typeToConvert.GetGenericTypeDefinition() == typeof(KeyValuePair<,>);

	public override JsonConverter CreateConverter(
		Type type,
		JsonSerializerOptions options)
	{
		var itemOneType = type.GetGenericArguments()[0];
		var itemTwoType = type.GetGenericArguments()[1];

		return (JsonConverter)Activator.CreateInstance(typeof(KeyValuePairConverter<,>).MakeGenericType(itemOneType, itemTwoType));
	}

	private class KeyValuePairConverter<TItem1, TItem2> : JsonConverter<KeyValuePair<TItem1, TItem2>>
	{
		public override KeyValuePair<TItem1, TItem2> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType != JsonTokenType.StartObject)
				throw new JsonException("Unexpected token for KeyValuePair");

			reader.Read(); // property name (key)
			var converter = (JsonConverter<TItem1>)options.GetConverter(typeof(TItem1));
			var key = converter.ReadAsPropertyName(ref reader, typeof(TItem1), options);

			reader.Read(); // value
			var value = JsonSerializer.Deserialize<TItem2>(ref reader, options);

			reader.Read(); // end object

			return new KeyValuePair<TItem1, TItem2>(key, value);
		}

		public override void Write(Utf8JsonWriter writer, KeyValuePair<TItem1, TItem2> value,
			JsonSerializerOptions options)
		{
			writer.WriteStartObject();

			var converter = (JsonConverter<TItem1>)options.GetConverter(typeof(TItem1));
			converter.WriteAsPropertyName(writer, value.Key, options);
			JsonSerializer.Serialize(writer, value.Value, options);

			writer.WriteEndObject();
		}
	}
}
