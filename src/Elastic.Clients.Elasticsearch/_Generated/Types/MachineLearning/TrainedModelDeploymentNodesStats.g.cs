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
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.MachineLearning;

public sealed partial class TrainedModelDeploymentNodesStats
{
	/// <summary>
	/// <para>The average time for each inference call to complete on this node.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("average_inference_time_ms")]
	public double AverageInferenceTimeMs { get; init; }

	/// <summary>
	/// <para>The number of errors when evaluating the trained model.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("error_count")]
	public int ErrorCount { get; init; }

	/// <summary>
	/// <para>The total number of inference calls made against this node for this model.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("inference_count")]
	public int InferenceCount { get; init; }

	/// <summary>
	/// <para>The epoch time stamp of the last inference call for the model on this node.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("last_access")]
	public long LastAccess { get; init; }

	/// <summary>
	/// <para>Information pertaining to the node.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("node")]
	public Elastic.Clients.Elasticsearch.MachineLearning.DiscoveryNode Node { get; init; }

	/// <summary>
	/// <para>The number of allocations assigned to this node.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("number_of_allocations")]
	public int NumberOfAllocations { get; init; }

	/// <summary>
	/// <para>The number of inference requests queued to be processed.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("number_of_pending_requests")]
	public int NumberOfPendingRequests { get; init; }

	/// <summary>
	/// <para>The number of inference requests that were not processed because the queue was full.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("rejection_execution_count")]
	public int RejectionExecutionCount { get; init; }

	/// <summary>
	/// <para>The current routing state and reason for the current routing state for this allocation.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("routing_state")]
	public Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelAssignmentRoutingTable RoutingState { get; init; }

	/// <summary>
	/// <para>The epoch timestamp when the allocation started.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("start_time")]
	public long StartTime { get; init; }

	/// <summary>
	/// <para>The number of threads used by each allocation during inference.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("threads_per_allocation")]
	public int ThreadsPerAllocation { get; init; }

	/// <summary>
	/// <para>The number of inference requests that timed out before being processed.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("timeout_count")]
	public int TimeoutCount { get; init; }
}