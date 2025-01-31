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

namespace Elastic.Clients.Elasticsearch;

internal sealed partial class SearchTemplateResponseConverter<TDocument> : System.Text.Json.Serialization.JsonConverter<SearchTemplateResponse<TDocument>>
{
	private static readonly System.Text.Json.JsonEncodedText PropAggregations = System.Text.Json.JsonEncodedText.Encode("aggregations");
	private static readonly System.Text.Json.JsonEncodedText PropClusters = System.Text.Json.JsonEncodedText.Encode("_clusters");
	private static readonly System.Text.Json.JsonEncodedText PropFields = System.Text.Json.JsonEncodedText.Encode("fields");
	private static readonly System.Text.Json.JsonEncodedText PropHits = System.Text.Json.JsonEncodedText.Encode("hits");
	private static readonly System.Text.Json.JsonEncodedText PropMaxScore = System.Text.Json.JsonEncodedText.Encode("max_score");
	private static readonly System.Text.Json.JsonEncodedText PropNumReducePhases = System.Text.Json.JsonEncodedText.Encode("num_reduce_phases");
	private static readonly System.Text.Json.JsonEncodedText PropPitId = System.Text.Json.JsonEncodedText.Encode("pit_id");
	private static readonly System.Text.Json.JsonEncodedText PropProfile = System.Text.Json.JsonEncodedText.Encode("profile");
	private static readonly System.Text.Json.JsonEncodedText PropScrollId = System.Text.Json.JsonEncodedText.Encode("_scroll_id");
	private static readonly System.Text.Json.JsonEncodedText PropShards = System.Text.Json.JsonEncodedText.Encode("_shards");
	private static readonly System.Text.Json.JsonEncodedText PropSuggest = System.Text.Json.JsonEncodedText.Encode("suggest");
	private static readonly System.Text.Json.JsonEncodedText PropTerminatedEarly = System.Text.Json.JsonEncodedText.Encode("terminated_early");
	private static readonly System.Text.Json.JsonEncodedText PropTimedOut = System.Text.Json.JsonEncodedText.Encode("timed_out");
	private static readonly System.Text.Json.JsonEncodedText PropTook = System.Text.Json.JsonEncodedText.Encode("took");

	public override SearchTemplateResponse<TDocument> Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Aggregations.AggregateDictionary?> propAggregations = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.ClusterStatistics?> propClusters = default;
		LocalJsonValue<IReadOnlyDictionary<string, object>?> propFields = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Core.Search.HitsMetadata<TDocument>> propHits = default;
		LocalJsonValue<double?> propMaxScore = default;
		LocalJsonValue<long?> propNumReducePhases = default;
		LocalJsonValue<string?> propPitId = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Core.Search.Profile?> propProfile = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.ScrollId?> propScrollId = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.ShardStatistics> propShards = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Core.Search.SuggestDictionary<TDocument>?> propSuggest = default;
		LocalJsonValue<bool?> propTerminatedEarly = default;
		LocalJsonValue<bool> propTimedOut = default;
		LocalJsonValue<long> propTook = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAggregations.TryRead(ref reader, options, PropAggregations))
			{
				continue;
			}

			if (propClusters.TryRead(ref reader, options, PropClusters))
			{
				continue;
			}

			if (propFields.TryRead(ref reader, options, PropFields))
			{
				continue;
			}

			if (propHits.TryRead(ref reader, options, PropHits))
			{
				continue;
			}

			if (propMaxScore.TryRead(ref reader, options, PropMaxScore))
			{
				continue;
			}

			if (propNumReducePhases.TryRead(ref reader, options, PropNumReducePhases))
			{
				continue;
			}

			if (propPitId.TryRead(ref reader, options, PropPitId))
			{
				continue;
			}

			if (propProfile.TryRead(ref reader, options, PropProfile))
			{
				continue;
			}

			if (propScrollId.TryRead(ref reader, options, PropScrollId))
			{
				continue;
			}

			if (propShards.TryRead(ref reader, options, PropShards))
			{
				continue;
			}

			if (propSuggest.TryRead(ref reader, options, PropSuggest))
			{
				continue;
			}

			if (propTerminatedEarly.TryRead(ref reader, options, PropTerminatedEarly))
			{
				continue;
			}

			if (propTimedOut.TryRead(ref reader, options, PropTimedOut))
			{
				continue;
			}

			if (propTook.TryRead(ref reader, options, PropTook))
			{
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new SearchTemplateResponse<TDocument>
		{
			Aggregations = propAggregations.Value
,
			Clusters = propClusters.Value
,
			Fields = propFields.Value
,
			Hits = propHits.Value
,
			MaxScore = propMaxScore.Value
,
			NumReducePhases = propNumReducePhases.Value
,
			PitId = propPitId.Value
,
			Profile = propProfile.Value
,
			ScrollId = propScrollId.Value
,
			Shards = propShards.Value
,
			Suggest = propSuggest.Value
,
			TerminatedEarly = propTerminatedEarly.Value
,
			TimedOut = propTimedOut.Value
,
			Took = propTook.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, SearchTemplateResponse<TDocument> value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAggregations, value.Aggregations);
		writer.WriteProperty(options, PropClusters, value.Clusters);
		writer.WriteProperty(options, PropFields, value.Fields);
		writer.WriteProperty(options, PropHits, value.Hits);
		writer.WriteProperty(options, PropMaxScore, value.MaxScore);
		writer.WriteProperty(options, PropNumReducePhases, value.NumReducePhases);
		writer.WriteProperty(options, PropPitId, value.PitId);
		writer.WriteProperty(options, PropProfile, value.Profile);
		writer.WriteProperty(options, PropScrollId, value.ScrollId);
		writer.WriteProperty(options, PropShards, value.Shards);
		writer.WriteProperty(options, PropSuggest, value.Suggest);
		writer.WriteProperty(options, PropTerminatedEarly, value.TerminatedEarly);
		writer.WriteProperty(options, PropTimedOut, value.TimedOut);
		writer.WriteProperty(options, PropTook, value.Took);
		writer.WriteEndObject();
	}
}

internal sealed partial class SearchTemplateResponseConverterFactory : System.Text.Json.Serialization.JsonConverterFactory
{
	public override bool CanConvert(System.Type typeToConvert)
	{
		return typeToConvert.IsGenericType && typeToConvert.GetGenericTypeDefinition() == typeof(SearchTemplateResponse<>);
	}

	public override System.Text.Json.Serialization.JsonConverter CreateConverter(System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		var args = typeToConvert.GetGenericArguments();
#pragma warning disable IL3050
		var converter = (System.Text.Json.Serialization.JsonConverter)System.Activator.CreateInstance(typeof(SearchTemplateResponseConverter<>).MakeGenericType(args[0]), System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public, binder: null, args: null, culture: null)!;
#pragma warning restore IL3050
		return converter;
	}
}

[JsonConverter(typeof(SearchTemplateResponseConverterFactory))]
public sealed partial class SearchTemplateResponse<TDocument> : ElasticsearchResponse
{
	public Elastic.Clients.Elasticsearch.Aggregations.AggregateDictionary? Aggregations { get; init; }
	public Elastic.Clients.Elasticsearch.ClusterStatistics? Clusters { get; init; }
	public IReadOnlyDictionary<string, object>? Fields { get; init; }
	public Elastic.Clients.Elasticsearch.Core.Search.HitsMetadata<TDocument> Hits { get; init; }
	public double? MaxScore { get; init; }
	public long? NumReducePhases { get; init; }
	public string? PitId { get; init; }
	public Elastic.Clients.Elasticsearch.Core.Search.Profile? Profile { get; init; }
	public Elastic.Clients.Elasticsearch.ScrollId? ScrollId { get; init; }
	public Elastic.Clients.Elasticsearch.ShardStatistics Shards { get; init; }
	public Elastic.Clients.Elasticsearch.Core.Search.SuggestDictionary<TDocument>? Suggest { get; init; }
	public bool? TerminatedEarly { get; init; }
	public bool TimedOut { get; init; }
	public long Took { get; init; }
}