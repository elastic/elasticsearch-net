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

namespace Elastic.Clients.Elasticsearch.Serverless.Core.Search;

public sealed partial class AggregationProfileDebug
{
	[JsonInclude, JsonPropertyName("built_buckets")]
	public int? BuiltBuckets { get; init; }
	[JsonInclude, JsonPropertyName("chars_fetched")]
	public int? CharsFetched { get; init; }
	[JsonInclude, JsonPropertyName("collect_analyzed_count")]
	public int? CollectAnalyzedCount { get; init; }
	[JsonInclude, JsonPropertyName("collect_analyzed_ns")]
	public int? CollectAnalyzedNs { get; init; }
	[JsonInclude, JsonPropertyName("collection_strategy")]
	public string? CollectionStrategy { get; init; }
	[JsonInclude, JsonPropertyName("deferred_aggregators")]
	public IReadOnlyCollection<string>? DeferredAggregators { get; init; }
	[JsonInclude, JsonPropertyName("delegate")]
	public string? Delegate { get; init; }
	[JsonInclude, JsonPropertyName("delegate_debug")]
	public Elastic.Clients.Elasticsearch.Serverless.Core.Search.AggregationProfileDebug? DelegateDebug { get; init; }
	[JsonInclude, JsonPropertyName("empty_collectors_used")]
	public int? EmptyCollectorsUsed { get; init; }
	[JsonInclude, JsonPropertyName("extract_count")]
	public int? ExtractCount { get; init; }
	[JsonInclude, JsonPropertyName("extract_ns")]
	public int? ExtractNs { get; init; }
	[JsonInclude, JsonPropertyName("filters")]
	public IReadOnlyCollection<Elastic.Clients.Elasticsearch.Serverless.Core.Search.AggregationProfileDelegateDebugFilter>? Filters { get; init; }
	[JsonInclude, JsonPropertyName("has_filter")]
	public bool? HasFilter { get; init; }
	[JsonInclude, JsonPropertyName("map_reducer")]
	public string? MapReducer { get; init; }
	[JsonInclude, JsonPropertyName("numeric_collectors_used")]
	public int? NumericCollectorsUsed { get; init; }
	[JsonInclude, JsonPropertyName("ordinals_collectors_overhead_too_high")]
	public int? OrdinalsCollectorsOverheadTooHigh { get; init; }
	[JsonInclude, JsonPropertyName("ordinals_collectors_used")]
	public int? OrdinalsCollectorsUsed { get; init; }
	[JsonInclude, JsonPropertyName("result_strategy")]
	public string? ResultStrategy { get; init; }
	[JsonInclude, JsonPropertyName("segments_collected")]
	public int? SegmentsCollected { get; init; }
	[JsonInclude, JsonPropertyName("segments_counted")]
	public int? SegmentsCounted { get; init; }
	[JsonInclude, JsonPropertyName("segments_with_deleted_docs")]
	public int? SegmentsWithDeletedDocs { get; init; }
	[JsonInclude, JsonPropertyName("segments_with_doc_count_field")]
	public int? SegmentsWithDocCountField { get; init; }
	[JsonInclude, JsonPropertyName("segments_with_multi_valued_ords")]
	public int? SegmentsWithMultiValuedOrds { get; init; }
	[JsonInclude, JsonPropertyName("segments_with_single_valued_ords")]
	public int? SegmentsWithSingleValuedOrds { get; init; }
	[JsonInclude, JsonPropertyName("string_hashing_collectors_used")]
	public int? StringHashingCollectorsUsed { get; init; }
	[JsonInclude, JsonPropertyName("surviving_buckets")]
	public int? SurvivingBuckets { get; init; }
	[JsonInclude, JsonPropertyName("total_buckets")]
	public int? TotalBuckets { get; init; }
	[JsonInclude, JsonPropertyName("values_fetched")]
	public int? ValuesFetched { get; init; }
}