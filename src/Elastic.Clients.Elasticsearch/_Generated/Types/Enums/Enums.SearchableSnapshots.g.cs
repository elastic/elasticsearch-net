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

namespace Elastic.Clients.Elasticsearch.SearchableSnapshots;

internal sealed partial class StatsLevelConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.SearchableSnapshots.StatsLevel>
{
	private static readonly System.Text.Json.JsonEncodedText MemberCluster = System.Text.Json.JsonEncodedText.Encode("cluster");
	private static readonly System.Text.Json.JsonEncodedText MemberIndices = System.Text.Json.JsonEncodedText.Encode("indices");
	private static readonly System.Text.Json.JsonEncodedText MemberShards = System.Text.Json.JsonEncodedText.Encode("shards");

	public override Elastic.Clients.Elasticsearch.SearchableSnapshots.StatsLevel Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		if (reader.ValueTextEquals(MemberCluster))
		{
			return Elastic.Clients.Elasticsearch.SearchableSnapshots.StatsLevel.Cluster;
		}

		if (reader.ValueTextEquals(MemberIndices))
		{
			return Elastic.Clients.Elasticsearch.SearchableSnapshots.StatsLevel.Indices;
		}

		if (reader.ValueTextEquals(MemberShards))
		{
			return Elastic.Clients.Elasticsearch.SearchableSnapshots.StatsLevel.Shards;
		}

		var value = reader.GetString()!;
		if (string.Equals(value, MemberCluster.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return Elastic.Clients.Elasticsearch.SearchableSnapshots.StatsLevel.Cluster;
		}

		if (string.Equals(value, MemberIndices.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return Elastic.Clients.Elasticsearch.SearchableSnapshots.StatsLevel.Indices;
		}

		if (string.Equals(value, MemberShards.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return Elastic.Clients.Elasticsearch.SearchableSnapshots.StatsLevel.Shards;
		}

		throw new System.Text.Json.JsonException($"Unknown member '{value}' for enum '{nameof(Elastic.Clients.Elasticsearch.SearchableSnapshots.StatsLevel)}'.");
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.SearchableSnapshots.StatsLevel value, System.Text.Json.JsonSerializerOptions options)
	{
		switch (value)
		{
			case Elastic.Clients.Elasticsearch.SearchableSnapshots.StatsLevel.Cluster:
				writer.WriteStringValue(MemberCluster);
				break;
			case Elastic.Clients.Elasticsearch.SearchableSnapshots.StatsLevel.Indices:
				writer.WriteStringValue(MemberIndices);
				break;
			case Elastic.Clients.Elasticsearch.SearchableSnapshots.StatsLevel.Shards:
				writer.WriteStringValue(MemberShards);
				break;
			default:
				throw new System.Text.Json.JsonException($"Invalid value '{value}' for enum '{nameof(Elastic.Clients.Elasticsearch.SearchableSnapshots.StatsLevel)}'.");
		}
	}

	public override Elastic.Clients.Elasticsearch.SearchableSnapshots.StatsLevel ReadAsPropertyName(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		return Read(ref reader, typeToConvert, options);
	}

	public override void WriteAsPropertyName(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.SearchableSnapshots.StatsLevel value, System.Text.Json.JsonSerializerOptions options)
	{
		Write(writer, value, options);
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.SearchableSnapshots.StatsLevelConverter))]
public enum StatsLevel
{
	[System.Runtime.Serialization.EnumMember(Value = "cluster")]
	Cluster,
	[System.Runtime.Serialization.EnumMember(Value = "indices")]
	Indices,
	[System.Runtime.Serialization.EnumMember(Value = "shards")]
	Shards
}