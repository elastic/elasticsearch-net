// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch.Mapping.Json;

public sealed class PropertiesConverter : JsonConverter<Properties>
{
	public override Properties? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (!options.TryGetClientSettings(out var settings))
			ThrowHelper.ThrowJsonExceptionForMissingSettings();

		if (reader.TokenType != JsonTokenType.StartObject)
			throw new JsonException("Expected start object token.");

		var properties = new Properties(settings);

		while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
		{
			if (reader.TokenType != JsonTokenType.PropertyName)
				throw new JsonException("Expected property name token.");

			var propertyName = reader.GetString();
			reader.Read();
			var property = reader.ReadValue<IProperty>(options);
			properties.Add(propertyName, property);
		}

		return properties;
	}

	public override void Write(Utf8JsonWriter writer, Properties value, JsonSerializerOptions options)
	{
		if (!options.TryGetClientSettings(out var settings))
			ThrowHelper.ThrowJsonExceptionForMissingSettings();

		if (value is null)
		{
			writer.WriteNullValue();
			return;
		}

		// HACK: Deduplicate property mappings with an instance of Properties that has access to ElasticsearchClientSettings to sanitize PropertyName keys.
		var properties = new Properties(settings);

		foreach (var kv in value)
		{
			// TODO - NEST checks for properties of IPropertyWithClrOrigin so that it can then skip ignored properties etc.
			// This functionality is missing for GA.

			properties[kv.Key] = kv.Value;
			continue;
		}

		writer.WriteDictionaryValue(options, properties.BackingDictionary, null, null);
	}
}
