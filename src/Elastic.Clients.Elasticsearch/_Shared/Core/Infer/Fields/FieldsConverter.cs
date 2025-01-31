// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch;

internal sealed class FieldsConverter : JsonConverter<Fields>
{
	public override Fields? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		switch (reader.TokenType)
		{
			case JsonTokenType.Null:
				return null;

			case JsonTokenType.StartArray:
				var fields = JsonSerializer.Deserialize<List<Field>>(ref reader, options);
				return new Fields(fields);

			default:
				throw new JsonException("Unexpected token.");
		}
	}

	public override void Write(Utf8JsonWriter writer, Fields value, JsonSerializerOptions options)
	{
		if (value is null)
		{
			writer.WriteNullValue();
			return;
		}

		JsonSerializer.Serialize(writer, value.ListOfFields, options);
	}
}

internal sealed class SingleOrManyFieldsConverter : JsonConverter<Fields>
{
	public override Fields? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		switch (reader.TokenType)
		{
			case JsonTokenType.Null:
				return null;

			case JsonTokenType.String:
				var field = JsonSerializer.Deserialize<Field>(ref reader, options);
				return new Fields([field]);

			case JsonTokenType.StartArray:
				var fields = JsonSerializer.Deserialize<List<Field>>(ref reader, options);
				return new Fields(fields);

			default:
				throw new JsonException("Unexpected token.");
		}
	}

	public override void Write(Utf8JsonWriter writer, Fields value, JsonSerializerOptions options)
	{
		if (value is null)
		{
			writer.WriteNullValue();
			return;
		}

		if (value.ListOfFields.Count == 1)
		{
			JsonSerializer.Serialize(writer, value.ListOfFields[0], options);
			return;
		}

		JsonSerializer.Serialize(writer, value.ListOfFields, options);
	}
}
