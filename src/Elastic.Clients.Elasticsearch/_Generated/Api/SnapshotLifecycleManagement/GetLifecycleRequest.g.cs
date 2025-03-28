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

namespace Elastic.Clients.Elasticsearch.SnapshotLifecycleManagement;

public sealed partial class GetLifecycleRequestParameters : RequestParameters
{
	/// <summary>
	/// <para>
	/// The period to wait for a connection to the master node.
	/// If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }

	/// <summary>
	/// <para>
	/// The period to wait for a response.
	/// If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }
}

/// <summary>
/// <para>
/// Get policy information.
/// Get snapshot lifecycle policy definitions and information about the latest snapshot attempts.
/// </para>
/// </summary>
public sealed partial class GetLifecycleRequest : PlainRequest<GetLifecycleRequestParameters>
{
	public GetLifecycleRequest()
	{
	}

	public GetLifecycleRequest(Elastic.Clients.Elasticsearch.Names? policyId) : base(r => r.Optional("policy_id", policyId))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.SnapshotLifecycleManagementGetLifecycle;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "slm.get_lifecycle";

	/// <summary>
	/// <para>
	/// The period to wait for a connection to the master node.
	/// If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }

	/// <summary>
	/// <para>
	/// The period to wait for a response.
	/// If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }
}

/// <summary>
/// <para>
/// Get policy information.
/// Get snapshot lifecycle policy definitions and information about the latest snapshot attempts.
/// </para>
/// </summary>
public sealed partial class GetLifecycleRequestDescriptor : RequestDescriptor<GetLifecycleRequestDescriptor, GetLifecycleRequestParameters>
{
	internal GetLifecycleRequestDescriptor(Action<GetLifecycleRequestDescriptor> configure) => configure.Invoke(this);

	public GetLifecycleRequestDescriptor(Elastic.Clients.Elasticsearch.Names? policyId) : base(r => r.Optional("policy_id", policyId))
	{
	}

	public GetLifecycleRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.SnapshotLifecycleManagementGetLifecycle;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "slm.get_lifecycle";

	public GetLifecycleRequestDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Duration? masterTimeout) => Qs("master_timeout", masterTimeout);
	public GetLifecycleRequestDescriptor Timeout(Elastic.Clients.Elasticsearch.Duration? timeout) => Qs("timeout", timeout);

	public GetLifecycleRequestDescriptor PolicyId(Elastic.Clients.Elasticsearch.Names? policyId)
	{
		RouteValues.Optional("policy_id", policyId);
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}