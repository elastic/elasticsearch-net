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

namespace Elastic.Clients.Elasticsearch.SearchableSnapshots;

internal sealed partial class SearchableSnapshotsStatsResponseConverter : System.Text.Json.Serialization.JsonConverter<SearchableSnapshotsStatsResponse>
{
	private static readonly System.Text.Json.JsonEncodedText PropStats = System.Text.Json.JsonEncodedText.Encode("stats");
	private static readonly System.Text.Json.JsonEncodedText PropTotal = System.Text.Json.JsonEncodedText.Encode("total");

	public override SearchableSnapshotsStatsResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<object> propStats = default;
		LocalJsonValue<object> propTotal = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propStats.TryReadProperty(ref reader, options, PropStats, null))
			{
				continue;
			}

			if (propTotal.TryReadProperty(ref reader, options, PropTotal, null))
			{
				continue;
			}

			if (options.UnmappedMemberHandling is System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new SearchableSnapshotsStatsResponse
		{
			Stats = propStats.Value
,
			Total = propTotal.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, SearchableSnapshotsStatsResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropStats, value.Stats, null, null);
		writer.WriteProperty(options, PropTotal, value.Total, null, null);
		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(SearchableSnapshotsStatsResponseConverter))]
public sealed partial class SearchableSnapshotsStatsResponse : ElasticsearchResponse
{
	public object Stats { get; init; }
	public object Total { get; init; }
}