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

namespace Elastic.Clients.Elasticsearch.IndexLifecycleManagement;

public sealed partial class DeleteLifecycleRequestParameters : RequestParameters
{
	/// <summary>
	/// <para>
	/// Period to wait for a connection to the master node. If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }

	/// <summary>
	/// <para>
	/// Period to wait for a response. If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }
}

/// <summary>
/// <para>
/// Deletes the specified lifecycle policy definition. You cannot delete policies that are currently in use. If the policy is being used to manage any indices, the request fails and returns an error.
/// </para>
/// </summary>
public sealed partial class DeleteLifecycleRequest : PlainRequest<DeleteLifecycleRequestParameters>
{
	public DeleteLifecycleRequest(Elastic.Clients.Elasticsearch.Name name) : base(r => r.Required("policy", name))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.IndexLifecycleManagementDeleteLifecycle;

	protected override HttpMethod StaticHttpMethod => HttpMethod.DELETE;

	internal override bool SupportsBody => false;

	internal override string OperationName => "ilm.delete_lifecycle";

	/// <summary>
	/// <para>
	/// Period to wait for a connection to the master node. If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }

	/// <summary>
	/// <para>
	/// Period to wait for a response. If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }
}

/// <summary>
/// <para>
/// Deletes the specified lifecycle policy definition. You cannot delete policies that are currently in use. If the policy is being used to manage any indices, the request fails and returns an error.
/// </para>
/// </summary>
public sealed partial class DeleteLifecycleRequestDescriptor : RequestDescriptor<DeleteLifecycleRequestDescriptor, DeleteLifecycleRequestParameters>
{
	internal DeleteLifecycleRequestDescriptor(Action<DeleteLifecycleRequestDescriptor> configure) => configure.Invoke(this);

	public DeleteLifecycleRequestDescriptor(Elastic.Clients.Elasticsearch.Name name) : base(r => r.Required("policy", name))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.IndexLifecycleManagementDeleteLifecycle;

	protected override HttpMethod StaticHttpMethod => HttpMethod.DELETE;

	internal override bool SupportsBody => false;

	internal override string OperationName => "ilm.delete_lifecycle";

	public DeleteLifecycleRequestDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Duration? masterTimeout) => Qs("master_timeout", masterTimeout);
	public DeleteLifecycleRequestDescriptor Timeout(Elastic.Clients.Elasticsearch.Duration? timeout) => Qs("timeout", timeout);

	public DeleteLifecycleRequestDescriptor Name(Elastic.Clients.Elasticsearch.Name name)
	{
		RouteValues.Required("policy", name);
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}