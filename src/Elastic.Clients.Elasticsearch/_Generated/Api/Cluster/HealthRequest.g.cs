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

public sealed partial class HealthRequestParameters : RequestParameters
{
	/// <summary>
	/// <para>
	/// Whether to expand wildcard expression to concrete indices that are open, closed or both.
	/// </para>
	/// </summary>
	public ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? ExpandWildcards { get => Q<ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>?>("expand_wildcards"); set => Q("expand_wildcards", value); }

	/// <summary>
	/// <para>
	/// Can be one of cluster, indices or shards. Controls the details level of the health information returned.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Level? Level { get => Q<Elastic.Clients.Elasticsearch.Level?>("level"); set => Q("level", value); }

	/// <summary>
	/// <para>
	/// If true, the request retrieves information from the local node only. Defaults to false, which means information is retrieved from the master node.
	/// </para>
	/// </summary>
	public bool? Local { get => Q<bool?>("local"); set => Q("local", value); }

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

	/// <summary>
	/// <para>
	/// A number controlling to how many active shards to wait for, all to wait for all shards in the cluster to be active, or 0 to not wait.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.WaitForActiveShards? WaitForActiveShards { get => Q<Elastic.Clients.Elasticsearch.WaitForActiveShards?>("wait_for_active_shards"); set => Q("wait_for_active_shards", value); }

	/// <summary>
	/// <para>
	/// Can be one of immediate, urgent, high, normal, low, languid. Wait until all currently queued events with the given priority are processed.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.WaitForEvents? WaitForEvents { get => Q<Elastic.Clients.Elasticsearch.WaitForEvents?>("wait_for_events"); set => Q("wait_for_events", value); }

	/// <summary>
	/// <para>
	/// The request waits until the specified number N of nodes is available. It also accepts >=N, &lt;=N, >N and &lt;N. Alternatively, it is possible to use ge(N), le(N), gt(N) and lt(N) notation.
	/// </para>
	/// </summary>
	public object? WaitForNodes { get => Q<object?>("wait_for_nodes"); set => Q("wait_for_nodes", value); }

	/// <summary>
	/// <para>
	/// A boolean value which controls whether to wait (until the timeout provided) for the cluster to have no shard initializations. Defaults to false, which means it will not wait for initializing shards.
	/// </para>
	/// </summary>
	public bool? WaitForNoInitializingShards { get => Q<bool?>("wait_for_no_initializing_shards"); set => Q("wait_for_no_initializing_shards", value); }

	/// <summary>
	/// <para>
	/// A boolean value which controls whether to wait (until the timeout provided) for the cluster to have no shard relocations. Defaults to false, which means it will not wait for relocating shards.
	/// </para>
	/// </summary>
	public bool? WaitForNoRelocatingShards { get => Q<bool?>("wait_for_no_relocating_shards"); set => Q("wait_for_no_relocating_shards", value); }

	/// <summary>
	/// <para>
	/// One of green, yellow or red. Will wait (until the timeout provided) until the status of the cluster changes to the one provided or better, i.e. green > yellow > red. By default, will not wait for any status.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.HealthStatus? WaitForStatus { get => Q<Elastic.Clients.Elasticsearch.HealthStatus?>("wait_for_status"); set => Q("wait_for_status", value); }
}

/// <summary>
/// <para>
/// Get the cluster health status.
/// You can also use the API to get the health status of only specified data streams and indices.
/// For data streams, the API retrieves the health status of the stream’s backing indices.
/// </para>
/// <para>
/// The cluster health status is: green, yellow or red.
/// On the shard level, a red status indicates that the specific shard is not allocated in the cluster. Yellow means that the primary shard is allocated but replicas are not. Green means that all shards are allocated.
/// The index level status is controlled by the worst shard status.
/// </para>
/// <para>
/// One of the main benefits of the API is the ability to wait until the cluster reaches a certain high watermark health level.
/// The cluster status is controlled by the worst index status.
/// </para>
/// </summary>
public sealed partial class HealthRequest : PlainRequest<HealthRequestParameters>
{
	[JsonConstructor]
	public HealthRequest()
	{
	}

	public HealthRequest(Elastic.Clients.Elasticsearch.Indices? indices) : base(r => r.Optional("index", indices))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.ClusterHealth;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "cluster.health";

	/// <summary>
	/// <para>
	/// Comma-separated list of data streams, indices, and index aliases used to limit the request. Wildcard expressions (<c>*</c>) are supported. To target all data streams and indices in a cluster, omit this parameter or use _all or <c>*</c>.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Indices? Indices { get => P<Elastic.Clients.Elasticsearch.Indices?>("index"); set => PO("index", value); }

	/// <summary>
	/// <para>
	/// Whether to expand wildcard expression to concrete indices that are open, closed or both.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? ExpandWildcards { get => Q<ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>?>("expand_wildcards"); set => Q("expand_wildcards", value); }

	/// <summary>
	/// <para>
	/// Can be one of cluster, indices or shards. Controls the details level of the health information returned.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Level? Level { get => Q<Elastic.Clients.Elasticsearch.Level?>("level"); set => Q("level", value); }

	/// <summary>
	/// <para>
	/// If true, the request retrieves information from the local node only. Defaults to false, which means information is retrieved from the master node.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public bool? Local { get => Q<bool?>("local"); set => Q("local", value); }

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

	/// <summary>
	/// <para>
	/// A number controlling to how many active shards to wait for, all to wait for all shards in the cluster to be active, or 0 to not wait.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.WaitForActiveShards? WaitForActiveShards { get => Q<Elastic.Clients.Elasticsearch.WaitForActiveShards?>("wait_for_active_shards"); set => Q("wait_for_active_shards", value); }

	/// <summary>
	/// <para>
	/// Can be one of immediate, urgent, high, normal, low, languid. Wait until all currently queued events with the given priority are processed.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.WaitForEvents? WaitForEvents { get => Q<Elastic.Clients.Elasticsearch.WaitForEvents?>("wait_for_events"); set => Q("wait_for_events", value); }

	/// <summary>
	/// <para>
	/// The request waits until the specified number N of nodes is available. It also accepts >=N, &lt;=N, >N and &lt;N. Alternatively, it is possible to use ge(N), le(N), gt(N) and lt(N) notation.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public object? WaitForNodes { get => Q<object?>("wait_for_nodes"); set => Q("wait_for_nodes", value); }

	/// <summary>
	/// <para>
	/// A boolean value which controls whether to wait (until the timeout provided) for the cluster to have no shard initializations. Defaults to false, which means it will not wait for initializing shards.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public bool? WaitForNoInitializingShards { get => Q<bool?>("wait_for_no_initializing_shards"); set => Q("wait_for_no_initializing_shards", value); }

	/// <summary>
	/// <para>
	/// A boolean value which controls whether to wait (until the timeout provided) for the cluster to have no shard relocations. Defaults to false, which means it will not wait for relocating shards.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public bool? WaitForNoRelocatingShards { get => Q<bool?>("wait_for_no_relocating_shards"); set => Q("wait_for_no_relocating_shards", value); }

	/// <summary>
	/// <para>
	/// One of green, yellow or red. Will wait (until the timeout provided) until the status of the cluster changes to the one provided or better, i.e. green > yellow > red. By default, will not wait for any status.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.HealthStatus? WaitForStatus { get => Q<Elastic.Clients.Elasticsearch.HealthStatus?>("wait_for_status"); set => Q("wait_for_status", value); }
}

/// <summary>
/// <para>
/// Get the cluster health status.
/// You can also use the API to get the health status of only specified data streams and indices.
/// For data streams, the API retrieves the health status of the stream’s backing indices.
/// </para>
/// <para>
/// The cluster health status is: green, yellow or red.
/// On the shard level, a red status indicates that the specific shard is not allocated in the cluster. Yellow means that the primary shard is allocated but replicas are not. Green means that all shards are allocated.
/// The index level status is controlled by the worst shard status.
/// </para>
/// <para>
/// One of the main benefits of the API is the ability to wait until the cluster reaches a certain high watermark health level.
/// The cluster status is controlled by the worst index status.
/// </para>
/// </summary>
public sealed partial class HealthRequestDescriptor<TDocument> : RequestDescriptor<HealthRequestDescriptor<TDocument>, HealthRequestParameters>
{
	internal HealthRequestDescriptor(Action<HealthRequestDescriptor<TDocument>> configure) => configure.Invoke(this);

	public HealthRequestDescriptor(Elastic.Clients.Elasticsearch.Indices? indices) : base(r => r.Optional("index", indices))
	{
	}

	public HealthRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.ClusterHealth;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "cluster.health";

	public HealthRequestDescriptor<TDocument> ExpandWildcards(ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? expandWildcards) => Qs("expand_wildcards", expandWildcards);
	public HealthRequestDescriptor<TDocument> Level(Elastic.Clients.Elasticsearch.Level? level) => Qs("level", level);
	public HealthRequestDescriptor<TDocument> Local(bool? local = true) => Qs("local", local);
	public HealthRequestDescriptor<TDocument> MasterTimeout(Elastic.Clients.Elasticsearch.Duration? masterTimeout) => Qs("master_timeout", masterTimeout);
	public HealthRequestDescriptor<TDocument> Timeout(Elastic.Clients.Elasticsearch.Duration? timeout) => Qs("timeout", timeout);
	public HealthRequestDescriptor<TDocument> WaitForActiveShards(Elastic.Clients.Elasticsearch.WaitForActiveShards? waitForActiveShards) => Qs("wait_for_active_shards", waitForActiveShards);
	public HealthRequestDescriptor<TDocument> WaitForEvents(Elastic.Clients.Elasticsearch.WaitForEvents? waitForEvents) => Qs("wait_for_events", waitForEvents);
	public HealthRequestDescriptor<TDocument> WaitForNodes(object? waitForNodes) => Qs("wait_for_nodes", waitForNodes);
	public HealthRequestDescriptor<TDocument> WaitForNoInitializingShards(bool? waitForNoInitializingShards = true) => Qs("wait_for_no_initializing_shards", waitForNoInitializingShards);
	public HealthRequestDescriptor<TDocument> WaitForNoRelocatingShards(bool? waitForNoRelocatingShards = true) => Qs("wait_for_no_relocating_shards", waitForNoRelocatingShards);
	public HealthRequestDescriptor<TDocument> WaitForStatus(Elastic.Clients.Elasticsearch.HealthStatus? waitForStatus) => Qs("wait_for_status", waitForStatus);

	public HealthRequestDescriptor<TDocument> Indices(Elastic.Clients.Elasticsearch.Indices? indices)
	{
		RouteValues.Optional("index", indices);
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}

/// <summary>
/// <para>
/// Get the cluster health status.
/// You can also use the API to get the health status of only specified data streams and indices.
/// For data streams, the API retrieves the health status of the stream’s backing indices.
/// </para>
/// <para>
/// The cluster health status is: green, yellow or red.
/// On the shard level, a red status indicates that the specific shard is not allocated in the cluster. Yellow means that the primary shard is allocated but replicas are not. Green means that all shards are allocated.
/// The index level status is controlled by the worst shard status.
/// </para>
/// <para>
/// One of the main benefits of the API is the ability to wait until the cluster reaches a certain high watermark health level.
/// The cluster status is controlled by the worst index status.
/// </para>
/// </summary>
public sealed partial class HealthRequestDescriptor : RequestDescriptor<HealthRequestDescriptor, HealthRequestParameters>
{
	internal HealthRequestDescriptor(Action<HealthRequestDescriptor> configure) => configure.Invoke(this);

	public HealthRequestDescriptor(Elastic.Clients.Elasticsearch.Indices? indices) : base(r => r.Optional("index", indices))
	{
	}

	public HealthRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.ClusterHealth;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "cluster.health";

	public HealthRequestDescriptor ExpandWildcards(ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? expandWildcards) => Qs("expand_wildcards", expandWildcards);
	public HealthRequestDescriptor Level(Elastic.Clients.Elasticsearch.Level? level) => Qs("level", level);
	public HealthRequestDescriptor Local(bool? local = true) => Qs("local", local);
	public HealthRequestDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Duration? masterTimeout) => Qs("master_timeout", masterTimeout);
	public HealthRequestDescriptor Timeout(Elastic.Clients.Elasticsearch.Duration? timeout) => Qs("timeout", timeout);
	public HealthRequestDescriptor WaitForActiveShards(Elastic.Clients.Elasticsearch.WaitForActiveShards? waitForActiveShards) => Qs("wait_for_active_shards", waitForActiveShards);
	public HealthRequestDescriptor WaitForEvents(Elastic.Clients.Elasticsearch.WaitForEvents? waitForEvents) => Qs("wait_for_events", waitForEvents);
	public HealthRequestDescriptor WaitForNodes(object? waitForNodes) => Qs("wait_for_nodes", waitForNodes);
	public HealthRequestDescriptor WaitForNoInitializingShards(bool? waitForNoInitializingShards = true) => Qs("wait_for_no_initializing_shards", waitForNoInitializingShards);
	public HealthRequestDescriptor WaitForNoRelocatingShards(bool? waitForNoRelocatingShards = true) => Qs("wait_for_no_relocating_shards", waitForNoRelocatingShards);
	public HealthRequestDescriptor WaitForStatus(Elastic.Clients.Elasticsearch.HealthStatus? waitForStatus) => Qs("wait_for_status", waitForStatus);

	public HealthRequestDescriptor Indices(Elastic.Clients.Elasticsearch.Indices? indices)
	{
		RouteValues.Optional("index", indices);
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}