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

internal sealed partial class FieldUsageStatsResponseConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.IndexManagement.FieldUsageStatsResponse>
{
	private static readonly System.Text.Json.JsonEncodedText PropShards = System.Text.Json.JsonEncodedText.Encode("_shards");

	public override Elastic.Clients.Elasticsearch.IndexManagement.FieldUsageStatsResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.ShardStatistics> propShards = default;
		System.Collections.Generic.Dictionary<string, Elastic.Clients.Elasticsearch.IndexManagement.UsageStatsIndex>? propStats = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propShards.TryReadProperty(ref reader, options, PropShards, null))
			{
				continue;
			}

			propStats ??= new System.Collections.Generic.Dictionary<string, Elastic.Clients.Elasticsearch.IndexManagement.UsageStatsIndex>();
			reader.ReadProperty(options, out string key, out Elastic.Clients.Elasticsearch.IndexManagement.UsageStatsIndex value, static string (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadPropertyName<string>(o)!, null);
			propStats[key] = value;
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Elastic.Clients.Elasticsearch.IndexManagement.FieldUsageStatsResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Shards = propShards.Value,
			Stats = propStats
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.IndexManagement.FieldUsageStatsResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropShards, value.Shards, null, null);
		if (value.Stats is not null)
		{
			foreach (var item in value.Stats)
			{
				writer.WriteProperty(options, item.Key, item.Value, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, string v) => w.WritePropertyName<string>(o, v), null);
			}
		}

		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.IndexManagement.FieldUsageStatsResponseConverter))]
public sealed partial class FieldUsageStatsResponse : Elastic.Transport.Products.Elasticsearch.ElasticsearchResponse
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public FieldUsageStatsResponse()
	{
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal FieldUsageStatsResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
		required
#endif
		Elastic.Clients.Elasticsearch.ShardStatistics Shards { get; set; }

	/// <summary>
	/// <para>
	/// Per index statistics
	/// </para>
	/// </summary>
	public System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.IndexManagement.UsageStatsIndex>? Stats { get; set; }
}