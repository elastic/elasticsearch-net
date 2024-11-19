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

public sealed partial class DeleteRoleMappingRequestParameters : RequestParameters
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
/// Delete role mappings.
/// </para>
/// </summary>
public sealed partial class DeleteRoleMappingRequest : PlainRequest<DeleteRoleMappingRequestParameters>
{
	public DeleteRoleMappingRequest(Elastic.Clients.Elasticsearch.Name name) : base(r => r.Required("name", name))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.SecurityDeleteRoleMapping;

	protected override HttpMethod StaticHttpMethod => HttpMethod.DELETE;

	internal override bool SupportsBody => false;

	internal override string OperationName => "security.delete_role_mapping";

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
/// Delete role mappings.
/// </para>
/// </summary>
public sealed partial class DeleteRoleMappingRequestDescriptor : RequestDescriptor<DeleteRoleMappingRequestDescriptor, DeleteRoleMappingRequestParameters>
{
	internal DeleteRoleMappingRequestDescriptor(Action<DeleteRoleMappingRequestDescriptor> configure) => configure.Invoke(this);

	public DeleteRoleMappingRequestDescriptor(Elastic.Clients.Elasticsearch.Name name) : base(r => r.Required("name", name))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.SecurityDeleteRoleMapping;

	protected override HttpMethod StaticHttpMethod => HttpMethod.DELETE;

	internal override bool SupportsBody => false;

	internal override string OperationName => "security.delete_role_mapping";

	public DeleteRoleMappingRequestDescriptor Refresh(Elastic.Clients.Elasticsearch.Refresh? refresh) => Qs("refresh", refresh);

	public DeleteRoleMappingRequestDescriptor Name(Elastic.Clients.Elasticsearch.Name name)
	{
		RouteValues.Required("name", name);
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}