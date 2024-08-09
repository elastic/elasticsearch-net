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
using Elastic.Clients.Elasticsearch.Serverless.Requests;
using Elastic.Clients.Elasticsearch.Serverless.Serialization;
using Elastic.Transport;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serverless.AsyncSearch;

public sealed partial class GetAsyncSearchRequestParameters : RequestParameters
{
	/// <summary>
	/// <para>
	/// Specifies how long the async search should be available in the cluster.
	/// When not specified, the <c>keep_alive</c> set with the corresponding submit async request will be used.
	/// Otherwise, it is possible to override the value and extend the validity of the request.
	/// When this period expires, the search, if still running, is cancelled.
	/// If the search is completed, its saved results are deleted.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Serverless.Duration? KeepAlive { get => Q<Elastic.Clients.Elasticsearch.Serverless.Duration?>("keep_alive"); set => Q("keep_alive", value); }

	/// <summary>
	/// <para>
	/// Specify whether aggregation and suggester names should be prefixed by their respective types in the response
	/// </para>
	/// </summary>
	public bool? TypedKeys { get => Q<bool?>("typed_keys"); set => Q("typed_keys", value); }

	/// <summary>
	/// <para>
	/// Specifies to wait for the search to be completed up until the provided timeout.
	/// Final results will be returned if available before the timeout expires, otherwise the currently available results will be returned once the timeout expires.
	/// By default no timeout is set meaning that the currently available results will be returned without any additional wait.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Serverless.Duration? WaitForCompletionTimeout { get => Q<Elastic.Clients.Elasticsearch.Serverless.Duration?>("wait_for_completion_timeout"); set => Q("wait_for_completion_timeout", value); }
}

/// <summary>
/// <para>
/// Retrieves the results of a previously submitted async search request given its identifier.
/// If the Elasticsearch security features are enabled, access to the results of a specific async search is restricted to the user or API key that submitted it.
/// </para>
/// </summary>
public sealed partial class GetAsyncSearchRequest : PlainRequest<GetAsyncSearchRequestParameters>
{
	public GetAsyncSearchRequest(Elastic.Clients.Elasticsearch.Serverless.Id id) : base(r => r.Required("id", id))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.AsyncSearchGet;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "async_search.get";

	/// <summary>
	/// <para>
	/// Specifies how long the async search should be available in the cluster.
	/// When not specified, the <c>keep_alive</c> set with the corresponding submit async request will be used.
	/// Otherwise, it is possible to override the value and extend the validity of the request.
	/// When this period expires, the search, if still running, is cancelled.
	/// If the search is completed, its saved results are deleted.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Serverless.Duration? KeepAlive { get => Q<Elastic.Clients.Elasticsearch.Serverless.Duration?>("keep_alive"); set => Q("keep_alive", value); }

	/// <summary>
	/// <para>
	/// Specify whether aggregation and suggester names should be prefixed by their respective types in the response
	/// </para>
	/// </summary>
	[JsonIgnore]
	public bool? TypedKeys { get => Q<bool?>("typed_keys"); set => Q("typed_keys", value); }

	/// <summary>
	/// <para>
	/// Specifies to wait for the search to be completed up until the provided timeout.
	/// Final results will be returned if available before the timeout expires, otherwise the currently available results will be returned once the timeout expires.
	/// By default no timeout is set meaning that the currently available results will be returned without any additional wait.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Serverless.Duration? WaitForCompletionTimeout { get => Q<Elastic.Clients.Elasticsearch.Serverless.Duration?>("wait_for_completion_timeout"); set => Q("wait_for_completion_timeout", value); }
}

/// <summary>
/// <para>
/// Retrieves the results of a previously submitted async search request given its identifier.
/// If the Elasticsearch security features are enabled, access to the results of a specific async search is restricted to the user or API key that submitted it.
/// </para>
/// </summary>
public sealed partial class GetAsyncSearchRequestDescriptor<TDocument> : RequestDescriptor<GetAsyncSearchRequestDescriptor<TDocument>, GetAsyncSearchRequestParameters>
{
	internal GetAsyncSearchRequestDescriptor(Action<GetAsyncSearchRequestDescriptor<TDocument>> configure) => configure.Invoke(this);

	public GetAsyncSearchRequestDescriptor(Elastic.Clients.Elasticsearch.Serverless.Id id) : base(r => r.Required("id", id))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.AsyncSearchGet;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "async_search.get";

	public GetAsyncSearchRequestDescriptor<TDocument> KeepAlive(Elastic.Clients.Elasticsearch.Serverless.Duration? keepAlive) => Qs("keep_alive", keepAlive);
	public GetAsyncSearchRequestDescriptor<TDocument> TypedKeys(bool? typedKeys = true) => Qs("typed_keys", typedKeys);
	public GetAsyncSearchRequestDescriptor<TDocument> WaitForCompletionTimeout(Elastic.Clients.Elasticsearch.Serverless.Duration? waitForCompletionTimeout) => Qs("wait_for_completion_timeout", waitForCompletionTimeout);

	public GetAsyncSearchRequestDescriptor<TDocument> Id(Elastic.Clients.Elasticsearch.Serverless.Id id)
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
/// Retrieves the results of a previously submitted async search request given its identifier.
/// If the Elasticsearch security features are enabled, access to the results of a specific async search is restricted to the user or API key that submitted it.
/// </para>
/// </summary>
public sealed partial class GetAsyncSearchRequestDescriptor : RequestDescriptor<GetAsyncSearchRequestDescriptor, GetAsyncSearchRequestParameters>
{
	internal GetAsyncSearchRequestDescriptor(Action<GetAsyncSearchRequestDescriptor> configure) => configure.Invoke(this);

	public GetAsyncSearchRequestDescriptor(Elastic.Clients.Elasticsearch.Serverless.Id id) : base(r => r.Required("id", id))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.AsyncSearchGet;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "async_search.get";

	public GetAsyncSearchRequestDescriptor KeepAlive(Elastic.Clients.Elasticsearch.Serverless.Duration? keepAlive) => Qs("keep_alive", keepAlive);
	public GetAsyncSearchRequestDescriptor TypedKeys(bool? typedKeys = true) => Qs("typed_keys", typedKeys);
	public GetAsyncSearchRequestDescriptor WaitForCompletionTimeout(Elastic.Clients.Elasticsearch.Serverless.Duration? waitForCompletionTimeout) => Qs("wait_for_completion_timeout", waitForCompletionTimeout);

	public GetAsyncSearchRequestDescriptor Id(Elastic.Clients.Elasticsearch.Serverless.Id id)
	{
		RouteValues.Required("id", id);
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}