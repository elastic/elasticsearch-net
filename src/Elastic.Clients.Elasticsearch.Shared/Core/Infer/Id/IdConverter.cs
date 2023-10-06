// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch;

internal sealed class IdConverter : JsonConverter<Id>
{
	private IElasticsearchClientSettings? _settings;

	public override void WriteAsPropertyName(Utf8JsonWriter writer, Id value, JsonSerializerOptions options)
	{
		InitializeSettings(options);
		writer.WritePropertyName(((IUrlParameter)value).GetString(_settings));
	}

	public override Id ReadAsPropertyName(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => reader.GetString();

	public override Id? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
		reader.TokenType == JsonTokenType.Number
			? new Id(reader.GetInt64())
			: new Id(reader.GetString());

	public override void Write(Utf8JsonWriter writer, Id value, JsonSerializerOptions options)
	{
		if (value is null)
		{
			writer.WriteNullValue();
			return;
		}

		if (value.Document is not null)
		{
			InitializeSettings(options);
			var documentId = _settings.Inferrer.Id(value.Document.GetType(), value.Document);
			writer.WriteStringValue(documentId);
		}
		else if (value.LongValue.HasValue)
		{
			writer.WriteNumberValue(value.LongValue.Value);
		}
		else
		{
			writer.WriteStringValue(value.StringValue);
		}
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
