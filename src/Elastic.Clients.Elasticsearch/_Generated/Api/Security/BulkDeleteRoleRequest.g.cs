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

public sealed partial class BulkDeleteRoleRequestParameters : RequestParameters
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
/// Bulk delete roles.
/// </para>
/// <para>
/// The role management APIs are generally the preferred way to manage roles, rather than using file-based role management.
/// The bulk delete roles API cannot delete roles that are defined in roles files.
/// </para>
/// </summary>
public sealed partial class BulkDeleteRoleRequest : PlainRequest<BulkDeleteRoleRequestParameters>
{
	internal override ApiUrls ApiUrls => ApiUrlLookup.SecurityBulkDeleteRole;

	protected override HttpMethod StaticHttpMethod => HttpMethod.DELETE;

	internal override bool SupportsBody => true;

	internal override string OperationName => "security.bulk_delete_role";

	/// <summary>
	/// <para>
	/// If <c>true</c> (the default) then refresh the affected shards to make this operation visible to search, if <c>wait_for</c> then wait for a refresh to make this operation visible to search, if <c>false</c> then do nothing with refreshes.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Refresh? Refresh { get => Q<Elastic.Clients.Elasticsearch.Refresh?>("refresh"); set => Q("refresh", value); }

	/// <summary>
	/// <para>
	/// An array of role names to delete
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("names")]
	public ICollection<string> Names { get; set; }
}

/// <summary>
/// <para>
/// Bulk delete roles.
/// </para>
/// <para>
/// The role management APIs are generally the preferred way to manage roles, rather than using file-based role management.
/// The bulk delete roles API cannot delete roles that are defined in roles files.
/// </para>
/// </summary>
public sealed partial class BulkDeleteRoleRequestDescriptor : RequestDescriptor<BulkDeleteRoleRequestDescriptor, BulkDeleteRoleRequestParameters>
{
	internal BulkDeleteRoleRequestDescriptor(Action<BulkDeleteRoleRequestDescriptor> configure) => configure.Invoke(this);

	public BulkDeleteRoleRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.SecurityBulkDeleteRole;

	protected override HttpMethod StaticHttpMethod => HttpMethod.DELETE;

	internal override bool SupportsBody => true;

	internal override string OperationName => "security.bulk_delete_role";

	public BulkDeleteRoleRequestDescriptor Refresh(Elastic.Clients.Elasticsearch.Refresh? refresh) => Qs("refresh", refresh);

	private ICollection<string> NamesValue { get; set; }

	/// <summary>
	/// <para>
	/// An array of role names to delete
	/// </para>
	/// </summary>
	public BulkDeleteRoleRequestDescriptor Names(ICollection<string> names)
	{
		NamesValue = names;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		writer.WritePropertyName("names");
		JsonSerializer.Serialize(writer, NamesValue, options);
		writer.WriteEndObject();
	}
}