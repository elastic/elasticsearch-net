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
using Elastic.Transport.Extensions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Sql;

public sealed partial class QueryRequestParameters : RequestParameters
{
	/// <summary>
	/// <para>
	/// The format for the response.
	/// You can also specify a format using the <c>Accept</c> HTTP header.
	/// If you specify both this parameter and the <c>Accept</c> HTTP header, this parameter takes precedence.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Sql.SqlFormat? Format { get => Q<Elastic.Clients.Elasticsearch.Sql.SqlFormat?>("format"); set => Q("format", value); }
}

/// <summary>
/// <para>
/// Get SQL search results.
/// Run an SQL request.
/// </para>
/// </summary>
public sealed partial class QueryRequest : PlainRequest<QueryRequestParameters>
{
	internal override ApiUrls ApiUrls => ApiUrlLookup.SqlQuery;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "sql.query";

	/// <summary>
	/// <para>
	/// The format for the response.
	/// You can also specify a format using the <c>Accept</c> HTTP header.
	/// If you specify both this parameter and the <c>Accept</c> HTTP header, this parameter takes precedence.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Sql.SqlFormat? Format { get => Q<Elastic.Clients.Elasticsearch.Sql.SqlFormat?>("format"); set => Q("format", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, the response has partial results when there are shard request timeouts or shard failures.
	/// If <c>false</c>, the API returns an error with no partial results.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("allow_partial_search_results")]
	public bool? AllowPartialSearchResults { get; set; }

	/// <summary>
	/// <para>
	/// The default catalog (cluster) for queries.
	/// If unspecified, the queries execute on the data in the local cluster only.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("catalog")]
	public string? Catalog { get; set; }

	/// <summary>
	/// <para>
	/// If <c>true</c>, the results are in a columnar fashion: one row represents all the values of a certain column from the current page of results.
	/// The API supports this parameter only for CBOR, JSON, SMILE, and YAML responses.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("columnar")]
	public bool? Columnar { get; set; }

	/// <summary>
	/// <para>
	/// The cursor used to retrieve a set of paginated results.
	/// If you specify a cursor, the API only uses the <c>columnar</c> and <c>time_zone</c> request body parameters.
	/// It ignores other request body parameters.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("cursor")]
	public string? Cursor { get; set; }

	/// <summary>
	/// <para>
	/// The maximum number of rows (or entries) to return in one response.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("fetch_size")]
	public int? FetchSize { get; set; }

	/// <summary>
	/// <para>
	/// If <c>false</c>, the API returns an exception when encountering multiple values for a field.
	/// If <c>true</c>, the API is lenient and returns the first value from the array with no guarantee of consistent results.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("field_multi_value_leniency")]
	public bool? FieldMultiValueLeniency { get; set; }

	/// <summary>
	/// <para>
	/// The Elasticsearch query DSL for additional filtering.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("filter")]
	public Elastic.Clients.Elasticsearch.QueryDsl.Query? Filter { get; set; }

	/// <summary>
	/// <para>
	/// If <c>true</c>, the search can run on frozen indices.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("index_using_frozen")]
	public bool? IndexUsingFrozen { get; set; }

	/// <summary>
	/// <para>
	/// The retention period for an async or saved synchronous search.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("keep_alive")]
	public Elastic.Clients.Elasticsearch.Duration? KeepAlive { get; set; }

	/// <summary>
	/// <para>
	/// If <c>true</c>, Elasticsearch stores synchronous searches if you also specify the <c>wait_for_completion_timeout</c> parameter.
	/// If <c>false</c>, Elasticsearch only stores async searches that don't finish before the <c>wait_for_completion_timeout</c>.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("keep_on_completion")]
	public bool? KeepOnCompletion { get; set; }

	/// <summary>
	/// <para>
	/// The minimum retention period for the scroll cursor.
	/// After this time period, a pagination request might fail because the scroll cursor is no longer available.
	/// Subsequent scroll requests prolong the lifetime of the scroll cursor by the duration of <c>page_timeout</c> in the scroll request.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("page_timeout")]
	public Elastic.Clients.Elasticsearch.Duration? PageTimeout { get; set; }

	/// <summary>
	/// <para>
	/// The values for parameters in the query.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("params")]
	public IDictionary<string, object>? Params { get; set; }

	/// <summary>
	/// <para>
	/// The SQL query to run.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("query")]
	public string? Query { get; set; }

	/// <summary>
	/// <para>
	/// The timeout before the request fails.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("request_timeout")]
	public Elastic.Clients.Elasticsearch.Duration? RequestTimeout { get; set; }

	/// <summary>
	/// <para>
	/// One or more runtime fields for the search request.
	/// These fields take precedence over mapped fields with the same name.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("runtime_mappings")]
	public IDictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.Mapping.RuntimeField>? RuntimeMappings { get; set; }

	/// <summary>
	/// <para>
	/// The ISO-8601 time zone ID for the search.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("time_zone")]
	public string? TimeZone { get; set; }

	/// <summary>
	/// <para>
	/// The period to wait for complete results.
	/// It defaults to no timeout, meaning the request waits for complete search results.
	/// If the search doesn't finish within this period, the search becomes async.
	/// </para>
	/// <para>
	/// To save a synchronous search, you must specify this parameter and the <c>keep_on_completion</c> parameter.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("wait_for_completion_timeout")]
	public Elastic.Clients.Elasticsearch.Duration? WaitForCompletionTimeout { get; set; }
}

/// <summary>
/// <para>
/// Get SQL search results.
/// Run an SQL request.
/// </para>
/// </summary>
public sealed partial class QueryRequestDescriptor<TDocument> : RequestDescriptor<QueryRequestDescriptor<TDocument>, QueryRequestParameters>
{
	internal QueryRequestDescriptor(Action<QueryRequestDescriptor<TDocument>> configure) => configure.Invoke(this);

	public QueryRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.SqlQuery;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "sql.query";

	public QueryRequestDescriptor<TDocument> Format(Elastic.Clients.Elasticsearch.Sql.SqlFormat? format) => Qs("format", format);

	private bool? AllowPartialSearchResultsValue { get; set; }
	private string? CatalogValue { get; set; }
	private bool? ColumnarValue { get; set; }
	private string? CursorValue { get; set; }
	private int? FetchSizeValue { get; set; }
	private bool? FieldMultiValueLeniencyValue { get; set; }
	private Elastic.Clients.Elasticsearch.QueryDsl.Query? FilterValue { get; set; }
	private Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument> FilterDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument>> FilterDescriptorAction { get; set; }
	private bool? IndexUsingFrozenValue { get; set; }
	private Elastic.Clients.Elasticsearch.Duration? KeepAliveValue { get; set; }
	private bool? KeepOnCompletionValue { get; set; }
	private Elastic.Clients.Elasticsearch.Duration? PageTimeoutValue { get; set; }
	private IDictionary<string, object>? ParamsValue { get; set; }
	private string? QueryValue { get; set; }
	private Elastic.Clients.Elasticsearch.Duration? RequestTimeoutValue { get; set; }
	private IDictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.Mapping.RuntimeFieldDescriptor<TDocument>> RuntimeMappingsValue { get; set; }
	private string? TimeZoneValue { get; set; }
	private Elastic.Clients.Elasticsearch.Duration? WaitForCompletionTimeoutValue { get; set; }

	/// <summary>
	/// <para>
	/// If <c>true</c>, the response has partial results when there are shard request timeouts or shard failures.
	/// If <c>false</c>, the API returns an error with no partial results.
	/// </para>
	/// </summary>
	public QueryRequestDescriptor<TDocument> AllowPartialSearchResults(bool? allowPartialSearchResults = true)
	{
		AllowPartialSearchResultsValue = allowPartialSearchResults;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The default catalog (cluster) for queries.
	/// If unspecified, the queries execute on the data in the local cluster only.
	/// </para>
	/// </summary>
	public QueryRequestDescriptor<TDocument> Catalog(string? catalog)
	{
		CatalogValue = catalog;
		return Self;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, the results are in a columnar fashion: one row represents all the values of a certain column from the current page of results.
	/// The API supports this parameter only for CBOR, JSON, SMILE, and YAML responses.
	/// </para>
	/// </summary>
	public QueryRequestDescriptor<TDocument> Columnar(bool? columnar = true)
	{
		ColumnarValue = columnar;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The cursor used to retrieve a set of paginated results.
	/// If you specify a cursor, the API only uses the <c>columnar</c> and <c>time_zone</c> request body parameters.
	/// It ignores other request body parameters.
	/// </para>
	/// </summary>
	public QueryRequestDescriptor<TDocument> Cursor(string? cursor)
	{
		CursorValue = cursor;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The maximum number of rows (or entries) to return in one response.
	/// </para>
	/// </summary>
	public QueryRequestDescriptor<TDocument> FetchSize(int? fetchSize)
	{
		FetchSizeValue = fetchSize;
		return Self;
	}

	/// <summary>
	/// <para>
	/// If <c>false</c>, the API returns an exception when encountering multiple values for a field.
	/// If <c>true</c>, the API is lenient and returns the first value from the array with no guarantee of consistent results.
	/// </para>
	/// </summary>
	public QueryRequestDescriptor<TDocument> FieldMultiValueLeniency(bool? fieldMultiValueLeniency = true)
	{
		FieldMultiValueLeniencyValue = fieldMultiValueLeniency;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The Elasticsearch query DSL for additional filtering.
	/// </para>
	/// </summary>
	public QueryRequestDescriptor<TDocument> Filter(Elastic.Clients.Elasticsearch.QueryDsl.Query? filter)
	{
		FilterDescriptor = null;
		FilterDescriptorAction = null;
		FilterValue = filter;
		return Self;
	}

	public QueryRequestDescriptor<TDocument> Filter(Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument> descriptor)
	{
		FilterValue = null;
		FilterDescriptorAction = null;
		FilterDescriptor = descriptor;
		return Self;
	}

	public QueryRequestDescriptor<TDocument> Filter(Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument>> configure)
	{
		FilterValue = null;
		FilterDescriptor = null;
		FilterDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, the search can run on frozen indices.
	/// </para>
	/// </summary>
	public QueryRequestDescriptor<TDocument> IndexUsingFrozen(bool? indexUsingFrozen = true)
	{
		IndexUsingFrozenValue = indexUsingFrozen;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The retention period for an async or saved synchronous search.
	/// </para>
	/// </summary>
	public QueryRequestDescriptor<TDocument> KeepAlive(Elastic.Clients.Elasticsearch.Duration? keepAlive)
	{
		KeepAliveValue = keepAlive;
		return Self;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, Elasticsearch stores synchronous searches if you also specify the <c>wait_for_completion_timeout</c> parameter.
	/// If <c>false</c>, Elasticsearch only stores async searches that don't finish before the <c>wait_for_completion_timeout</c>.
	/// </para>
	/// </summary>
	public QueryRequestDescriptor<TDocument> KeepOnCompletion(bool? keepOnCompletion = true)
	{
		KeepOnCompletionValue = keepOnCompletion;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The minimum retention period for the scroll cursor.
	/// After this time period, a pagination request might fail because the scroll cursor is no longer available.
	/// Subsequent scroll requests prolong the lifetime of the scroll cursor by the duration of <c>page_timeout</c> in the scroll request.
	/// </para>
	/// </summary>
	public QueryRequestDescriptor<TDocument> PageTimeout(Elastic.Clients.Elasticsearch.Duration? pageTimeout)
	{
		PageTimeoutValue = pageTimeout;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The values for parameters in the query.
	/// </para>
	/// </summary>
	public QueryRequestDescriptor<TDocument> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
	{
		ParamsValue = selector?.Invoke(new FluentDictionary<string, object>());
		return Self;
	}

	/// <summary>
	/// <para>
	/// The SQL query to run.
	/// </para>
	/// </summary>
	public QueryRequestDescriptor<TDocument> Query(string? query)
	{
		QueryValue = query;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The timeout before the request fails.
	/// </para>
	/// </summary>
	public QueryRequestDescriptor<TDocument> RequestTimeout(Elastic.Clients.Elasticsearch.Duration? requestTimeout)
	{
		RequestTimeoutValue = requestTimeout;
		return Self;
	}

	/// <summary>
	/// <para>
	/// One or more runtime fields for the search request.
	/// These fields take precedence over mapped fields with the same name.
	/// </para>
	/// </summary>
	public QueryRequestDescriptor<TDocument> RuntimeMappings(Func<FluentDescriptorDictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.Mapping.RuntimeFieldDescriptor<TDocument>>, FluentDescriptorDictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.Mapping.RuntimeFieldDescriptor<TDocument>>> selector)
	{
		RuntimeMappingsValue = selector?.Invoke(new FluentDescriptorDictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.Mapping.RuntimeFieldDescriptor<TDocument>>());
		return Self;
	}

	/// <summary>
	/// <para>
	/// The ISO-8601 time zone ID for the search.
	/// </para>
	/// </summary>
	public QueryRequestDescriptor<TDocument> TimeZone(string? timeZone)
	{
		TimeZoneValue = timeZone;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The period to wait for complete results.
	/// It defaults to no timeout, meaning the request waits for complete search results.
	/// If the search doesn't finish within this period, the search becomes async.
	/// </para>
	/// <para>
	/// To save a synchronous search, you must specify this parameter and the <c>keep_on_completion</c> parameter.
	/// </para>
	/// </summary>
	public QueryRequestDescriptor<TDocument> WaitForCompletionTimeout(Elastic.Clients.Elasticsearch.Duration? waitForCompletionTimeout)
	{
		WaitForCompletionTimeoutValue = waitForCompletionTimeout;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (AllowPartialSearchResultsValue.HasValue)
		{
			writer.WritePropertyName("allow_partial_search_results");
			writer.WriteBooleanValue(AllowPartialSearchResultsValue.Value);
		}

		if (!string.IsNullOrEmpty(CatalogValue))
		{
			writer.WritePropertyName("catalog");
			writer.WriteStringValue(CatalogValue);
		}

		if (ColumnarValue.HasValue)
		{
			writer.WritePropertyName("columnar");
			writer.WriteBooleanValue(ColumnarValue.Value);
		}

		if (!string.IsNullOrEmpty(CursorValue))
		{
			writer.WritePropertyName("cursor");
			writer.WriteStringValue(CursorValue);
		}

		if (FetchSizeValue.HasValue)
		{
			writer.WritePropertyName("fetch_size");
			writer.WriteNumberValue(FetchSizeValue.Value);
		}

		if (FieldMultiValueLeniencyValue.HasValue)
		{
			writer.WritePropertyName("field_multi_value_leniency");
			writer.WriteBooleanValue(FieldMultiValueLeniencyValue.Value);
		}

		if (FilterDescriptor is not null)
		{
			writer.WritePropertyName("filter");
			JsonSerializer.Serialize(writer, FilterDescriptor, options);
		}
		else if (FilterDescriptorAction is not null)
		{
			writer.WritePropertyName("filter");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument>(FilterDescriptorAction), options);
		}
		else if (FilterValue is not null)
		{
			writer.WritePropertyName("filter");
			JsonSerializer.Serialize(writer, FilterValue, options);
		}

		if (IndexUsingFrozenValue.HasValue)
		{
			writer.WritePropertyName("index_using_frozen");
			writer.WriteBooleanValue(IndexUsingFrozenValue.Value);
		}

		if (KeepAliveValue is not null)
		{
			writer.WritePropertyName("keep_alive");
			JsonSerializer.Serialize(writer, KeepAliveValue, options);
		}

		if (KeepOnCompletionValue.HasValue)
		{
			writer.WritePropertyName("keep_on_completion");
			writer.WriteBooleanValue(KeepOnCompletionValue.Value);
		}

		if (PageTimeoutValue is not null)
		{
			writer.WritePropertyName("page_timeout");
			JsonSerializer.Serialize(writer, PageTimeoutValue, options);
		}

		if (ParamsValue is not null)
		{
			writer.WritePropertyName("params");
			JsonSerializer.Serialize(writer, ParamsValue, options);
		}

		if (!string.IsNullOrEmpty(QueryValue))
		{
			writer.WritePropertyName("query");
			writer.WriteStringValue(QueryValue);
		}

		if (RequestTimeoutValue is not null)
		{
			writer.WritePropertyName("request_timeout");
			JsonSerializer.Serialize(writer, RequestTimeoutValue, options);
		}

		if (RuntimeMappingsValue is not null)
		{
			writer.WritePropertyName("runtime_mappings");
			JsonSerializer.Serialize(writer, RuntimeMappingsValue, options);
		}

		if (!string.IsNullOrEmpty(TimeZoneValue))
		{
			writer.WritePropertyName("time_zone");
			writer.WriteStringValue(TimeZoneValue);
		}

		if (WaitForCompletionTimeoutValue is not null)
		{
			writer.WritePropertyName("wait_for_completion_timeout");
			JsonSerializer.Serialize(writer, WaitForCompletionTimeoutValue, options);
		}

		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Get SQL search results.
/// Run an SQL request.
/// </para>
/// </summary>
public sealed partial class QueryRequestDescriptor : RequestDescriptor<QueryRequestDescriptor, QueryRequestParameters>
{
	internal QueryRequestDescriptor(Action<QueryRequestDescriptor> configure) => configure.Invoke(this);

	public QueryRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.SqlQuery;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "sql.query";

	public QueryRequestDescriptor Format(Elastic.Clients.Elasticsearch.Sql.SqlFormat? format) => Qs("format", format);

	private bool? AllowPartialSearchResultsValue { get; set; }
	private string? CatalogValue { get; set; }
	private bool? ColumnarValue { get; set; }
	private string? CursorValue { get; set; }
	private int? FetchSizeValue { get; set; }
	private bool? FieldMultiValueLeniencyValue { get; set; }
	private Elastic.Clients.Elasticsearch.QueryDsl.Query? FilterValue { get; set; }
	private Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor FilterDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor> FilterDescriptorAction { get; set; }
	private bool? IndexUsingFrozenValue { get; set; }
	private Elastic.Clients.Elasticsearch.Duration? KeepAliveValue { get; set; }
	private bool? KeepOnCompletionValue { get; set; }
	private Elastic.Clients.Elasticsearch.Duration? PageTimeoutValue { get; set; }
	private IDictionary<string, object>? ParamsValue { get; set; }
	private string? QueryValue { get; set; }
	private Elastic.Clients.Elasticsearch.Duration? RequestTimeoutValue { get; set; }
	private IDictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.Mapping.RuntimeFieldDescriptor> RuntimeMappingsValue { get; set; }
	private string? TimeZoneValue { get; set; }
	private Elastic.Clients.Elasticsearch.Duration? WaitForCompletionTimeoutValue { get; set; }

	/// <summary>
	/// <para>
	/// If <c>true</c>, the response has partial results when there are shard request timeouts or shard failures.
	/// If <c>false</c>, the API returns an error with no partial results.
	/// </para>
	/// </summary>
	public QueryRequestDescriptor AllowPartialSearchResults(bool? allowPartialSearchResults = true)
	{
		AllowPartialSearchResultsValue = allowPartialSearchResults;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The default catalog (cluster) for queries.
	/// If unspecified, the queries execute on the data in the local cluster only.
	/// </para>
	/// </summary>
	public QueryRequestDescriptor Catalog(string? catalog)
	{
		CatalogValue = catalog;
		return Self;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, the results are in a columnar fashion: one row represents all the values of a certain column from the current page of results.
	/// The API supports this parameter only for CBOR, JSON, SMILE, and YAML responses.
	/// </para>
	/// </summary>
	public QueryRequestDescriptor Columnar(bool? columnar = true)
	{
		ColumnarValue = columnar;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The cursor used to retrieve a set of paginated results.
	/// If you specify a cursor, the API only uses the <c>columnar</c> and <c>time_zone</c> request body parameters.
	/// It ignores other request body parameters.
	/// </para>
	/// </summary>
	public QueryRequestDescriptor Cursor(string? cursor)
	{
		CursorValue = cursor;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The maximum number of rows (or entries) to return in one response.
	/// </para>
	/// </summary>
	public QueryRequestDescriptor FetchSize(int? fetchSize)
	{
		FetchSizeValue = fetchSize;
		return Self;
	}

	/// <summary>
	/// <para>
	/// If <c>false</c>, the API returns an exception when encountering multiple values for a field.
	/// If <c>true</c>, the API is lenient and returns the first value from the array with no guarantee of consistent results.
	/// </para>
	/// </summary>
	public QueryRequestDescriptor FieldMultiValueLeniency(bool? fieldMultiValueLeniency = true)
	{
		FieldMultiValueLeniencyValue = fieldMultiValueLeniency;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The Elasticsearch query DSL for additional filtering.
	/// </para>
	/// </summary>
	public QueryRequestDescriptor Filter(Elastic.Clients.Elasticsearch.QueryDsl.Query? filter)
	{
		FilterDescriptor = null;
		FilterDescriptorAction = null;
		FilterValue = filter;
		return Self;
	}

	public QueryRequestDescriptor Filter(Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor descriptor)
	{
		FilterValue = null;
		FilterDescriptorAction = null;
		FilterDescriptor = descriptor;
		return Self;
	}

	public QueryRequestDescriptor Filter(Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor> configure)
	{
		FilterValue = null;
		FilterDescriptor = null;
		FilterDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, the search can run on frozen indices.
	/// </para>
	/// </summary>
	public QueryRequestDescriptor IndexUsingFrozen(bool? indexUsingFrozen = true)
	{
		IndexUsingFrozenValue = indexUsingFrozen;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The retention period for an async or saved synchronous search.
	/// </para>
	/// </summary>
	public QueryRequestDescriptor KeepAlive(Elastic.Clients.Elasticsearch.Duration? keepAlive)
	{
		KeepAliveValue = keepAlive;
		return Self;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, Elasticsearch stores synchronous searches if you also specify the <c>wait_for_completion_timeout</c> parameter.
	/// If <c>false</c>, Elasticsearch only stores async searches that don't finish before the <c>wait_for_completion_timeout</c>.
	/// </para>
	/// </summary>
	public QueryRequestDescriptor KeepOnCompletion(bool? keepOnCompletion = true)
	{
		KeepOnCompletionValue = keepOnCompletion;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The minimum retention period for the scroll cursor.
	/// After this time period, a pagination request might fail because the scroll cursor is no longer available.
	/// Subsequent scroll requests prolong the lifetime of the scroll cursor by the duration of <c>page_timeout</c> in the scroll request.
	/// </para>
	/// </summary>
	public QueryRequestDescriptor PageTimeout(Elastic.Clients.Elasticsearch.Duration? pageTimeout)
	{
		PageTimeoutValue = pageTimeout;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The values for parameters in the query.
	/// </para>
	/// </summary>
	public QueryRequestDescriptor Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
	{
		ParamsValue = selector?.Invoke(new FluentDictionary<string, object>());
		return Self;
	}

	/// <summary>
	/// <para>
	/// The SQL query to run.
	/// </para>
	/// </summary>
	public QueryRequestDescriptor Query(string? query)
	{
		QueryValue = query;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The timeout before the request fails.
	/// </para>
	/// </summary>
	public QueryRequestDescriptor RequestTimeout(Elastic.Clients.Elasticsearch.Duration? requestTimeout)
	{
		RequestTimeoutValue = requestTimeout;
		return Self;
	}

	/// <summary>
	/// <para>
	/// One or more runtime fields for the search request.
	/// These fields take precedence over mapped fields with the same name.
	/// </para>
	/// </summary>
	public QueryRequestDescriptor RuntimeMappings(Func<FluentDescriptorDictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.Mapping.RuntimeFieldDescriptor>, FluentDescriptorDictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.Mapping.RuntimeFieldDescriptor>> selector)
	{
		RuntimeMappingsValue = selector?.Invoke(new FluentDescriptorDictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.Mapping.RuntimeFieldDescriptor>());
		return Self;
	}

	/// <summary>
	/// <para>
	/// The ISO-8601 time zone ID for the search.
	/// </para>
	/// </summary>
	public QueryRequestDescriptor TimeZone(string? timeZone)
	{
		TimeZoneValue = timeZone;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The period to wait for complete results.
	/// It defaults to no timeout, meaning the request waits for complete search results.
	/// If the search doesn't finish within this period, the search becomes async.
	/// </para>
	/// <para>
	/// To save a synchronous search, you must specify this parameter and the <c>keep_on_completion</c> parameter.
	/// </para>
	/// </summary>
	public QueryRequestDescriptor WaitForCompletionTimeout(Elastic.Clients.Elasticsearch.Duration? waitForCompletionTimeout)
	{
		WaitForCompletionTimeoutValue = waitForCompletionTimeout;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (AllowPartialSearchResultsValue.HasValue)
		{
			writer.WritePropertyName("allow_partial_search_results");
			writer.WriteBooleanValue(AllowPartialSearchResultsValue.Value);
		}

		if (!string.IsNullOrEmpty(CatalogValue))
		{
			writer.WritePropertyName("catalog");
			writer.WriteStringValue(CatalogValue);
		}

		if (ColumnarValue.HasValue)
		{
			writer.WritePropertyName("columnar");
			writer.WriteBooleanValue(ColumnarValue.Value);
		}

		if (!string.IsNullOrEmpty(CursorValue))
		{
			writer.WritePropertyName("cursor");
			writer.WriteStringValue(CursorValue);
		}

		if (FetchSizeValue.HasValue)
		{
			writer.WritePropertyName("fetch_size");
			writer.WriteNumberValue(FetchSizeValue.Value);
		}

		if (FieldMultiValueLeniencyValue.HasValue)
		{
			writer.WritePropertyName("field_multi_value_leniency");
			writer.WriteBooleanValue(FieldMultiValueLeniencyValue.Value);
		}

		if (FilterDescriptor is not null)
		{
			writer.WritePropertyName("filter");
			JsonSerializer.Serialize(writer, FilterDescriptor, options);
		}
		else if (FilterDescriptorAction is not null)
		{
			writer.WritePropertyName("filter");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor(FilterDescriptorAction), options);
		}
		else if (FilterValue is not null)
		{
			writer.WritePropertyName("filter");
			JsonSerializer.Serialize(writer, FilterValue, options);
		}

		if (IndexUsingFrozenValue.HasValue)
		{
			writer.WritePropertyName("index_using_frozen");
			writer.WriteBooleanValue(IndexUsingFrozenValue.Value);
		}

		if (KeepAliveValue is not null)
		{
			writer.WritePropertyName("keep_alive");
			JsonSerializer.Serialize(writer, KeepAliveValue, options);
		}

		if (KeepOnCompletionValue.HasValue)
		{
			writer.WritePropertyName("keep_on_completion");
			writer.WriteBooleanValue(KeepOnCompletionValue.Value);
		}

		if (PageTimeoutValue is not null)
		{
			writer.WritePropertyName("page_timeout");
			JsonSerializer.Serialize(writer, PageTimeoutValue, options);
		}

		if (ParamsValue is not null)
		{
			writer.WritePropertyName("params");
			JsonSerializer.Serialize(writer, ParamsValue, options);
		}

		if (!string.IsNullOrEmpty(QueryValue))
		{
			writer.WritePropertyName("query");
			writer.WriteStringValue(QueryValue);
		}

		if (RequestTimeoutValue is not null)
		{
			writer.WritePropertyName("request_timeout");
			JsonSerializer.Serialize(writer, RequestTimeoutValue, options);
		}

		if (RuntimeMappingsValue is not null)
		{
			writer.WritePropertyName("runtime_mappings");
			JsonSerializer.Serialize(writer, RuntimeMappingsValue, options);
		}

		if (!string.IsNullOrEmpty(TimeZoneValue))
		{
			writer.WritePropertyName("time_zone");
			writer.WriteStringValue(TimeZoneValue);
		}

		if (WaitForCompletionTimeoutValue is not null)
		{
			writer.WritePropertyName("wait_for_completion_timeout");
			JsonSerializer.Serialize(writer, WaitForCompletionTimeoutValue, options);
		}

		writer.WriteEndObject();
	}
}