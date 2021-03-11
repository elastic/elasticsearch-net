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
// Run the following in the root of the repository:
//
// TODO - RUN INSTRUCTIONS
//
// ------------------------------------------------
using System;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Transport;

#nullable restore
namespace Nest
{
    public class BulkRequestParameters : RequestParameters<BulkRequestParameters>
    {
        public string? Pipeline { get => Q<string?>("pipeline"); set => Q("pipeline", value); }

        public Refresh? Refresh { get => Q<Refresh?>("refresh"); set => Q("refresh", value); }

        public Routing? Routing { get => Q<Routing?>("routing"); set => Q("routing", value); }

        public bool? Source { get => Q<bool?>("_source"); set => Q("_source", value); }

        public Fields? SourceExcludes { get => Q<Fields?>("_source_excludes"); set => Q("_source_excludes", value); }

        public Fields? SourceIncludes { get => Q<Fields?>("_source_includes"); set => Q("_source_includes", value); }

        public Time? Timeout { get => Q<Time?>("timeout"); set => Q("timeout", value); }

        public string? TypeQueryString { get => Q<string?>("type_query_string"); set => Q("type_query_string", value); }

        public string? WaitForActiveShards { get => Q<string?>("wait_for_active_shards"); set => Q("wait_for_active_shards", value); }

        public bool? RequireAlias { get => Q<bool?>("require_alias"); set => Q("require_alias", value); }
    }

    public class ClearScrollRequestParameters : RequestParameters<ClearScrollRequestParameters>
    {
    }

    public class CountRequestParameters : RequestParameters<CountRequestParameters>
    {
        public bool? AllowNoIndices { get => Q<bool?>("allow_no_indices"); set => Q("allow_no_indices", value); }

        public string? Analyzer { get => Q<string?>("analyzer"); set => Q("analyzer", value); }

        public bool? AnalyzeWildcard { get => Q<bool?>("analyze_wildcard"); set => Q("analyze_wildcard", value); }

        public DefaultOperator? DefaultOperator { get => Q<DefaultOperator?>("default_operator"); set => Q("default_operator", value); }

        public string? Df { get => Q<string?>("df"); set => Q("df", value); }

        public ExpandWildcards? ExpandWildcards { get => Q<ExpandWildcards?>("expand_wildcards"); set => Q("expand_wildcards", value); }

        public bool? IgnoreThrottled { get => Q<bool?>("ignore_throttled"); set => Q("ignore_throttled", value); }

        public bool? IgnoreUnavailable { get => Q<bool?>("ignore_unavailable"); set => Q("ignore_unavailable", value); }

        public bool? Lenient { get => Q<bool?>("lenient"); set => Q("lenient", value); }

        public double? MinScore { get => Q<double?>("min_score"); set => Q("min_score", value); }

        public string? Preference { get => Q<string?>("preference"); set => Q("preference", value); }

        public string? QueryOnQueryString { get => Q<string?>("query_on_query_string"); set => Q("query_on_query_string", value); }

        public Routing? Routing { get => Q<Routing?>("routing"); set => Q("routing", value); }

        public long? TerminateAfter { get => Q<long?>("terminate_after"); set => Q("terminate_after", value); }

        public string? QueryLuceneSyntax { get => Q<string?>("q"); set => Q("q", value); }
    }

    public class CreateRequestParameters : RequestParameters<CreateRequestParameters>
    {
        public string? Pipeline { get => Q<string?>("pipeline"); set => Q("pipeline", value); }

        public Refresh? Refresh { get => Q<Refresh?>("refresh"); set => Q("refresh", value); }

        public Routing? Routing { get => Q<Routing?>("routing"); set => Q("routing", value); }

        public Time? Timeout { get => Q<Time?>("timeout"); set => Q("timeout", value); }

        public long? Version { get => Q<long?>("version"); set => Q("version", value); }

        public VersionType? VersionType { get => Q<VersionType?>("version_type"); set => Q("version_type", value); }

        public string? WaitForActiveShards { get => Q<string?>("wait_for_active_shards"); set => Q("wait_for_active_shards", value); }
    }

    public class DeleteRequestParameters : RequestParameters<DeleteRequestParameters>
    {
        public long? IfPrimaryTerm { get => Q<long?>("if_primary_term"); set => Q("if_primary_term", value); }

        public long? IfSeqNo { get => Q<long?>("if_seq_no"); set => Q("if_seq_no", value); }

        public Refresh? Refresh { get => Q<Refresh?>("refresh"); set => Q("refresh", value); }

        public Routing? Routing { get => Q<Routing?>("routing"); set => Q("routing", value); }

        public Time? Timeout { get => Q<Time?>("timeout"); set => Q("timeout", value); }

        public long? Version { get => Q<long?>("version"); set => Q("version", value); }

        public VersionType? VersionType { get => Q<VersionType?>("version_type"); set => Q("version_type", value); }

        public string? WaitForActiveShards { get => Q<string?>("wait_for_active_shards"); set => Q("wait_for_active_shards", value); }
    }

    public class DeleteByQueryRequestParameters : RequestParameters<DeleteByQueryRequestParameters>
    {
        public bool? AllowNoIndices { get => Q<bool?>("allow_no_indices"); set => Q("allow_no_indices", value); }

        public string? Analyzer { get => Q<string?>("analyzer"); set => Q("analyzer", value); }

        public bool? AnalyzeWildcard { get => Q<bool?>("analyze_wildcard"); set => Q("analyze_wildcard", value); }

        public Conflicts? Conflicts { get => Q<Conflicts?>("conflicts"); set => Q("conflicts", value); }

        public DefaultOperator? DefaultOperator { get => Q<DefaultOperator?>("default_operator"); set => Q("default_operator", value); }

        public string? Df { get => Q<string?>("df"); set => Q("df", value); }

        public ExpandWildcards? ExpandWildcards { get => Q<ExpandWildcards?>("expand_wildcards"); set => Q("expand_wildcards", value); }

        public long? From { get => Q<long?>("from"); set => Q("from", value); }

        public bool? IgnoreUnavailable { get => Q<bool?>("ignore_unavailable"); set => Q("ignore_unavailable", value); }

        public bool? Lenient { get => Q<bool?>("lenient"); set => Q("lenient", value); }

        public string? Preference { get => Q<string?>("preference"); set => Q("preference", value); }

        public string? QueryOnQueryString { get => Q<string?>("query_on_query_string"); set => Q("query_on_query_string", value); }

        public bool? Refresh { get => Q<bool?>("refresh"); set => Q("refresh", value); }

        public bool? RequestCache { get => Q<bool?>("request_cache"); set => Q("request_cache", value); }

        public long? RequestsPerSecond { get => Q<long?>("requests_per_second"); set => Q("requests_per_second", value); }

        public Routing? Routing { get => Q<Routing?>("routing"); set => Q("routing", value); }

        public Time? Scroll { get => Q<Time?>("scroll"); set => Q("scroll", value); }

        public long? ScrollSize { get => Q<long?>("scroll_size"); set => Q("scroll_size", value); }

        public Time? SearchTimeout { get => Q<Time?>("search_timeout"); set => Q("search_timeout", value); }

        public SearchType? SearchType { get => Q<SearchType?>("search_type"); set => Q("search_type", value); }

        public long? Size { get => Q<long?>("size"); set => Q("size", value); }

        public long? Slices { get => Q<long?>("slices"); set => Q("slices", value); }

        public bool? SourceEnabled { get => Q<bool?>("source_enabled"); set => Q("source_enabled", value); }

        public Fields? SourceExcludes { get => Q<Fields?>("source_excludes"); set => Q("source_excludes", value); }

        public Fields? SourceIncludes { get => Q<Fields?>("source_includes"); set => Q("source_includes", value); }

        public long? TerminateAfter { get => Q<long?>("terminate_after"); set => Q("terminate_after", value); }

        public Time? Timeout { get => Q<Time?>("timeout"); set => Q("timeout", value); }

        public bool? Version { get => Q<bool?>("version"); set => Q("version", value); }

        public string? WaitForActiveShards { get => Q<string?>("wait_for_active_shards"); set => Q("wait_for_active_shards", value); }

        public bool? WaitForCompletion { get => Q<bool?>("wait_for_completion"); set => Q("wait_for_completion", value); }
    }

    public class DeleteByQueryRethrottleRequestParameters : RequestParameters<DeleteByQueryRethrottleRequestParameters>
    {
        public long? RequestsPerSecond { get => Q<long?>("requests_per_second"); set => Q("requests_per_second", value); }
    }

    public class DeleteScriptRequestParameters : RequestParameters<DeleteScriptRequestParameters>
    {
        public Time? MasterTimeout { get => Q<Time?>("master_timeout"); set => Q("master_timeout", value); }

        public Time? Timeout { get => Q<Time?>("timeout"); set => Q("timeout", value); }
    }

    public class DocumentExistsRequestParameters : RequestParameters<DocumentExistsRequestParameters>
    {
        public string? Preference { get => Q<string?>("preference"); set => Q("preference", value); }

        public bool? Realtime { get => Q<bool?>("realtime"); set => Q("realtime", value); }

        public bool? Refresh { get => Q<bool?>("refresh"); set => Q("refresh", value); }

        public Routing? Routing { get => Q<Routing?>("routing"); set => Q("routing", value); }

        public bool? SourceEnabled { get => Q<bool?>("source_enabled"); set => Q("source_enabled", value); }

        public Fields? SourceExcludes { get => Q<Fields?>("source_excludes"); set => Q("source_excludes", value); }

        public Fields? SourceIncludes { get => Q<Fields?>("source_includes"); set => Q("source_includes", value); }

        public Fields? StoredFields { get => Q<Fields?>("stored_fields"); set => Q("stored_fields", value); }

        public long? Version { get => Q<long?>("version"); set => Q("version", value); }

        public VersionType? VersionType { get => Q<VersionType?>("version_type"); set => Q("version_type", value); }
    }

    public class SourceExistsRequestParameters : RequestParameters<SourceExistsRequestParameters>
    {
        public string? Preference { get => Q<string?>("preference"); set => Q("preference", value); }

        public bool? Realtime { get => Q<bool?>("realtime"); set => Q("realtime", value); }

        public bool? Refresh { get => Q<bool?>("refresh"); set => Q("refresh", value); }

        public Routing? Routing { get => Q<Routing?>("routing"); set => Q("routing", value); }

        public bool? SourceEnabled { get => Q<bool?>("source_enabled"); set => Q("source_enabled", value); }

        public Fields? SourceExcludes { get => Q<Fields?>("source_excludes"); set => Q("source_excludes", value); }

        public Fields? SourceIncludes { get => Q<Fields?>("source_includes"); set => Q("source_includes", value); }

        public long? Version { get => Q<long?>("version"); set => Q("version", value); }

        public VersionType? VersionType { get => Q<VersionType?>("version_type"); set => Q("version_type", value); }
    }

    public class ExplainRequestParameters : RequestParameters<ExplainRequestParameters>
    {
        public string? Analyzer { get => Q<string?>("analyzer"); set => Q("analyzer", value); }

        public bool? AnalyzeWildcard { get => Q<bool?>("analyze_wildcard"); set => Q("analyze_wildcard", value); }

        public DefaultOperator? DefaultOperator { get => Q<DefaultOperator?>("default_operator"); set => Q("default_operator", value); }

        public string? Df { get => Q<string?>("df"); set => Q("df", value); }

        public bool? Lenient { get => Q<bool?>("lenient"); set => Q("lenient", value); }

        public string? Preference { get => Q<string?>("preference"); set => Q("preference", value); }

        public string? QueryOnQueryString { get => Q<string?>("query_on_query_string"); set => Q("query_on_query_string", value); }

        public Routing? Routing { get => Q<Routing?>("routing"); set => Q("routing", value); }

        public Fields? SourceExcludes { get => Q<Fields?>("_source_excludes"); set => Q("_source_excludes", value); }

        public Fields? SourceIncludes { get => Q<Fields?>("_source_includes"); set => Q("_source_includes", value); }

        public Fields? StoredFields { get => Q<Fields?>("stored_fields"); set => Q("stored_fields", value); }

        public string? QueryLuceneSyntax { get => Q<string?>("q"); set => Q("q", value); }
    }

    public class FieldCapabilitiesRequestParameters : RequestParameters<FieldCapabilitiesRequestParameters>
    {
        public bool? AllowNoIndices { get => Q<bool?>("allow_no_indices"); set => Q("allow_no_indices", value); }

        public ExpandWildcards? ExpandWildcards { get => Q<ExpandWildcards?>("expand_wildcards"); set => Q("expand_wildcards", value); }

        public Fields? Fields { get => Q<Fields?>("fields"); set => Q("fields", value); }

        public bool? IgnoreUnavailable { get => Q<bool?>("ignore_unavailable"); set => Q("ignore_unavailable", value); }

        public bool? IncludeUnmapped { get => Q<bool?>("include_unmapped"); set => Q("include_unmapped", value); }
    }

    public class GetRequestParameters : RequestParameters<GetRequestParameters>
    {
        public string? Preference { get => Q<string?>("preference"); set => Q("preference", value); }

        public bool? Realtime { get => Q<bool?>("realtime"); set => Q("realtime", value); }

        public bool? Refresh { get => Q<bool?>("refresh"); set => Q("refresh", value); }

        public Routing? Routing { get => Q<Routing?>("routing"); set => Q("routing", value); }

        public bool? SourceEnabled { get => Q<bool?>("source_enabled"); set => Q("source_enabled", value); }

        public Fields? SourceExcludes { get => Q<Fields?>("_source_excludes"); set => Q("_source_excludes", value); }

        public Fields? SourceIncludes { get => Q<Fields?>("_source_includes"); set => Q("_source_includes", value); }

        public Fields? StoredFields { get => Q<Fields?>("stored_fields"); set => Q("stored_fields", value); }

        public long? Version { get => Q<long?>("version"); set => Q("version", value); }

        public VersionType? VersionType { get => Q<VersionType?>("version_type"); set => Q("version_type", value); }
    }

    public class GetScriptRequestParameters : RequestParameters<GetScriptRequestParameters>
    {
        public Time? MasterTimeout { get => Q<Time?>("master_timeout"); set => Q("master_timeout", value); }
    }

    public class SourceRequestParameters : RequestParameters<SourceRequestParameters>
    {
        public string? Preference { get => Q<string?>("preference"); set => Q("preference", value); }

        public bool? Realtime { get => Q<bool?>("realtime"); set => Q("realtime", value); }

        public bool? Refresh { get => Q<bool?>("refresh"); set => Q("refresh", value); }

        public Routing? Routing { get => Q<Routing?>("routing"); set => Q("routing", value); }

        public bool? SourceEnabled { get => Q<bool?>("source_enabled"); set => Q("source_enabled", value); }

        public Fields? SourceExcludes { get => Q<Fields?>("_source_excludes"); set => Q("_source_excludes", value); }

        public Fields? SourceIncludes { get => Q<Fields?>("_source_includes"); set => Q("_source_includes", value); }

        public long? Version { get => Q<long?>("version"); set => Q("version", value); }

        public VersionType? VersionType { get => Q<VersionType?>("version_type"); set => Q("version_type", value); }
    }

    public class IndexRequestParameters : RequestParameters<IndexRequestParameters>
    {
        public long? IfPrimaryTerm { get => Q<long?>("if_primary_term"); set => Q("if_primary_term", value); }

        public long? IfSeqNo { get => Q<long?>("if_seq_no"); set => Q("if_seq_no", value); }

        public OpType? OpType { get => Q<OpType?>("op_type"); set => Q("op_type", value); }

        public string? Pipeline { get => Q<string?>("pipeline"); set => Q("pipeline", value); }

        public Refresh? Refresh { get => Q<Refresh?>("refresh"); set => Q("refresh", value); }

        public Routing? Routing { get => Q<Routing?>("routing"); set => Q("routing", value); }

        public Time? Timeout { get => Q<Time?>("timeout"); set => Q("timeout", value); }

        public long? Version { get => Q<long?>("version"); set => Q("version", value); }

        public VersionType? VersionType { get => Q<VersionType?>("version_type"); set => Q("version_type", value); }

        public string? WaitForActiveShards { get => Q<string?>("wait_for_active_shards"); set => Q("wait_for_active_shards", value); }

        public bool? RequireAlias { get => Q<bool?>("require_alias"); set => Q("require_alias", value); }
    }

    public class RootNodeInfoRequestParameters : RequestParameters<RootNodeInfoRequestParameters>
    {
    }

    public class MultiGetRequestParameters : RequestParameters<MultiGetRequestParameters>
    {
        public string? Preference { get => Q<string?>("preference"); set => Q("preference", value); }

        public bool? Realtime { get => Q<bool?>("realtime"); set => Q("realtime", value); }

        public bool? Refresh { get => Q<bool?>("refresh"); set => Q("refresh", value); }

        public Routing? Routing { get => Q<Routing?>("routing"); set => Q("routing", value); }

        public bool? SourceEnabled { get => Q<bool?>("source_enabled"); set => Q("source_enabled", value); }

        public Fields? SourceExcludes { get => Q<Fields?>("_source_excludes"); set => Q("_source_excludes", value); }

        public Fields? SourceIncludes { get => Q<Fields?>("_source_includes"); set => Q("_source_includes", value); }

        public Fields? StoredFields { get => Q<Fields?>("stored_fields"); set => Q("stored_fields", value); }
    }

    public class MultiSearchRequestParameters : RequestParameters<MultiSearchRequestParameters>
    {
        public bool? CcsMinimizeRoundtrips { get => Q<bool?>("ccs_minimize_roundtrips"); set => Q("ccs_minimize_roundtrips", value); }

        public long? MaxConcurrentSearches { get => Q<long?>("max_concurrent_searches"); set => Q("max_concurrent_searches", value); }

        public long? MaxConcurrentShardRequests { get => Q<long?>("max_concurrent_shard_requests"); set => Q("max_concurrent_shard_requests", value); }

        public long? PreFilterShardSize { get => Q<long?>("pre_filter_shard_size"); set => Q("pre_filter_shard_size", value); }

        public SearchType? SearchType { get => Q<SearchType?>("search_type"); set => Q("search_type", value); }

        public bool? TotalHitsAsInteger { get => Q<bool?>("total_hits_as_integer"); set => Q("total_hits_as_integer", value); }

        public bool? TypedKeys { get => Q<bool?>("typed_keys"); set => Q("typed_keys", value); }
    }

    public class MultiTermVectorsRequestParameters : RequestParameters<MultiTermVectorsRequestParameters>
    {
        public Fields? Fields { get => Q<Fields?>("fields"); set => Q("fields", value); }

        public bool? FieldStatistics { get => Q<bool?>("field_statistics"); set => Q("field_statistics", value); }

        public bool? Offsets { get => Q<bool?>("offsets"); set => Q("offsets", value); }

        public bool? Payloads { get => Q<bool?>("payloads"); set => Q("payloads", value); }

        public bool? Positions { get => Q<bool?>("positions"); set => Q("positions", value); }

        public string? Preference { get => Q<string?>("preference"); set => Q("preference", value); }

        public bool? Realtime { get => Q<bool?>("realtime"); set => Q("realtime", value); }

        public Routing? Routing { get => Q<Routing?>("routing"); set => Q("routing", value); }

        public bool? TermStatistics { get => Q<bool?>("term_statistics"); set => Q("term_statistics", value); }

        public long? Version { get => Q<long?>("version"); set => Q("version", value); }

        public VersionType? VersionType { get => Q<VersionType?>("version_type"); set => Q("version_type", value); }
    }

    public class PingRequestParameters : RequestParameters<PingRequestParameters>
    {
    }

    public class PutScriptRequestParameters : RequestParameters<PutScriptRequestParameters>
    {
        public Time? MasterTimeout { get => Q<Time?>("master_timeout"); set => Q("master_timeout", value); }

        public Time? Timeout { get => Q<Time?>("timeout"); set => Q("timeout", value); }
    }

    public class ReindexRequestParameters : RequestParameters<ReindexRequestParameters>
    {
        public bool? Refresh { get => Q<bool?>("refresh"); set => Q("refresh", value); }

        public long? RequestsPerSecond { get => Q<long?>("requests_per_second"); set => Q("requests_per_second", value); }

        public Time? Scroll { get => Q<Time?>("scroll"); set => Q("scroll", value); }

        public long? Slices { get => Q<long?>("slices"); set => Q("slices", value); }

        public Time? Timeout { get => Q<Time?>("timeout"); set => Q("timeout", value); }

        public string? WaitForActiveShards { get => Q<string?>("wait_for_active_shards"); set => Q("wait_for_active_shards", value); }

        public bool? WaitForCompletion { get => Q<bool?>("wait_for_completion"); set => Q("wait_for_completion", value); }

        public bool? RequireAlias { get => Q<bool?>("require_alias"); set => Q("require_alias", value); }
    }

    public class ReindexRethrottleRequestParameters : RequestParameters<ReindexRethrottleRequestParameters>
    {
        public long? RequestsPerSecond { get => Q<long?>("requests_per_second"); set => Q("requests_per_second", value); }
    }

    public class RenderSearchTemplateRequestParameters : RequestParameters<RenderSearchTemplateRequestParameters>
    {
    }

    public class ExecutePainlessScriptRequestParameters : RequestParameters<ExecutePainlessScriptRequestParameters>
    {
    }

    public class ScrollRequestParameters : RequestParameters<ScrollRequestParameters>
    {
        public Time? Scroll { get => Q<Time?>("scroll"); set => Q("scroll", value); }

        public ScrollId? ScrollId { get => Q<ScrollId?>("scroll_id"); set => Q("scroll_id", value); }

        public bool? RestTotalHitsAsInt { get => Q<bool?>("rest_total_hits_as_int"); set => Q("rest_total_hits_as_int", value); }

        public bool? TotalHitsAsInteger { get => Q<bool?>("total_hits_as_integer"); set => Q("total_hits_as_integer", value); }
    }

    public class SearchRequestParameters : RequestParameters<SearchRequestParameters>
    {
        public bool? AllowNoIndices { get => Q<bool?>("allow_no_indices"); set => Q("allow_no_indices", value); }

        public bool? AllowPartialSearchResults { get => Q<bool?>("allow_partial_search_results"); set => Q("allow_partial_search_results", value); }

        public string? Analyzer { get => Q<string?>("analyzer"); set => Q("analyzer", value); }

        public bool? AnalyzeWildcard { get => Q<bool?>("analyze_wildcard"); set => Q("analyze_wildcard", value); }

        public long? BatchedReduceSize { get => Q<long?>("batched_reduce_size"); set => Q("batched_reduce_size", value); }

        public bool? CcsMinimizeRoundtrips { get => Q<bool?>("ccs_minimize_roundtrips"); set => Q("ccs_minimize_roundtrips", value); }

        public DefaultOperator? DefaultOperator { get => Q<DefaultOperator?>("default_operator"); set => Q("default_operator", value); }

        public string? Df { get => Q<string?>("df"); set => Q("df", value); }

        public Fields? DocvalueFields { get => Q<Fields?>("docvalue_fields"); set => Q("docvalue_fields", value); }

        public ExpandWildcards? ExpandWildcards { get => Q<ExpandWildcards?>("expand_wildcards"); set => Q("expand_wildcards", value); }

        public bool? IgnoreThrottled { get => Q<bool?>("ignore_throttled"); set => Q("ignore_throttled", value); }

        public bool? IgnoreUnavailable { get => Q<bool?>("ignore_unavailable"); set => Q("ignore_unavailable", value); }

        public bool? Lenient { get => Q<bool?>("lenient"); set => Q("lenient", value); }

        public long? MaxConcurrentShardRequests { get => Q<long?>("max_concurrent_shard_requests"); set => Q("max_concurrent_shard_requests", value); }

        public string? Preference { get => Q<string?>("preference"); set => Q("preference", value); }

        public long? PreFilterShardSize { get => Q<long?>("pre_filter_shard_size"); set => Q("pre_filter_shard_size", value); }

        public string? QueryOnQueryString { get => Q<string?>("query_on_query_string"); set => Q("query_on_query_string", value); }

        public bool? RequestCache { get => Q<bool?>("request_cache"); set => Q("request_cache", value); }

        public Routing? Routing { get => Q<Routing?>("routing"); set => Q("routing", value); }

        public Time? Scroll { get => Q<Time?>("scroll"); set => Q("scroll", value); }

        public SearchType? SearchType { get => Q<SearchType?>("search_type"); set => Q("search_type", value); }

        public bool? SequenceNumberPrimaryTerm { get => Q<bool?>("sequence_number_primary_term"); set => Q("sequence_number_primary_term", value); }

        public Fields? StoredFields { get => Q<Fields?>("stored_fields"); set => Q("stored_fields", value); }

        public Field? SuggestField { get => Q<Field?>("suggest_field"); set => Q("suggest_field", value); }

        public SuggestMode? SuggestMode { get => Q<SuggestMode?>("suggest_mode"); set => Q("suggest_mode", value); }

        public long? SuggestSize { get => Q<long?>("suggest_size"); set => Q("suggest_size", value); }

        public string? SuggestText { get => Q<string?>("suggest_text"); set => Q("suggest_text", value); }

        public bool? TotalHitsAsInteger { get => Q<bool?>("total_hits_as_integer"); set => Q("total_hits_as_integer", value); }

        public bool? TypedKeys { get => Q<bool?>("typed_keys"); set => Q("typed_keys", value); }

        public bool? RestTotalHitsAsInt { get => Q<bool?>("rest_total_hits_as_int"); set => Q("rest_total_hits_as_int", value); }

        public Fields? SourceExcludes { get => Q<Fields?>("_source_excludes"); set => Q("_source_excludes", value); }

        public Fields? SourceIncludes { get => Q<Fields?>("_source_includes"); set => Q("_source_includes", value); }

        public bool? SeqNoPrimaryTerm { get => Q<bool?>("seq_no_primary_term"); set => Q("seq_no_primary_term", value); }

        public string? QueryLuceneSyntax { get => Q<string?>("q"); set => Q("q", value); }

        public int? Size { get => Q<int?>("size"); set => Q("size", value); }

        public int? From { get => Q<int?>("from"); set => Q("from", value); }
    }

    public class SearchShardsRequestParameters : RequestParameters<SearchShardsRequestParameters>
    {
        public bool? AllowNoIndices { get => Q<bool?>("allow_no_indices"); set => Q("allow_no_indices", value); }

        public ExpandWildcards? ExpandWildcards { get => Q<ExpandWildcards?>("expand_wildcards"); set => Q("expand_wildcards", value); }

        public bool? IgnoreUnavailable { get => Q<bool?>("ignore_unavailable"); set => Q("ignore_unavailable", value); }

        public bool? Local { get => Q<bool?>("local"); set => Q("local", value); }

        public string? Preference { get => Q<string?>("preference"); set => Q("preference", value); }

        public Routing? Routing { get => Q<Routing?>("routing"); set => Q("routing", value); }
    }

    public class SearchTemplateRequestParameters : RequestParameters<SearchTemplateRequestParameters>
    {
        public bool? AllowNoIndices { get => Q<bool?>("allow_no_indices"); set => Q("allow_no_indices", value); }

        public bool? CcsMinimizeRoundtrips { get => Q<bool?>("ccs_minimize_roundtrips"); set => Q("ccs_minimize_roundtrips", value); }

        public ExpandWildcards? ExpandWildcards { get => Q<ExpandWildcards?>("expand_wildcards"); set => Q("expand_wildcards", value); }

        public bool? Explain { get => Q<bool?>("explain"); set => Q("explain", value); }

        public bool? IgnoreThrottled { get => Q<bool?>("ignore_throttled"); set => Q("ignore_throttled", value); }

        public bool? IgnoreUnavailable { get => Q<bool?>("ignore_unavailable"); set => Q("ignore_unavailable", value); }

        public string? Preference { get => Q<string?>("preference"); set => Q("preference", value); }

        public bool? Profile { get => Q<bool?>("profile"); set => Q("profile", value); }

        public Routing? Routing { get => Q<Routing?>("routing"); set => Q("routing", value); }

        public Time? Scroll { get => Q<Time?>("scroll"); set => Q("scroll", value); }

        public SearchType? SearchType { get => Q<SearchType?>("search_type"); set => Q("search_type", value); }

        public bool? TotalHitsAsInteger { get => Q<bool?>("total_hits_as_integer"); set => Q("total_hits_as_integer", value); }

        public bool? TypedKeys { get => Q<bool?>("typed_keys"); set => Q("typed_keys", value); }
    }

    public class TermVectorsRequestParameters : RequestParameters<TermVectorsRequestParameters>
    {
        public Fields? Fields { get => Q<Fields?>("fields"); set => Q("fields", value); }

        public bool? FieldStatistics { get => Q<bool?>("field_statistics"); set => Q("field_statistics", value); }

        public bool? Offsets { get => Q<bool?>("offsets"); set => Q("offsets", value); }

        public bool? Payloads { get => Q<bool?>("payloads"); set => Q("payloads", value); }

        public bool? Positions { get => Q<bool?>("positions"); set => Q("positions", value); }

        public string? Preference { get => Q<string?>("preference"); set => Q("preference", value); }

        public bool? Realtime { get => Q<bool?>("realtime"); set => Q("realtime", value); }

        public Routing? Routing { get => Q<Routing?>("routing"); set => Q("routing", value); }

        public bool? TermStatistics { get => Q<bool?>("term_statistics"); set => Q("term_statistics", value); }

        public long? Version { get => Q<long?>("version"); set => Q("version", value); }

        public VersionType? VersionType { get => Q<VersionType?>("version_type"); set => Q("version_type", value); }
    }

    public class UpdateRequestParameters : RequestParameters<UpdateRequestParameters>
    {
        public long? IfPrimaryTerm { get => Q<long?>("if_primary_term"); set => Q("if_primary_term", value); }

        public long? IfSeqNo { get => Q<long?>("if_seq_no"); set => Q("if_seq_no", value); }

        public string? Lang { get => Q<string?>("lang"); set => Q("lang", value); }

        public Refresh? Refresh { get => Q<Refresh?>("refresh"); set => Q("refresh", value); }

        public bool? RequireAlias { get => Q<bool?>("require_alias"); set => Q("require_alias", value); }

        public long? RetryOnConflict { get => Q<long?>("retry_on_conflict"); set => Q("retry_on_conflict", value); }

        public Routing? Routing { get => Q<Routing?>("routing"); set => Q("routing", value); }

        public bool? SourceEnabled { get => Q<bool?>("source_enabled"); set => Q("source_enabled", value); }

        public Time? Timeout { get => Q<Time?>("timeout"); set => Q("timeout", value); }

        public string? WaitForActiveShards { get => Q<string?>("wait_for_active_shards"); set => Q("wait_for_active_shards", value); }

        public Fields? SourceExcludes { get => Q<Fields?>("_source_excludes"); set => Q("_source_excludes", value); }

        public Fields? SourceIncludes { get => Q<Fields?>("_source_includes"); set => Q("_source_includes", value); }
    }

    public class UpdateByQueryRequestParameters : RequestParameters<UpdateByQueryRequestParameters>
    {
        public bool? AllowNoIndices { get => Q<bool?>("allow_no_indices"); set => Q("allow_no_indices", value); }

        public string? Analyzer { get => Q<string?>("analyzer"); set => Q("analyzer", value); }

        public bool? AnalyzeWildcard { get => Q<bool?>("analyze_wildcard"); set => Q("analyze_wildcard", value); }

        public Conflicts? Conflicts { get => Q<Conflicts?>("conflicts"); set => Q("conflicts", value); }

        public DefaultOperator? DefaultOperator { get => Q<DefaultOperator?>("default_operator"); set => Q("default_operator", value); }

        public string? Df { get => Q<string?>("df"); set => Q("df", value); }

        public ExpandWildcards? ExpandWildcards { get => Q<ExpandWildcards?>("expand_wildcards"); set => Q("expand_wildcards", value); }

        public long? From { get => Q<long?>("from"); set => Q("from", value); }

        public bool? IgnoreUnavailable { get => Q<bool?>("ignore_unavailable"); set => Q("ignore_unavailable", value); }

        public bool? Lenient { get => Q<bool?>("lenient"); set => Q("lenient", value); }

        public string? Pipeline { get => Q<string?>("pipeline"); set => Q("pipeline", value); }

        public string? Preference { get => Q<string?>("preference"); set => Q("preference", value); }

        public string? QueryOnQueryString { get => Q<string?>("query_on_query_string"); set => Q("query_on_query_string", value); }

        public bool? Refresh { get => Q<bool?>("refresh"); set => Q("refresh", value); }

        public bool? RequestCache { get => Q<bool?>("request_cache"); set => Q("request_cache", value); }

        public long? RequestsPerSecond { get => Q<long?>("requests_per_second"); set => Q("requests_per_second", value); }

        public Routing? Routing { get => Q<Routing?>("routing"); set => Q("routing", value); }

        public Time? Scroll { get => Q<Time?>("scroll"); set => Q("scroll", value); }

        public long? ScrollSize { get => Q<long?>("scroll_size"); set => Q("scroll_size", value); }

        public Time? SearchTimeout { get => Q<Time?>("search_timeout"); set => Q("search_timeout", value); }

        public SearchType? SearchType { get => Q<SearchType?>("search_type"); set => Q("search_type", value); }

        public long? Size { get => Q<long?>("size"); set => Q("size", value); }

        public long? Slices { get => Q<long?>("slices"); set => Q("slices", value); }

        public bool? SourceEnabled { get => Q<bool?>("source_enabled"); set => Q("source_enabled", value); }

        public Fields? SourceExcludes { get => Q<Fields?>("source_excludes"); set => Q("source_excludes", value); }

        public Fields? SourceIncludes { get => Q<Fields?>("source_includes"); set => Q("source_includes", value); }

        public long? TerminateAfter { get => Q<long?>("terminate_after"); set => Q("terminate_after", value); }

        public Time? Timeout { get => Q<Time?>("timeout"); set => Q("timeout", value); }

        public bool? Version { get => Q<bool?>("version"); set => Q("version", value); }

        public bool? VersionType { get => Q<bool?>("version_type"); set => Q("version_type", value); }

        public string? WaitForActiveShards { get => Q<string?>("wait_for_active_shards"); set => Q("wait_for_active_shards", value); }

        public bool? WaitForCompletion { get => Q<bool?>("wait_for_completion"); set => Q("wait_for_completion", value); }
    }
}