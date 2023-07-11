// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch;

internal sealed class IdsConverter : JsonConverter<Ids>
{
	public override Ids? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType != JsonTokenType.StartArray)
			throw new JsonException($"Unexpected JSON token. Expected {JsonTokenType.StartArray} but read {reader.TokenType}");

		var ids = new List<Id>();

		while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
		{
			var id = JsonSerializer.Deserialize<Id>(ref reader, options);

			if (id is not null)
				ids.Add(id);
		}

		return new Ids(ids);
	}

	public override void Write(Utf8JsonWriter writer, Ids value, JsonSerializerOptions options)
	{
		if (value is null)
		{
			writer.WriteNullValue();
			return;
		}

		writer.WriteStartArray();

		foreach (var id in value.IdsToSerialize)
		{
			JsonSerializer.Serialize<Id>(writer, id, options);
		}

		writer.WriteEndArray();
	}
}
