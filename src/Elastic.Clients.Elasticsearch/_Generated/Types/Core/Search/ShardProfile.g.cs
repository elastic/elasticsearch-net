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

using System;
using System.Linq;
using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch.Core.Search;

internal sealed partial class ShardProfileConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Core.Search.ShardProfile>
{
	private static readonly System.Text.Json.JsonEncodedText PropAggregations = System.Text.Json.JsonEncodedText.Encode("aggregations");
	private static readonly System.Text.Json.JsonEncodedText PropCluster = System.Text.Json.JsonEncodedText.Encode("cluster");
	private static readonly System.Text.Json.JsonEncodedText PropDfs = System.Text.Json.JsonEncodedText.Encode("dfs");
	private static readonly System.Text.Json.JsonEncodedText PropFetch = System.Text.Json.JsonEncodedText.Encode("fetch");
	private static readonly System.Text.Json.JsonEncodedText PropId = System.Text.Json.JsonEncodedText.Encode("id");
	private static readonly System.Text.Json.JsonEncodedText PropIndex = System.Text.Json.JsonEncodedText.Encode("index");
	private static readonly System.Text.Json.JsonEncodedText PropNodeId = System.Text.Json.JsonEncodedText.Encode("node_id");
	private static readonly System.Text.Json.JsonEncodedText PropSearches = System.Text.Json.JsonEncodedText.Encode("searches");
	private static readonly System.Text.Json.JsonEncodedText PropShardId = System.Text.Json.JsonEncodedText.Encode("shard_id");

	public override Elastic.Clients.Elasticsearch.Core.Search.ShardProfile Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Core.Search.AggregationProfile>> propAggregations = default;
		LocalJsonValue<string> propCluster = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Core.Search.DfsProfile?> propDfs = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Core.Search.FetchProfile?> propFetch = default;
		LocalJsonValue<string> propId = default;
		LocalJsonValue<string> propIndex = default;
		LocalJsonValue<string> propNodeId = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Core.Search.SearchProfile>> propSearches = default;
		LocalJsonValue<long> propShardId = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAggregations.TryReadProperty(ref reader, options, PropAggregations, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Core.Search.AggregationProfile> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.Core.Search.AggregationProfile>(o, null)!))
			{
				continue;
			}

			if (propCluster.TryReadProperty(ref reader, options, PropCluster, null))
			{
				continue;
			}

			if (propDfs.TryReadProperty(ref reader, options, PropDfs, null))
			{
				continue;
			}

			if (propFetch.TryReadProperty(ref reader, options, PropFetch, null))
			{
				continue;
			}

			if (propId.TryReadProperty(ref reader, options, PropId, null))
			{
				continue;
			}

			if (propIndex.TryReadProperty(ref reader, options, PropIndex, null))
			{
				continue;
			}

			if (propNodeId.TryReadProperty(ref reader, options, PropNodeId, null))
			{
				continue;
			}

			if (propSearches.TryReadProperty(ref reader, options, PropSearches, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Core.Search.SearchProfile> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.Core.Search.SearchProfile>(o, null)!))
			{
				continue;
			}

			if (propShardId.TryReadProperty(ref reader, options, PropShardId, null))
			{
				continue;
			}

			if (options.UnmappedMemberHandling is System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Elastic.Clients.Elasticsearch.Core.Search.ShardProfile(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Aggregations = propAggregations.Value,
			Cluster = propCluster.Value,
			Dfs = propDfs.Value,
			Fetch = propFetch.Value,
			Id = propId.Value,
			Index = propIndex.Value,
			NodeId = propNodeId.Value,
			Searches = propSearches.Value,
			ShardId = propShardId.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Core.Search.ShardProfile value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAggregations, value.Aggregations, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Core.Search.AggregationProfile> v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.Core.Search.AggregationProfile>(o, v, null));
		writer.WriteProperty(options, PropCluster, value.Cluster, null, null);
		writer.WriteProperty(options, PropDfs, value.Dfs, null, null);
		writer.WriteProperty(options, PropFetch, value.Fetch, null, null);
		writer.WriteProperty(options, PropId, value.Id, null, null);
		writer.WriteProperty(options, PropIndex, value.Index, null, null);
		writer.WriteProperty(options, PropNodeId, value.NodeId, null, null);
		writer.WriteProperty(options, PropSearches, value.Searches, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Core.Search.SearchProfile> v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.Core.Search.SearchProfile>(o, v, null));
		writer.WriteProperty(options, PropShardId, value.ShardId, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Core.Search.ShardProfileConverter))]
public sealed partial class ShardProfile
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ShardProfile(System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Core.Search.AggregationProfile> aggregations, string cluster, string id, string index, string nodeId, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Core.Search.SearchProfile> searches, long shardId)
	{
		Aggregations = aggregations;
		Cluster = cluster;
		Id = id;
		Index = index;
		NodeId = nodeId;
		Searches = searches;
		ShardId = shardId;
	}
#if NET7_0_OR_GREATER
	public ShardProfile()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public ShardProfile()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal ShardProfile(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Core.Search.AggregationProfile> Aggregations { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Cluster { get; set; }
	public Elastic.Clients.Elasticsearch.Core.Search.DfsProfile? Dfs { get; set; }
	public Elastic.Clients.Elasticsearch.Core.Search.FetchProfile? Fetch { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Id { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Index { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	string NodeId { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Core.Search.SearchProfile> Searches { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long ShardId { get; set; }
}