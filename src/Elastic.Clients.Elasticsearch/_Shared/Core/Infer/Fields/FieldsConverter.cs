// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch;

internal sealed class FieldsConverter :
	JsonConverter<Fields>
{
	public override Fields Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		reader.ValidateToken(JsonTokenType.StartArray);

		var fields = reader.ReadValue<List<Field>>(options);

		return new Fields(fields);
	}

	public override void Write(Utf8JsonWriter writer, Fields value, JsonSerializerOptions options)
	{
		writer.WriteValue(options, value.ListOfFields);
	}
}

internal sealed class SingleOrManyFieldsMarker;

internal sealed class SingleOrManyFieldsMarkerConverter :
	JsonConverter<SingleOrManyFieldsMarker>,
	IMarkerTypeConverter
{
	public JsonConverter WrappedConverter { get; }

	public SingleOrManyFieldsMarkerConverter()
	{
		WrappedConverter = new SingleOrManyFieldsConverter();
	}

	public override SingleOrManyFieldsMarker Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		throw new InvalidOperationException();
	}

	public override void Write(Utf8JsonWriter writer, SingleOrManyFieldsMarker value, JsonSerializerOptions options)
	{
		throw new InvalidOperationException();
	}
}

internal sealed class SingleOrManyFieldsConverter :
	JsonConverter<Fields>
{
	public override Fields Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		return reader.TokenType switch
		{
			JsonTokenType.String => new Fields([reader.ReadValue<Field>(options)]),
			JsonTokenType.StartArray => new Fields(reader.ReadValue<List<Field>>(options)),
			_ => throw new JsonException($"Expected JSON '{JsonTokenType.String}' or '{JsonTokenType.StartArray}' token, but got '{reader.TokenType}'.")
		};
	}

	public override void Write(Utf8JsonWriter writer, Fields value, JsonSerializerOptions options)
	{
		if (value.ListOfFields.Count == 1)
		{
			writer.WriteValue(options, value.ListOfFields[0]);
			return;
		}

		writer.WriteValue(options, value.ListOfFields);
	}
}
