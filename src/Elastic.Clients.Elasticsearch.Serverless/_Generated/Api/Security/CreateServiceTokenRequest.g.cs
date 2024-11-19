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
using Elastic.Transport.Extensions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serverless.Security;

public sealed partial class CreateServiceTokenRequestParameters : RequestParameters
{
	/// <summary>
	/// <para>
	/// If <c>true</c> then refresh the affected shards to make this operation visible to search, if <c>wait_for</c> (the default) then wait for a refresh to make this operation visible to search, if <c>false</c> then do nothing with refreshes.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Serverless.Refresh? Refresh { get => Q<Elastic.Clients.Elasticsearch.Serverless.Refresh?>("refresh"); set => Q("refresh", value); }
}

/// <summary>
/// <para>
/// Create a service account token.
/// </para>
/// <para>
/// Create a service accounts token for access without requiring basic authentication.
/// </para>
/// </summary>
public sealed partial class CreateServiceTokenRequest : PlainRequest<CreateServiceTokenRequestParameters>
{
	public CreateServiceTokenRequest(string ns, string service, Elastic.Clients.Elasticsearch.Serverless.Name? name) : base(r => r.Required("namespace", ns).Required("service", service).Optional("name", name))
	{
	}

	public CreateServiceTokenRequest(string ns, string service) : base(r => r.Required("namespace", ns).Required("service", service))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.SecurityCreateServiceToken;

	protected override HttpMethod StaticHttpMethod => HttpMethod.PUT;

	internal override bool SupportsBody => false;

	internal override string OperationName => "security.create_service_token";

	/// <summary>
	/// <para>
	/// If <c>true</c> then refresh the affected shards to make this operation visible to search, if <c>wait_for</c> (the default) then wait for a refresh to make this operation visible to search, if <c>false</c> then do nothing with refreshes.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Serverless.Refresh? Refresh { get => Q<Elastic.Clients.Elasticsearch.Serverless.Refresh?>("refresh"); set => Q("refresh", value); }
}

/// <summary>
/// <para>
/// Create a service account token.
/// </para>
/// <para>
/// Create a service accounts token for access without requiring basic authentication.
/// </para>
/// </summary>
public sealed partial class CreateServiceTokenRequestDescriptor : RequestDescriptor<CreateServiceTokenRequestDescriptor, CreateServiceTokenRequestParameters>
{
	internal CreateServiceTokenRequestDescriptor(Action<CreateServiceTokenRequestDescriptor> configure) => configure.Invoke(this);

	public CreateServiceTokenRequestDescriptor(string ns, string service, Elastic.Clients.Elasticsearch.Serverless.Name? name) : base(r => r.Required("namespace", ns).Required("service", service).Optional("name", name))
	{
	}

	public CreateServiceTokenRequestDescriptor(string ns, string service) : base(r => r.Required("namespace", ns).Required("service", service))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.SecurityCreateServiceToken;

	protected override HttpMethod StaticHttpMethod => HttpMethod.PUT;

	internal override bool SupportsBody => false;

	internal override string OperationName => "security.create_service_token";

	public CreateServiceTokenRequestDescriptor Refresh(Elastic.Clients.Elasticsearch.Serverless.Refresh? refresh) => Qs("refresh", refresh);

	public CreateServiceTokenRequestDescriptor Name(Elastic.Clients.Elasticsearch.Serverless.Name? name)
	{
		RouteValues.Optional("name", name);
		return Self;
	}

	public CreateServiceTokenRequestDescriptor Namespace(string ns)
	{
		RouteValues.Required("namespace", ns);
		return Self;
	}

	public CreateServiceTokenRequestDescriptor Service(string service)
	{
		RouteValues.Required("service", service);
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}