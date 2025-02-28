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

namespace Elastic.Clients.Elasticsearch.MachineLearning;

internal sealed partial class GetDatafeedStatsResponseConverter : System.Text.Json.Serialization.JsonConverter<GetDatafeedStatsResponse>
{
	private static readonly System.Text.Json.JsonEncodedText PropCount = System.Text.Json.JsonEncodedText.Encode("count");
	private static readonly System.Text.Json.JsonEncodedText PropDatafeeds = System.Text.Json.JsonEncodedText.Encode("datafeeds");

	public override GetDatafeedStatsResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<long> propCount = default;
		LocalJsonValue<IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.DatafeedStats>> propDatafeeds = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propCount.TryReadProperty(ref reader, options, PropCount, null))
			{
				continue;
			}

			if (propDatafeeds.TryReadProperty(ref reader, options, PropDatafeeds, static IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.DatafeedStats> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.MachineLearning.DatafeedStats>(o, null)!))
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
		return new GetDatafeedStatsResponse
		{
			Count = propCount.Value
,
			Datafeeds = propDatafeeds.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, GetDatafeedStatsResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropCount, value.Count, null, null);
		writer.WriteProperty(options, PropDatafeeds, value.Datafeeds, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.DatafeedStats> v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.MachineLearning.DatafeedStats>(o, v, null));
		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(GetDatafeedStatsResponseConverter))]
public sealed partial class GetDatafeedStatsResponse : ElasticsearchResponse
{
	public long Count { get; init; }
	public IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.DatafeedStats> Datafeeds { get; init; }
}