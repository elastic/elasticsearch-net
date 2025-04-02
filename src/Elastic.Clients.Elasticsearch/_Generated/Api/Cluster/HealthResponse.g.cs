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

namespace Elastic.Clients.Elasticsearch.Cluster;

internal sealed partial class HealthResponseConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Cluster.HealthResponse>
{
	private static readonly System.Text.Json.JsonEncodedText PropActivePrimaryShards = System.Text.Json.JsonEncodedText.Encode("active_primary_shards");
	private static readonly System.Text.Json.JsonEncodedText PropActiveShards = System.Text.Json.JsonEncodedText.Encode("active_shards");
	private static readonly System.Text.Json.JsonEncodedText PropActiveShardsPercentAsNumber = System.Text.Json.JsonEncodedText.Encode("active_shards_percent_as_number");
	private static readonly System.Text.Json.JsonEncodedText PropClusterName = System.Text.Json.JsonEncodedText.Encode("cluster_name");
	private static readonly System.Text.Json.JsonEncodedText PropDelayedUnassignedShards = System.Text.Json.JsonEncodedText.Encode("delayed_unassigned_shards");
	private static readonly System.Text.Json.JsonEncodedText PropIndices = System.Text.Json.JsonEncodedText.Encode("indices");
	private static readonly System.Text.Json.JsonEncodedText PropInitializingShards = System.Text.Json.JsonEncodedText.Encode("initializing_shards");
	private static readonly System.Text.Json.JsonEncodedText PropNumberOfDataNodes = System.Text.Json.JsonEncodedText.Encode("number_of_data_nodes");
	private static readonly System.Text.Json.JsonEncodedText PropNumberOfInFlightFetch = System.Text.Json.JsonEncodedText.Encode("number_of_in_flight_fetch");
	private static readonly System.Text.Json.JsonEncodedText PropNumberOfNodes = System.Text.Json.JsonEncodedText.Encode("number_of_nodes");
	private static readonly System.Text.Json.JsonEncodedText PropNumberOfPendingTasks = System.Text.Json.JsonEncodedText.Encode("number_of_pending_tasks");
	private static readonly System.Text.Json.JsonEncodedText PropRelocatingShards = System.Text.Json.JsonEncodedText.Encode("relocating_shards");
	private static readonly System.Text.Json.JsonEncodedText PropStatus = System.Text.Json.JsonEncodedText.Encode("status");
	private static readonly System.Text.Json.JsonEncodedText PropTaskMaxWaitingInQueue = System.Text.Json.JsonEncodedText.Encode("task_max_waiting_in_queue");
	private static readonly System.Text.Json.JsonEncodedText PropTaskMaxWaitingInQueueMillis = System.Text.Json.JsonEncodedText.Encode("task_max_waiting_in_queue_millis");
	private static readonly System.Text.Json.JsonEncodedText PropTimedOut = System.Text.Json.JsonEncodedText.Encode("timed_out");
	private static readonly System.Text.Json.JsonEncodedText PropUnassignedPrimaryShards = System.Text.Json.JsonEncodedText.Encode("unassigned_primary_shards");
	private static readonly System.Text.Json.JsonEncodedText PropUnassignedShards = System.Text.Json.JsonEncodedText.Encode("unassigned_shards");

	public override Elastic.Clients.Elasticsearch.Cluster.HealthResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<int> propActivePrimaryShards = default;
		LocalJsonValue<int> propActiveShards = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Percentage> propActiveShardsPercentAsNumber = default;
		LocalJsonValue<string> propClusterName = default;
		LocalJsonValue<int> propDelayedUnassignedShards = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Cluster.IndexHealthStats>?> propIndices = default;
		LocalJsonValue<int> propInitializingShards = default;
		LocalJsonValue<int> propNumberOfDataNodes = default;
		LocalJsonValue<int> propNumberOfInFlightFetch = default;
		LocalJsonValue<int> propNumberOfNodes = default;
		LocalJsonValue<int> propNumberOfPendingTasks = default;
		LocalJsonValue<int> propRelocatingShards = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.HealthStatus> propStatus = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Duration?> propTaskMaxWaitingInQueue = default;
		LocalJsonValue<System.TimeSpan> propTaskMaxWaitingInQueueMillis = default;
		LocalJsonValue<bool> propTimedOut = default;
		LocalJsonValue<int> propUnassignedPrimaryShards = default;
		LocalJsonValue<int> propUnassignedShards = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propActivePrimaryShards.TryReadProperty(ref reader, options, PropActivePrimaryShards, null))
			{
				continue;
			}

			if (propActiveShards.TryReadProperty(ref reader, options, PropActiveShards, null))
			{
				continue;
			}

			if (propActiveShardsPercentAsNumber.TryReadProperty(ref reader, options, PropActiveShardsPercentAsNumber, null))
			{
				continue;
			}

			if (propClusterName.TryReadProperty(ref reader, options, PropClusterName, null))
			{
				continue;
			}

			if (propDelayedUnassignedShards.TryReadProperty(ref reader, options, PropDelayedUnassignedShards, null))
			{
				continue;
			}

			if (propIndices.TryReadProperty(ref reader, options, PropIndices, static System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Cluster.IndexHealthStats>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, Elastic.Clients.Elasticsearch.Cluster.IndexHealthStats>(o, null, null)))
			{
				continue;
			}

			if (propInitializingShards.TryReadProperty(ref reader, options, PropInitializingShards, null))
			{
				continue;
			}

			if (propNumberOfDataNodes.TryReadProperty(ref reader, options, PropNumberOfDataNodes, null))
			{
				continue;
			}

			if (propNumberOfInFlightFetch.TryReadProperty(ref reader, options, PropNumberOfInFlightFetch, null))
			{
				continue;
			}

			if (propNumberOfNodes.TryReadProperty(ref reader, options, PropNumberOfNodes, null))
			{
				continue;
			}

			if (propNumberOfPendingTasks.TryReadProperty(ref reader, options, PropNumberOfPendingTasks, null))
			{
				continue;
			}

			if (propRelocatingShards.TryReadProperty(ref reader, options, PropRelocatingShards, null))
			{
				continue;
			}

			if (propStatus.TryReadProperty(ref reader, options, PropStatus, null))
			{
				continue;
			}

			if (propTaskMaxWaitingInQueue.TryReadProperty(ref reader, options, PropTaskMaxWaitingInQueue, null))
			{
				continue;
			}

			if (propTaskMaxWaitingInQueueMillis.TryReadProperty(ref reader, options, PropTaskMaxWaitingInQueueMillis, static System.TimeSpan (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.TimeSpan>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanMillisMarker))))
			{
				continue;
			}

			if (propTimedOut.TryReadProperty(ref reader, options, PropTimedOut, null))
			{
				continue;
			}

			if (propUnassignedPrimaryShards.TryReadProperty(ref reader, options, PropUnassignedPrimaryShards, null))
			{
				continue;
			}

			if (propUnassignedShards.TryReadProperty(ref reader, options, PropUnassignedShards, null))
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
		return new Elastic.Clients.Elasticsearch.Cluster.HealthResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			ActivePrimaryShards = propActivePrimaryShards.Value,
			ActiveShards = propActiveShards.Value,
			ActiveShardsPercentAsNumber = propActiveShardsPercentAsNumber.Value,
			ClusterName = propClusterName.Value,
			DelayedUnassignedShards = propDelayedUnassignedShards.Value,
			Indices = propIndices.Value,
			InitializingShards = propInitializingShards.Value,
			NumberOfDataNodes = propNumberOfDataNodes.Value,
			NumberOfInFlightFetch = propNumberOfInFlightFetch.Value,
			NumberOfNodes = propNumberOfNodes.Value,
			NumberOfPendingTasks = propNumberOfPendingTasks.Value,
			RelocatingShards = propRelocatingShards.Value,
			Status = propStatus.Value,
			TaskMaxWaitingInQueue = propTaskMaxWaitingInQueue.Value,
			TaskMaxWaitingInQueueMillis = propTaskMaxWaitingInQueueMillis.Value,
			TimedOut = propTimedOut.Value,
			UnassignedPrimaryShards = propUnassignedPrimaryShards.Value,
			UnassignedShards = propUnassignedShards.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Cluster.HealthResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropActivePrimaryShards, value.ActivePrimaryShards, null, null);
		writer.WriteProperty(options, PropActiveShards, value.ActiveShards, null, null);
		writer.WriteProperty(options, PropActiveShardsPercentAsNumber, value.ActiveShardsPercentAsNumber, null, null);
		writer.WriteProperty(options, PropClusterName, value.ClusterName, null, null);
		writer.WriteProperty(options, PropDelayedUnassignedShards, value.DelayedUnassignedShards, null, null);
		writer.WriteProperty(options, PropIndices, value.Indices, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Cluster.IndexHealthStats>? v) => w.WriteDictionaryValue<string, Elastic.Clients.Elasticsearch.Cluster.IndexHealthStats>(o, v, null, null));
		writer.WriteProperty(options, PropInitializingShards, value.InitializingShards, null, null);
		writer.WriteProperty(options, PropNumberOfDataNodes, value.NumberOfDataNodes, null, null);
		writer.WriteProperty(options, PropNumberOfInFlightFetch, value.NumberOfInFlightFetch, null, null);
		writer.WriteProperty(options, PropNumberOfNodes, value.NumberOfNodes, null, null);
		writer.WriteProperty(options, PropNumberOfPendingTasks, value.NumberOfPendingTasks, null, null);
		writer.WriteProperty(options, PropRelocatingShards, value.RelocatingShards, null, null);
		writer.WriteProperty(options, PropStatus, value.Status, null, null);
		writer.WriteProperty(options, PropTaskMaxWaitingInQueue, value.TaskMaxWaitingInQueue, null, null);
		writer.WriteProperty(options, PropTaskMaxWaitingInQueueMillis, value.TaskMaxWaitingInQueueMillis, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.TimeSpan v) => w.WriteValueEx<System.TimeSpan>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanMillisMarker)));
		writer.WriteProperty(options, PropTimedOut, value.TimedOut, null, null);
		writer.WriteProperty(options, PropUnassignedPrimaryShards, value.UnassignedPrimaryShards, null, null);
		writer.WriteProperty(options, PropUnassignedShards, value.UnassignedShards, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Cluster.HealthResponseConverter))]
public sealed partial class HealthResponse : Elastic.Transport.Products.Elasticsearch.ElasticsearchResponse
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public HealthResponse(int activePrimaryShards, int activeShards, Elastic.Clients.Elasticsearch.Percentage activeShardsPercentAsNumber, string clusterName, int delayedUnassignedShards, int initializingShards, int numberOfDataNodes, int numberOfInFlightFetch, int numberOfNodes, int numberOfPendingTasks, int relocatingShards, Elastic.Clients.Elasticsearch.HealthStatus status, System.TimeSpan taskMaxWaitingInQueueMillis, bool timedOut, int unassignedPrimaryShards, int unassignedShards)
	{
		ActivePrimaryShards = activePrimaryShards;
		ActiveShards = activeShards;
		ActiveShardsPercentAsNumber = activeShardsPercentAsNumber;
		ClusterName = clusterName;
		DelayedUnassignedShards = delayedUnassignedShards;
		InitializingShards = initializingShards;
		NumberOfDataNodes = numberOfDataNodes;
		NumberOfInFlightFetch = numberOfInFlightFetch;
		NumberOfNodes = numberOfNodes;
		NumberOfPendingTasks = numberOfPendingTasks;
		RelocatingShards = relocatingShards;
		Status = status;
		TaskMaxWaitingInQueueMillis = taskMaxWaitingInQueueMillis;
		TimedOut = timedOut;
		UnassignedPrimaryShards = unassignedPrimaryShards;
		UnassignedShards = unassignedShards;
	}

	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public HealthResponse()
	{
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal HealthResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// The number of active primary shards.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	int ActivePrimaryShards { get; set; }

	/// <summary>
	/// <para>
	/// The total number of active primary and replica shards.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	int ActiveShards { get; set; }

	/// <summary>
	/// <para>
	/// The ratio of active shards in the cluster expressed as a percentage.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Percentage ActiveShardsPercentAsNumber { get; set; }

	/// <summary>
	/// <para>
	/// The name of the cluster.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string ClusterName { get; set; }

	/// <summary>
	/// <para>
	/// The number of shards whose allocation has been delayed by the timeout settings.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	int DelayedUnassignedShards { get; set; }
	public System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Cluster.IndexHealthStats>? Indices { get; set; }

	/// <summary>
	/// <para>
	/// The number of shards that are under initialization.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	int InitializingShards { get; set; }

	/// <summary>
	/// <para>
	/// The number of nodes that are dedicated data nodes.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	int NumberOfDataNodes { get; set; }

	/// <summary>
	/// <para>
	/// The number of unfinished fetches.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	int NumberOfInFlightFetch { get; set; }

	/// <summary>
	/// <para>
	/// The number of nodes within the cluster.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	int NumberOfNodes { get; set; }

	/// <summary>
	/// <para>
	/// The number of cluster-level changes that have not yet been executed.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	int NumberOfPendingTasks { get; set; }

	/// <summary>
	/// <para>
	/// The number of shards that are under relocation.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	int RelocatingShards { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.HealthStatus Status { get; set; }

	/// <summary>
	/// <para>
	/// The time since the earliest initiated task is waiting for being performed.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? TaskMaxWaitingInQueue { get; set; }

	/// <summary>
	/// <para>
	/// The time expressed in milliseconds since the earliest initiated task is waiting for being performed.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.TimeSpan TaskMaxWaitingInQueueMillis { get; set; }

	/// <summary>
	/// <para>
	/// If false the response returned within the period of time that is specified by the timeout parameter (30s by default)
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	bool TimedOut { get; set; }

	/// <summary>
	/// <para>
	/// The number of primary shards that are not allocated.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	int UnassignedPrimaryShards { get; set; }

	/// <summary>
	/// <para>
	/// The number of shards that are not allocated.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	int UnassignedShards { get; set; }
}