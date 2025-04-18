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

namespace Elastic.Clients.Elasticsearch;

internal sealed partial class SearchShardsResponseConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.SearchShardsResponse>
{
	private static readonly System.Text.Json.JsonEncodedText PropIndices = System.Text.Json.JsonEncodedText.Encode("indices");
	private static readonly System.Text.Json.JsonEncodedText PropNodes = System.Text.Json.JsonEncodedText.Encode("nodes");
	private static readonly System.Text.Json.JsonEncodedText PropShards = System.Text.Json.JsonEncodedText.Encode("shards");

	public override Elastic.Clients.Elasticsearch.SearchShardsResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Core.SearchShards.ShardStoreIndex>> propIndices = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Core.SearchShards.SearchShardsNodeAttributes>> propNodes = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.NodeShard>>> propShards = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propIndices.TryReadProperty(ref reader, options, PropIndices, static System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Core.SearchShards.ShardStoreIndex> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, Elastic.Clients.Elasticsearch.Core.SearchShards.ShardStoreIndex>(o, null, null)!))
			{
				continue;
			}

			if (propNodes.TryReadProperty(ref reader, options, PropNodes, static System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Core.SearchShards.SearchShardsNodeAttributes> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, Elastic.Clients.Elasticsearch.Core.SearchShards.SearchShardsNodeAttributes>(o, null, null)!))
			{
				continue;
			}

			if (propShards.TryReadProperty(ref reader, options, PropShards, static System.Collections.Generic.IReadOnlyCollection<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.NodeShard>> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.NodeShard>>(o, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.NodeShard> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.NodeShard>(o, null)!)!))
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
		return new Elastic.Clients.Elasticsearch.SearchShardsResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Indices = propIndices.Value,
			Nodes = propNodes.Value,
			Shards = propShards.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.SearchShardsResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropIndices, value.Indices, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Core.SearchShards.ShardStoreIndex> v) => w.WriteDictionaryValue<string, Elastic.Clients.Elasticsearch.Core.SearchShards.ShardStoreIndex>(o, v, null, null));
		writer.WriteProperty(options, PropNodes, value.Nodes, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Core.SearchShards.SearchShardsNodeAttributes> v) => w.WriteDictionaryValue<string, Elastic.Clients.Elasticsearch.Core.SearchShards.SearchShardsNodeAttributes>(o, v, null, null));
		writer.WriteProperty(options, PropShards, value.Shards, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.NodeShard>> v) => w.WriteCollectionValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.NodeShard>>(o, v, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.NodeShard> v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.NodeShard>(o, v, null)));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.SearchShardsResponseConverter))]
public sealed partial class SearchShardsResponse : Elastic.Transport.Products.Elasticsearch.ElasticsearchResponse
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SearchShardsResponse()
	{
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal SearchShardsResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
		required
#endif
		System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Core.SearchShards.ShardStoreIndex> Indices { get; set; }
	public
#if NET7_0_OR_GREATER
		required
#endif
		System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Core.SearchShards.SearchShardsNodeAttributes> Nodes { get; set; }
	public
#if NET7_0_OR_GREATER
		required
#endif
		System.Collections.Generic.IReadOnlyCollection<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.NodeShard>> Shards { get; set; }
}