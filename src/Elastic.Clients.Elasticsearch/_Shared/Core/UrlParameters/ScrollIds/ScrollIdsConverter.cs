// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch.Json;

public sealed class ScrollIdsConverter :
	JsonConverter<ScrollIds>
{
	public override ScrollIds Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		return reader.TokenType switch
		{
			JsonTokenType.String => new ScrollIds([reader.ReadValue<ScrollId>(options)]),
			JsonTokenType.StartArray => new ScrollIds(reader.ReadValue<List<ScrollId>>(options)),
			_ => throw new JsonException($"Expected JSON '{JsonTokenType.String}' or '{JsonTokenType.StartArray}' token, but got '{reader.TokenType}'.")
		};
	}

	public override void Write(Utf8JsonWriter writer, ScrollIds value, JsonSerializerOptions options)
	{
		if (value.Ids.Count == 1)
		{
			writer.WriteValue(options, value.Ids[0]);
			return;
		}

		writer.WriteValue(options, value.Ids);
	}
}
