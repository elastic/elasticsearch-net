// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Core.Bulk;

internal sealed class BulkResponseItemConverter : JsonConverter<IReadOnlyList<ResponseItem>>
{
	public override IReadOnlyList<ResponseItem>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType != JsonTokenType.StartArray)
			throw new JsonException($"Unexpected token in bulk response items. Read {reader.TokenType} but was expecting {JsonTokenType.StartArray}.");

		var responseItems = new List<ResponseItem>();

		while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
		{
			if (reader.TokenType != JsonTokenType.StartObject)
				throw new JsonException($"Unexpected token in bulk response items. Read {reader.TokenType} but was expecting {JsonTokenType.StartObject}.");

			reader.Read();

			if (reader.TokenType != JsonTokenType.PropertyName)
				throw new JsonException($"Unexpected token in bulk response items. Read {reader.TokenType} but was expecting {JsonTokenType.PropertyName}.");

			ResponseItem responseItem;

			if (reader.ValueTextEquals("index"))
			{
				responseItem = JsonSerializer.Deserialize<BulkIndexResponseItem>(ref reader, options);
			}
			else if (reader.ValueTextEquals("delete"))
			{
				responseItem = JsonSerializer.Deserialize<BulkDeleteResponseItem>(ref reader, options);
			}
			else if (reader.ValueTextEquals("create"))
			{
				responseItem = JsonSerializer.Deserialize<CreateResponseItem>(ref reader, options);
			}
			else if (reader.ValueTextEquals("update"))
			{
				responseItem = JsonSerializer.Deserialize<BulkUpdateResponseItem>(ref reader, options);
			}
			else
			{
				throw new JsonException("Unexpected operation type in bulk response items.");
			}

			responseItems.Add(responseItem);

			reader.Read();

			if (reader.TokenType != JsonTokenType.EndObject)
				throw new JsonException($"Unexpected token in bulk response items. Read {reader.TokenType} but was expecting {JsonTokenType.EndObject}.");
		}

		return responseItems;
	}

	public override void Write(Utf8JsonWriter writer, IReadOnlyList<ResponseItem> value, JsonSerializerOptions options) => throw new NotImplementedException();
}
