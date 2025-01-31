// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Elastic.Clients.Elasticsearch.Mapping;

namespace Elastic.Clients.Elasticsearch;

[JsonConverter(typeof(FieldSortConverter))]
public partial class FieldSort
{
	public static FieldSort Empty { get; } = new();
}

internal sealed class FieldSortConverter : JsonConverter<FieldSort>
{
	// This is temporarily a manual converter until we code-gen for shortcut properties.
	// This serves as the template for those converters.

	public override FieldSort? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType == JsonTokenType.StartObject)
		{
			string? format = null;
			FieldValue? missing = null;
			SortMode? mode = null;
			NestedSortValue? nested = null;
			FieldSortNumericType? numericType = null;
			SortOrder? order = null;
			FieldType? unmappedType = null;

			while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
			{
				if (reader.TokenType == JsonTokenType.PropertyName)
				{
					var propertyName = reader.GetString();
					reader.Read();

					switch (propertyName)
					{
						case "format":
							format = reader.GetString();
							break;
						case "missing":
							missing = JsonSerializer.Deserialize<FieldValue>(ref reader, options);
							break;
						case "mode":
							mode = JsonSerializer.Deserialize<SortMode>(ref reader, options);
							break;
						case "nested":
							nested = JsonSerializer.Deserialize<NestedSortValue>(ref reader, options);
							break;
						case "numeric_type":
							numericType = JsonSerializer.Deserialize<FieldSortNumericType>(ref reader, options);
							break;
						case "order":
							order = JsonSerializer.Deserialize<SortOrder>(ref reader, options);
							break;
						case "unmapped_type":
							unmappedType = JsonSerializer.Deserialize<FieldType>(ref reader, options);
							break;
						default:
							throw new JsonException("Unexpected property while reading `Field`.");
					}
				}
			}

			return new FieldSort
			{
				Format = format,
				Missing = missing,
				Mode = mode,
				Nested = nested,
				NumericType = numericType,
				Order = order,
				UnmappedType = unmappedType
			};
		}

		else if (reader.TokenType == JsonTokenType.String) // Shortcut property
		{
			var sortOrder = JsonSerializer.Deserialize<SortOrder>(ref reader, options);
			return new FieldSort { Order = sortOrder };
		}

		throw new JsonException($"Unexpected JSON token '{reader.TokenType}' encountered while deserializing FieldSort.");
	}

	public override void Write(Utf8JsonWriter writer, FieldSort value, JsonSerializerOptions options)
	{
		if (value is null)
		{
			writer.WriteNullValue();
			return;
		}

		writer.WriteStartObject();

		if (value.Format is not null)
		{
			writer.WritePropertyName("format");
			writer.WriteStringValue(value.Format);
		}

		if (value.Missing.HasValue)
		{
			writer.WritePropertyName("missing");
			JsonSerializer.Serialize(writer, value.Missing.Value, options);
		}

		if (value.Mode.HasValue)
		{
			writer.WritePropertyName("mode");
			JsonSerializer.Serialize(writer, value.Mode.Value, options);
		}

		if (value.Nested is not null)
		{
			writer.WritePropertyName("nested");
			JsonSerializer.Serialize(writer, value.Nested, options);
		}

		if (value.NumericType.HasValue)
		{
			writer.WritePropertyName("numeric_type");
			JsonSerializer.Serialize(writer, value.NumericType.Value, options);
		}

		if (value.Order.HasValue)
		{
			writer.WritePropertyName("order");
			JsonSerializer.Serialize(writer, value.Order.Value, options);
		}

		if (value.UnmappedType.HasValue)
		{
			writer.WritePropertyName("unmapped_type");
			JsonSerializer.Serialize(writer, value.UnmappedType.Value, options);
		}

		writer.WriteEndObject();
	}
}
