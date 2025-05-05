// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch.Serialization;

internal sealed class KeyValuePairConverterFactory : JsonConverterFactory
{
	private readonly IElasticsearchClientSettings _settings;

	public KeyValuePairConverterFactory(IElasticsearchClientSettings settings) => _settings = settings;

	public override bool CanConvert(Type typeToConvert) => typeToConvert.IsGenericType
		&& typeToConvert.Name == typeof(KeyValuePair<,>).Name
		&& typeof(IUrlParameter).IsAssignableFrom(typeToConvert.GetGenericArguments()[0]);

	public override JsonConverter CreateConverter(
		Type type,
		JsonSerializerOptions options)
	{
		var itemOneType = type.GetGenericArguments()[0];
		var itemTwoType = type.GetGenericArguments()[1];

		return (JsonConverter)Activator.CreateInstance(typeof(KeyValuePairConverter<,>).MakeGenericType(itemOneType, itemTwoType), _settings);
	}

	private class KeyValuePairConverter<TItem1, TItem2> : JsonConverter<KeyValuePair<TItem1, TItem2>> where TItem1 : class, IUrlParameter
	{
		private readonly IElasticsearchClientSettings _settings;

		public KeyValuePairConverter(IElasticsearchClientSettings settings) => _settings = settings;

		public override KeyValuePair<TItem1, TItem2> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType != JsonTokenType.StartObject)
				throw new JsonException("Unexpected token for KeyValuePair");

			reader.Read(); // property name (key)
			var keyString = reader.GetString();

			reader.Read(); // value
			var value = JsonSerializer.Deserialize<TItem2>(ref reader, options);

			reader.Read(); // end object

			var key = (TItem1)Activator.CreateInstance(typeof(TItem1), keyString);
			return new KeyValuePair<TItem1, TItem2>(key, value);
		}

		public override void Write(Utf8JsonWriter writer, KeyValuePair<TItem1, TItem2> value,
			JsonSerializerOptions options)
		{
			writer.WriteStartObject();
			writer.WritePropertyName(value.Key.GetString(_settings));
			JsonSerializer.Serialize<TItem2>(writer, value.Value, options);
			writer.WriteEndObject();
		}
	}
}
