// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch;

/// <summary>
/// Converts an <see cref="IndexName"/> to and from its JSON representation.
/// </summary>
internal class IndexNameConverter : JsonConverter<IndexName>
{
	private IElasticsearchClientSettings _settings;

	public override void WriteAsPropertyName(Utf8JsonWriter writer, IndexName value, JsonSerializerOptions options)
	{
		InitializeSettings(options);
		writer.WritePropertyName(((IUrlParameter)value).GetString(_settings));
	}

	public override IndexName ReadAsPropertyName(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => reader.GetString();

	public override IndexName? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType != JsonTokenType.String)
		{
			reader.Read();
			return null;
		}

		IndexName? indexName = reader.GetString();
		return indexName;
	}

	public override void Write(Utf8JsonWriter writer, IndexName? value, JsonSerializerOptions options)
	{
		InitializeSettings(options);

		if (value is null)
		{
			writer.WriteNullValue();
			return;
		}

		writer.WriteStringValue(_settings.Inferrer.IndexName(value));
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
