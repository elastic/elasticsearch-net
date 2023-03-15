// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch;

public partial class SortOptions
{
	public static SortOptions Field(Field field) => new(field, FieldSort.Empty);
}

internal sealed class SortOptionsConverter : JsonConverter<SortOptions>
{
	// We manually define this converter since we simplify SortCombinations union from the spec as SortOptions instance.
	// This requires a custom read method to handle deserialization of the potential union JSON as specified.

	public override SortOptions Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType == JsonTokenType.StartObject)
		{
			reader.Read();
			if (reader.TokenType != JsonTokenType.PropertyName)
			{
				throw new JsonException("Expected a property name token representing the variant held within this container.");
			}

			var propertyName = reader.GetString();
			reader.Read();
			if (propertyName == "_doc")
			{
				var variant = JsonSerializer.Deserialize<ScoreSort?>(ref reader, options);
				reader.Read();
				return new SortOptions(propertyName, variant);
			}

			if (propertyName == "_score")
			{
				var variant = JsonSerializer.Deserialize<ScoreSort?>(ref reader, options);
				reader.Read();
				return new SortOptions(propertyName, variant);
			}

			if (propertyName == "_script")
			{
				var variant = JsonSerializer.Deserialize<ScriptSort?>(ref reader, options);
				reader.Read();
				return new SortOptions(propertyName, variant);
			}

			if (propertyName == "_geo_distance")
			{
				var variant = JsonSerializer.Deserialize<GeoDistanceSort?>(ref reader, options);
				reader.Read();
				return new SortOptions(propertyName, variant);
			}

			// For field sorts, the property name will be the field name
			{
				var variant = JsonSerializer.Deserialize<FieldSort>(ref reader, options);
				reader.Read();
				return new SortOptions(propertyName, variant);
			}
		}

		else if (reader.TokenType == JsonTokenType.String)
		{
			var fieldName = reader.GetString();
			return SortOptions.Field(fieldName, FieldSort.Empty);
		}

		throw new JsonException($"Unexpected JSON token '{reader.TokenType}' encountered while deserializing SortOptions.");
	}

	public override void Write(Utf8JsonWriter writer, SortOptions value, JsonSerializerOptions options)
	{
		if (!options.TryGetClientSettings(out var settings))
			ThrowHelper.ThrowJsonExceptionForMissingSettings();

		string? fieldName = null;

		if (value.AdditionalPropertyName is IUrlParameter urlParameter)
		{
			fieldName = urlParameter.GetString(settings);
		}

		// Special handling for shortcut on sorting with a basic field sort
		if (value.Variant.Equals(FieldSort.Empty))
		{
			writer.WriteStringValue(fieldName ?? value.VariantName);
			return;
		}

		writer.WriteStartObject();

		writer.WritePropertyName(fieldName ?? value.VariantName);

		switch (value.VariantName)
		{
			case "_doc":
				JsonSerializer.Serialize<ScoreSort>(writer, (ScoreSort)value.Variant, options);
				break;
			case "_score":
				JsonSerializer.Serialize<ScoreSort>(writer, (ScoreSort)value.Variant, options);
				break;
			case "_script":
				JsonSerializer.Serialize<ScriptSort>(writer, (ScriptSort)value.Variant, options);
				break;
			case "_geo_distance":
				JsonSerializer.Serialize<GeoDistanceSort>(writer, (GeoDistanceSort)value.Variant, options);
				break;
			default:
				JsonSerializer.Serialize<FieldSort>(writer, (FieldSort)value.Variant, options);
				break;
		}

		writer.WriteEndObject();
	}
}
