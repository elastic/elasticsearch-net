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

namespace Elastic.Clients.Elasticsearch.Cluster;

internal sealed partial class ClusterStatsResponseConverter : System.Text.Json.Serialization.JsonConverter<ClusterStatsResponse>
{
	private static readonly System.Text.Json.JsonEncodedText PropClusterName = System.Text.Json.JsonEncodedText.Encode("cluster_name");
	private static readonly System.Text.Json.JsonEncodedText PropClusterUuid = System.Text.Json.JsonEncodedText.Encode("cluster_uuid");
	private static readonly System.Text.Json.JsonEncodedText PropIndices = System.Text.Json.JsonEncodedText.Encode("indices");
	private static readonly System.Text.Json.JsonEncodedText PropNodes = System.Text.Json.JsonEncodedText.Encode("nodes");
	private static readonly System.Text.Json.JsonEncodedText PropNodeStats = System.Text.Json.JsonEncodedText.Encode("_nodes");
	private static readonly System.Text.Json.JsonEncodedText PropStatus = System.Text.Json.JsonEncodedText.Encode("status");
	private static readonly System.Text.Json.JsonEncodedText PropTimestamp = System.Text.Json.JsonEncodedText.Encode("timestamp");

	public override ClusterStatsResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string> propClusterName = default;
		LocalJsonValue<string> propClusterUuid = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Cluster.ClusterIndices> propIndices = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Cluster.ClusterNodes> propNodes = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.NodeStatistics?> propNodeStats = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.HealthStatus> propStatus = default;
		LocalJsonValue<long> propTimestamp = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propClusterName.TryReadProperty(ref reader, options, PropClusterName, null))
			{
				continue;
			}

			if (propClusterUuid.TryReadProperty(ref reader, options, PropClusterUuid, null))
			{
				continue;
			}

			if (propIndices.TryReadProperty(ref reader, options, PropIndices, null))
			{
				continue;
			}

			if (propNodes.TryReadProperty(ref reader, options, PropNodes, null))
			{
				continue;
			}

			if (propNodeStats.TryReadProperty(ref reader, options, PropNodeStats, null))
			{
				continue;
			}

			if (propStatus.TryReadProperty(ref reader, options, PropStatus, null))
			{
				continue;
			}

			if (propTimestamp.TryReadProperty(ref reader, options, PropTimestamp, null))
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
		return new ClusterStatsResponse
		{
			ClusterName = propClusterName.Value
,
			ClusterUuid = propClusterUuid.Value
,
			Indices = propIndices.Value
,
			Nodes = propNodes.Value
,
			NodeStats = propNodeStats.Value
,
			Status = propStatus.Value
,
			Timestamp = propTimestamp.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, ClusterStatsResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropClusterName, value.ClusterName, null, null);
		writer.WriteProperty(options, PropClusterUuid, value.ClusterUuid, null, null);
		writer.WriteProperty(options, PropIndices, value.Indices, null, null);
		writer.WriteProperty(options, PropNodes, value.Nodes, null, null);
		writer.WriteProperty(options, PropNodeStats, value.NodeStats, null, null);
		writer.WriteProperty(options, PropStatus, value.Status, null, null);
		writer.WriteProperty(options, PropTimestamp, value.Timestamp, null, null);
		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(ClusterStatsResponseConverter))]
public sealed partial class ClusterStatsResponse : ElasticsearchResponse
{
	/// <summary>
	/// <para>
	/// Name of the cluster, based on the cluster name setting.
	/// </para>
	/// </summary>
	public string ClusterName { get; init; }

	/// <summary>
	/// <para>
	/// Unique identifier for the cluster.
	/// </para>
	/// </summary>
	public string ClusterUuid { get; init; }

	/// <summary>
	/// <para>
	/// Contains statistics about indices with shards assigned to selected nodes.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Cluster.ClusterIndices Indices { get; init; }

	/// <summary>
	/// <para>
	/// Contains statistics about nodes selected by the request’s node filters.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Cluster.ClusterNodes Nodes { get; init; }

	/// <summary>
	/// <para>
	/// Contains statistics about the number of nodes selected by the request’s node filters.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.NodeStatistics? NodeStats { get; init; }

	/// <summary>
	/// <para>
	/// Health status of the cluster, based on the state of its primary and replica shards.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.HealthStatus Status { get; init; }

	/// <summary>
	/// <para>
	/// Unix timestamp, in milliseconds, for the last time the cluster statistics were refreshed.
	/// </para>
	/// </summary>
	public long Timestamp { get; init; }
}