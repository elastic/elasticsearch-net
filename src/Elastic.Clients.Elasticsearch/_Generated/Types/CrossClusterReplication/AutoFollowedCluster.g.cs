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

namespace Elastic.Clients.Elasticsearch.CrossClusterReplication;

internal sealed partial class AutoFollowedClusterConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.CrossClusterReplication.AutoFollowedCluster>
{
	private static readonly System.Text.Json.JsonEncodedText PropClusterName = System.Text.Json.JsonEncodedText.Encode("cluster_name");
	private static readonly System.Text.Json.JsonEncodedText PropLastSeenMetadataVersion = System.Text.Json.JsonEncodedText.Encode("last_seen_metadata_version");
	private static readonly System.Text.Json.JsonEncodedText PropTimeSinceLastCheckMillis = System.Text.Json.JsonEncodedText.Encode("time_since_last_check_millis");

	public override Elastic.Clients.Elasticsearch.CrossClusterReplication.AutoFollowedCluster Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string> propClusterName = default;
		LocalJsonValue<long> propLastSeenMetadataVersion = default;
		LocalJsonValue<System.TimeSpan> propTimeSinceLastCheckMillis = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propClusterName.TryReadProperty(ref reader, options, PropClusterName, null))
			{
				continue;
			}

			if (propLastSeenMetadataVersion.TryReadProperty(ref reader, options, PropLastSeenMetadataVersion, null))
			{
				continue;
			}

			if (propTimeSinceLastCheckMillis.TryReadProperty(ref reader, options, PropTimeSinceLastCheckMillis, static System.TimeSpan (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.TimeSpan>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanMillisMarker))))
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
		return new Elastic.Clients.Elasticsearch.CrossClusterReplication.AutoFollowedCluster(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			ClusterName = propClusterName.Value,
			LastSeenMetadataVersion = propLastSeenMetadataVersion.Value,
			TimeSinceLastCheckMillis = propTimeSinceLastCheckMillis.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.CrossClusterReplication.AutoFollowedCluster value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropClusterName, value.ClusterName, null, null);
		writer.WriteProperty(options, PropLastSeenMetadataVersion, value.LastSeenMetadataVersion, null, null);
		writer.WriteProperty(options, PropTimeSinceLastCheckMillis, value.TimeSinceLastCheckMillis, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.TimeSpan v) => w.WriteValueEx<System.TimeSpan>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanMillisMarker)));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.CrossClusterReplication.AutoFollowedClusterConverter))]
public sealed partial class AutoFollowedCluster
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public AutoFollowedCluster(string clusterName, long lastSeenMetadataVersion, System.TimeSpan timeSinceLastCheckMillis)
	{
		ClusterName = clusterName;
		LastSeenMetadataVersion = lastSeenMetadataVersion;
		TimeSinceLastCheckMillis = timeSinceLastCheckMillis;
	}
#if NET7_0_OR_GREATER
	public AutoFollowedCluster()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public AutoFollowedCluster()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal AutoFollowedCluster(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	string ClusterName { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long LastSeenMetadataVersion { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.TimeSpan TimeSinceLastCheckMillis { get; set; }
}