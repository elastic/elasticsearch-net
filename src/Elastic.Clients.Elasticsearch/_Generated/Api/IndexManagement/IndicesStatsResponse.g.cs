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

namespace Elastic.Clients.Elasticsearch.IndexManagement;

internal sealed partial class IndicesStatsResponseConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.IndexManagement.IndicesStatsResponse>
{
	private static readonly System.Text.Json.JsonEncodedText PropAll = System.Text.Json.JsonEncodedText.Encode("_all");
	private static readonly System.Text.Json.JsonEncodedText PropIndices = System.Text.Json.JsonEncodedText.Encode("indices");
	private static readonly System.Text.Json.JsonEncodedText PropShards = System.Text.Json.JsonEncodedText.Encode("_shards");

	public override Elastic.Clients.Elasticsearch.IndexManagement.IndicesStatsResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.IndexManagement.IndicesStats> propAll = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.IndexManagement.IndicesStats>?> propIndices = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.ShardStatistics> propShards = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAll.TryReadProperty(ref reader, options, PropAll, null))
			{
				continue;
			}

			if (propIndices.TryReadProperty(ref reader, options, PropIndices, static System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.IndexManagement.IndicesStats>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, Elastic.Clients.Elasticsearch.IndexManagement.IndicesStats>(o, null, null)))
			{
				continue;
			}

			if (propShards.TryReadProperty(ref reader, options, PropShards, null))
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
		return new Elastic.Clients.Elasticsearch.IndexManagement.IndicesStatsResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			All = propAll.Value,
			Indices = propIndices.Value,
			Shards = propShards.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.IndexManagement.IndicesStatsResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAll, value.All, null, null);
		writer.WriteProperty(options, PropIndices, value.Indices, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.IndexManagement.IndicesStats>? v) => w.WriteDictionaryValue<string, Elastic.Clients.Elasticsearch.IndexManagement.IndicesStats>(o, v, null, null));
		writer.WriteProperty(options, PropShards, value.Shards, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.IndexManagement.IndicesStatsResponseConverter))]
public sealed partial class IndicesStatsResponse : Elastic.Transport.Products.Elasticsearch.ElasticsearchResponse
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public IndicesStatsResponse(Elastic.Clients.Elasticsearch.IndexManagement.IndicesStats all, Elastic.Clients.Elasticsearch.ShardStatistics shards)
	{
		All = all;
		Shards = shards;
	}

	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public IndicesStatsResponse()
	{
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal IndicesStatsResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
		required
#endif
		Elastic.Clients.Elasticsearch.IndexManagement.IndicesStats All { get; set; }
	public System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.IndexManagement.IndicesStats>? Indices { get; set; }
	public
#if NET7_0_OR_GREATER
		required
#endif
		Elastic.Clients.Elasticsearch.ShardStatistics Shards { get; set; }
}