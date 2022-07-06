// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch;

internal sealed class FieldConverter : JsonConverter<Field>
{
	private readonly IElasticsearchClientSettings _settings;

	public FieldConverter(IElasticsearchClientSettings settings) => _settings = settings;

	public override void WriteAsPropertyName(Utf8JsonWriter writer, Field value, JsonSerializerOptions options) => writer.WritePropertyName(((IUrlParameter)value).GetString(_settings));

	public override Field ReadAsPropertyName(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => reader.GetString();

	public override Field? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		switch (reader.TokenType)
		{
			case JsonTokenType.Null:
				return null;

			case JsonTokenType.String:
				return new Field(reader.GetString());
		}

		return null;
	}

	public override void Write(Utf8JsonWriter writer, Field value, JsonSerializerOptions options)
	{
		if (value is null)
		{
			writer.WriteNullValue();
			return;
		}

		var fieldName = _settings.Inferrer.Field(value);

		if (string.IsNullOrEmpty(value.Format))
		{
			writer.WriteStringValue(fieldName);
		}
		else
		{
			writer.WriteStartObject();
			writer.WritePropertyName("field");
			writer.WriteStringValue(fieldName);
			writer.WritePropertyName("format");
			writer.WriteStringValue(value.Format);
			writer.WriteEndObject();
		}
	}
}
