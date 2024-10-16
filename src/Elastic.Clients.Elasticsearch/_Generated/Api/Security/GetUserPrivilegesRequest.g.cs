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

public sealed partial class GetUserPrivilegesRequestParameters : RequestParameters
{
	/// <summary>
	/// <para>
	/// The name of the application. Application privileges are always associated with exactly one application. If you do not specify this parameter, the API returns information about all privileges for all applications.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Name? Application { get => Q<Elastic.Clients.Elasticsearch.Name?>("application"); set => Q("application", value); }

	/// <summary>
	/// <para>
	/// The name of the privilege. If you do not specify this parameter, the API returns information about all privileges for the requested application.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Name? Priviledge { get => Q<Elastic.Clients.Elasticsearch.Name?>("priviledge"); set => Q("priviledge", value); }
	public Elastic.Clients.Elasticsearch.Name? Username { get => Q<Elastic.Clients.Elasticsearch.Name?>("username"); set => Q("username", value); }
}

/// <summary>
/// <para>
/// Retrieves security privileges for the logged in user.
/// </para>
/// </summary>
public sealed partial class GetUserPrivilegesRequest : PlainRequest<GetUserPrivilegesRequestParameters>
{
	internal override ApiUrls ApiUrls => ApiUrlLookup.SecurityGetUserPrivileges;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "security.get_user_privileges";

	/// <summary>
	/// <para>
	/// The name of the application. Application privileges are always associated with exactly one application. If you do not specify this parameter, the API returns information about all privileges for all applications.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Name? Application { get => Q<Elastic.Clients.Elasticsearch.Name?>("application"); set => Q("application", value); }

	/// <summary>
	/// <para>
	/// The name of the privilege. If you do not specify this parameter, the API returns information about all privileges for the requested application.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Name? Priviledge { get => Q<Elastic.Clients.Elasticsearch.Name?>("priviledge"); set => Q("priviledge", value); }
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Name? Username { get => Q<Elastic.Clients.Elasticsearch.Name?>("username"); set => Q("username", value); }
}

/// <summary>
/// <para>
/// Retrieves security privileges for the logged in user.
/// </para>
/// </summary>
public sealed partial class GetUserPrivilegesRequestDescriptor : RequestDescriptor<GetUserPrivilegesRequestDescriptor, GetUserPrivilegesRequestParameters>
{
	internal GetUserPrivilegesRequestDescriptor(Action<GetUserPrivilegesRequestDescriptor> configure) => configure.Invoke(this);

	public GetUserPrivilegesRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.SecurityGetUserPrivileges;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "security.get_user_privileges";

	public GetUserPrivilegesRequestDescriptor Application(Elastic.Clients.Elasticsearch.Name? application) => Qs("application", application);
	public GetUserPrivilegesRequestDescriptor Priviledge(Elastic.Clients.Elasticsearch.Name? priviledge) => Qs("priviledge", priviledge);
	public GetUserPrivilegesRequestDescriptor Username(Elastic.Clients.Elasticsearch.Name? username) => Qs("username", username);

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}