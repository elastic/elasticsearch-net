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

namespace Elastic.Clients.Elasticsearch.Enrich;

internal sealed partial class CoordinatorStatsConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Enrich.CoordinatorStats>
{
	private static readonly System.Text.Json.JsonEncodedText PropExecutedSearchesTotal = System.Text.Json.JsonEncodedText.Encode("executed_searches_total");
	private static readonly System.Text.Json.JsonEncodedText PropNodeId = System.Text.Json.JsonEncodedText.Encode("node_id");
	private static readonly System.Text.Json.JsonEncodedText PropQueueSize = System.Text.Json.JsonEncodedText.Encode("queue_size");
	private static readonly System.Text.Json.JsonEncodedText PropRemoteRequestsCurrent = System.Text.Json.JsonEncodedText.Encode("remote_requests_current");
	private static readonly System.Text.Json.JsonEncodedText PropRemoteRequestsTotal = System.Text.Json.JsonEncodedText.Encode("remote_requests_total");

	public override Elastic.Clients.Elasticsearch.Enrich.CoordinatorStats Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<long> propExecutedSearchesTotal = default;
		LocalJsonValue<string> propNodeId = default;
		LocalJsonValue<int> propQueueSize = default;
		LocalJsonValue<int> propRemoteRequestsCurrent = default;
		LocalJsonValue<long> propRemoteRequestsTotal = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propExecutedSearchesTotal.TryReadProperty(ref reader, options, PropExecutedSearchesTotal, null))
			{
				continue;
			}

			if (propNodeId.TryReadProperty(ref reader, options, PropNodeId, null))
			{
				continue;
			}

			if (propQueueSize.TryReadProperty(ref reader, options, PropQueueSize, null))
			{
				continue;
			}

			if (propRemoteRequestsCurrent.TryReadProperty(ref reader, options, PropRemoteRequestsCurrent, null))
			{
				continue;
			}

			if (propRemoteRequestsTotal.TryReadProperty(ref reader, options, PropRemoteRequestsTotal, null))
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
		return new Elastic.Clients.Elasticsearch.Enrich.CoordinatorStats(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			ExecutedSearchesTotal = propExecutedSearchesTotal.Value,
			NodeId = propNodeId.Value,
			QueueSize = propQueueSize.Value,
			RemoteRequestsCurrent = propRemoteRequestsCurrent.Value,
			RemoteRequestsTotal = propRemoteRequestsTotal.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Enrich.CoordinatorStats value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropExecutedSearchesTotal, value.ExecutedSearchesTotal, null, null);
		writer.WriteProperty(options, PropNodeId, value.NodeId, null, null);
		writer.WriteProperty(options, PropQueueSize, value.QueueSize, null, null);
		writer.WriteProperty(options, PropRemoteRequestsCurrent, value.RemoteRequestsCurrent, null, null);
		writer.WriteProperty(options, PropRemoteRequestsTotal, value.RemoteRequestsTotal, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Enrich.CoordinatorStatsConverter))]
public sealed partial class CoordinatorStats
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public CoordinatorStats(long executedSearchesTotal, string nodeId, int queueSize, int remoteRequestsCurrent, long remoteRequestsTotal)
	{
		ExecutedSearchesTotal = executedSearchesTotal;
		NodeId = nodeId;
		QueueSize = queueSize;
		RemoteRequestsCurrent = remoteRequestsCurrent;
		RemoteRequestsTotal = remoteRequestsTotal;
	}
#if NET7_0_OR_GREATER
	public CoordinatorStats()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public CoordinatorStats()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal CoordinatorStats(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	long ExecutedSearchesTotal { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	string NodeId { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	int QueueSize { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	int RemoteRequestsCurrent { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long RemoteRequestsTotal { get; set; }
}