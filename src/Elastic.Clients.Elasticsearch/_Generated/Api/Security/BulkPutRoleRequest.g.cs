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

public sealed partial class BulkPutRoleRequestParameters : RequestParameters
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
/// Bulk create or update roles.
/// </para>
/// <para>
/// The role management APIs are generally the preferred way to manage roles, rather than using file-based role management.
/// The bulk create or update roles API cannot update roles that are defined in roles files.
/// </para>
/// </summary>
public sealed partial class BulkPutRoleRequest : PlainRequest<BulkPutRoleRequestParameters>
{
	internal override ApiUrls ApiUrls => ApiUrlLookup.SecurityBulkPutRole;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "security.bulk_put_role";

	/// <summary>
	/// <para>
	/// If <c>true</c> (the default) then refresh the affected shards to make this operation visible to search, if <c>wait_for</c> then wait for a refresh to make this operation visible to search, if <c>false</c> then do nothing with refreshes.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Refresh? Refresh { get => Q<Elastic.Clients.Elasticsearch.Refresh?>("refresh"); set => Q("refresh", value); }

	/// <summary>
	/// <para>
	/// A dictionary of role name to RoleDescriptor objects to add or update
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("roles")]
	public IDictionary<string, Elastic.Clients.Elasticsearch.Security.RoleDescriptor> Roles { get; set; }
}

/// <summary>
/// <para>
/// Bulk create or update roles.
/// </para>
/// <para>
/// The role management APIs are generally the preferred way to manage roles, rather than using file-based role management.
/// The bulk create or update roles API cannot update roles that are defined in roles files.
/// </para>
/// </summary>
public sealed partial class BulkPutRoleRequestDescriptor<TDocument> : RequestDescriptor<BulkPutRoleRequestDescriptor<TDocument>, BulkPutRoleRequestParameters>
{
	internal BulkPutRoleRequestDescriptor(Action<BulkPutRoleRequestDescriptor<TDocument>> configure) => configure.Invoke(this);

	public BulkPutRoleRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.SecurityBulkPutRole;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "security.bulk_put_role";

	public BulkPutRoleRequestDescriptor<TDocument> Refresh(Elastic.Clients.Elasticsearch.Refresh? refresh) => Qs("refresh", refresh);

	private IDictionary<string, Elastic.Clients.Elasticsearch.Security.RoleDescriptorDescriptor<TDocument>> RolesValue { get; set; }

	/// <summary>
	/// <para>
	/// A dictionary of role name to RoleDescriptor objects to add or update
	/// </para>
	/// </summary>
	public BulkPutRoleRequestDescriptor<TDocument> Roles(Func<FluentDescriptorDictionary<string, Elastic.Clients.Elasticsearch.Security.RoleDescriptorDescriptor<TDocument>>, FluentDescriptorDictionary<string, Elastic.Clients.Elasticsearch.Security.RoleDescriptorDescriptor<TDocument>>> selector)
	{
		RolesValue = selector?.Invoke(new FluentDescriptorDictionary<string, Elastic.Clients.Elasticsearch.Security.RoleDescriptorDescriptor<TDocument>>());
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		writer.WritePropertyName("roles");
		JsonSerializer.Serialize(writer, RolesValue, options);
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Bulk create or update roles.
/// </para>
/// <para>
/// The role management APIs are generally the preferred way to manage roles, rather than using file-based role management.
/// The bulk create or update roles API cannot update roles that are defined in roles files.
/// </para>
/// </summary>
public sealed partial class BulkPutRoleRequestDescriptor : RequestDescriptor<BulkPutRoleRequestDescriptor, BulkPutRoleRequestParameters>
{
	internal BulkPutRoleRequestDescriptor(Action<BulkPutRoleRequestDescriptor> configure) => configure.Invoke(this);

	public BulkPutRoleRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.SecurityBulkPutRole;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "security.bulk_put_role";

	public BulkPutRoleRequestDescriptor Refresh(Elastic.Clients.Elasticsearch.Refresh? refresh) => Qs("refresh", refresh);

	private IDictionary<string, Elastic.Clients.Elasticsearch.Security.RoleDescriptorDescriptor> RolesValue { get; set; }

	/// <summary>
	/// <para>
	/// A dictionary of role name to RoleDescriptor objects to add or update
	/// </para>
	/// </summary>
	public BulkPutRoleRequestDescriptor Roles(Func<FluentDescriptorDictionary<string, Elastic.Clients.Elasticsearch.Security.RoleDescriptorDescriptor>, FluentDescriptorDictionary<string, Elastic.Clients.Elasticsearch.Security.RoleDescriptorDescriptor>> selector)
	{
		RolesValue = selector?.Invoke(new FluentDescriptorDictionary<string, Elastic.Clients.Elasticsearch.Security.RoleDescriptorDescriptor>());
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		writer.WritePropertyName("roles");
		JsonSerializer.Serialize(writer, RolesValue, options);
		writer.WriteEndObject();
	}
}