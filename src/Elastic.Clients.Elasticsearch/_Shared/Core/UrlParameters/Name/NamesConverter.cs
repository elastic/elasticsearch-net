// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch.Json;

public sealed class NamesConverter :
	JsonConverter<Names>
{
	public override Names Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		return reader.TokenType switch
		{
			JsonTokenType.String => new Names([reader.ReadValue<Name>(options)]),
			JsonTokenType.StartArray => new Names(reader.ReadValue<List<Name>>(options)),
			_ => throw reader.UnexpectedTokenException(JsonTokenType.String, JsonTokenType.StartArray)
		};
	}

	public override void Write(Utf8JsonWriter writer, Names value, JsonSerializerOptions options)
	{
		if (value.Values is [{ } single])
		{
			writer.WriteValue(options, single);
			return;
		}

		writer.WriteValue(options, value.Values);
	}
}
