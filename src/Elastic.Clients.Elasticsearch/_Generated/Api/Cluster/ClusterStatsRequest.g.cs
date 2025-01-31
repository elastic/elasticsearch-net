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

namespace Elastic.Clients.Elasticsearch.Cluster;

public sealed partial class ClusterStatsRequestParameters : RequestParameters
{
	/// <summary>
	/// <para>
	/// Include remote cluster data into the response
	/// </para>
	/// </summary>
	public bool? IncludeRemotes { get => Q<bool?>("include_remotes"); set => Q("include_remotes", value); }

	/// <summary>
	/// <para>
	/// Period to wait for each node to respond.
	/// If a node does not respond before its timeout expires, the response does not include its stats.
	/// However, timed out nodes are included in the response’s <c>_nodes.failed</c> property. Defaults to no timeout.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }
}

/// <summary>
/// <para>
/// Get cluster statistics.
/// Get basic index metrics (shard numbers, store size, memory usage) and information about the current nodes that form the cluster (number, roles, os, jvm versions, memory usage, cpu and installed plugins).
/// </para>
/// </summary>
public sealed partial class ClusterStatsRequest : PlainRequest<ClusterStatsRequestParameters>
{
	[JsonConstructor]
	public ClusterStatsRequest()
	{
	}

	public ClusterStatsRequest(Elastic.Clients.Elasticsearch.NodeIds? nodeId) : base(r => r.Optional("node_id", nodeId))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.ClusterStats;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "cluster.stats";

	/// <summary>
	/// <para>
	/// Comma-separated list of node filters used to limit returned information. Defaults to all nodes in the cluster.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.NodeIds? NodeId { get => P<Elastic.Clients.Elasticsearch.NodeIds?>("node_id"); set => PO("node_id", value); }

	/// <summary>
	/// <para>
	/// Include remote cluster data into the response
	/// </para>
	/// </summary>
	[JsonIgnore]
	public bool? IncludeRemotes { get => Q<bool?>("include_remotes"); set => Q("include_remotes", value); }

	/// <summary>
	/// <para>
	/// Period to wait for each node to respond.
	/// If a node does not respond before its timeout expires, the response does not include its stats.
	/// However, timed out nodes are included in the response’s <c>_nodes.failed</c> property. Defaults to no timeout.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }
}

/// <summary>
/// <para>
/// Get cluster statistics.
/// Get basic index metrics (shard numbers, store size, memory usage) and information about the current nodes that form the cluster (number, roles, os, jvm versions, memory usage, cpu and installed plugins).
/// </para>
/// </summary>
public sealed partial class ClusterStatsRequestDescriptor : RequestDescriptor<ClusterStatsRequestDescriptor, ClusterStatsRequestParameters>
{
	internal ClusterStatsRequestDescriptor(Action<ClusterStatsRequestDescriptor> configure) => configure.Invoke(this);

	public ClusterStatsRequestDescriptor(Elastic.Clients.Elasticsearch.NodeIds? nodeId) : base(r => r.Optional("node_id", nodeId))
	{
	}

	public ClusterStatsRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.ClusterStats;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "cluster.stats";

	public ClusterStatsRequestDescriptor IncludeRemotes(bool? includeRemotes = true) => Qs("include_remotes", includeRemotes);
	public ClusterStatsRequestDescriptor Timeout(Elastic.Clients.Elasticsearch.Duration? timeout) => Qs("timeout", timeout);

	public ClusterStatsRequestDescriptor NodeId(Elastic.Clients.Elasticsearch.NodeIds? nodeId)
	{
		RouteValues.Optional("node_id", nodeId);
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}