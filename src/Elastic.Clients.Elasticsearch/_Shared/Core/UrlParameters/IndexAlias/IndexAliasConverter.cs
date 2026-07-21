// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Json;

public sealed class IndexAliasConverter : JsonConverter<IndexAlias>
{
	public override IndexAlias? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType != JsonTokenType.String)
			throw new JsonException($"Unexpected token '{reader.TokenType}' for IndexAlias");

		return reader.GetString();
	}

	public override void Write(Utf8JsonWriter writer, IndexAlias value, JsonSerializerOptions options)
	{
		if (value is null || value.Alias is null)
		{
			writer.WriteNullValue();
			return;
		}

		writer.WriteStringValue(value.Alias);
	}
}
