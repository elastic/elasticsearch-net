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
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch;

public sealed partial class ScrollResponse<TDocument> : ElasticsearchResponse
{
	[JsonInclude, JsonPropertyName("aggregations")]
	public Elastic.Clients.Elasticsearch.Aggregations.AggregateDictionary? Aggregations { get; init; }
	[JsonInclude, JsonPropertyName("_clusters")]
	public Elastic.Clients.Elasticsearch.ClusterStatistics? Clusters { get; init; }
	[JsonInclude, JsonPropertyName("fields")]
	public IReadOnlyDictionary<string, object>? Fields { get; init; }

	/// <summary>
	/// <para>
	/// The returned documents and metadata.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("hits")]
	public Elastic.Clients.Elasticsearch.Core.Search.HitsMetadata<TDocument> HitsMetadata { get; init; }
	[JsonInclude, JsonPropertyName("max_score")]
	public double? MaxScore { get; init; }
	[JsonInclude, JsonPropertyName("num_reduce_phases")]
	public long? NumReducePhases { get; init; }
	[JsonInclude, JsonPropertyName("pit_id")]
	public string? PitId { get; init; }
	[JsonInclude, JsonPropertyName("profile")]
	public Elastic.Clients.Elasticsearch.Core.Search.Profile? Profile { get; init; }

	/// <summary>
	/// <para>
	/// The identifier for the search and its search context.
	/// You can use this scroll ID with the scroll API to retrieve the next batch of search results for the request.
	/// This property is returned only if the <c>scroll</c> query parameter is specified in the request.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("_scroll_id")]
	public Elastic.Clients.Elasticsearch.ScrollId? ScrollId { get; init; }

	/// <summary>
	/// <para>
	/// A count of shards used for the request.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("_shards")]
	public Elastic.Clients.Elasticsearch.ShardStatistics Shards { get; init; }
	[JsonInclude, JsonPropertyName("suggest")]
	public Elastic.Clients.Elasticsearch.Core.Search.SuggestDictionary<TDocument>? Suggest { get; init; }
	[JsonInclude, JsonPropertyName("terminated_early")]
	public bool? TerminatedEarly { get; init; }

	/// <summary>
	/// <para>
	/// If <c>true</c>, the request timed out before completion; returned results may be partial or empty.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("timed_out")]
	public bool TimedOut { get; init; }

	/// <summary>
	/// <para>
	/// The number of milliseconds it took Elasticsearch to run the request.
	/// This value is calculated by measuring the time elapsed between receipt of a request on the coordinating node and the time at which the coordinating node is ready to send the response.
	/// It includes:
	/// </para>
	/// <list type="bullet">
	/// <item>
	/// <para>
	/// Communication time between the coordinating node and data nodes
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// Time the request spends in the search thread pool, queued for execution
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// Actual run time
	/// </para>
	/// </item>
	/// </list>
	/// <para>
	/// It does not include:
	/// </para>
	/// <list type="bullet">
	/// <item>
	/// <para>
	/// Time needed to send the request to Elasticsearch
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// Time needed to serialize the JSON response
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// Time needed to send the response to a client
	/// </para>
	/// </item>
	/// </list>
	/// </summary>
	[JsonInclude, JsonPropertyName("took")]
	public long Took { get; init; }
}