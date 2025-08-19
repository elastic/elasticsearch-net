// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json.Serialization;
using System.Text.Json;
using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch.Json;

public sealed class RelationNameConverter : JsonConverter<RelationName>
{
	private IElasticsearchClientSettings? _settings;

	public override RelationName? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType == JsonTokenType.String)
		{
			RelationName relationName = reader.GetString();
			return relationName;
		}

		return null;
	}

	public override void Write(Utf8JsonWriter writer, RelationName value, JsonSerializerOptions options)
	{
		if (value is null)
		{
			writer.WriteNullValue();
			return;
		}

		InitializeSettings(options);
		var relationName = _settings.Inferrer.RelationName(value);
		writer.WriteStringValue(relationName);
	}

	private void InitializeSettings(JsonSerializerOptions options)
	{
		if (_settings is null)
		{
			if (!options.TryGetClientSettings(out var settings))
				ThrowHelper.ThrowJsonExceptionForMissingSettings();

			_settings = settings;
		}
	}
}
