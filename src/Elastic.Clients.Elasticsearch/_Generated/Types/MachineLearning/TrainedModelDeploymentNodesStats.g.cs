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

namespace Elastic.Clients.Elasticsearch.MachineLearning;

internal sealed partial class TrainedModelDeploymentNodesStatsConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelDeploymentNodesStats>
{
	private static readonly System.Text.Json.JsonEncodedText PropAverageInferenceTimeMs = System.Text.Json.JsonEncodedText.Encode("average_inference_time_ms");
	private static readonly System.Text.Json.JsonEncodedText PropAverageInferenceTimeMsExcludingCacheHits = System.Text.Json.JsonEncodedText.Encode("average_inference_time_ms_excluding_cache_hits");
	private static readonly System.Text.Json.JsonEncodedText PropAverageInferenceTimeMsLastMinute = System.Text.Json.JsonEncodedText.Encode("average_inference_time_ms_last_minute");
	private static readonly System.Text.Json.JsonEncodedText PropErrorCount = System.Text.Json.JsonEncodedText.Encode("error_count");
	private static readonly System.Text.Json.JsonEncodedText PropInferenceCacheHitCount = System.Text.Json.JsonEncodedText.Encode("inference_cache_hit_count");
	private static readonly System.Text.Json.JsonEncodedText PropInferenceCacheHitCountLastMinute = System.Text.Json.JsonEncodedText.Encode("inference_cache_hit_count_last_minute");
	private static readonly System.Text.Json.JsonEncodedText PropInferenceCount = System.Text.Json.JsonEncodedText.Encode("inference_count");
	private static readonly System.Text.Json.JsonEncodedText PropLastAccess = System.Text.Json.JsonEncodedText.Encode("last_access");
	private static readonly System.Text.Json.JsonEncodedText PropNode = System.Text.Json.JsonEncodedText.Encode("node");
	private static readonly System.Text.Json.JsonEncodedText PropNumberOfAllocations = System.Text.Json.JsonEncodedText.Encode("number_of_allocations");
	private static readonly System.Text.Json.JsonEncodedText PropNumberOfPendingRequests = System.Text.Json.JsonEncodedText.Encode("number_of_pending_requests");
	private static readonly System.Text.Json.JsonEncodedText PropPeakThroughputPerMinute = System.Text.Json.JsonEncodedText.Encode("peak_throughput_per_minute");
	private static readonly System.Text.Json.JsonEncodedText PropRejectedExecutionCount = System.Text.Json.JsonEncodedText.Encode("rejected_execution_count");
	private static readonly System.Text.Json.JsonEncodedText PropRoutingState = System.Text.Json.JsonEncodedText.Encode("routing_state");
	private static readonly System.Text.Json.JsonEncodedText PropStartTime = System.Text.Json.JsonEncodedText.Encode("start_time");
	private static readonly System.Text.Json.JsonEncodedText PropThreadsPerAllocation = System.Text.Json.JsonEncodedText.Encode("threads_per_allocation");
	private static readonly System.Text.Json.JsonEncodedText PropThroughputLastMinute = System.Text.Json.JsonEncodedText.Encode("throughput_last_minute");
	private static readonly System.Text.Json.JsonEncodedText PropTimeoutCount = System.Text.Json.JsonEncodedText.Encode("timeout_count");

	public override Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelDeploymentNodesStats Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.TimeSpan?> propAverageInferenceTimeMs = default;
		LocalJsonValue<System.TimeSpan?> propAverageInferenceTimeMsExcludingCacheHits = default;
		LocalJsonValue<System.TimeSpan?> propAverageInferenceTimeMsLastMinute = default;
		LocalJsonValue<int?> propErrorCount = default;
		LocalJsonValue<long?> propInferenceCacheHitCount = default;
		LocalJsonValue<long?> propInferenceCacheHitCountLastMinute = default;
		LocalJsonValue<long?> propInferenceCount = default;
		LocalJsonValue<System.DateTime?> propLastAccess = default;
		LocalJsonValue<System.Collections.Generic.KeyValuePair<string, Elastic.Clients.Elasticsearch.MachineLearning.DiscoveryNodeContent>?> propNode = default;
		LocalJsonValue<int?> propNumberOfAllocations = default;
		LocalJsonValue<int?> propNumberOfPendingRequests = default;
		LocalJsonValue<long> propPeakThroughputPerMinute = default;
		LocalJsonValue<int?> propRejectedExecutionCount = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelAssignmentRoutingStateAndReason> propRoutingState = default;
		LocalJsonValue<System.DateTime?> propStartTime = default;
		LocalJsonValue<int?> propThreadsPerAllocation = default;
		LocalJsonValue<int> propThroughputLastMinute = default;
		LocalJsonValue<int?> propTimeoutCount = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAverageInferenceTimeMs.TryReadProperty(ref reader, options, PropAverageInferenceTimeMs, static System.TimeSpan? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.TimeSpan?>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanMillisMarker))))
			{
				continue;
			}

			if (propAverageInferenceTimeMsExcludingCacheHits.TryReadProperty(ref reader, options, PropAverageInferenceTimeMsExcludingCacheHits, static System.TimeSpan? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.TimeSpan?>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanMillisMarker))))
			{
				continue;
			}

			if (propAverageInferenceTimeMsLastMinute.TryReadProperty(ref reader, options, PropAverageInferenceTimeMsLastMinute, static System.TimeSpan? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.TimeSpan?>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanMillisMarker))))
			{
				continue;
			}

			if (propErrorCount.TryReadProperty(ref reader, options, PropErrorCount, null))
			{
				continue;
			}

			if (propInferenceCacheHitCount.TryReadProperty(ref reader, options, PropInferenceCacheHitCount, null))
			{
				continue;
			}

			if (propInferenceCacheHitCountLastMinute.TryReadProperty(ref reader, options, PropInferenceCacheHitCountLastMinute, null))
			{
				continue;
			}

			if (propInferenceCount.TryReadProperty(ref reader, options, PropInferenceCount, null))
			{
				continue;
			}

			if (propLastAccess.TryReadProperty(ref reader, options, PropLastAccess, static System.DateTime? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.DateTime?>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMillisMarker))))
			{
				continue;
			}

			if (propNode.TryReadProperty(ref reader, options, PropNode, static System.Collections.Generic.KeyValuePair<string, Elastic.Clients.Elasticsearch.MachineLearning.DiscoveryNodeContent>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadKeyValuePairValue<string, Elastic.Clients.Elasticsearch.MachineLearning.DiscoveryNodeContent>(o, null, null)))
			{
				continue;
			}

			if (propNumberOfAllocations.TryReadProperty(ref reader, options, PropNumberOfAllocations, null))
			{
				continue;
			}

			if (propNumberOfPendingRequests.TryReadProperty(ref reader, options, PropNumberOfPendingRequests, null))
			{
				continue;
			}

			if (propPeakThroughputPerMinute.TryReadProperty(ref reader, options, PropPeakThroughputPerMinute, null))
			{
				continue;
			}

			if (propRejectedExecutionCount.TryReadProperty(ref reader, options, PropRejectedExecutionCount, null))
			{
				continue;
			}

			if (propRoutingState.TryReadProperty(ref reader, options, PropRoutingState, null))
			{
				continue;
			}

			if (propStartTime.TryReadProperty(ref reader, options, PropStartTime, static System.DateTime? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.DateTime?>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMillisMarker))))
			{
				continue;
			}

			if (propThreadsPerAllocation.TryReadProperty(ref reader, options, PropThreadsPerAllocation, null))
			{
				continue;
			}

			if (propThroughputLastMinute.TryReadProperty(ref reader, options, PropThroughputLastMinute, null))
			{
				continue;
			}

			if (propTimeoutCount.TryReadProperty(ref reader, options, PropTimeoutCount, null))
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
		return new Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelDeploymentNodesStats(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			AverageInferenceTimeMs = propAverageInferenceTimeMs.Value,
			AverageInferenceTimeMsExcludingCacheHits = propAverageInferenceTimeMsExcludingCacheHits.Value,
			AverageInferenceTimeMsLastMinute = propAverageInferenceTimeMsLastMinute.Value,
			ErrorCount = propErrorCount.Value,
			InferenceCacheHitCount = propInferenceCacheHitCount.Value,
			InferenceCacheHitCountLastMinute = propInferenceCacheHitCountLastMinute.Value,
			InferenceCount = propInferenceCount.Value,
			LastAccess = propLastAccess.Value,
			Node = propNode.Value,
			NumberOfAllocations = propNumberOfAllocations.Value,
			NumberOfPendingRequests = propNumberOfPendingRequests.Value,
			PeakThroughputPerMinute = propPeakThroughputPerMinute.Value,
			RejectedExecutionCount = propRejectedExecutionCount.Value,
			RoutingState = propRoutingState.Value,
			StartTime = propStartTime.Value,
			ThreadsPerAllocation = propThreadsPerAllocation.Value,
			ThroughputLastMinute = propThroughputLastMinute.Value,
			TimeoutCount = propTimeoutCount.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelDeploymentNodesStats value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAverageInferenceTimeMs, value.AverageInferenceTimeMs, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.TimeSpan? v) => w.WriteValueEx<System.TimeSpan?>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanMillisMarker)));
		writer.WriteProperty(options, PropAverageInferenceTimeMsExcludingCacheHits, value.AverageInferenceTimeMsExcludingCacheHits, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.TimeSpan? v) => w.WriteValueEx<System.TimeSpan?>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanMillisMarker)));
		writer.WriteProperty(options, PropAverageInferenceTimeMsLastMinute, value.AverageInferenceTimeMsLastMinute, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.TimeSpan? v) => w.WriteValueEx<System.TimeSpan?>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanMillisMarker)));
		writer.WriteProperty(options, PropErrorCount, value.ErrorCount, null, null);
		writer.WriteProperty(options, PropInferenceCacheHitCount, value.InferenceCacheHitCount, null, null);
		writer.WriteProperty(options, PropInferenceCacheHitCountLastMinute, value.InferenceCacheHitCountLastMinute, null, null);
		writer.WriteProperty(options, PropInferenceCount, value.InferenceCount, null, null);
		writer.WriteProperty(options, PropLastAccess, value.LastAccess, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.DateTime? v) => w.WriteValueEx<System.DateTime?>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMillisMarker)));
		writer.WriteProperty(options, PropNode, value.Node, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.KeyValuePair<string, Elastic.Clients.Elasticsearch.MachineLearning.DiscoveryNodeContent>? v) => w.WriteKeyValuePairValue<string, Elastic.Clients.Elasticsearch.MachineLearning.DiscoveryNodeContent>(o, v, null, null));
		writer.WriteProperty(options, PropNumberOfAllocations, value.NumberOfAllocations, null, null);
		writer.WriteProperty(options, PropNumberOfPendingRequests, value.NumberOfPendingRequests, null, null);
		writer.WriteProperty(options, PropPeakThroughputPerMinute, value.PeakThroughputPerMinute, null, null);
		writer.WriteProperty(options, PropRejectedExecutionCount, value.RejectedExecutionCount, null, null);
		writer.WriteProperty(options, PropRoutingState, value.RoutingState, null, null);
		writer.WriteProperty(options, PropStartTime, value.StartTime, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.DateTime? v) => w.WriteValueEx<System.DateTime?>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMillisMarker)));
		writer.WriteProperty(options, PropThreadsPerAllocation, value.ThreadsPerAllocation, null, null);
		writer.WriteProperty(options, PropThroughputLastMinute, value.ThroughputLastMinute, null, null);
		writer.WriteProperty(options, PropTimeoutCount, value.TimeoutCount, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelDeploymentNodesStatsConverter))]
public sealed partial class TrainedModelDeploymentNodesStats
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public TrainedModelDeploymentNodesStats(long peakThroughputPerMinute, Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelAssignmentRoutingStateAndReason routingState, int throughputLastMinute)
	{
		PeakThroughputPerMinute = peakThroughputPerMinute;
		RoutingState = routingState;
		ThroughputLastMinute = throughputLastMinute;
	}
#if NET7_0_OR_GREATER
	public TrainedModelDeploymentNodesStats()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public TrainedModelDeploymentNodesStats()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal TrainedModelDeploymentNodesStats(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// The average time for each inference call to complete on this node.
	/// </para>
	/// </summary>
	public System.TimeSpan? AverageInferenceTimeMs { get; set; }

	/// <summary>
	/// <para>
	/// The average time for each inference call to complete on this node, excluding cache
	/// </para>
	/// </summary>
	public System.TimeSpan? AverageInferenceTimeMsExcludingCacheHits { get; set; }
	public System.TimeSpan? AverageInferenceTimeMsLastMinute { get; set; }

	/// <summary>
	/// <para>
	/// The number of errors when evaluating the trained model.
	/// </para>
	/// </summary>
	public int? ErrorCount { get; set; }
	public long? InferenceCacheHitCount { get; set; }
	public long? InferenceCacheHitCountLastMinute { get; set; }

	/// <summary>
	/// <para>
	/// The total number of inference calls made against this node for this model.
	/// </para>
	/// </summary>
	public long? InferenceCount { get; set; }

	/// <summary>
	/// <para>
	/// The epoch time stamp of the last inference call for the model on this node.
	/// </para>
	/// </summary>
	public System.DateTime? LastAccess { get; set; }

	/// <summary>
	/// <para>
	/// Information pertaining to the node.
	/// </para>
	/// </summary>
	public System.Collections.Generic.KeyValuePair<string, Elastic.Clients.Elasticsearch.MachineLearning.DiscoveryNodeContent>? Node { get; set; }

	/// <summary>
	/// <para>
	/// The number of allocations assigned to this node.
	/// </para>
	/// </summary>
	public int? NumberOfAllocations { get; set; }

	/// <summary>
	/// <para>
	/// The number of inference requests queued to be processed.
	/// </para>
	/// </summary>
	public int? NumberOfPendingRequests { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long PeakThroughputPerMinute { get; set; }

	/// <summary>
	/// <para>
	/// The number of inference requests that were not processed because the queue was full.
	/// </para>
	/// </summary>
	public int? RejectedExecutionCount { get; set; }

	/// <summary>
	/// <para>
	/// The current routing state and reason for the current routing state for this allocation.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelAssignmentRoutingStateAndReason RoutingState { get; set; }

	/// <summary>
	/// <para>
	/// The epoch timestamp when the allocation started.
	/// </para>
	/// </summary>
	public System.DateTime? StartTime { get; set; }

	/// <summary>
	/// <para>
	/// The number of threads used by each allocation during inference.
	/// </para>
	/// </summary>
	public int? ThreadsPerAllocation { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	int ThroughputLastMinute { get; set; }

	/// <summary>
	/// <para>
	/// The number of inference requests that timed out before being processed.
	/// </para>
	/// </summary>
	public int? TimeoutCount { get; set; }
}