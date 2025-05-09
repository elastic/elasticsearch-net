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

internal sealed partial class UsageStatsShardsConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.IndexManagement.UsageStatsShards>
{
	private static readonly System.Text.Json.JsonEncodedText PropRouting = System.Text.Json.JsonEncodedText.Encode("routing");
	private static readonly System.Text.Json.JsonEncodedText PropStats = System.Text.Json.JsonEncodedText.Encode("stats");
	private static readonly System.Text.Json.JsonEncodedText PropTrackingId = System.Text.Json.JsonEncodedText.Encode("tracking_id");
	private static readonly System.Text.Json.JsonEncodedText PropTrackingStartedAtMillis = System.Text.Json.JsonEncodedText.Encode("tracking_started_at_millis");

	public override Elastic.Clients.Elasticsearch.IndexManagement.UsageStatsShards Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.IndexManagement.ShardRouting> propRouting = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.IndexManagement.ShardsStats> propStats = default;
		LocalJsonValue<string> propTrackingId = default;
		LocalJsonValue<System.DateTimeOffset> propTrackingStartedAtMillis = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propRouting.TryReadProperty(ref reader, options, PropRouting, null))
			{
				continue;
			}

			if (propStats.TryReadProperty(ref reader, options, PropStats, null))
			{
				continue;
			}

			if (propTrackingId.TryReadProperty(ref reader, options, PropTrackingId, null))
			{
				continue;
			}

			if (propTrackingStartedAtMillis.TryReadProperty(ref reader, options, PropTrackingStartedAtMillis, static System.DateTimeOffset (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.DateTimeOffset>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMillisMarker))))
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
		return new Elastic.Clients.Elasticsearch.IndexManagement.UsageStatsShards(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Routing = propRouting.Value,
			Stats = propStats.Value,
			TrackingId = propTrackingId.Value,
			TrackingStartedAtMillis = propTrackingStartedAtMillis.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.IndexManagement.UsageStatsShards value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropRouting, value.Routing, null, null);
		writer.WriteProperty(options, PropStats, value.Stats, null, null);
		writer.WriteProperty(options, PropTrackingId, value.TrackingId, null, null);
		writer.WriteProperty(options, PropTrackingStartedAtMillis, value.TrackingStartedAtMillis, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.DateTimeOffset v) => w.WriteValueEx<System.DateTimeOffset>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMillisMarker)));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.IndexManagement.UsageStatsShardsConverter))]
public sealed partial class UsageStatsShards
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public UsageStatsShards(Elastic.Clients.Elasticsearch.IndexManagement.ShardRouting routing, Elastic.Clients.Elasticsearch.IndexManagement.ShardsStats stats, string trackingId, System.DateTimeOffset trackingStartedAtMillis)
	{
		Routing = routing;
		Stats = stats;
		TrackingId = trackingId;
		TrackingStartedAtMillis = trackingStartedAtMillis;
	}
#if NET7_0_OR_GREATER
	public UsageStatsShards()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public UsageStatsShards()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal UsageStatsShards(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.IndexManagement.ShardRouting Routing { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.IndexManagement.ShardsStats Stats { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	string TrackingId { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.DateTimeOffset TrackingStartedAtMillis { get; set; }
}