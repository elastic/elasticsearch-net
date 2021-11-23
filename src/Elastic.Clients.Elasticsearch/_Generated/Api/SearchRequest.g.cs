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

using Elastic.Transport;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch
{
	public class SearchRequestParameters : RequestParameters<SearchRequestParameters>
	{
		[JsonIgnore]
		public bool? AllowNoIndices { get => Q<bool?>("allow_no_indices"); set => Q("allow_no_indices", value); }

		[JsonIgnore]
		public bool? AllowPartialSearchResults { get => Q<bool?>("allow_partial_search_results"); set => Q("allow_partial_search_results", value); }

		[JsonIgnore]
		public string? Analyzer { get => Q<string?>("analyzer"); set => Q("analyzer", value); }

		[JsonIgnore]
		public bool? AnalyzeWildcard { get => Q<bool?>("analyze_wildcard"); set => Q("analyze_wildcard", value); }

		[JsonIgnore]
		public long? BatchedReduceSize { get => Q<long?>("batched_reduce_size"); set => Q("batched_reduce_size", value); }

		[JsonIgnore]
		public bool? CcsMinimizeRoundtrips { get => Q<bool?>("ccs_minimize_roundtrips"); set => Q("ccs_minimize_roundtrips", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.QueryDsl.Operator? DefaultOperator { get => Q<Elastic.Clients.Elasticsearch.QueryDsl.Operator?>("default_operator"); set => Q("default_operator", value); }

		[JsonIgnore]
		public string? Df { get => Q<string?>("df"); set => Q("df", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.Fields? DocvalueFields { get => Q<Elastic.Clients.Elasticsearch.Fields?>("docvalue_fields"); set => Q("docvalue_fields", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.ExpandWildcards? ExpandWildcards { get => Q<Elastic.Clients.Elasticsearch.ExpandWildcards?>("expand_wildcards"); set => Q("expand_wildcards", value); }

		[JsonIgnore]
		public bool? Explain { get => Q<bool?>("explain"); set => Q("explain", value); }

		[JsonIgnore]
		public bool? IgnoreThrottled { get => Q<bool?>("ignore_throttled"); set => Q("ignore_throttled", value); }

		[JsonIgnore]
		public bool? IgnoreUnavailable { get => Q<bool?>("ignore_unavailable"); set => Q("ignore_unavailable", value); }

		[JsonIgnore]
		public bool? Lenient { get => Q<bool?>("lenient"); set => Q("lenient", value); }

		[JsonIgnore]
		public long? MaxConcurrentShardRequests { get => Q<long?>("max_concurrent_shard_requests"); set => Q("max_concurrent_shard_requests", value); }

		[JsonIgnore]
		public string? MinCompatibleShardNode { get => Q<string?>("min_compatible_shard_node"); set => Q("min_compatible_shard_node", value); }

		[JsonIgnore]
		public string? Preference { get => Q<string?>("preference"); set => Q("preference", value); }

		[JsonIgnore]
		public long? PreFilterShardSize { get => Q<long?>("pre_filter_shard_size"); set => Q("pre_filter_shard_size", value); }

		[JsonIgnore]
		public bool? RequestCache { get => Q<bool?>("request_cache"); set => Q("request_cache", value); }

		[JsonIgnore]
		public string? Routing { get => Q<string?>("routing"); set => Q("routing", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.Time? Scroll { get => Q<Elastic.Clients.Elasticsearch.Time?>("scroll"); set => Q("scroll", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.SearchType? SearchType { get => Q<Elastic.Clients.Elasticsearch.SearchType?>("search_type"); set => Q("search_type", value); }

		[JsonIgnore]
		public IEnumerable<string>? Stats { get => Q<IEnumerable<string>?>("stats"); set => Q("stats", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.Fields? StoredFields { get => Q<Elastic.Clients.Elasticsearch.Fields?>("stored_fields"); set => Q("stored_fields", value); }

		[JsonIgnore]
		public string? SuggestField { get => Q<string?>("suggest_field"); set => Q("suggest_field", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.SuggestMode? SuggestMode { get => Q<Elastic.Clients.Elasticsearch.SuggestMode?>("suggest_mode"); set => Q("suggest_mode", value); }

		[JsonIgnore]
		public long? SuggestSize { get => Q<long?>("suggest_size"); set => Q("suggest_size", value); }

		[JsonIgnore]
		public string? SuggestText { get => Q<string?>("suggest_text"); set => Q("suggest_text", value); }

		[JsonIgnore]
		public long? TerminateAfter { get => Q<long?>("terminate_after"); set => Q("terminate_after", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.Time? Timeout { get => Q<Elastic.Clients.Elasticsearch.Time?>("timeout"); set => Q("timeout", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.TrackHits? TrackTotalHits { get => Q<Elastic.Clients.Elasticsearch.TrackHits?>("track_total_hits"); set => Q("track_total_hits", value); }

		[JsonIgnore]
		public bool? TrackScores { get => Q<bool?>("track_scores"); set => Q("track_scores", value); }

		[JsonIgnore]
		public bool? TypedKeys { get => Q<bool?>("typed_keys"); set => Q("typed_keys", value); }

		[JsonIgnore]
		public bool? RestTotalHitsAsInt { get => Q<bool?>("rest_total_hits_as_int"); set => Q("rest_total_hits_as_int", value); }

		[JsonIgnore]
		public bool? Version { get => Q<bool?>("version"); set => Q("version", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.SourceConfigParam? Source { get => Q<Elastic.Clients.Elasticsearch.SourceConfigParam?>("_source"); set => Q("_source", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.Fields? SourceExcludes { get => Q<Elastic.Clients.Elasticsearch.Fields?>("_source_excludes"); set => Q("_source_excludes", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.Fields? SourceIncludes { get => Q<Elastic.Clients.Elasticsearch.Fields?>("_source_includes"); set => Q("_source_includes", value); }

		[JsonIgnore]
		public bool? SeqNoPrimaryTerm { get => Q<bool?>("seq_no_primary_term"); set => Q("seq_no_primary_term", value); }

		[JsonIgnore]
		public string? QueryLuceneSyntax { get => Q<string?>("q"); set => Q("q", value); }

		[JsonIgnore]
		public int? Size { get => Q<int?>("size"); set => Q("size", value); }

		[JsonIgnore]
		public int? From { get => Q<int?>("from"); set => Q("from", value); }

		[JsonIgnore]
		public string? Sort { get => Q<string?>("sort"); set => Q("sort", value); }
	}

	public partial class SearchRequest : PlainRequestBase<SearchRequestParameters>
	{
		public SearchRequest()
		{
		}

		public SearchRequest(Elastic.Clients.Elasticsearch.Indices? indices) : base(r => r.Optional("index", indices))
		{
		}

		internal override ApiUrls ApiUrls => ApiUrlsLookups.NoNamespaceSearch;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => true;
		[JsonIgnore]
		public bool? AllowNoIndices { get => Q<bool?>("allow_no_indices"); set => Q("allow_no_indices", value); }

		[JsonIgnore]
		public bool? AllowPartialSearchResults { get => Q<bool?>("allow_partial_search_results"); set => Q("allow_partial_search_results", value); }

		[JsonIgnore]
		public string? Analyzer { get => Q<string?>("analyzer"); set => Q("analyzer", value); }

		[JsonIgnore]
		public bool? AnalyzeWildcard { get => Q<bool?>("analyze_wildcard"); set => Q("analyze_wildcard", value); }

		[JsonIgnore]
		public long? BatchedReduceSize { get => Q<long?>("batched_reduce_size"); set => Q("batched_reduce_size", value); }

		[JsonIgnore]
		public bool? CcsMinimizeRoundtrips { get => Q<bool?>("ccs_minimize_roundtrips"); set => Q("ccs_minimize_roundtrips", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.QueryDsl.Operator? DefaultOperator { get => Q<Elastic.Clients.Elasticsearch.QueryDsl.Operator?>("default_operator"); set => Q("default_operator", value); }

		[JsonIgnore]
		public string? Df { get => Q<string?>("df"); set => Q("df", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.Fields? DocvalueFields { get => Q<Elastic.Clients.Elasticsearch.Fields?>("docvalue_fields"); set => Q("docvalue_fields", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.ExpandWildcards? ExpandWildcards { get => Q<Elastic.Clients.Elasticsearch.ExpandWildcards?>("expand_wildcards"); set => Q("expand_wildcards", value); }

		[JsonIgnore]
		public bool? Explain { get => Q<bool?>("explain"); set => Q("explain", value); }

		[JsonIgnore]
		public bool? IgnoreThrottled { get => Q<bool?>("ignore_throttled"); set => Q("ignore_throttled", value); }

		[JsonIgnore]
		public bool? IgnoreUnavailable { get => Q<bool?>("ignore_unavailable"); set => Q("ignore_unavailable", value); }

		[JsonIgnore]
		public bool? Lenient { get => Q<bool?>("lenient"); set => Q("lenient", value); }

		[JsonIgnore]
		public long? MaxConcurrentShardRequests { get => Q<long?>("max_concurrent_shard_requests"); set => Q("max_concurrent_shard_requests", value); }

		[JsonIgnore]
		public string? MinCompatibleShardNode { get => Q<string?>("min_compatible_shard_node"); set => Q("min_compatible_shard_node", value); }

		[JsonIgnore]
		public string? Preference { get => Q<string?>("preference"); set => Q("preference", value); }

		[JsonIgnore]
		public long? PreFilterShardSize { get => Q<long?>("pre_filter_shard_size"); set => Q("pre_filter_shard_size", value); }

		[JsonIgnore]
		public bool? RequestCache { get => Q<bool?>("request_cache"); set => Q("request_cache", value); }

		[JsonIgnore]
		public string? Routing { get => Q<string?>("routing"); set => Q("routing", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.Time? Scroll { get => Q<Elastic.Clients.Elasticsearch.Time?>("scroll"); set => Q("scroll", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.SearchType? SearchType { get => Q<Elastic.Clients.Elasticsearch.SearchType?>("search_type"); set => Q("search_type", value); }

		[JsonIgnore]
		public IEnumerable<string>? Stats { get => Q<IEnumerable<string>?>("stats"); set => Q("stats", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.Fields? StoredFields { get => Q<Elastic.Clients.Elasticsearch.Fields?>("stored_fields"); set => Q("stored_fields", value); }

		[JsonIgnore]
		public string? SuggestField { get => Q<string?>("suggest_field"); set => Q("suggest_field", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.SuggestMode? SuggestMode { get => Q<Elastic.Clients.Elasticsearch.SuggestMode?>("suggest_mode"); set => Q("suggest_mode", value); }

		[JsonIgnore]
		public long? SuggestSize { get => Q<long?>("suggest_size"); set => Q("suggest_size", value); }

		[JsonIgnore]
		public string? SuggestText { get => Q<string?>("suggest_text"); set => Q("suggest_text", value); }

		[JsonIgnore]
		public long? TerminateAfter { get => Q<long?>("terminate_after"); set => Q("terminate_after", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.Time? Timeout { get => Q<Elastic.Clients.Elasticsearch.Time?>("timeout"); set => Q("timeout", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.TrackHits? TrackTotalHits { get => Q<Elastic.Clients.Elasticsearch.TrackHits?>("track_total_hits"); set => Q("track_total_hits", value); }

		[JsonIgnore]
		public bool? TrackScores { get => Q<bool?>("track_scores"); set => Q("track_scores", value); }

		[JsonIgnore]
		public bool? TypedKeys { get => Q<bool?>("typed_keys"); set => Q("typed_keys", value); }

		[JsonIgnore]
		public bool? RestTotalHitsAsInt { get => Q<bool?>("rest_total_hits_as_int"); set => Q("rest_total_hits_as_int", value); }

		[JsonIgnore]
		public bool? Version { get => Q<bool?>("version"); set => Q("version", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.SourceConfigParam? Source { get => Q<Elastic.Clients.Elasticsearch.SourceConfigParam?>("_source"); set => Q("_source", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.Fields? SourceExcludes { get => Q<Elastic.Clients.Elasticsearch.Fields?>("_source_excludes"); set => Q("_source_excludes", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.Fields? SourceIncludes { get => Q<Elastic.Clients.Elasticsearch.Fields?>("_source_includes"); set => Q("_source_includes", value); }

		[JsonIgnore]
		public bool? SeqNoPrimaryTerm { get => Q<bool?>("seq_no_primary_term"); set => Q("seq_no_primary_term", value); }

		[JsonIgnore]
		public string? QueryLuceneSyntax { get => Q<string?>("q"); set => Q("q", value); }

		[JsonIgnore]
		public int? Size { get => Q<int?>("size"); set => Q("size", value); }

		[JsonIgnore]
		public int? From { get => Q<int?>("from"); set => Q("from", value); }

		[JsonIgnore]
		public string? Sort { get => Q<string?>("sort"); set => Q("sort", value); }

		[JsonInclude]
		[JsonPropertyName("aggregations")]
		public Elastic.Clients.Elasticsearch.Aggregations.AggregationDictionary? Aggregations { get; set; }

		[JsonInclude]
		[JsonPropertyName("collapse")]
		public Elastic.Clients.Elasticsearch.FieldCollapse? Collapse { get; set; }

		[JsonInclude]
		[JsonPropertyName("highlight")]
		public Elastic.Clients.Elasticsearch.Highlight? Highlight { get; set; }

		[JsonInclude]
		[JsonPropertyName("indices_boost")]
		public IEnumerable<Dictionary<Elastic.Clients.Elasticsearch.IndexName, double>>? IndicesBoost { get; set; }

		[JsonInclude]
		[JsonPropertyName("min_score")]
		public double? MinScore { get; set; }

		[JsonInclude]
		[JsonPropertyName("post_filter")]
		public Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer? PostFilter { get; set; }

		[JsonInclude]
		[JsonPropertyName("profile")]
		public bool? Profile { get; set; }

		[JsonInclude]
		[JsonPropertyName("query")]
		public Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer? Query { get; set; }

		[JsonInclude]
		[JsonPropertyName("rescore")]
		public Elastic.Clients.Elasticsearch.Rescore? Rescore { get; set; }

		[JsonInclude]
		[JsonPropertyName("script_fields")]
		public Dictionary<string, Elastic.Clients.Elasticsearch.ScriptField>? ScriptFields { get; set; }

		[JsonInclude]
		[JsonPropertyName("search_after")]
		public IEnumerable<object>? SearchAfter { get; set; }

		[JsonInclude]
		[JsonPropertyName("slice")]
		public Elastic.Clients.Elasticsearch.SlicedScroll? Slice { get; set; }

		[JsonInclude]
		[JsonPropertyName("fields")]
		public IEnumerable<Elastic.Clients.Elasticsearch.QueryDsl.FieldAndFormat>? Fields { get; set; }

		[JsonInclude]
		[JsonPropertyName("suggest")]
		public Elastic.Clients.Elasticsearch.Suggester? Suggest { get; set; }

		[JsonInclude]
		[JsonPropertyName("pit")]
		public Elastic.Clients.Elasticsearch.PointInTimeReference? Pit { get; set; }

		[JsonInclude]
		[JsonPropertyName("runtime_mappings")]
		public Dictionary<string, Elastic.Clients.Elasticsearch.Mapping.RuntimeField>? RuntimeMappings { get; set; }
	}

	public sealed partial class SearchRequestDescriptor<T> : RequestDescriptorBase<SearchRequestDescriptor<T>, SearchRequestParameters>
	{
		public SearchRequestDescriptor()
		{
		}

		public SearchRequestDescriptor(Elastic.Clients.Elasticsearch.Indices? indices) : base(r => r.Optional("index", indices))
		{
		}

		internal SearchRequestDescriptor(Action<SearchRequestDescriptor<T>> configure) => configure.Invoke(this);
		internal override ApiUrls ApiUrls => ApiUrlsLookups.NoNamespaceSearch;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => true;
		public SearchRequestDescriptor<T> AllowNoIndices(bool? allowNoIndices) => Qs("allow_no_indices", allowNoIndices);
		public SearchRequestDescriptor<T> AllowPartialSearchResults(bool? allowPartialSearchResults) => Qs("allow_partial_search_results", allowPartialSearchResults);
		public SearchRequestDescriptor<T> Analyzer(string? analyzer) => Qs("analyzer", analyzer);
		public SearchRequestDescriptor<T> AnalyzeWildcard(bool? analyzeWildcard) => Qs("analyze_wildcard", analyzeWildcard);
		public SearchRequestDescriptor<T> BatchedReduceSize(long? batchedReduceSize) => Qs("batched_reduce_size", batchedReduceSize);
		public SearchRequestDescriptor<T> CcsMinimizeRoundtrips(bool? ccsMinimizeRoundtrips) => Qs("ccs_minimize_roundtrips", ccsMinimizeRoundtrips);
		public SearchRequestDescriptor<T> DefaultOperator(Elastic.Clients.Elasticsearch.QueryDsl.Operator? defaultOperator) => Qs("default_operator", defaultOperator);
		public SearchRequestDescriptor<T> Df(string? df) => Qs("df", df);
		public SearchRequestDescriptor<T> DocvalueFields(Elastic.Clients.Elasticsearch.Fields? docvalueFields) => Qs("docvalue_fields", docvalueFields);
		public SearchRequestDescriptor<T> ExpandWildcards(Elastic.Clients.Elasticsearch.ExpandWildcards? expandWildcards) => Qs("expand_wildcards", expandWildcards);
		public SearchRequestDescriptor<T> Explain(bool? explain) => Qs("explain", explain);
		public SearchRequestDescriptor<T> IgnoreThrottled(bool? ignoreThrottled) => Qs("ignore_throttled", ignoreThrottled);
		public SearchRequestDescriptor<T> IgnoreUnavailable(bool? ignoreUnavailable) => Qs("ignore_unavailable", ignoreUnavailable);
		public SearchRequestDescriptor<T> Lenient(bool? lenient) => Qs("lenient", lenient);
		public SearchRequestDescriptor<T> MaxConcurrentShardRequests(long? maxConcurrentShardRequests) => Qs("max_concurrent_shard_requests", maxConcurrentShardRequests);
		public SearchRequestDescriptor<T> MinCompatibleShardNode(string? minCompatibleShardNode) => Qs("min_compatible_shard_node", minCompatibleShardNode);
		public SearchRequestDescriptor<T> Preference(string? preference) => Qs("preference", preference);
		public SearchRequestDescriptor<T> PreFilterShardSize(long? preFilterShardSize) => Qs("pre_filter_shard_size", preFilterShardSize);
		public SearchRequestDescriptor<T> RequestCache(bool? requestCache) => Qs("request_cache", requestCache);
		public SearchRequestDescriptor<T> Routing(string? routing) => Qs("routing", routing);
		public SearchRequestDescriptor<T> Scroll(Elastic.Clients.Elasticsearch.Time? scroll) => Qs("scroll", scroll);
		public SearchRequestDescriptor<T> SearchType(Elastic.Clients.Elasticsearch.SearchType? searchType) => Qs("search_type", searchType);
		public SearchRequestDescriptor<T> Stats(IEnumerable<string>? stats) => Qs("stats", stats);
		public SearchRequestDescriptor<T> StoredFields(Elastic.Clients.Elasticsearch.Fields? storedFields) => Qs("stored_fields", storedFields);
		public SearchRequestDescriptor<T> SuggestField(string? suggestField) => Qs("suggest_field", suggestField);
		public SearchRequestDescriptor<T> SuggestMode(Elastic.Clients.Elasticsearch.SuggestMode? suggestMode) => Qs("suggest_mode", suggestMode);
		public SearchRequestDescriptor<T> SuggestSize(long? suggestSize) => Qs("suggest_size", suggestSize);
		public SearchRequestDescriptor<T> SuggestText(string? suggestText) => Qs("suggest_text", suggestText);
		public SearchRequestDescriptor<T> TerminateAfter(long? terminateAfter) => Qs("terminate_after", terminateAfter);
		public SearchRequestDescriptor<T> Timeout(Elastic.Clients.Elasticsearch.Time? timeout) => Qs("timeout", timeout);
		public SearchRequestDescriptor<T> TrackTotalHits(Elastic.Clients.Elasticsearch.TrackHits? trackTotalHits) => Qs("track_total_hits", trackTotalHits);
		public SearchRequestDescriptor<T> TrackScores(bool? trackScores) => Qs("track_scores", trackScores);
		public SearchRequestDescriptor<T> TypedKeys(bool? typedKeys) => Qs("typed_keys", typedKeys);
		public SearchRequestDescriptor<T> RestTotalHitsAsInt(bool? restTotalHitsAsInt) => Qs("rest_total_hits_as_int", restTotalHitsAsInt);
		public SearchRequestDescriptor<T> Version(bool? version) => Qs("version", version);
		public SearchRequestDescriptor<T> Source(Elastic.Clients.Elasticsearch.SourceConfigParam? source) => Qs("_source", source);
		public SearchRequestDescriptor<T> SourceExcludes(Elastic.Clients.Elasticsearch.Fields? sourceExcludes) => Qs("_source_excludes", sourceExcludes);
		public SearchRequestDescriptor<T> SourceIncludes(Elastic.Clients.Elasticsearch.Fields? sourceIncludes) => Qs("_source_includes", sourceIncludes);
		public SearchRequestDescriptor<T> SeqNoPrimaryTerm(bool? seqNoPrimaryTerm) => Qs("seq_no_primary_term", seqNoPrimaryTerm);
		public SearchRequestDescriptor<T> QueryLuceneSyntax(string? q) => Qs("q", q);
		public SearchRequestDescriptor<T> Size(int? size) => Qs("size", size);
		public SearchRequestDescriptor<T> From(int? from) => Qs("from", from);
		public SearchRequestDescriptor<T> Sort(string? sort) => Qs("sort", sort);
		internal Elastic.Clients.Elasticsearch.Aggregations.AggregationDictionary? AggregationsValue { get; private set; }

		internal Elastic.Clients.Elasticsearch.FieldCollapse? CollapseValue { get; private set; }

		internal Elastic.Clients.Elasticsearch.Highlight? HighlightValue { get; private set; }

		internal IEnumerable<Dictionary<Elastic.Clients.Elasticsearch.IndexName, double>>? IndicesBoostValue { get; private set; }

		internal double? MinScoreValue { get; private set; }

		internal Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer? PostFilterValue { get; private set; }

		internal bool? ProfileValue { get; private set; }

		internal Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer? QueryValue { get; private set; }

		internal Elastic.Clients.Elasticsearch.Rescore? RescoreValue { get; private set; }

		internal Dictionary<string, Elastic.Clients.Elasticsearch.ScriptField>? ScriptFieldsValue { get; private set; }

		internal IEnumerable<object>? SearchAfterValue { get; private set; }

		internal Elastic.Clients.Elasticsearch.SlicedScroll? SliceValue { get; private set; }

		internal IEnumerable<Elastic.Clients.Elasticsearch.QueryDsl.FieldAndFormat>? FieldsValue { get; private set; }

		internal Elastic.Clients.Elasticsearch.Suggester? SuggestValue { get; private set; }

		internal Elastic.Clients.Elasticsearch.PointInTimeReference? PitValue { get; private set; }

		internal Dictionary<string, Elastic.Clients.Elasticsearch.Mapping.RuntimeField>? RuntimeMappingsValue { get; private set; }

		internal FieldCollapseDescriptor<T> CollapseDescriptor { get; private set; }

		internal HighlightDescriptor<T> HighlightDescriptor { get; private set; }

		internal QueryDsl.QueryContainerDescriptor<T> PostFilterDescriptor { get; private set; }

		internal QueryDsl.QueryContainerDescriptor<T> QueryDescriptor { get; private set; }

		internal RescoreDescriptor<T> RescoreDescriptor { get; private set; }

		internal SlicedScrollDescriptor<T> SliceDescriptor { get; private set; }

		internal SuggesterDescriptor SuggestDescriptor { get; private set; }

		internal PointInTimeReferenceDescriptor PitDescriptor { get; private set; }

		internal Action<FieldCollapseDescriptor<T>> CollapseDescriptorAction { get; private set; }

		internal Action<HighlightDescriptor<T>> HighlightDescriptorAction { get; private set; }

		internal Action<QueryDsl.QueryContainerDescriptor<T>> PostFilterDescriptorAction { get; private set; }

		internal Action<QueryDsl.QueryContainerDescriptor<T>> QueryDescriptorAction { get; private set; }

		internal Action<RescoreDescriptor<T>> RescoreDescriptorAction { get; private set; }

		internal Action<SlicedScrollDescriptor<T>> SliceDescriptorAction { get; private set; }

		internal Action<SuggesterDescriptor> SuggestDescriptorAction { get; private set; }

		internal Action<PointInTimeReferenceDescriptor> PitDescriptorAction { get; private set; }

		public SearchRequestDescriptor<T> Aggregations(Elastic.Clients.Elasticsearch.Aggregations.AggregationDictionary? aggregations) => Assign(aggregations, (a, v) => a.AggregationsValue = v);
		public SearchRequestDescriptor<T> Collapse(Elastic.Clients.Elasticsearch.FieldCollapse? collapse)
		{
			CollapseDescriptor = null;
			CollapseDescriptorAction = null;
			return Assign(collapse, (a, v) => a.CollapseValue = v);
		}

		public SearchRequestDescriptor<T> Collapse(Elastic.Clients.Elasticsearch.FieldCollapseDescriptor<T> descriptor)
		{
			CollapseValue = null;
			CollapseDescriptorAction = null;
			return Assign(descriptor, (a, v) => a.CollapseDescriptor = v);
		}

		public SearchRequestDescriptor<T> Collapse(Action<Elastic.Clients.Elasticsearch.FieldCollapseDescriptor<T>> configure)
		{
			CollapseValue = null;
			CollapseDescriptorAction = null;
			return Assign(configure, (a, v) => a.CollapseDescriptorAction = v);
		}

		public SearchRequestDescriptor<T> Highlight(Elastic.Clients.Elasticsearch.Highlight? highlight)
		{
			HighlightDescriptor = null;
			HighlightDescriptorAction = null;
			return Assign(highlight, (a, v) => a.HighlightValue = v);
		}

		public SearchRequestDescriptor<T> Highlight(Elastic.Clients.Elasticsearch.HighlightDescriptor<T> descriptor)
		{
			HighlightValue = null;
			HighlightDescriptorAction = null;
			return Assign(descriptor, (a, v) => a.HighlightDescriptor = v);
		}

		public SearchRequestDescriptor<T> Highlight(Action<Elastic.Clients.Elasticsearch.HighlightDescriptor<T>> configure)
		{
			HighlightValue = null;
			HighlightDescriptorAction = null;
			return Assign(configure, (a, v) => a.HighlightDescriptorAction = v);
		}

		public SearchRequestDescriptor<T> IndicesBoost(IEnumerable<Dictionary<Elastic.Clients.Elasticsearch.IndexName, double>>? indicesBoost) => Assign(indicesBoost, (a, v) => a.IndicesBoostValue = v);
		public SearchRequestDescriptor<T> MinScore(double? minScore) => Assign(minScore, (a, v) => a.MinScoreValue = v);
		public SearchRequestDescriptor<T> PostFilter(Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer? postFilter)
		{
			PostFilterDescriptor = null;
			PostFilterDescriptorAction = null;
			return Assign(postFilter, (a, v) => a.PostFilterValue = v);
		}

		public SearchRequestDescriptor<T> PostFilter(Elastic.Clients.Elasticsearch.QueryDsl.QueryContainerDescriptor<T> descriptor)
		{
			PostFilterValue = null;
			PostFilterDescriptorAction = null;
			return Assign(descriptor, (a, v) => a.PostFilterDescriptor = v);
		}

		public SearchRequestDescriptor<T> PostFilter(Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryContainerDescriptor<T>> configure)
		{
			PostFilterValue = null;
			PostFilterDescriptorAction = null;
			return Assign(configure, (a, v) => a.PostFilterDescriptorAction = v);
		}

		public SearchRequestDescriptor<T> Profile(bool? profile = true) => Assign(profile, (a, v) => a.ProfileValue = v);
		public SearchRequestDescriptor<T> Query(Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer? query)
		{
			QueryDescriptor = null;
			QueryDescriptorAction = null;
			return Assign(query, (a, v) => a.QueryValue = v);
		}

		public SearchRequestDescriptor<T> Query(Elastic.Clients.Elasticsearch.QueryDsl.QueryContainerDescriptor<T> descriptor)
		{
			QueryValue = null;
			QueryDescriptorAction = null;
			return Assign(descriptor, (a, v) => a.QueryDescriptor = v);
		}

		public SearchRequestDescriptor<T> Query(Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryContainerDescriptor<T>> configure)
		{
			QueryValue = null;
			QueryDescriptorAction = null;
			return Assign(configure, (a, v) => a.QueryDescriptorAction = v);
		}

		public SearchRequestDescriptor<T> Rescore(Elastic.Clients.Elasticsearch.Rescore? rescore)
		{
			RescoreDescriptor = null;
			RescoreDescriptorAction = null;
			return Assign(rescore, (a, v) => a.RescoreValue = v);
		}

		public SearchRequestDescriptor<T> Rescore(Elastic.Clients.Elasticsearch.RescoreDescriptor<T> descriptor)
		{
			RescoreValue = null;
			RescoreDescriptorAction = null;
			return Assign(descriptor, (a, v) => a.RescoreDescriptor = v);
		}

		public SearchRequestDescriptor<T> Rescore(Action<Elastic.Clients.Elasticsearch.RescoreDescriptor<T>> configure)
		{
			RescoreValue = null;
			RescoreDescriptorAction = null;
			return Assign(configure, (a, v) => a.RescoreDescriptorAction = v);
		}

		public SearchRequestDescriptor<T> ScriptFields(Func<FluentDictionary<string?, Elastic.Clients.Elasticsearch.ScriptField?>, FluentDictionary<string?, Elastic.Clients.Elasticsearch.ScriptField?>> selector) => Assign(selector, (a, v) => a.ScriptFieldsValue = v?.Invoke(new FluentDictionary<string?, Elastic.Clients.Elasticsearch.ScriptField?>()));
		public SearchRequestDescriptor<T> SearchAfter(IEnumerable<object>? searchAfter) => Assign(searchAfter, (a, v) => a.SearchAfterValue = v);
		public SearchRequestDescriptor<T> Slice(Elastic.Clients.Elasticsearch.SlicedScroll? slice)
		{
			SliceDescriptor = null;
			SliceDescriptorAction = null;
			return Assign(slice, (a, v) => a.SliceValue = v);
		}

		public SearchRequestDescriptor<T> Slice(Elastic.Clients.Elasticsearch.SlicedScrollDescriptor<T> descriptor)
		{
			SliceValue = null;
			SliceDescriptorAction = null;
			return Assign(descriptor, (a, v) => a.SliceDescriptor = v);
		}

		public SearchRequestDescriptor<T> Slice(Action<Elastic.Clients.Elasticsearch.SlicedScrollDescriptor<T>> configure)
		{
			SliceValue = null;
			SliceDescriptorAction = null;
			return Assign(configure, (a, v) => a.SliceDescriptorAction = v);
		}

		public SearchRequestDescriptor<T> Fields(IEnumerable<Elastic.Clients.Elasticsearch.QueryDsl.FieldAndFormat>? fields) => Assign(fields, (a, v) => a.FieldsValue = v);
		public SearchRequestDescriptor<T> Suggest(Elastic.Clients.Elasticsearch.Suggester? suggest)
		{
			SuggestDescriptor = null;
			SuggestDescriptorAction = null;
			return Assign(suggest, (a, v) => a.SuggestValue = v);
		}

		public SearchRequestDescriptor<T> Suggest(Elastic.Clients.Elasticsearch.SuggesterDescriptor descriptor)
		{
			SuggestValue = null;
			SuggestDescriptorAction = null;
			return Assign(descriptor, (a, v) => a.SuggestDescriptor = v);
		}

		public SearchRequestDescriptor<T> Suggest(Action<Elastic.Clients.Elasticsearch.SuggesterDescriptor> configure)
		{
			SuggestValue = null;
			SuggestDescriptorAction = null;
			return Assign(configure, (a, v) => a.SuggestDescriptorAction = v);
		}

		public SearchRequestDescriptor<T> Pit(Elastic.Clients.Elasticsearch.PointInTimeReference? pit)
		{
			PitDescriptor = null;
			PitDescriptorAction = null;
			return Assign(pit, (a, v) => a.PitValue = v);
		}

		public SearchRequestDescriptor<T> Pit(Elastic.Clients.Elasticsearch.PointInTimeReferenceDescriptor descriptor)
		{
			PitValue = null;
			PitDescriptorAction = null;
			return Assign(descriptor, (a, v) => a.PitDescriptor = v);
		}

		public SearchRequestDescriptor<T> Pit(Action<Elastic.Clients.Elasticsearch.PointInTimeReferenceDescriptor> configure)
		{
			PitValue = null;
			PitDescriptorAction = null;
			return Assign(configure, (a, v) => a.PitDescriptorAction = v);
		}

		public SearchRequestDescriptor<T> RuntimeMappings(Dictionary<string, Elastic.Clients.Elasticsearch.Mapping.RuntimeField>? runtimeMappings) => Assign(runtimeMappings, (a, v) => a.RuntimeMappingsValue = v);
		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (AggregationsValue is not null)
			{
				writer.WritePropertyName("aggregations");
				JsonSerializer.Serialize(writer, AggregationsValue, options);
			}

			if (CollapseDescriptor is not null)
			{
				writer.WritePropertyName("collapse");
				JsonSerializer.Serialize(writer, CollapseDescriptor, options);
			}
			else if (CollapseDescriptorAction is not null)
			{
				writer.WritePropertyName("collapse");
				JsonSerializer.Serialize(writer, new FieldCollapseDescriptor<T>(CollapseDescriptorAction), options);
			}
			else if (CollapseValue is not null)
			{
				writer.WritePropertyName("collapse");
				JsonSerializer.Serialize(writer, CollapseValue, options);
			}

			if (HighlightDescriptor is not null)
			{
				writer.WritePropertyName("highlight");
				JsonSerializer.Serialize(writer, HighlightDescriptor, options);
			}
			else if (HighlightDescriptorAction is not null)
			{
				writer.WritePropertyName("highlight");
				JsonSerializer.Serialize(writer, new HighlightDescriptor<T>(HighlightDescriptorAction), options);
			}
			else if (HighlightValue is not null)
			{
				writer.WritePropertyName("highlight");
				JsonSerializer.Serialize(writer, HighlightValue, options);
			}

			if (IndicesBoostValue is not null)
			{
				writer.WritePropertyName("indices_boost");
				JsonSerializer.Serialize(writer, IndicesBoostValue, options);
			}

			if (MinScoreValue.HasValue)
			{
				writer.WritePropertyName("min_score");
				writer.WriteNumberValue(MinScoreValue.Value);
			}

			if (PostFilterDescriptor is not null)
			{
				writer.WritePropertyName("post_filter");
				JsonSerializer.Serialize(writer, PostFilterDescriptor, options);
			}
			else if (PostFilterDescriptorAction is not null)
			{
				writer.WritePropertyName("post_filter");
				JsonSerializer.Serialize(writer, new QueryDsl.QueryContainerDescriptor<T>(PostFilterDescriptorAction), options);
			}
			else if (PostFilterValue is not null)
			{
				writer.WritePropertyName("post_filter");
				JsonSerializer.Serialize(writer, PostFilterValue, options);
			}

			if (ProfileValue.HasValue)
			{
				writer.WritePropertyName("profile");
				writer.WriteBooleanValue(ProfileValue.Value);
			}

			if (QueryDescriptor is not null)
			{
				writer.WritePropertyName("query");
				JsonSerializer.Serialize(writer, QueryDescriptor, options);
			}
			else if (QueryDescriptorAction is not null)
			{
				writer.WritePropertyName("query");
				JsonSerializer.Serialize(writer, new QueryDsl.QueryContainerDescriptor<T>(QueryDescriptorAction), options);
			}
			else if (QueryValue is not null)
			{
				writer.WritePropertyName("query");
				JsonSerializer.Serialize(writer, QueryValue, options);
			}

			if (RescoreDescriptor is not null)
			{
				writer.WritePropertyName("rescore");
				JsonSerializer.Serialize(writer, RescoreDescriptor, options);
			}
			else if (RescoreDescriptorAction is not null)
			{
				writer.WritePropertyName("rescore");
				JsonSerializer.Serialize(writer, new RescoreDescriptor<T>(RescoreDescriptorAction), options);
			}
			else if (RescoreValue is not null)
			{
				writer.WritePropertyName("rescore");
				JsonSerializer.Serialize(writer, RescoreValue, options);
			}

			if (ScriptFieldsValue is not null)
			{
				writer.WritePropertyName("script_fields");
				JsonSerializer.Serialize(writer, ScriptFieldsValue, options);
			}

			if (SearchAfterValue is not null)
			{
				writer.WritePropertyName("search_after");
				JsonSerializer.Serialize(writer, SearchAfterValue, options);
			}

			if (SliceDescriptor is not null)
			{
				writer.WritePropertyName("slice");
				JsonSerializer.Serialize(writer, SliceDescriptor, options);
			}
			else if (SliceDescriptorAction is not null)
			{
				writer.WritePropertyName("slice");
				JsonSerializer.Serialize(writer, new SlicedScrollDescriptor<T>(SliceDescriptorAction), options);
			}
			else if (SliceValue is not null)
			{
				writer.WritePropertyName("slice");
				JsonSerializer.Serialize(writer, SliceValue, options);
			}

			if (FieldsValue is not null)
			{
				writer.WritePropertyName("fields");
				JsonSerializer.Serialize(writer, FieldsValue, options);
			}

			if (SuggestDescriptor is not null)
			{
				writer.WritePropertyName("suggest");
				JsonSerializer.Serialize(writer, SuggestDescriptor, options);
			}
			else if (SuggestDescriptorAction is not null)
			{
				writer.WritePropertyName("suggest");
				JsonSerializer.Serialize(writer, new SuggesterDescriptor(SuggestDescriptorAction), options);
			}
			else if (SuggestValue is not null)
			{
				writer.WritePropertyName("suggest");
				JsonSerializer.Serialize(writer, SuggestValue, options);
			}

			if (PitDescriptor is not null)
			{
				writer.WritePropertyName("pit");
				JsonSerializer.Serialize(writer, PitDescriptor, options);
			}
			else if (PitDescriptorAction is not null)
			{
				writer.WritePropertyName("pit");
				JsonSerializer.Serialize(writer, new PointInTimeReferenceDescriptor(PitDescriptorAction), options);
			}
			else if (PitValue is not null)
			{
				writer.WritePropertyName("pit");
				JsonSerializer.Serialize(writer, PitValue, options);
			}

			if (RuntimeMappingsValue is not null)
			{
				writer.WritePropertyName("runtime_mappings");
				JsonSerializer.Serialize(writer, RuntimeMappingsValue, options);
			}

			writer.WriteEndObject();
		}
	}
}