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
using Elastic.Clients.Elasticsearch.Requests;
using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch;

public sealed partial class DeleteByQueryRequestParameters : RequestParameters
{
	/// <summary>
	/// <para>If `false`, the request returns an error if any wildcard expression, index alias, or `_all` value targets only missing or closed indices.<br/>This behavior applies even if the request targets other open indices.<br/>For example, a request targeting `foo*,bar*` returns an error if an index starts with `foo` but no index starts with `bar`.</para>
	/// </summary>
	public bool? AllowNoIndices { get => Q<bool?>("allow_no_indices"); set => Q("allow_no_indices", value); }

	/// <summary>
	/// <para>Analyzer to use for the query string.</para>
	/// </summary>
	public string? Analyzer { get => Q<string?>("analyzer"); set => Q("analyzer", value); }

	/// <summary>
	/// <para>If `true`, wildcard and prefix queries are analyzed.</para>
	/// </summary>
	public bool? AnalyzeWildcard { get => Q<bool?>("analyze_wildcard"); set => Q("analyze_wildcard", value); }

	/// <summary>
	/// <para>What to do if delete by query hits version conflicts: `abort` or `proceed`.</para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Conflicts? Conflicts { get => Q<Elastic.Clients.Elasticsearch.Conflicts?>("conflicts"); set => Q("conflicts", value); }

	/// <summary>
	/// <para>The default operator for query string query: `AND` or `OR`.</para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.Operator? DefaultOperator { get => Q<Elastic.Clients.Elasticsearch.QueryDsl.Operator?>("default_operator"); set => Q("default_operator", value); }

	/// <summary>
	/// <para>Field to use as default where no field prefix is given in the query string.</para>
	/// </summary>
	public string? Df { get => Q<string?>("df"); set => Q("df", value); }

	/// <summary>
	/// <para>Type of index that wildcard patterns can match.<br/>If the request can target data streams, this argument determines whether wildcard expressions match hidden data streams.<br/>Supports comma-separated values, such as `open,hidden`. Valid values are: `all`, `open`, `closed`, `hidden`, `none`.</para>
	/// </summary>
	public ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? ExpandWildcards { get => Q<ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>?>("expand_wildcards"); set => Q("expand_wildcards", value); }

	/// <summary>
	/// <para>Starting offset (default: 0)</para>
	/// </summary>
	public long? From { get => Q<long?>("from"); set => Q("from", value); }

	/// <summary>
	/// <para>If `false`, the request returns an error if it targets a missing or closed index.</para>
	/// </summary>
	public bool? IgnoreUnavailable { get => Q<bool?>("ignore_unavailable"); set => Q("ignore_unavailable", value); }

	/// <summary>
	/// <para>If `true`, format-based query failures (such as providing text to a numeric field) in the query string will be ignored.</para>
	/// </summary>
	public bool? Lenient { get => Q<bool?>("lenient"); set => Q("lenient", value); }

	/// <summary>
	/// <para>Specifies the node or shard the operation should be performed on.<br/>Random by default.</para>
	/// </summary>
	public string? Preference { get => Q<string?>("preference"); set => Q("preference", value); }

	/// <summary>
	/// <para>Query in the Lucene query string syntax.</para>
	/// </summary>
	public string? QueryLuceneSyntax { get => Q<string?>("q"); set => Q("q", value); }

	/// <summary>
	/// <para>If `true`, Elasticsearch refreshes all shards involved in the delete by query after the request completes.</para>
	/// </summary>
	public bool? Refresh { get => Q<bool?>("refresh"); set => Q("refresh", value); }

	/// <summary>
	/// <para>If `true`, the request cache is used for this request.<br/>Defaults to the index-level setting.</para>
	/// </summary>
	public bool? RequestCache { get => Q<bool?>("request_cache"); set => Q("request_cache", value); }

	/// <summary>
	/// <para>The throttle for this request in sub-requests per second.</para>
	/// </summary>
	public float? RequestsPerSecond { get => Q<float?>("requests_per_second"); set => Q("requests_per_second", value); }

	/// <summary>
	/// <para>Custom value used to route operations to a specific shard.</para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Routing? Routing { get => Q<Elastic.Clients.Elasticsearch.Routing?>("routing"); set => Q("routing", value); }

	/// <summary>
	/// <para>Period to retain the search context for scrolling.</para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? Scroll { get => Q<Elastic.Clients.Elasticsearch.Duration?>("scroll"); set => Q("scroll", value); }

	/// <summary>
	/// <para>Size of the scroll request that powers the operation.</para>
	/// </summary>
	public long? ScrollSize { get => Q<long?>("scroll_size"); set => Q("scroll_size", value); }

	/// <summary>
	/// <para>Explicit timeout for each search request.<br/>Defaults to no timeout.</para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? SearchTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("search_timeout"); set => Q("search_timeout", value); }

	/// <summary>
	/// <para>The type of the search operation.<br/>Available options: `query_then_fetch`, `dfs_query_then_fetch`.</para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.SearchType? SearchType { get => Q<Elastic.Clients.Elasticsearch.SearchType?>("search_type"); set => Q("search_type", value); }

	/// <summary>
	/// <para>The number of slices this task should be divided into.</para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Slices? Slices { get => Q<Elastic.Clients.Elasticsearch.Slices?>("slices"); set => Q("slices", value); }

	/// <summary>
	/// <para>A comma-separated list of <field>:<direction> pairs.</para>
	/// </summary>
	public ICollection<string>? Sort { get => Q<ICollection<string>?>("sort"); set => Q("sort", value); }

	/// <summary>
	/// <para>Specific `tag` of the request for logging and statistical purposes.</para>
	/// </summary>
	public ICollection<string>? Stats { get => Q<ICollection<string>?>("stats"); set => Q("stats", value); }

	/// <summary>
	/// <para>Maximum number of documents to collect for each shard.<br/>If a query reaches this limit, Elasticsearch terminates the query early.<br/>Elasticsearch collects documents before sorting.<br/>Use with caution.<br/>Elasticsearch applies this parameter to each shard handling the request.<br/>When possible, let Elasticsearch perform early termination automatically.<br/>Avoid specifying this parameter for requests that target data streams with backing indices across multiple data tiers.</para>
	/// </summary>
	public long? TerminateAfter { get => Q<long?>("terminate_after"); set => Q("terminate_after", value); }

	/// <summary>
	/// <para>Period each deletion request waits for active shards.</para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }

	/// <summary>
	/// <para>If `true`, returns the document version as part of a hit.</para>
	/// </summary>
	public bool? Version { get => Q<bool?>("version"); set => Q("version", value); }

	/// <summary>
	/// <para>The number of shard copies that must be active before proceeding with the operation.<br/>Set to all or any positive integer up to the total number of shards in the index (`number_of_replicas+1`).</para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.WaitForActiveShards? WaitForActiveShards { get => Q<Elastic.Clients.Elasticsearch.WaitForActiveShards?>("wait_for_active_shards"); set => Q("wait_for_active_shards", value); }

	/// <summary>
	/// <para>If `true`, the request blocks until the operation is complete.</para>
	/// </summary>
	public bool? WaitForCompletion { get => Q<bool?>("wait_for_completion"); set => Q("wait_for_completion", value); }
}

/// <summary>
/// <para>Deletes documents that match the specified query.</para>
/// </summary>
public sealed partial class DeleteByQueryRequest : PlainRequest<DeleteByQueryRequestParameters>
{
	public DeleteByQueryRequest(Elastic.Clients.Elasticsearch.Indices indices) : base(r => r.Required("index", indices))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.NoNamespaceDeleteByQuery;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "delete_by_query";

	/// <summary>
	/// <para>If `false`, the request returns an error if any wildcard expression, index alias, or `_all` value targets only missing or closed indices.<br/>This behavior applies even if the request targets other open indices.<br/>For example, a request targeting `foo*,bar*` returns an error if an index starts with `foo` but no index starts with `bar`.</para>
	/// </summary>
	[JsonIgnore]
	public bool? AllowNoIndices { get => Q<bool?>("allow_no_indices"); set => Q("allow_no_indices", value); }

	/// <summary>
	/// <para>Analyzer to use for the query string.</para>
	/// </summary>
	[JsonIgnore]
	public string? Analyzer { get => Q<string?>("analyzer"); set => Q("analyzer", value); }

	/// <summary>
	/// <para>If `true`, wildcard and prefix queries are analyzed.</para>
	/// </summary>
	[JsonIgnore]
	public bool? AnalyzeWildcard { get => Q<bool?>("analyze_wildcard"); set => Q("analyze_wildcard", value); }

	/// <summary>
	/// <para>What to do if delete by query hits version conflicts: `abort` or `proceed`.</para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Conflicts? Conflicts { get => Q<Elastic.Clients.Elasticsearch.Conflicts?>("conflicts"); set => Q("conflicts", value); }

	/// <summary>
	/// <para>The default operator for query string query: `AND` or `OR`.</para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.QueryDsl.Operator? DefaultOperator { get => Q<Elastic.Clients.Elasticsearch.QueryDsl.Operator?>("default_operator"); set => Q("default_operator", value); }

	/// <summary>
	/// <para>Field to use as default where no field prefix is given in the query string.</para>
	/// </summary>
	[JsonIgnore]
	public string? Df { get => Q<string?>("df"); set => Q("df", value); }

	/// <summary>
	/// <para>Type of index that wildcard patterns can match.<br/>If the request can target data streams, this argument determines whether wildcard expressions match hidden data streams.<br/>Supports comma-separated values, such as `open,hidden`. Valid values are: `all`, `open`, `closed`, `hidden`, `none`.</para>
	/// </summary>
	[JsonIgnore]
	public ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? ExpandWildcards { get => Q<ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>?>("expand_wildcards"); set => Q("expand_wildcards", value); }

	/// <summary>
	/// <para>Starting offset (default: 0)</para>
	/// </summary>
	[JsonIgnore]
	public long? From { get => Q<long?>("from"); set => Q("from", value); }

	/// <summary>
	/// <para>If `false`, the request returns an error if it targets a missing or closed index.</para>
	/// </summary>
	[JsonIgnore]
	public bool? IgnoreUnavailable { get => Q<bool?>("ignore_unavailable"); set => Q("ignore_unavailable", value); }

	/// <summary>
	/// <para>If `true`, format-based query failures (such as providing text to a numeric field) in the query string will be ignored.</para>
	/// </summary>
	[JsonIgnore]
	public bool? Lenient { get => Q<bool?>("lenient"); set => Q("lenient", value); }

	/// <summary>
	/// <para>Specifies the node or shard the operation should be performed on.<br/>Random by default.</para>
	/// </summary>
	[JsonIgnore]
	public string? Preference { get => Q<string?>("preference"); set => Q("preference", value); }

	/// <summary>
	/// <para>Query in the Lucene query string syntax.</para>
	/// </summary>
	[JsonIgnore]
	public string? QueryLuceneSyntax { get => Q<string?>("q"); set => Q("q", value); }

	/// <summary>
	/// <para>If `true`, Elasticsearch refreshes all shards involved in the delete by query after the request completes.</para>
	/// </summary>
	[JsonIgnore]
	public bool? Refresh { get => Q<bool?>("refresh"); set => Q("refresh", value); }

	/// <summary>
	/// <para>If `true`, the request cache is used for this request.<br/>Defaults to the index-level setting.</para>
	/// </summary>
	[JsonIgnore]
	public bool? RequestCache { get => Q<bool?>("request_cache"); set => Q("request_cache", value); }

	/// <summary>
	/// <para>The throttle for this request in sub-requests per second.</para>
	/// </summary>
	[JsonIgnore]
	public float? RequestsPerSecond { get => Q<float?>("requests_per_second"); set => Q("requests_per_second", value); }

	/// <summary>
	/// <para>Custom value used to route operations to a specific shard.</para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Routing? Routing { get => Q<Elastic.Clients.Elasticsearch.Routing?>("routing"); set => Q("routing", value); }

	/// <summary>
	/// <para>Period to retain the search context for scrolling.</para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Duration? Scroll { get => Q<Elastic.Clients.Elasticsearch.Duration?>("scroll"); set => Q("scroll", value); }

	/// <summary>
	/// <para>Size of the scroll request that powers the operation.</para>
	/// </summary>
	[JsonIgnore]
	public long? ScrollSize { get => Q<long?>("scroll_size"); set => Q("scroll_size", value); }

	/// <summary>
	/// <para>Explicit timeout for each search request.<br/>Defaults to no timeout.</para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Duration? SearchTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("search_timeout"); set => Q("search_timeout", value); }

	/// <summary>
	/// <para>The type of the search operation.<br/>Available options: `query_then_fetch`, `dfs_query_then_fetch`.</para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.SearchType? SearchType { get => Q<Elastic.Clients.Elasticsearch.SearchType?>("search_type"); set => Q("search_type", value); }

	/// <summary>
	/// <para>The number of slices this task should be divided into.</para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Slices? Slices { get => Q<Elastic.Clients.Elasticsearch.Slices?>("slices"); set => Q("slices", value); }

	/// <summary>
	/// <para>A comma-separated list of <field>:<direction> pairs.</para>
	/// </summary>
	[JsonIgnore]
	public ICollection<string>? Sort { get => Q<ICollection<string>?>("sort"); set => Q("sort", value); }

	/// <summary>
	/// <para>Specific `tag` of the request for logging and statistical purposes.</para>
	/// </summary>
	[JsonIgnore]
	public ICollection<string>? Stats { get => Q<ICollection<string>?>("stats"); set => Q("stats", value); }

	/// <summary>
	/// <para>Maximum number of documents to collect for each shard.<br/>If a query reaches this limit, Elasticsearch terminates the query early.<br/>Elasticsearch collects documents before sorting.<br/>Use with caution.<br/>Elasticsearch applies this parameter to each shard handling the request.<br/>When possible, let Elasticsearch perform early termination automatically.<br/>Avoid specifying this parameter for requests that target data streams with backing indices across multiple data tiers.</para>
	/// </summary>
	[JsonIgnore]
	public long? TerminateAfter { get => Q<long?>("terminate_after"); set => Q("terminate_after", value); }

	/// <summary>
	/// <para>Period each deletion request waits for active shards.</para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }

	/// <summary>
	/// <para>If `true`, returns the document version as part of a hit.</para>
	/// </summary>
	[JsonIgnore]
	public bool? Version { get => Q<bool?>("version"); set => Q("version", value); }

	/// <summary>
	/// <para>The number of shard copies that must be active before proceeding with the operation.<br/>Set to all or any positive integer up to the total number of shards in the index (`number_of_replicas+1`).</para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.WaitForActiveShards? WaitForActiveShards { get => Q<Elastic.Clients.Elasticsearch.WaitForActiveShards?>("wait_for_active_shards"); set => Q("wait_for_active_shards", value); }

	/// <summary>
	/// <para>If `true`, the request blocks until the operation is complete.</para>
	/// </summary>
	[JsonIgnore]
	public bool? WaitForCompletion { get => Q<bool?>("wait_for_completion"); set => Q("wait_for_completion", value); }

	/// <summary>
	/// <para>The maximum number of documents to delete.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("max_docs")]
	public long? MaxDocs { get; set; }

	/// <summary>
	/// <para>Specifies the documents to delete using the Query DSL.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("query")]
	public Elastic.Clients.Elasticsearch.QueryDsl.Query? Query { get; set; }

	/// <summary>
	/// <para>Slice the request manually using the provided slice ID and total number of slices.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("slice")]
	public Elastic.Clients.Elasticsearch.SlicedScroll? Slice { get; set; }
}

/// <summary>
/// <para>Deletes documents that match the specified query.</para>
/// </summary>
public sealed partial class DeleteByQueryRequestDescriptor<TDocument> : RequestDescriptor<DeleteByQueryRequestDescriptor<TDocument>, DeleteByQueryRequestParameters>
{
	internal DeleteByQueryRequestDescriptor(Action<DeleteByQueryRequestDescriptor<TDocument>> configure) => configure.Invoke(this);

	public DeleteByQueryRequestDescriptor(Elastic.Clients.Elasticsearch.Indices indices) : base(r => r.Required("index", indices))
	{
	}

	internal DeleteByQueryRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.NoNamespaceDeleteByQuery;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "delete_by_query";

	public DeleteByQueryRequestDescriptor<TDocument> AllowNoIndices(bool? allowNoIndices = true) => Qs("allow_no_indices", allowNoIndices);
	public DeleteByQueryRequestDescriptor<TDocument> Analyzer(string? analyzer) => Qs("analyzer", analyzer);
	public DeleteByQueryRequestDescriptor<TDocument> AnalyzeWildcard(bool? analyzeWildcard = true) => Qs("analyze_wildcard", analyzeWildcard);
	public DeleteByQueryRequestDescriptor<TDocument> Conflicts(Elastic.Clients.Elasticsearch.Conflicts? conflicts) => Qs("conflicts", conflicts);
	public DeleteByQueryRequestDescriptor<TDocument> DefaultOperator(Elastic.Clients.Elasticsearch.QueryDsl.Operator? defaultOperator) => Qs("default_operator", defaultOperator);
	public DeleteByQueryRequestDescriptor<TDocument> Df(string? df) => Qs("df", df);
	public DeleteByQueryRequestDescriptor<TDocument> ExpandWildcards(ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? expandWildcards) => Qs("expand_wildcards", expandWildcards);
	public DeleteByQueryRequestDescriptor<TDocument> From(long? from) => Qs("from", from);
	public DeleteByQueryRequestDescriptor<TDocument> IgnoreUnavailable(bool? ignoreUnavailable = true) => Qs("ignore_unavailable", ignoreUnavailable);
	public DeleteByQueryRequestDescriptor<TDocument> Lenient(bool? lenient = true) => Qs("lenient", lenient);
	public DeleteByQueryRequestDescriptor<TDocument> Preference(string? preference) => Qs("preference", preference);
	public DeleteByQueryRequestDescriptor<TDocument> QueryLuceneSyntax(string? queryLuceneSyntax) => Qs("q", queryLuceneSyntax);
	public DeleteByQueryRequestDescriptor<TDocument> Refresh(bool? refresh = true) => Qs("refresh", refresh);
	public DeleteByQueryRequestDescriptor<TDocument> RequestCache(bool? requestCache = true) => Qs("request_cache", requestCache);
	public DeleteByQueryRequestDescriptor<TDocument> RequestsPerSecond(float? requestsPerSecond) => Qs("requests_per_second", requestsPerSecond);
	public DeleteByQueryRequestDescriptor<TDocument> Routing(Elastic.Clients.Elasticsearch.Routing? routing) => Qs("routing", routing);
	public DeleteByQueryRequestDescriptor<TDocument> Scroll(Elastic.Clients.Elasticsearch.Duration? scroll) => Qs("scroll", scroll);
	public DeleteByQueryRequestDescriptor<TDocument> ScrollSize(long? scrollSize) => Qs("scroll_size", scrollSize);
	public DeleteByQueryRequestDescriptor<TDocument> SearchTimeout(Elastic.Clients.Elasticsearch.Duration? searchTimeout) => Qs("search_timeout", searchTimeout);
	public DeleteByQueryRequestDescriptor<TDocument> SearchType(Elastic.Clients.Elasticsearch.SearchType? searchType) => Qs("search_type", searchType);
	public DeleteByQueryRequestDescriptor<TDocument> Slices(Elastic.Clients.Elasticsearch.Slices? slices) => Qs("slices", slices);
	public DeleteByQueryRequestDescriptor<TDocument> Sort(ICollection<string>? sort) => Qs("sort", sort);
	public DeleteByQueryRequestDescriptor<TDocument> Stats(ICollection<string>? stats) => Qs("stats", stats);
	public DeleteByQueryRequestDescriptor<TDocument> TerminateAfter(long? terminateAfter) => Qs("terminate_after", terminateAfter);
	public DeleteByQueryRequestDescriptor<TDocument> Timeout(Elastic.Clients.Elasticsearch.Duration? timeout) => Qs("timeout", timeout);
	public DeleteByQueryRequestDescriptor<TDocument> Version(bool? version = true) => Qs("version", version);
	public DeleteByQueryRequestDescriptor<TDocument> WaitForActiveShards(Elastic.Clients.Elasticsearch.WaitForActiveShards? waitForActiveShards) => Qs("wait_for_active_shards", waitForActiveShards);
	public DeleteByQueryRequestDescriptor<TDocument> WaitForCompletion(bool? waitForCompletion = true) => Qs("wait_for_completion", waitForCompletion);

	public DeleteByQueryRequestDescriptor<TDocument> Indices(Elastic.Clients.Elasticsearch.Indices indices)
	{
		RouteValues.Required("index", indices);
		return Self;
	}

	private long? MaxDocsValue { get; set; }
	private Elastic.Clients.Elasticsearch.QueryDsl.Query? QueryValue { get; set; }
	private QueryDsl.QueryDescriptor<TDocument> QueryDescriptor { get; set; }
	private Action<QueryDsl.QueryDescriptor<TDocument>> QueryDescriptorAction { get; set; }
	private Elastic.Clients.Elasticsearch.SlicedScroll? SliceValue { get; set; }
	private SlicedScrollDescriptor<TDocument> SliceDescriptor { get; set; }
	private Action<SlicedScrollDescriptor<TDocument>> SliceDescriptorAction { get; set; }

	/// <summary>
	/// <para>The maximum number of documents to delete.</para>
	/// </summary>
	public DeleteByQueryRequestDescriptor<TDocument> MaxDocs(long? maxDocs)
	{
		MaxDocsValue = maxDocs;
		return Self;
	}

	/// <summary>
	/// <para>Specifies the documents to delete using the Query DSL.</para>
	/// </summary>
	public DeleteByQueryRequestDescriptor<TDocument> Query(Elastic.Clients.Elasticsearch.QueryDsl.Query? query)
	{
		QueryDescriptor = null;
		QueryDescriptorAction = null;
		QueryValue = query;
		return Self;
	}

	public DeleteByQueryRequestDescriptor<TDocument> Query(QueryDsl.QueryDescriptor<TDocument> descriptor)
	{
		QueryValue = null;
		QueryDescriptorAction = null;
		QueryDescriptor = descriptor;
		return Self;
	}

	public DeleteByQueryRequestDescriptor<TDocument> Query(Action<QueryDsl.QueryDescriptor<TDocument>> configure)
	{
		QueryValue = null;
		QueryDescriptor = null;
		QueryDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>Slice the request manually using the provided slice ID and total number of slices.</para>
	/// </summary>
	public DeleteByQueryRequestDescriptor<TDocument> Slice(Elastic.Clients.Elasticsearch.SlicedScroll? slice)
	{
		SliceDescriptor = null;
		SliceDescriptorAction = null;
		SliceValue = slice;
		return Self;
	}

	public DeleteByQueryRequestDescriptor<TDocument> Slice(SlicedScrollDescriptor<TDocument> descriptor)
	{
		SliceValue = null;
		SliceDescriptorAction = null;
		SliceDescriptor = descriptor;
		return Self;
	}

	public DeleteByQueryRequestDescriptor<TDocument> Slice(Action<SlicedScrollDescriptor<TDocument>> configure)
	{
		SliceValue = null;
		SliceDescriptor = null;
		SliceDescriptorAction = configure;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (MaxDocsValue.HasValue)
		{
			writer.WritePropertyName("max_docs");
			writer.WriteNumberValue(MaxDocsValue.Value);
		}

		if (QueryDescriptor is not null)
		{
			writer.WritePropertyName("query");
			JsonSerializer.Serialize(writer, QueryDescriptor, options);
		}
		else if (QueryDescriptorAction is not null)
		{
			writer.WritePropertyName("query");
			JsonSerializer.Serialize(writer, new QueryDsl.QueryDescriptor<TDocument>(QueryDescriptorAction), options);
		}
		else if (QueryValue is not null)
		{
			writer.WritePropertyName("query");
			JsonSerializer.Serialize(writer, QueryValue, options);
		}

		if (SliceDescriptor is not null)
		{
			writer.WritePropertyName("slice");
			JsonSerializer.Serialize(writer, SliceDescriptor, options);
		}
		else if (SliceDescriptorAction is not null)
		{
			writer.WritePropertyName("slice");
			JsonSerializer.Serialize(writer, new SlicedScrollDescriptor<TDocument>(SliceDescriptorAction), options);
		}
		else if (SliceValue is not null)
		{
			writer.WritePropertyName("slice");
			JsonSerializer.Serialize(writer, SliceValue, options);
		}

		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>Deletes documents that match the specified query.</para>
/// </summary>
public sealed partial class DeleteByQueryRequestDescriptor : RequestDescriptor<DeleteByQueryRequestDescriptor, DeleteByQueryRequestParameters>
{
	internal DeleteByQueryRequestDescriptor(Action<DeleteByQueryRequestDescriptor> configure) => configure.Invoke(this);

	public DeleteByQueryRequestDescriptor(Elastic.Clients.Elasticsearch.Indices indices) : base(r => r.Required("index", indices))
	{
	}

	internal DeleteByQueryRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.NoNamespaceDeleteByQuery;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "delete_by_query";

	public DeleteByQueryRequestDescriptor AllowNoIndices(bool? allowNoIndices = true) => Qs("allow_no_indices", allowNoIndices);
	public DeleteByQueryRequestDescriptor Analyzer(string? analyzer) => Qs("analyzer", analyzer);
	public DeleteByQueryRequestDescriptor AnalyzeWildcard(bool? analyzeWildcard = true) => Qs("analyze_wildcard", analyzeWildcard);
	public DeleteByQueryRequestDescriptor Conflicts(Elastic.Clients.Elasticsearch.Conflicts? conflicts) => Qs("conflicts", conflicts);
	public DeleteByQueryRequestDescriptor DefaultOperator(Elastic.Clients.Elasticsearch.QueryDsl.Operator? defaultOperator) => Qs("default_operator", defaultOperator);
	public DeleteByQueryRequestDescriptor Df(string? df) => Qs("df", df);
	public DeleteByQueryRequestDescriptor ExpandWildcards(ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? expandWildcards) => Qs("expand_wildcards", expandWildcards);
	public DeleteByQueryRequestDescriptor From(long? from) => Qs("from", from);
	public DeleteByQueryRequestDescriptor IgnoreUnavailable(bool? ignoreUnavailable = true) => Qs("ignore_unavailable", ignoreUnavailable);
	public DeleteByQueryRequestDescriptor Lenient(bool? lenient = true) => Qs("lenient", lenient);
	public DeleteByQueryRequestDescriptor Preference(string? preference) => Qs("preference", preference);
	public DeleteByQueryRequestDescriptor QueryLuceneSyntax(string? queryLuceneSyntax) => Qs("q", queryLuceneSyntax);
	public DeleteByQueryRequestDescriptor Refresh(bool? refresh = true) => Qs("refresh", refresh);
	public DeleteByQueryRequestDescriptor RequestCache(bool? requestCache = true) => Qs("request_cache", requestCache);
	public DeleteByQueryRequestDescriptor RequestsPerSecond(float? requestsPerSecond) => Qs("requests_per_second", requestsPerSecond);
	public DeleteByQueryRequestDescriptor Routing(Elastic.Clients.Elasticsearch.Routing? routing) => Qs("routing", routing);
	public DeleteByQueryRequestDescriptor Scroll(Elastic.Clients.Elasticsearch.Duration? scroll) => Qs("scroll", scroll);
	public DeleteByQueryRequestDescriptor ScrollSize(long? scrollSize) => Qs("scroll_size", scrollSize);
	public DeleteByQueryRequestDescriptor SearchTimeout(Elastic.Clients.Elasticsearch.Duration? searchTimeout) => Qs("search_timeout", searchTimeout);
	public DeleteByQueryRequestDescriptor SearchType(Elastic.Clients.Elasticsearch.SearchType? searchType) => Qs("search_type", searchType);
	public DeleteByQueryRequestDescriptor Slices(Elastic.Clients.Elasticsearch.Slices? slices) => Qs("slices", slices);
	public DeleteByQueryRequestDescriptor Sort(ICollection<string>? sort) => Qs("sort", sort);
	public DeleteByQueryRequestDescriptor Stats(ICollection<string>? stats) => Qs("stats", stats);
	public DeleteByQueryRequestDescriptor TerminateAfter(long? terminateAfter) => Qs("terminate_after", terminateAfter);
	public DeleteByQueryRequestDescriptor Timeout(Elastic.Clients.Elasticsearch.Duration? timeout) => Qs("timeout", timeout);
	public DeleteByQueryRequestDescriptor Version(bool? version = true) => Qs("version", version);
	public DeleteByQueryRequestDescriptor WaitForActiveShards(Elastic.Clients.Elasticsearch.WaitForActiveShards? waitForActiveShards) => Qs("wait_for_active_shards", waitForActiveShards);
	public DeleteByQueryRequestDescriptor WaitForCompletion(bool? waitForCompletion = true) => Qs("wait_for_completion", waitForCompletion);

	public DeleteByQueryRequestDescriptor Indices(Elastic.Clients.Elasticsearch.Indices indices)
	{
		RouteValues.Required("index", indices);
		return Self;
	}

	private long? MaxDocsValue { get; set; }
	private Elastic.Clients.Elasticsearch.QueryDsl.Query? QueryValue { get; set; }
	private QueryDsl.QueryDescriptor QueryDescriptor { get; set; }
	private Action<QueryDsl.QueryDescriptor> QueryDescriptorAction { get; set; }
	private Elastic.Clients.Elasticsearch.SlicedScroll? SliceValue { get; set; }
	private SlicedScrollDescriptor SliceDescriptor { get; set; }
	private Action<SlicedScrollDescriptor> SliceDescriptorAction { get; set; }

	/// <summary>
	/// <para>The maximum number of documents to delete.</para>
	/// </summary>
	public DeleteByQueryRequestDescriptor MaxDocs(long? maxDocs)
	{
		MaxDocsValue = maxDocs;
		return Self;
	}

	/// <summary>
	/// <para>Specifies the documents to delete using the Query DSL.</para>
	/// </summary>
	public DeleteByQueryRequestDescriptor Query(Elastic.Clients.Elasticsearch.QueryDsl.Query? query)
	{
		QueryDescriptor = null;
		QueryDescriptorAction = null;
		QueryValue = query;
		return Self;
	}

	public DeleteByQueryRequestDescriptor Query(QueryDsl.QueryDescriptor descriptor)
	{
		QueryValue = null;
		QueryDescriptorAction = null;
		QueryDescriptor = descriptor;
		return Self;
	}

	public DeleteByQueryRequestDescriptor Query(Action<QueryDsl.QueryDescriptor> configure)
	{
		QueryValue = null;
		QueryDescriptor = null;
		QueryDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>Slice the request manually using the provided slice ID and total number of slices.</para>
	/// </summary>
	public DeleteByQueryRequestDescriptor Slice(Elastic.Clients.Elasticsearch.SlicedScroll? slice)
	{
		SliceDescriptor = null;
		SliceDescriptorAction = null;
		SliceValue = slice;
		return Self;
	}

	public DeleteByQueryRequestDescriptor Slice(SlicedScrollDescriptor descriptor)
	{
		SliceValue = null;
		SliceDescriptorAction = null;
		SliceDescriptor = descriptor;
		return Self;
	}

	public DeleteByQueryRequestDescriptor Slice(Action<SlicedScrollDescriptor> configure)
	{
		SliceValue = null;
		SliceDescriptor = null;
		SliceDescriptorAction = configure;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (MaxDocsValue.HasValue)
		{
			writer.WritePropertyName("max_docs");
			writer.WriteNumberValue(MaxDocsValue.Value);
		}

		if (QueryDescriptor is not null)
		{
			writer.WritePropertyName("query");
			JsonSerializer.Serialize(writer, QueryDescriptor, options);
		}
		else if (QueryDescriptorAction is not null)
		{
			writer.WritePropertyName("query");
			JsonSerializer.Serialize(writer, new QueryDsl.QueryDescriptor(QueryDescriptorAction), options);
		}
		else if (QueryValue is not null)
		{
			writer.WritePropertyName("query");
			JsonSerializer.Serialize(writer, QueryValue, options);
		}

		if (SliceDescriptor is not null)
		{
			writer.WritePropertyName("slice");
			JsonSerializer.Serialize(writer, SliceDescriptor, options);
		}
		else if (SliceDescriptorAction is not null)
		{
			writer.WritePropertyName("slice");
			JsonSerializer.Serialize(writer, new SlicedScrollDescriptor(SliceDescriptorAction), options);
		}
		else if (SliceValue is not null)
		{
			writer.WritePropertyName("slice");
			JsonSerializer.Serialize(writer, SliceValue, options);
		}

		writer.WriteEndObject();
	}
}