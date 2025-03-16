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

public sealed partial class DeletePrivilegesRequestParameters : RequestParameters
{
	/// <summary>
	/// <para>
	/// If <c>true</c> (the default) then refresh the affected shards to make this operation visible to search, if <c>wait_for</c> then wait for a refresh to make this operation visible to search, if <c>false</c> then do nothing with refreshes.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Refresh? Refresh { get => Q<Elastic.Clients.Elasticsearch.Refresh?>("refresh"); set => Q("refresh", value); }
}

/// <summary>
/// <para>
/// Delete application privileges.
/// </para>
/// <para>
/// To use this API, you must have one of the following privileges:
/// </para>
/// <list type="bullet">
/// <item>
/// <para>
/// The <c>manage_security</c> cluster privilege (or a greater privilege such as <c>all</c>).
/// </para>
/// </item>
/// <item>
/// <para>
/// The "Manage Application Privileges" global privilege for the application being referenced in the request.
/// </para>
/// </item>
/// </list>
/// </summary>
public sealed partial class DeletePrivilegesRequest : PlainRequest<DeletePrivilegesRequestParameters>
{
	public DeletePrivilegesRequest(Elastic.Clients.Elasticsearch.Name application, Elastic.Clients.Elasticsearch.Names name) : base(r => r.Required("application", application).Required("name", name))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.SecurityDeletePrivileges;

	protected override HttpMethod StaticHttpMethod => HttpMethod.DELETE;

	internal override bool SupportsBody => false;

	internal override string OperationName => "security.delete_privileges";

	/// <summary>
	/// <para>
	/// If <c>true</c> (the default) then refresh the affected shards to make this operation visible to search, if <c>wait_for</c> then wait for a refresh to make this operation visible to search, if <c>false</c> then do nothing with refreshes.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Refresh? Refresh { get => Q<Elastic.Clients.Elasticsearch.Refresh?>("refresh"); set => Q("refresh", value); }
}

/// <summary>
/// <para>
/// Delete application privileges.
/// </para>
/// <para>
/// To use this API, you must have one of the following privileges:
/// </para>
/// <list type="bullet">
/// <item>
/// <para>
/// The <c>manage_security</c> cluster privilege (or a greater privilege such as <c>all</c>).
/// </para>
/// </item>
/// <item>
/// <para>
/// The "Manage Application Privileges" global privilege for the application being referenced in the request.
/// </para>
/// </item>
/// </list>
/// </summary>
public sealed partial class DeletePrivilegesRequestDescriptor : RequestDescriptor<DeletePrivilegesRequestDescriptor, DeletePrivilegesRequestParameters>
{
	internal DeletePrivilegesRequestDescriptor(Action<DeletePrivilegesRequestDescriptor> configure) => configure.Invoke(this);

	public DeletePrivilegesRequestDescriptor(Elastic.Clients.Elasticsearch.Name application, Elastic.Clients.Elasticsearch.Names name) : base(r => r.Required("application", application).Required("name", name))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.SecurityDeletePrivileges;

	protected override HttpMethod StaticHttpMethod => HttpMethod.DELETE;

	internal override bool SupportsBody => false;

	internal override string OperationName => "security.delete_privileges";

	public DeletePrivilegesRequestDescriptor Refresh(Elastic.Clients.Elasticsearch.Refresh? refresh) => Qs("refresh", refresh);

	public DeletePrivilegesRequestDescriptor Application(Elastic.Clients.Elasticsearch.Name application)
	{
		RouteValues.Required("application", application);
		return Self;
	}

	public DeletePrivilegesRequestDescriptor Name(Elastic.Clients.Elasticsearch.Names name)
	{
		RouteValues.Required("name", name);
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}