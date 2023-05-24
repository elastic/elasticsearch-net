// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch;

internal sealed class FieldConverter : JsonConverter<Field>
{
	private static readonly JsonEncodedText FieldProperty = JsonEncodedText.Encode("field");
	private static readonly JsonEncodedText FormatProperty = JsonEncodedText.Encode("format");

	private IElasticsearchClientSettings _settings;

	public override void WriteAsPropertyName(Utf8JsonWriter writer, Field value, JsonSerializerOptions options)
	{
		InitializeSettings(options);
		writer.WritePropertyName(((IUrlParameter)value).GetString(_settings));
	}

	public override Field ReadAsPropertyName(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => reader.GetString();

	public override Field? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		switch (reader.TokenType)
		{
			case JsonTokenType.Null:
				return null;

			case JsonTokenType.String:
				return new Field(reader.GetString());

			case JsonTokenType.StartObject:
				return ReadObjectField(ref reader);
		}

		return null;
	}

	private static Field ReadObjectField(ref Utf8JsonReader reader)
	{
		var field = string.Empty;
		var format = string.Empty;

		while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
		{
			if (reader.TokenType == JsonTokenType.PropertyName)
			{
				if (reader.ValueTextEquals(FieldProperty.EncodedUtf8Bytes))
				{
					reader.Read();
					field = reader.GetString();
				}
				else if (reader.ValueTextEquals(FormatProperty.EncodedUtf8Bytes))
				{
					reader.Read();
					format = reader.GetString();
				}
				else
				{
					throw new JsonException($"Unexpected property while reading `{nameof(Field)}`.");
				}
			}
		}

		if (!string.IsNullOrEmpty(field) && !string.IsNullOrEmpty(format))
		{
			return new Field(field, format);
		}

		throw new JsonException($"Unable to read `{nameof(Field)}` from JSON.");
	}

	public override void Write(Utf8JsonWriter writer, Field value, JsonSerializerOptions options)
	{
		InitializeSettings(options);

		var fieldName = _settings.Inferrer.Field(value);

		if (string.IsNullOrEmpty(value.Format))
		{
			writer.WriteStringValue(fieldName);
		}
		else
		{
			writer.WriteStartObject();
			writer.WriteString(FieldProperty, fieldName);
			writer.WriteString(FormatProperty, value.Format);
			writer.WriteEndObject();
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
