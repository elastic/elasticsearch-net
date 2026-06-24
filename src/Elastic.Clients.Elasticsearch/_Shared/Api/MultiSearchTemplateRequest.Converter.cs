// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

using Elastic.Clients.Elasticsearch.Core.MSearch;
using Elastic.Clients.Elasticsearch.Core.MSearchTemplate;
using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch;

/// <summary>
/// Reads the NDJSON body of a multi-search-template request — alternating header/template-body line pairs — back into a
/// <see cref="MultiSearchTemplateRequest"/>. See <see cref="MultiSearchRequestConverter"/> for the array-vs-NDJSON
/// handling; writing flows through <see cref="IStreamSerializable"/>.
/// </summary>
public sealed class MultiSearchTemplateRequestConverter : JsonConverter<MultiSearchTemplateRequest>
{
	public override MultiSearchTemplateRequest Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var searchTemplates = new List<SearchTemplateRequestItem>();

		var isArray = reader.TokenType is JsonTokenType.StartArray;
		if (isArray)
			reader.Read();

		while (reader.TokenType is not JsonTokenType.EndArray)
		{
			var header = reader.ReadValue<MultisearchHeader>(options);
			if (!reader.Read())
				throw new JsonException("Expected a template body line following the header in the multi-search-template NDJSON body.");

			var body = reader.ReadValue<TemplateConfig>(options);
			searchTemplates.Add(new SearchTemplateRequestItem(header!, body!));

			if (!reader.Read())
				break;
		}

		return new MultiSearchTemplateRequest(JsonConstructorSentinel.Instance) { SearchTemplates = searchTemplates };
	}

	public override void Write(Utf8JsonWriter writer, MultiSearchTemplateRequest value, JsonSerializerOptions options) =>
		throw new NotSupportedException("'MultiSearchTemplateRequest' is written as NDJSON through 'IStreamSerializable', not via this converter.");
}
