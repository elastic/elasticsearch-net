// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

using Elastic.Clients.Elasticsearch.Core.MSearch;
using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch;

/// <summary>
/// Reads the NDJSON body of a multi-search request (a sequence of alternating header/body line pairs) back into a
/// <see cref="MultiSearchRequest"/>. Accepts either a JSON array of line values (as recorded by tooling) or genuine
/// multi-top-level-value NDJSON; the enclosing <see cref="Utf8JsonReader"/> is created with
/// <c>JsonReaderOptions.AllowMultipleValues</c> so the loop can walk successive top-level values. Writing continues to
/// flow through the <see cref="IStreamSerializable"/> path, so <see cref="Write"/> is not used on the normal request path.
/// </summary>
public sealed class MultiSearchRequestConverter : JsonConverter<MultiSearchRequest>
{
	public override MultiSearchRequest Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var searches = new List<SearchRequestItem>();

		// Either a JSON array of line-values ([header, body, ...]) or top-level NDJSON values. For the array form we
		// step inside; for NDJSON we are already positioned on the first header.
		var isArray = reader.TokenType is JsonTokenType.StartArray;
		if (isArray)
			reader.Read();

		while (reader.TokenType is not JsonTokenType.EndArray)
		{
			var header = reader.ReadValue<MultisearchHeader>(options);
			if (!reader.Read())
				throw new JsonException("Expected a search body line following the header in the multi-search NDJSON body.");

			var body = reader.ReadValue<MultisearchBody>(options);
			searches.Add(new SearchRequestItem(header!, body!));

			// Advance to the next header (array element or top-level value); stop when the NDJSON stream ends.
			if (!reader.Read())
				break;
		}

		return new MultiSearchRequest(JsonConstructorSentinel.Instance) { Searches = searches };
	}

	public override void Write(Utf8JsonWriter writer, MultiSearchRequest value, JsonSerializerOptions options) =>
		throw new NotSupportedException("'MultiSearchRequest' is written as NDJSON through 'IStreamSerializable', not via this converter.");
}
