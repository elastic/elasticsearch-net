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

internal sealed partial class AllocationExplainResponseConverter : System.Text.Json.Serialization.JsonConverter<AllocationExplainResponse>
{
	private static readonly System.Text.Json.JsonEncodedText PropAllocateExplanation = System.Text.Json.JsonEncodedText.Encode("allocate_explanation");
	private static readonly System.Text.Json.JsonEncodedText PropAllocationDelay = System.Text.Json.JsonEncodedText.Encode("allocation_delay");
	private static readonly System.Text.Json.JsonEncodedText PropAllocationDelayInMillis = System.Text.Json.JsonEncodedText.Encode("allocation_delay_in_millis");
	private static readonly System.Text.Json.JsonEncodedText PropCanAllocate = System.Text.Json.JsonEncodedText.Encode("can_allocate");
	private static readonly System.Text.Json.JsonEncodedText PropCanMoveToOtherNode = System.Text.Json.JsonEncodedText.Encode("can_move_to_other_node");
	private static readonly System.Text.Json.JsonEncodedText PropCanRebalanceCluster = System.Text.Json.JsonEncodedText.Encode("can_rebalance_cluster");
	private static readonly System.Text.Json.JsonEncodedText PropCanRebalanceClusterDecisions = System.Text.Json.JsonEncodedText.Encode("can_rebalance_cluster_decisions");
	private static readonly System.Text.Json.JsonEncodedText PropCanRebalanceToOtherNode = System.Text.Json.JsonEncodedText.Encode("can_rebalance_to_other_node");
	private static readonly System.Text.Json.JsonEncodedText PropCanRemainDecisions = System.Text.Json.JsonEncodedText.Encode("can_remain_decisions");
	private static readonly System.Text.Json.JsonEncodedText PropCanRemainOnCurrentNode = System.Text.Json.JsonEncodedText.Encode("can_remain_on_current_node");
	private static readonly System.Text.Json.JsonEncodedText PropClusterInfo = System.Text.Json.JsonEncodedText.Encode("cluster_info");
	private static readonly System.Text.Json.JsonEncodedText PropConfiguredDelay = System.Text.Json.JsonEncodedText.Encode("configured_delay");
	private static readonly System.Text.Json.JsonEncodedText PropConfiguredDelayInMillis = System.Text.Json.JsonEncodedText.Encode("configured_delay_in_millis");
	private static readonly System.Text.Json.JsonEncodedText PropCurrentNode = System.Text.Json.JsonEncodedText.Encode("current_node");
	private static readonly System.Text.Json.JsonEncodedText PropCurrentState = System.Text.Json.JsonEncodedText.Encode("current_state");
	private static readonly System.Text.Json.JsonEncodedText PropIndex = System.Text.Json.JsonEncodedText.Encode("index");
	private static readonly System.Text.Json.JsonEncodedText PropMoveExplanation = System.Text.Json.JsonEncodedText.Encode("move_explanation");
	private static readonly System.Text.Json.JsonEncodedText PropNodeAllocationDecisions = System.Text.Json.JsonEncodedText.Encode("node_allocation_decisions");
	private static readonly System.Text.Json.JsonEncodedText PropNote = System.Text.Json.JsonEncodedText.Encode("note");
	private static readonly System.Text.Json.JsonEncodedText PropPrimary = System.Text.Json.JsonEncodedText.Encode("primary");
	private static readonly System.Text.Json.JsonEncodedText PropRebalanceExplanation = System.Text.Json.JsonEncodedText.Encode("rebalance_explanation");
	private static readonly System.Text.Json.JsonEncodedText PropRemainingDelay = System.Text.Json.JsonEncodedText.Encode("remaining_delay");
	private static readonly System.Text.Json.JsonEncodedText PropRemainingDelayInMillis = System.Text.Json.JsonEncodedText.Encode("remaining_delay_in_millis");
	private static readonly System.Text.Json.JsonEncodedText PropShard = System.Text.Json.JsonEncodedText.Encode("shard");
	private static readonly System.Text.Json.JsonEncodedText PropUnassignedInfo = System.Text.Json.JsonEncodedText.Encode("unassigned_info");

	public override AllocationExplainResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string?> propAllocateExplanation = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Duration?> propAllocationDelay = default;
		LocalJsonValue<long?> propAllocationDelayInMillis = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Cluster.Decision?> propCanAllocate = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Cluster.Decision?> propCanMoveToOtherNode = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Cluster.Decision?> propCanRebalanceCluster = default;
		LocalJsonValue<IReadOnlyCollection<Elastic.Clients.Elasticsearch.Cluster.AllocationDecision>?> propCanRebalanceClusterDecisions = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Cluster.Decision?> propCanRebalanceToOtherNode = default;
		LocalJsonValue<IReadOnlyCollection<Elastic.Clients.Elasticsearch.Cluster.AllocationDecision>?> propCanRemainDecisions = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Cluster.Decision?> propCanRemainOnCurrentNode = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Cluster.ClusterInfo?> propClusterInfo = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Duration?> propConfiguredDelay = default;
		LocalJsonValue<long?> propConfiguredDelayInMillis = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Cluster.CurrentNode?> propCurrentNode = default;
		LocalJsonValue<string> propCurrentState = default;
		LocalJsonValue<string> propIndex = default;
		LocalJsonValue<string?> propMoveExplanation = default;
		LocalJsonValue<IReadOnlyCollection<Elastic.Clients.Elasticsearch.Cluster.NodeAllocationExplanation>?> propNodeAllocationDecisions = default;
		LocalJsonValue<string?> propNote = default;
		LocalJsonValue<bool> propPrimary = default;
		LocalJsonValue<string?> propRebalanceExplanation = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Duration?> propRemainingDelay = default;
		LocalJsonValue<long?> propRemainingDelayInMillis = default;
		LocalJsonValue<int> propShard = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Cluster.UnassignedInformation?> propUnassignedInfo = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAllocateExplanation.TryRead(ref reader, options, PropAllocateExplanation))
			{
				continue;
			}

			if (propAllocationDelay.TryRead(ref reader, options, PropAllocationDelay))
			{
				continue;
			}

			if (propAllocationDelayInMillis.TryRead(ref reader, options, PropAllocationDelayInMillis))
			{
				continue;
			}

			if (propCanAllocate.TryRead(ref reader, options, PropCanAllocate))
			{
				continue;
			}

			if (propCanMoveToOtherNode.TryRead(ref reader, options, PropCanMoveToOtherNode))
			{
				continue;
			}

			if (propCanRebalanceCluster.TryRead(ref reader, options, PropCanRebalanceCluster))
			{
				continue;
			}

			if (propCanRebalanceClusterDecisions.TryRead(ref reader, options, PropCanRebalanceClusterDecisions))
			{
				continue;
			}

			if (propCanRebalanceToOtherNode.TryRead(ref reader, options, PropCanRebalanceToOtherNode))
			{
				continue;
			}

			if (propCanRemainDecisions.TryRead(ref reader, options, PropCanRemainDecisions))
			{
				continue;
			}

			if (propCanRemainOnCurrentNode.TryRead(ref reader, options, PropCanRemainOnCurrentNode))
			{
				continue;
			}

			if (propClusterInfo.TryRead(ref reader, options, PropClusterInfo))
			{
				continue;
			}

			if (propConfiguredDelay.TryRead(ref reader, options, PropConfiguredDelay))
			{
				continue;
			}

			if (propConfiguredDelayInMillis.TryRead(ref reader, options, PropConfiguredDelayInMillis))
			{
				continue;
			}

			if (propCurrentNode.TryRead(ref reader, options, PropCurrentNode))
			{
				continue;
			}

			if (propCurrentState.TryRead(ref reader, options, PropCurrentState))
			{
				continue;
			}

			if (propIndex.TryRead(ref reader, options, PropIndex))
			{
				continue;
			}

			if (propMoveExplanation.TryRead(ref reader, options, PropMoveExplanation))
			{
				continue;
			}

			if (propNodeAllocationDecisions.TryRead(ref reader, options, PropNodeAllocationDecisions))
			{
				continue;
			}

			if (propNote.TryRead(ref reader, options, PropNote))
			{
				continue;
			}

			if (propPrimary.TryRead(ref reader, options, PropPrimary))
			{
				continue;
			}

			if (propRebalanceExplanation.TryRead(ref reader, options, PropRebalanceExplanation))
			{
				continue;
			}

			if (propRemainingDelay.TryRead(ref reader, options, PropRemainingDelay))
			{
				continue;
			}

			if (propRemainingDelayInMillis.TryRead(ref reader, options, PropRemainingDelayInMillis))
			{
				continue;
			}

			if (propShard.TryRead(ref reader, options, PropShard))
			{
				continue;
			}

			if (propUnassignedInfo.TryRead(ref reader, options, PropUnassignedInfo))
			{
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new AllocationExplainResponse
		{
			AllocateExplanation = propAllocateExplanation.Value
,
			AllocationDelay = propAllocationDelay.Value
,
			AllocationDelayInMillis = propAllocationDelayInMillis.Value
,
			CanAllocate = propCanAllocate.Value
,
			CanMoveToOtherNode = propCanMoveToOtherNode.Value
,
			CanRebalanceCluster = propCanRebalanceCluster.Value
,
			CanRebalanceClusterDecisions = propCanRebalanceClusterDecisions.Value
,
			CanRebalanceToOtherNode = propCanRebalanceToOtherNode.Value
,
			CanRemainDecisions = propCanRemainDecisions.Value
,
			CanRemainOnCurrentNode = propCanRemainOnCurrentNode.Value
,
			ClusterInfo = propClusterInfo.Value
,
			ConfiguredDelay = propConfiguredDelay.Value
,
			ConfiguredDelayInMillis = propConfiguredDelayInMillis.Value
,
			CurrentNode = propCurrentNode.Value
,
			CurrentState = propCurrentState.Value
,
			Index = propIndex.Value
,
			MoveExplanation = propMoveExplanation.Value
,
			NodeAllocationDecisions = propNodeAllocationDecisions.Value
,
			Note = propNote.Value
,
			Primary = propPrimary.Value
,
			RebalanceExplanation = propRebalanceExplanation.Value
,
			RemainingDelay = propRemainingDelay.Value
,
			RemainingDelayInMillis = propRemainingDelayInMillis.Value
,
			Shard = propShard.Value
,
			UnassignedInfo = propUnassignedInfo.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, AllocationExplainResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAllocateExplanation, value.AllocateExplanation);
		writer.WriteProperty(options, PropAllocationDelay, value.AllocationDelay);
		writer.WriteProperty(options, PropAllocationDelayInMillis, value.AllocationDelayInMillis);
		writer.WriteProperty(options, PropCanAllocate, value.CanAllocate);
		writer.WriteProperty(options, PropCanMoveToOtherNode, value.CanMoveToOtherNode);
		writer.WriteProperty(options, PropCanRebalanceCluster, value.CanRebalanceCluster);
		writer.WriteProperty(options, PropCanRebalanceClusterDecisions, value.CanRebalanceClusterDecisions);
		writer.WriteProperty(options, PropCanRebalanceToOtherNode, value.CanRebalanceToOtherNode);
		writer.WriteProperty(options, PropCanRemainDecisions, value.CanRemainDecisions);
		writer.WriteProperty(options, PropCanRemainOnCurrentNode, value.CanRemainOnCurrentNode);
		writer.WriteProperty(options, PropClusterInfo, value.ClusterInfo);
		writer.WriteProperty(options, PropConfiguredDelay, value.ConfiguredDelay);
		writer.WriteProperty(options, PropConfiguredDelayInMillis, value.ConfiguredDelayInMillis);
		writer.WriteProperty(options, PropCurrentNode, value.CurrentNode);
		writer.WriteProperty(options, PropCurrentState, value.CurrentState);
		writer.WriteProperty(options, PropIndex, value.Index);
		writer.WriteProperty(options, PropMoveExplanation, value.MoveExplanation);
		writer.WriteProperty(options, PropNodeAllocationDecisions, value.NodeAllocationDecisions);
		writer.WriteProperty(options, PropNote, value.Note);
		writer.WriteProperty(options, PropPrimary, value.Primary);
		writer.WriteProperty(options, PropRebalanceExplanation, value.RebalanceExplanation);
		writer.WriteProperty(options, PropRemainingDelay, value.RemainingDelay);
		writer.WriteProperty(options, PropRemainingDelayInMillis, value.RemainingDelayInMillis);
		writer.WriteProperty(options, PropShard, value.Shard);
		writer.WriteProperty(options, PropUnassignedInfo, value.UnassignedInfo);
		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(AllocationExplainResponseConverter))]
public sealed partial class AllocationExplainResponse : ElasticsearchResponse
{
	public string? AllocateExplanation { get; init; }
	public Elastic.Clients.Elasticsearch.Duration? AllocationDelay { get; init; }
	public long? AllocationDelayInMillis { get; init; }
	public Elastic.Clients.Elasticsearch.Cluster.Decision? CanAllocate { get; init; }
	public Elastic.Clients.Elasticsearch.Cluster.Decision? CanMoveToOtherNode { get; init; }
	public Elastic.Clients.Elasticsearch.Cluster.Decision? CanRebalanceCluster { get; init; }
	public IReadOnlyCollection<Elastic.Clients.Elasticsearch.Cluster.AllocationDecision>? CanRebalanceClusterDecisions { get; init; }
	public Elastic.Clients.Elasticsearch.Cluster.Decision? CanRebalanceToOtherNode { get; init; }
	public IReadOnlyCollection<Elastic.Clients.Elasticsearch.Cluster.AllocationDecision>? CanRemainDecisions { get; init; }
	public Elastic.Clients.Elasticsearch.Cluster.Decision? CanRemainOnCurrentNode { get; init; }
	public Elastic.Clients.Elasticsearch.Cluster.ClusterInfo? ClusterInfo { get; init; }
	public Elastic.Clients.Elasticsearch.Duration? ConfiguredDelay { get; init; }
	public long? ConfiguredDelayInMillis { get; init; }
	public Elastic.Clients.Elasticsearch.Cluster.CurrentNode? CurrentNode { get; init; }
	public string CurrentState { get; init; }
	public string Index { get; init; }
	public string? MoveExplanation { get; init; }
	public IReadOnlyCollection<Elastic.Clients.Elasticsearch.Cluster.NodeAllocationExplanation>? NodeAllocationDecisions { get; init; }
	public string? Note { get; init; }
	public bool Primary { get; init; }
	public string? RebalanceExplanation { get; init; }
	public Elastic.Clients.Elasticsearch.Duration? RemainingDelay { get; init; }
	public long? RemainingDelayInMillis { get; init; }
	public int Shard { get; init; }
	public Elastic.Clients.Elasticsearch.Cluster.UnassignedInformation? UnassignedInfo { get; init; }
}