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

internal sealed partial class IndicesStatsConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.IndexManagement.IndicesStats>
{
	private static readonly System.Text.Json.JsonEncodedText PropHealth = System.Text.Json.JsonEncodedText.Encode("health");
	private static readonly System.Text.Json.JsonEncodedText PropPrimaries = System.Text.Json.JsonEncodedText.Encode("primaries");
	private static readonly System.Text.Json.JsonEncodedText PropShards = System.Text.Json.JsonEncodedText.Encode("shards");
	private static readonly System.Text.Json.JsonEncodedText PropStatus = System.Text.Json.JsonEncodedText.Encode("status");
	private static readonly System.Text.Json.JsonEncodedText PropTotal = System.Text.Json.JsonEncodedText.Encode("total");
	private static readonly System.Text.Json.JsonEncodedText PropUuid = System.Text.Json.JsonEncodedText.Encode("uuid");

	public override Elastic.Clients.Elasticsearch.IndexManagement.IndicesStats Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.HealthStatus?> propHealth = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.IndexManagement.IndexStats?> propPrimaries = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.IndexManagement.ShardStats>>?> propShards = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.IndexManagement.IndexMetadataState?> propStatus = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.IndexManagement.IndexStats?> propTotal = default;
		LocalJsonValue<string?> propUuid = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propHealth.TryReadProperty(ref reader, options, PropHealth, null))
			{
				continue;
			}

			if (propPrimaries.TryReadProperty(ref reader, options, PropPrimaries, null))
			{
				continue;
			}

			if (propShards.TryReadProperty(ref reader, options, PropShards, static System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.IndexManagement.ShardStats>>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.IndexManagement.ShardStats>>(o, null, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.IndexManagement.ShardStats> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.IndexManagement.ShardStats>(o, null)!)))
			{
				continue;
			}

			if (propStatus.TryReadProperty(ref reader, options, PropStatus, null))
			{
				continue;
			}

			if (propTotal.TryReadProperty(ref reader, options, PropTotal, null))
			{
				continue;
			}

			if (propUuid.TryReadProperty(ref reader, options, PropUuid, null))
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
		return new Elastic.Clients.Elasticsearch.IndexManagement.IndicesStats(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Health = propHealth.Value,
			Primaries = propPrimaries.Value,
			Shards = propShards.Value,
			Status = propStatus.Value,
			Total = propTotal.Value,
			Uuid = propUuid.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.IndexManagement.IndicesStats value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropHealth, value.Health, null, null);
		writer.WriteProperty(options, PropPrimaries, value.Primaries, null, null);
		writer.WriteProperty(options, PropShards, value.Shards, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.IndexManagement.ShardStats>>? v) => w.WriteDictionaryValue<string, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.IndexManagement.ShardStats>>(o, v, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.IndexManagement.ShardStats> v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.IndexManagement.ShardStats>(o, v, null)));
		writer.WriteProperty(options, PropStatus, value.Status, null, null);
		writer.WriteProperty(options, PropTotal, value.Total, null, null);
		writer.WriteProperty(options, PropUuid, value.Uuid, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.IndexManagement.IndicesStatsConverter))]
public sealed partial class IndicesStats
{
#if NET7_0_OR_GREATER
	public IndicesStats()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public IndicesStats()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal IndicesStats(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public Elastic.Clients.Elasticsearch.HealthStatus? Health { get; set; }
	public Elastic.Clients.Elasticsearch.IndexManagement.IndexStats? Primaries { get; set; }
	public System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.IndexManagement.ShardStats>>? Shards { get; set; }
	public Elastic.Clients.Elasticsearch.IndexManagement.IndexMetadataState? Status { get; set; }
	public Elastic.Clients.Elasticsearch.IndexManagement.IndexStats? Total { get; set; }
	public string? Uuid { get; set; }
}