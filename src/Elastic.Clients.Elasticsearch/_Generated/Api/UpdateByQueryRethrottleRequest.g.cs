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

namespace Elastic.Clients.Elasticsearch;

public sealed partial class UpdateByQueryRethrottleRequestParameters : RequestParameters
{
	/// <summary>
	/// <para>
	/// The throttle for this request in sub-requests per second.
	/// To turn off throttling, set it to <c>-1</c>.
	/// </para>
	/// </summary>
	public float? RequestsPerSecond { get => Q<float?>("requests_per_second"); set => Q("requests_per_second", value); }
}

/// <summary>
/// <para>
/// Throttle an update by query operation.
/// </para>
/// <para>
/// Change the number of requests per second for a particular update by query operation.
/// Rethrottling that speeds up the query takes effect immediately but rethrotting that slows down the query takes effect after completing the current batch to prevent scroll timeouts.
/// </para>
/// </summary>
public sealed partial class UpdateByQueryRethrottleRequest : PlainRequest<UpdateByQueryRethrottleRequestParameters>
{
	public UpdateByQueryRethrottleRequest(Elastic.Clients.Elasticsearch.Id taskId) : base(r => r.Required("task_id", taskId))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.NoNamespaceUpdateByQueryRethrottle;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => false;

	internal override string OperationName => "update_by_query_rethrottle";

	/// <summary>
	/// <para>
	/// The throttle for this request in sub-requests per second.
	/// To turn off throttling, set it to <c>-1</c>.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public float? RequestsPerSecond { get => Q<float?>("requests_per_second"); set => Q("requests_per_second", value); }
}

/// <summary>
/// <para>
/// Throttle an update by query operation.
/// </para>
/// <para>
/// Change the number of requests per second for a particular update by query operation.
/// Rethrottling that speeds up the query takes effect immediately but rethrotting that slows down the query takes effect after completing the current batch to prevent scroll timeouts.
/// </para>
/// </summary>
public sealed partial class UpdateByQueryRethrottleRequestDescriptor : RequestDescriptor<UpdateByQueryRethrottleRequestDescriptor, UpdateByQueryRethrottleRequestParameters>
{
	internal UpdateByQueryRethrottleRequestDescriptor(Action<UpdateByQueryRethrottleRequestDescriptor> configure) => configure.Invoke(this);

	public UpdateByQueryRethrottleRequestDescriptor(Elastic.Clients.Elasticsearch.Id taskId) : base(r => r.Required("task_id", taskId))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.NoNamespaceUpdateByQueryRethrottle;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => false;

	internal override string OperationName => "update_by_query_rethrottle";

	public UpdateByQueryRethrottleRequestDescriptor RequestsPerSecond(float? requestsPerSecond) => Qs("requests_per_second", requestsPerSecond);

	public UpdateByQueryRethrottleRequestDescriptor TaskId(Elastic.Clients.Elasticsearch.Id taskId)
	{
		RouteValues.Required("task_id", taskId);
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}