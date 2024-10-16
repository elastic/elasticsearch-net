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

public sealed partial class DeleteServiceTokenRequestParameters : RequestParameters
{
	/// <summary>
	/// <para>
	/// If <c>true</c> then refresh the affected shards to make this operation visible to search, if <c>wait_for</c> (the default) then wait for a refresh to make this operation visible to search, if <c>false</c> then do nothing with refreshes.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Refresh? Refresh { get => Q<Elastic.Clients.Elasticsearch.Refresh?>("refresh"); set => Q("refresh", value); }
}

/// <summary>
/// <para>
/// Deletes a service account token.
/// </para>
/// </summary>
public sealed partial class DeleteServiceTokenRequest : PlainRequest<DeleteServiceTokenRequestParameters>
{
	public DeleteServiceTokenRequest(string ns, string service, Elastic.Clients.Elasticsearch.Name name) : base(r => r.Required("namespace", ns).Required("service", service).Required("name", name))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.SecurityDeleteServiceToken;

	protected override HttpMethod StaticHttpMethod => HttpMethod.DELETE;

	internal override bool SupportsBody => false;

	internal override string OperationName => "security.delete_service_token";

	/// <summary>
	/// <para>
	/// If <c>true</c> then refresh the affected shards to make this operation visible to search, if <c>wait_for</c> (the default) then wait for a refresh to make this operation visible to search, if <c>false</c> then do nothing with refreshes.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Refresh? Refresh { get => Q<Elastic.Clients.Elasticsearch.Refresh?>("refresh"); set => Q("refresh", value); }
}

/// <summary>
/// <para>
/// Deletes a service account token.
/// </para>
/// </summary>
public sealed partial class DeleteServiceTokenRequestDescriptor : RequestDescriptor<DeleteServiceTokenRequestDescriptor, DeleteServiceTokenRequestParameters>
{
	internal DeleteServiceTokenRequestDescriptor(Action<DeleteServiceTokenRequestDescriptor> configure) => configure.Invoke(this);

	public DeleteServiceTokenRequestDescriptor(string ns, string service, Elastic.Clients.Elasticsearch.Name name) : base(r => r.Required("namespace", ns).Required("service", service).Required("name", name))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.SecurityDeleteServiceToken;

	protected override HttpMethod StaticHttpMethod => HttpMethod.DELETE;

	internal override bool SupportsBody => false;

	internal override string OperationName => "security.delete_service_token";

	public DeleteServiceTokenRequestDescriptor Refresh(Elastic.Clients.Elasticsearch.Refresh? refresh) => Qs("refresh", refresh);

	public DeleteServiceTokenRequestDescriptor Name(Elastic.Clients.Elasticsearch.Name name)
	{
		RouteValues.Required("name", name);
		return Self;
	}

	public DeleteServiceTokenRequestDescriptor Namespace(string ns)
	{
		RouteValues.Required("namespace", ns);
		return Self;
	}

	public DeleteServiceTokenRequestDescriptor Service(string service)
	{
		RouteValues.Required("service", service);
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}