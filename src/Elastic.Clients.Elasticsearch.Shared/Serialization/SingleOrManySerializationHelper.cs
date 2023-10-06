// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

#if ELASTICSEARCH_SERVERLESS
namespace Elastic.Clients.Elasticsearch.Serverless.Serialization;
#else
namespace Elastic.Clients.Elasticsearch.Serialization;
#endif

internal static class SingleOrManySerializationHelper
{
	public static IList<TItem> Deserialize<TItem>(ref Utf8JsonReader reader, JsonSerializerOptions options)
	{
		if (reader.TokenType == JsonTokenType.StartObject)
		{
			var singleItem = JsonSerializer.Deserialize<TItem>(ref reader, options);
			return new TItem[] { singleItem };
		}

		if (reader.TokenType == JsonTokenType.StartArray)
		{
			var list = new List<TItem>();
			while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
			{
				var item = JsonSerializer.Deserialize<TItem>(ref reader, options);
				list.Add(item);
			}
			return list;
		}

		// Handles situations such as a single sort value which can be a string
		// e.g. GET nuget-stats/_search
		// {
		//    "sort": "version"
		// }
		if (reader.TokenType == JsonTokenType.String)
		{
			var item = (TItem)JsonSerializer.Deserialize(ref reader, typeof(TItem), options);
			return new TItem[] { item };
		}

		throw new JsonException("Unexpected token.");
	}

	public static void Serialize<TItem>(ICollection<TItem> value, Utf8JsonWriter writer, JsonSerializerOptions options)
	{
		var count = value.Count;

		if (count == 0)
		{
			writer.WriteStartObject();
			writer.WriteEndObject();
			return;
		}

		if (count == 1)
		{
			JsonSerializer.Serialize<TItem>(writer, value.Single(), options);
			return;
		}

		writer.WriteStartArray();
		foreach (var item in value)
		{
			JsonSerializer.Serialize<TItem>(writer, item, options);
		}
		writer.WriteEndArray();
	}
}
