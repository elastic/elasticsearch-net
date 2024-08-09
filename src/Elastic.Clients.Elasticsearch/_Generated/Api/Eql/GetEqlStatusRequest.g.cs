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

namespace Elastic.Clients.Elasticsearch.Eql;

public sealed partial class GetEqlStatusRequestParameters : RequestParameters
{
}

/// <summary>
/// <para>
/// Returns the current status for an async EQL search or a stored synchronous EQL search without returning results.
/// </para>
/// </summary>
public sealed partial class GetEqlStatusRequest : PlainRequest<GetEqlStatusRequestParameters>
{
	public GetEqlStatusRequest(Elastic.Clients.Elasticsearch.Id id) : base(r => r.Required("id", id))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.EqlGetStatus;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "eql.get_status";
}

/// <summary>
/// <para>
/// Returns the current status for an async EQL search or a stored synchronous EQL search without returning results.
/// </para>
/// </summary>
public sealed partial class GetEqlStatusRequestDescriptor<TDocument> : RequestDescriptor<GetEqlStatusRequestDescriptor<TDocument>, GetEqlStatusRequestParameters>
{
	internal GetEqlStatusRequestDescriptor(Action<GetEqlStatusRequestDescriptor<TDocument>> configure) => configure.Invoke(this);

	public GetEqlStatusRequestDescriptor(Elastic.Clients.Elasticsearch.Id id) : base(r => r.Required("id", id))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.EqlGetStatus;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "eql.get_status";

	public GetEqlStatusRequestDescriptor<TDocument> Id(Elastic.Clients.Elasticsearch.Id id)
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
/// Returns the current status for an async EQL search or a stored synchronous EQL search without returning results.
/// </para>
/// </summary>
public sealed partial class GetEqlStatusRequestDescriptor : RequestDescriptor<GetEqlStatusRequestDescriptor, GetEqlStatusRequestParameters>
{
	internal GetEqlStatusRequestDescriptor(Action<GetEqlStatusRequestDescriptor> configure) => configure.Invoke(this);

	public GetEqlStatusRequestDescriptor(Elastic.Clients.Elasticsearch.Id id) : base(r => r.Required("id", id))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.EqlGetStatus;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "eql.get_status";

	public GetEqlStatusRequestDescriptor Id(Elastic.Clients.Elasticsearch.Id id)
	{
		RouteValues.Required("id", id);
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}