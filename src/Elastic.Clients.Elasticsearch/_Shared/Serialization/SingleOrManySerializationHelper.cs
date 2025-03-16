// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Elastic.Clients.Elasticsearch.Serialization;

internal static class SingleOrManySerializationHelper
{
	public static List<TItem> Deserialize<TItem>(ref Utf8JsonReader reader, JsonSerializerOptions options)
	{
		switch (reader.TokenType)
		{
			case JsonTokenType.Null:
				return [default];

			case JsonTokenType.StartArray:
				return JsonSerializer.Deserialize<List<TItem>>(ref reader, options);

			default:
			{
				var item = (TItem)JsonSerializer.Deserialize(ref reader, typeof(TItem), options);
				return [item];
			}
		}
	}

	public static void Serialize<TItem>(ICollection<TItem> value, Utf8JsonWriter writer, JsonSerializerOptions options)
	{
		switch (value)
		{
			case null:
				writer.WriteNullValue();
				break;

			case { Count: 1 }:
				JsonSerializer.Serialize(writer, value.First(), options);
				break;

			default:
				JsonSerializer.Serialize(writer, value, options);
				break;
		}
	}
}
