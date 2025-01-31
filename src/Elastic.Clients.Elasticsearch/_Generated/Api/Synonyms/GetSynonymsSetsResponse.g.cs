// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.
//
// ███╗   ██╗ ██████╗ ████████╗██╗ ██████╗███████╗
// ████╗  ██║██╔═══██╗╚══██╔══╝██║██╔════╝██╔════╝
// ██╔██╗ ██║██║   ██║   ██║   ██║██║     █████╗
// ██║╚██╗██║██║   ██║   ██║   ██║██║     ██╔══╝
// ██║ ╚████║╚██████╔╝   ██║   ██║╚██████╗███████╗
// ╚═╝  ╚═══╝ ╚═════╝    ╚═╝   ╚═╝ ╚═════╝╚══════╝
// ------------------------------------------------
//
// This file is automatically generated.
// Please do not edit these files manually.
//
// ------------------------------------------------

#nullable restore

using Elastic.Clients.Elasticsearch.Fluent;
using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport.Products.Elasticsearch;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Synonyms;

internal sealed partial class GetSynonymsSetsResponseConverter : System.Text.Json.Serialization.JsonConverter<GetSynonymsSetsResponse>
{
	private static readonly System.Text.Json.JsonEncodedText PropCount = System.Text.Json.JsonEncodedText.Encode("count");
	private static readonly System.Text.Json.JsonEncodedText PropResults = System.Text.Json.JsonEncodedText.Encode("results");

	public override GetSynonymsSetsResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<int> propCount = default;
		LocalJsonValue<IReadOnlyCollection<Elastic.Clients.Elasticsearch.Synonyms.SynonymsSetItem>> propResults = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propCount.TryRead(ref reader, options, PropCount))
			{
				continue;
			}

			if (propResults.TryRead(ref reader, options, PropResults))
			{
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new GetSynonymsSetsResponse
		{
			Count = propCount.Value
,
			Results = propResults.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, GetSynonymsSetsResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropCount, value.Count);
		writer.WriteProperty(options, PropResults, value.Results);
		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(GetSynonymsSetsResponseConverter))]
public sealed partial class GetSynonymsSetsResponse : ElasticsearchResponse
{
	public int Count { get; init; }
	public IReadOnlyCollection<Elastic.Clients.Elasticsearch.Synonyms.SynonymsSetItem> Results { get; init; }
}