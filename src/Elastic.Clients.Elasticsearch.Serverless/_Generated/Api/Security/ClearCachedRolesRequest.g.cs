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

namespace Elastic.Clients.Elasticsearch.Serverless.Security;

public sealed partial class ClearCachedRolesRequestParameters : RequestParameters
{
}

/// <summary>
/// <para>
/// Evicts roles from the native role cache.
/// </para>
/// </summary>
public sealed partial class ClearCachedRolesRequest : PlainRequest<ClearCachedRolesRequestParameters>
{
	public ClearCachedRolesRequest(Elastic.Clients.Elasticsearch.Serverless.Names name) : base(r => r.Required("name", name))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.SecurityClearCachedRoles;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => false;

	internal override string OperationName => "security.clear_cached_roles";
}

/// <summary>
/// <para>
/// Evicts roles from the native role cache.
/// </para>
/// </summary>
public sealed partial class ClearCachedRolesRequestDescriptor : RequestDescriptor<ClearCachedRolesRequestDescriptor, ClearCachedRolesRequestParameters>
{
	internal ClearCachedRolesRequestDescriptor(Action<ClearCachedRolesRequestDescriptor> configure) => configure.Invoke(this);

	public ClearCachedRolesRequestDescriptor(Elastic.Clients.Elasticsearch.Serverless.Names name) : base(r => r.Required("name", name))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.SecurityClearCachedRoles;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => false;

	internal override string OperationName => "security.clear_cached_roles";

	public ClearCachedRolesRequestDescriptor Name(Elastic.Clients.Elasticsearch.Serverless.Names name)
	{
		RouteValues.Required("name", name);
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}