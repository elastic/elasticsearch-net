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

namespace Elastic.Clients.Elasticsearch.Security;

public sealed partial class GetRoleRequestParameters : RequestParameters
{
}

/// <summary>
/// <para>
/// Get roles.
/// </para>
/// <para>
/// Get roles in the native realm.
/// The role management APIs are generally the preferred way to manage roles, rather than using file-based role management.
/// The get roles API cannot retrieve roles that are defined in roles files.
/// </para>
/// </summary>
public sealed partial class GetRoleRequest : PlainRequest<GetRoleRequestParameters>
{
	public GetRoleRequest()
	{
	}

	public GetRoleRequest(Elastic.Clients.Elasticsearch.Names? name) : base(r => r.Optional("name", name))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.SecurityGetRole;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "security.get_role";
}

/// <summary>
/// <para>
/// Get roles.
/// </para>
/// <para>
/// Get roles in the native realm.
/// The role management APIs are generally the preferred way to manage roles, rather than using file-based role management.
/// The get roles API cannot retrieve roles that are defined in roles files.
/// </para>
/// </summary>
public sealed partial class GetRoleRequestDescriptor : RequestDescriptor<GetRoleRequestDescriptor, GetRoleRequestParameters>
{
	internal GetRoleRequestDescriptor(Action<GetRoleRequestDescriptor> configure) => configure.Invoke(this);

	public GetRoleRequestDescriptor(Elastic.Clients.Elasticsearch.Names? name) : base(r => r.Optional("name", name))
	{
	}

	public GetRoleRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.SecurityGetRole;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "security.get_role";

	public GetRoleRequestDescriptor Name(Elastic.Clients.Elasticsearch.Names? name)
	{
		RouteValues.Optional("name", name);
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}