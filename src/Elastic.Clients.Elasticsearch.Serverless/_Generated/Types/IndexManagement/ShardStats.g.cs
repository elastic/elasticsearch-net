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

namespace Elastic.Clients.Elasticsearch.Serverless.IndexManagement;

public sealed partial class ShardStats
{
	[JsonInclude, JsonPropertyName("bulk")]
	public Elastic.Clients.Elasticsearch.Serverless.BulkStats? Bulk { get; init; }
	[JsonInclude, JsonPropertyName("commit")]
	public Elastic.Clients.Elasticsearch.Serverless.IndexManagement.ShardCommit? Commit { get; init; }
	[JsonInclude, JsonPropertyName("completion")]
	public Elastic.Clients.Elasticsearch.Serverless.CompletionStats? Completion { get; init; }
	[JsonInclude, JsonPropertyName("docs")]
	public Elastic.Clients.Elasticsearch.Serverless.DocStats? Docs { get; init; }
	[JsonInclude, JsonPropertyName("fielddata")]
	public Elastic.Clients.Elasticsearch.Serverless.FielddataStats? Fielddata { get; init; }
	[JsonInclude, JsonPropertyName("flush")]
	public Elastic.Clients.Elasticsearch.Serverless.FlushStats? Flush { get; init; }
	[JsonInclude, JsonPropertyName("get")]
	public Elastic.Clients.Elasticsearch.Serverless.GetStats? Get { get; init; }
	[JsonInclude, JsonPropertyName("indexing")]
	public Elastic.Clients.Elasticsearch.Serverless.IndexingStats? Indexing { get; init; }
	[JsonInclude, JsonPropertyName("indices")]
	public Elastic.Clients.Elasticsearch.Serverless.IndexManagement.IndicesStats? Indices { get; init; }
	[JsonInclude, JsonPropertyName("mappings")]
	public Elastic.Clients.Elasticsearch.Serverless.IndexManagement.MappingStats? Mappings { get; init; }
	[JsonInclude, JsonPropertyName("merges")]
	public Elastic.Clients.Elasticsearch.Serverless.MergesStats? Merges { get; init; }
	[JsonInclude, JsonPropertyName("query_cache")]
	public Elastic.Clients.Elasticsearch.Serverless.IndexManagement.ShardQueryCache? QueryCache { get; init; }
	[JsonInclude, JsonPropertyName("recovery")]
	public Elastic.Clients.Elasticsearch.Serverless.RecoveryStats? Recovery { get; init; }
	[JsonInclude, JsonPropertyName("refresh")]
	public Elastic.Clients.Elasticsearch.Serverless.RefreshStats? Refresh { get; init; }
	[JsonInclude, JsonPropertyName("request_cache")]
	public Elastic.Clients.Elasticsearch.Serverless.RequestCacheStats? RequestCache { get; init; }
	[JsonInclude, JsonPropertyName("retention_leases")]
	public Elastic.Clients.Elasticsearch.Serverless.IndexManagement.ShardRetentionLeases? RetentionLeases { get; init; }
	[JsonInclude, JsonPropertyName("routing")]
	public Elastic.Clients.Elasticsearch.Serverless.IndexManagement.ShardRouting? Routing { get; init; }
	[JsonInclude, JsonPropertyName("search")]
	public Elastic.Clients.Elasticsearch.Serverless.SearchStats? Search { get; init; }
	[JsonInclude, JsonPropertyName("segments")]
	public Elastic.Clients.Elasticsearch.Serverless.SegmentsStats? Segments { get; init; }
	[JsonInclude, JsonPropertyName("seq_no")]
	public Elastic.Clients.Elasticsearch.Serverless.IndexManagement.ShardSequenceNumber? SeqNo { get; init; }
	[JsonInclude, JsonPropertyName("shard_path")]
	public Elastic.Clients.Elasticsearch.Serverless.IndexManagement.ShardPath? ShardPath { get; init; }
	[JsonInclude, JsonPropertyName("shards")]
	[ReadOnlyIndexNameDictionaryConverter(typeof(object))]
	public IReadOnlyDictionary<Elastic.Clients.Elasticsearch.Serverless.IndexName, object>? Shards { get; init; }
	[JsonInclude, JsonPropertyName("shard_stats")]
	public Elastic.Clients.Elasticsearch.Serverless.IndexManagement.ShardsTotalStats? ShardStats2 { get; init; }
	[JsonInclude, JsonPropertyName("store")]
	public Elastic.Clients.Elasticsearch.Serverless.StoreStats? Store { get; init; }
	[JsonInclude, JsonPropertyName("translog")]
	public Elastic.Clients.Elasticsearch.Serverless.TranslogStats? Translog { get; init; }
	[JsonInclude, JsonPropertyName("warmer")]
	public Elastic.Clients.Elasticsearch.Serverless.WarmerStats? Warmer { get; init; }
}