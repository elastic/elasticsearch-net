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

public sealed partial class ExecuteLifecycleRequestParameters : RequestParameters
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
/// Run a policy.
/// Immediately create a snapshot according to the snapshot lifecycle policy without waiting for the scheduled time.
/// The snapshot policy is normally applied according to its schedule, but you might want to manually run a policy before performing an upgrade or other maintenance.
/// </para>
/// </summary>
public sealed partial class ExecuteLifecycleRequest : PlainRequest<ExecuteLifecycleRequestParameters>
{
	public ExecuteLifecycleRequest(Elastic.Clients.Elasticsearch.Name policyId) : base(r => r.Required("policy_id", policyId))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.SnapshotLifecycleManagementExecuteLifecycle;

	protected override HttpMethod StaticHttpMethod => HttpMethod.PUT;

	internal override bool SupportsBody => false;

	internal override string OperationName => "slm.execute_lifecycle";

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
/// Run a policy.
/// Immediately create a snapshot according to the snapshot lifecycle policy without waiting for the scheduled time.
/// The snapshot policy is normally applied according to its schedule, but you might want to manually run a policy before performing an upgrade or other maintenance.
/// </para>
/// </summary>
public sealed partial class ExecuteLifecycleRequestDescriptor : RequestDescriptor<ExecuteLifecycleRequestDescriptor, ExecuteLifecycleRequestParameters>
{
	internal ExecuteLifecycleRequestDescriptor(Action<ExecuteLifecycleRequestDescriptor> configure) => configure.Invoke(this);

	public ExecuteLifecycleRequestDescriptor(Elastic.Clients.Elasticsearch.Name policyId) : base(r => r.Required("policy_id", policyId))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.SnapshotLifecycleManagementExecuteLifecycle;

	protected override HttpMethod StaticHttpMethod => HttpMethod.PUT;

	internal override bool SupportsBody => false;

	internal override string OperationName => "slm.execute_lifecycle";

	public ExecuteLifecycleRequestDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Duration? masterTimeout) => Qs("master_timeout", masterTimeout);
	public ExecuteLifecycleRequestDescriptor Timeout(Elastic.Clients.Elasticsearch.Duration? timeout) => Qs("timeout", timeout);

	public ExecuteLifecycleRequestDescriptor PolicyId(Elastic.Clients.Elasticsearch.Name policyId)
	{
		RouteValues.Required("policy_id", policyId);
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}