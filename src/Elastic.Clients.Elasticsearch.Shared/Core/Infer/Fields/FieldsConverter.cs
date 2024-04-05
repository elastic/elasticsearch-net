// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

#if ELASTICSEARCH_SERVERLESS
namespace Elastic.Clients.Elasticsearch.Serverless;
#else
namespace Elastic.Clients.Elasticsearch;
#endif

internal sealed class FieldsConverter : JsonConverter<Fields>
{
	public override Fields? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType == JsonTokenType.String)
		{
			Fields fields = reader.GetString();
			return fields;
		}
		else if (reader.TokenType == JsonTokenType.StartArray)
		{
			var fields = new List<Field>();
			while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
			{
				var field = JsonSerializer.Deserialize<Field>(ref reader, options);
				fields.Add(field);
			}
			return new Fields(fields);
		}

		reader.Read();
		return null;
	}

	public override void Write(Utf8JsonWriter writer, Fields value, JsonSerializerOptions options)
	{
		if (value is null)
		{
			writer.WriteNullValue();
			return;
		}

		writer.WriteStartArray();
		foreach (var field in value.ListOfFields)
		{
			JsonSerializer.Serialize(writer, field, options);
		}
		writer.WriteEndArray();
	}
}

internal sealed class SingleOrManyFieldsConverter : JsonConverter<Fields>
{
	public override Fields? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType == JsonTokenType.String)
		{
			Fields fields = reader.GetString();
			return fields;
		}

		if (reader.TokenType == JsonTokenType.StartArray)
		{
			var fields = new List<Field>();
			while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
			{
				var field = JsonSerializer.Deserialize<Field>(ref reader, options);
				fields.Add(field);
			}
			return new Fields(fields);
		}

		reader.Read();
		return null;
	}

	public override void Write(Utf8JsonWriter writer, Fields value, JsonSerializerOptions options)
	{
		if (value is null)
		{
			writer.WriteNullValue();
			return;
		}

		//if (value.ListOfFields.Count == 0)
		//{
		//	writer.WriteStartObject();
		//	writer.WriteEndObject();
		//	return;
		//}

		if (value.ListOfFields.Count == 1)
		{
			JsonSerializer.Serialize(writer, value.ListOfFields[0], options);
			return;
		}

		writer.WriteStartArray();
		foreach (var field in value.ListOfFields)
		{
			JsonSerializer.Serialize(writer, field, options);
		}
		writer.WriteEndArray();
	}
}
