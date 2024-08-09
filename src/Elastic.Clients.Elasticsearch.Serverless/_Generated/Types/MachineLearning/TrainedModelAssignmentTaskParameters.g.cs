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

using Elastic.Clients.Elasticsearch.Serverless.Fluent;
using Elastic.Clients.Elasticsearch.Serverless.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serverless.MachineLearning;

public sealed partial class TrainedModelAssignmentTaskParameters
{
	/// <summary>
	/// <para>
	/// The size of the trained model cache.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("cache_size")]
	public Elastic.Clients.Elasticsearch.Serverless.ByteSize CacheSize { get; init; }

	/// <summary>
	/// <para>
	/// The unique identifier for the trained model deployment.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("deployment_id")]
	public string DeploymentId { get; init; }

	/// <summary>
	/// <para>
	/// The size of the trained model in bytes.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("model_bytes")]
	public int ModelBytes { get; init; }

	/// <summary>
	/// <para>
	/// The unique identifier for the trained model.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("model_id")]
	public string ModelId { get; init; }

	/// <summary>
	/// <para>
	/// The total number of allocations this model is assigned across ML nodes.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("number_of_allocations")]
	public int NumberOfAllocations { get; init; }
	[JsonInclude, JsonPropertyName("priority")]
	public Elastic.Clients.Elasticsearch.Serverless.MachineLearning.TrainingPriority Priority { get; init; }

	/// <summary>
	/// <para>
	/// Number of inference requests are allowed in the queue at a time.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("queue_capacity")]
	public int QueueCapacity { get; init; }

	/// <summary>
	/// <para>
	/// Number of threads per allocation.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("threads_per_allocation")]
	public int ThreadsPerAllocation { get; init; }
}