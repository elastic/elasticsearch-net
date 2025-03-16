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

namespace Elastic.Clients.Elasticsearch.Esql;

public sealed partial class AsyncQueryGetRequestParameters : RequestParameters
{
	/// <summary>
	/// <para>
	/// Indicates whether columns that are entirely <c>null</c> will be removed from the <c>columns</c> and <c>values</c> portion of the results.
	/// If <c>true</c>, the response will include an extra section under the name <c>all_columns</c> which has the name of all the columns.
	/// </para>
	/// </summary>
	public bool? DropNullColumns { get => Q<bool?>("drop_null_columns"); set => Q("drop_null_columns", value); }

	/// <summary>
	/// <para>
	/// The period for which the query and its results are stored in the cluster.
	/// When this period expires, the query and its results are deleted, even if the query is still ongoing.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? KeepAlive { get => Q<Elastic.Clients.Elasticsearch.Duration?>("keep_alive"); set => Q("keep_alive", value); }

	/// <summary>
	/// <para>
	/// The period to wait for the request to finish.
	/// By default, the request waits for complete query results.
	/// If the request completes during the period specified in this parameter, complete query results are returned.
	/// Otherwise, the response returns an <c>is_running</c> value of <c>true</c> and no results.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? WaitForCompletionTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("wait_for_completion_timeout"); set => Q("wait_for_completion_timeout", value); }
}

/// <summary>
/// <para>
/// Get async ES|QL query results.
/// Get the current status and available results or stored results for an ES|QL asynchronous query.
/// If the Elasticsearch security features are enabled, only the user who first submitted the ES|QL query can retrieve the results using this API.
/// </para>
/// </summary>
public sealed partial class AsyncQueryGetRequest : PlainRequest<AsyncQueryGetRequestParameters>
{
	public AsyncQueryGetRequest(Elastic.Clients.Elasticsearch.Id id) : base(r => r.Required("id", id))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.EsqlAsyncQueryGet;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "esql.async_query_get";

	/// <summary>
	/// <para>
	/// Indicates whether columns that are entirely <c>null</c> will be removed from the <c>columns</c> and <c>values</c> portion of the results.
	/// If <c>true</c>, the response will include an extra section under the name <c>all_columns</c> which has the name of all the columns.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public bool? DropNullColumns { get => Q<bool?>("drop_null_columns"); set => Q("drop_null_columns", value); }

	/// <summary>
	/// <para>
	/// The period for which the query and its results are stored in the cluster.
	/// When this period expires, the query and its results are deleted, even if the query is still ongoing.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Duration? KeepAlive { get => Q<Elastic.Clients.Elasticsearch.Duration?>("keep_alive"); set => Q("keep_alive", value); }

	/// <summary>
	/// <para>
	/// The period to wait for the request to finish.
	/// By default, the request waits for complete query results.
	/// If the request completes during the period specified in this parameter, complete query results are returned.
	/// Otherwise, the response returns an <c>is_running</c> value of <c>true</c> and no results.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Duration? WaitForCompletionTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("wait_for_completion_timeout"); set => Q("wait_for_completion_timeout", value); }
}

/// <summary>
/// <para>
/// Get async ES|QL query results.
/// Get the current status and available results or stored results for an ES|QL asynchronous query.
/// If the Elasticsearch security features are enabled, only the user who first submitted the ES|QL query can retrieve the results using this API.
/// </para>
/// </summary>
public sealed partial class AsyncQueryGetRequestDescriptor<TDocument> : RequestDescriptor<AsyncQueryGetRequestDescriptor<TDocument>, AsyncQueryGetRequestParameters>
{
	internal AsyncQueryGetRequestDescriptor(Action<AsyncQueryGetRequestDescriptor<TDocument>> configure) => configure.Invoke(this);

	public AsyncQueryGetRequestDescriptor(Elastic.Clients.Elasticsearch.Id id) : base(r => r.Required("id", id))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.EsqlAsyncQueryGet;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "esql.async_query_get";

	public AsyncQueryGetRequestDescriptor<TDocument> DropNullColumns(bool? dropNullColumns = true) => Qs("drop_null_columns", dropNullColumns);
	public AsyncQueryGetRequestDescriptor<TDocument> KeepAlive(Elastic.Clients.Elasticsearch.Duration? keepAlive) => Qs("keep_alive", keepAlive);
	public AsyncQueryGetRequestDescriptor<TDocument> WaitForCompletionTimeout(Elastic.Clients.Elasticsearch.Duration? waitForCompletionTimeout) => Qs("wait_for_completion_timeout", waitForCompletionTimeout);

	public AsyncQueryGetRequestDescriptor<TDocument> Id(Elastic.Clients.Elasticsearch.Id id)
	{
		RouteValues.Required("id", id);
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}

/// <summary>
/// <para>
/// Get async ES|QL query results.
/// Get the current status and available results or stored results for an ES|QL asynchronous query.
/// If the Elasticsearch security features are enabled, only the user who first submitted the ES|QL query can retrieve the results using this API.
/// </para>
/// </summary>
public sealed partial class AsyncQueryGetRequestDescriptor : RequestDescriptor<AsyncQueryGetRequestDescriptor, AsyncQueryGetRequestParameters>
{
	internal AsyncQueryGetRequestDescriptor(Action<AsyncQueryGetRequestDescriptor> configure) => configure.Invoke(this);

	public AsyncQueryGetRequestDescriptor(Elastic.Clients.Elasticsearch.Id id) : base(r => r.Required("id", id))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.EsqlAsyncQueryGet;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "esql.async_query_get";

	public AsyncQueryGetRequestDescriptor DropNullColumns(bool? dropNullColumns = true) => Qs("drop_null_columns", dropNullColumns);
	public AsyncQueryGetRequestDescriptor KeepAlive(Elastic.Clients.Elasticsearch.Duration? keepAlive) => Qs("keep_alive", keepAlive);
	public AsyncQueryGetRequestDescriptor WaitForCompletionTimeout(Elastic.Clients.Elasticsearch.Duration? waitForCompletionTimeout) => Qs("wait_for_completion_timeout", waitForCompletionTimeout);

	public AsyncQueryGetRequestDescriptor Id(Elastic.Clients.Elasticsearch.Id id)
	{
		RouteValues.Required("id", id);
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}