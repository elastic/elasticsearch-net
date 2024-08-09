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

namespace Elastic.Clients.Elasticsearch.Serverless;

public sealed partial class QueryCacheStats
{
	/// <summary>
	/// <para>
	/// Total number of entries added to the query cache across all shards assigned to selected nodes.
	/// This number includes current and evicted entries.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("cache_count")]
	public long CacheCount { get; init; }

	/// <summary>
	/// <para>
	/// Total number of entries currently in the query cache across all shards assigned to selected nodes.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("cache_size")]
	public long CacheSize { get; init; }

	/// <summary>
	/// <para>
	/// Total number of query cache evictions across all shards assigned to selected nodes.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("evictions")]
	public long Evictions { get; init; }

	/// <summary>
	/// <para>
	/// Total count of query cache hits across all shards assigned to selected nodes.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("hit_count")]
	public long HitCount { get; init; }

	/// <summary>
	/// <para>
	/// Total amount of memory used for the query cache across all shards assigned to selected nodes.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("memory_size")]
	public Elastic.Clients.Elasticsearch.Serverless.ByteSize? MemorySize { get; init; }

	/// <summary>
	/// <para>
	/// Total amount, in bytes, of memory used for the query cache across all shards assigned to selected nodes.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("memory_size_in_bytes")]
	public long MemorySizeInBytes { get; init; }

	/// <summary>
	/// <para>
	/// Total count of query cache misses across all shards assigned to selected nodes.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("miss_count")]
	public long MissCount { get; init; }

	/// <summary>
	/// <para>
	/// Total count of hits and misses in the query cache across all shards assigned to selected nodes.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("total_count")]
	public long TotalCount { get; init; }
}