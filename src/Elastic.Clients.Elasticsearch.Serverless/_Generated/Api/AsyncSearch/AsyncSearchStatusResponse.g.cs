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
using Elastic.Transport.Products.Elasticsearch;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serverless.AsyncSearch;

public sealed partial class AsyncSearchStatusResponse : ElasticsearchResponse
{
	/// <summary>
	/// <para>Metadata about clusters involved in the cross-cluster search.<br/>Not shown for local-only searches.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("_clusters")]
	public Elastic.Clients.Elasticsearch.Serverless.ClusterStatistics? Clusters { get; init; }

	/// <summary>
	/// <para>If the async search completed, this field shows the status code of the search.<br/>For example, 200 indicates that the async search was successfully completed.<br/>503 indicates that the async search was completed with an error.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("completion_status")]
	public int? CompletionStatus { get; init; }

	/// <summary>
	/// <para>Indicates when the async search completed. Only present<br/>when the search has completed.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("completion_time")]
	public DateTimeOffset? CompletionTime { get; init; }
	[JsonInclude, JsonPropertyName("completion_time_in_millis")]
	public long? CompletionTimeInMillis { get; init; }

	/// <summary>
	/// <para>Indicates when the async search will expire.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("expiration_time")]
	public DateTimeOffset? ExpirationTime { get; init; }
	[JsonInclude, JsonPropertyName("expiration_time_in_millis")]
	public long ExpirationTimeInMillis { get; init; }
	[JsonInclude, JsonPropertyName("id")]
	public string? Id { get; init; }

	/// <summary>
	/// <para>When the query is no longer running, this property indicates whether the search failed or was successfully completed on all shards.<br/>While the query is running, `is_partial` is always set to `true`.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("is_partial")]
	public bool IsPartial { get; init; }

	/// <summary>
	/// <para>Indicates whether the search is still running or has completed.<br/>NOTE: If the search failed after some shards returned their results or the node that is coordinating the async search dies, results may be partial even though `is_running` is `false`.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("is_running")]
	public bool IsRunning { get; init; }

	/// <summary>
	/// <para>Indicates how many shards have run the query so far.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("_shards")]
	public Elastic.Clients.Elasticsearch.Serverless.ShardStatistics Shards { get; init; }
	[JsonInclude, JsonPropertyName("start_time")]
	public DateTimeOffset? StartTime { get; init; }
	[JsonInclude, JsonPropertyName("start_time_in_millis")]
	public long StartTimeInMillis { get; init; }
}