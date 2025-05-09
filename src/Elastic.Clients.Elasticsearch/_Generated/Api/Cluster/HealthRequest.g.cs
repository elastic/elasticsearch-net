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

using System;
using System.Linq;
using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch.Cluster;

public sealed partial class HealthRequestParameters : Elastic.Transport.RequestParameters
{
	/// <summary>
	/// <para>
	/// Whether to expand wildcard expression to concrete indices that are open, closed or both.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? ExpandWildcards { get => Q<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>?>("expand_wildcards"); set => Q("expand_wildcards", value); }

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
	public Elastic.Clients.Elasticsearch.Cluster.WaitForNodes? WaitForNodes { get => Q<Elastic.Clients.Elasticsearch.Cluster.WaitForNodes?>("wait_for_nodes"); set => Q("wait_for_nodes", value); }

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

internal sealed partial class HealthRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Cluster.HealthRequest>
{
	public override Elastic.Clients.Elasticsearch.Cluster.HealthRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (options.UnmappedMemberHandling is System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Elastic.Clients.Elasticsearch.Cluster.HealthRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Cluster.HealthRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Get the cluster health status.
/// </para>
/// <para>
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
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Cluster.HealthRequestConverter))]
public sealed partial class HealthRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.Cluster.HealthRequestParameters>
{
	public HealthRequest(Elastic.Clients.Elasticsearch.Indices? indices) : base(r => r.Optional("index", indices))
	{
	}
#if NET7_0_OR_GREATER
	public HealthRequest()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public HealthRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal HealthRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.ClusterHealth;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "cluster.health";

	/// <summary>
	/// <para>
	/// Comma-separated list of data streams, indices, and index aliases used to limit the request. Wildcard expressions (<c>*</c>) are supported. To target all data streams and indices in a cluster, omit this parameter or use _all or <c>*</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Indices? Indices { get => P<Elastic.Clients.Elasticsearch.Indices?>("index"); set => PO("index", value); }

	/// <summary>
	/// <para>
	/// Whether to expand wildcard expression to concrete indices that are open, closed or both.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? ExpandWildcards { get => Q<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>?>("expand_wildcards"); set => Q("expand_wildcards", value); }

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
	public Elastic.Clients.Elasticsearch.Cluster.WaitForNodes? WaitForNodes { get => Q<Elastic.Clients.Elasticsearch.Cluster.WaitForNodes?>("wait_for_nodes"); set => Q("wait_for_nodes", value); }

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
/// </para>
/// <para>
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
public readonly partial struct HealthRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.Cluster.HealthRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public HealthRequestDescriptor(Elastic.Clients.Elasticsearch.Cluster.HealthRequest instance)
	{
		Instance = instance;
	}

	public HealthRequestDescriptor(Elastic.Clients.Elasticsearch.Indices? indices)
	{
		Instance = new Elastic.Clients.Elasticsearch.Cluster.HealthRequest(indices);
	}

	public HealthRequestDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Cluster.HealthRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Cluster.HealthRequestDescriptor(Elastic.Clients.Elasticsearch.Cluster.HealthRequest instance) => new Elastic.Clients.Elasticsearch.Cluster.HealthRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Cluster.HealthRequest(Elastic.Clients.Elasticsearch.Cluster.HealthRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Comma-separated list of data streams, indices, and index aliases used to limit the request. Wildcard expressions (<c>*</c>) are supported. To target all data streams and indices in a cluster, omit this parameter or use _all or <c>*</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Cluster.HealthRequestDescriptor Indices(Elastic.Clients.Elasticsearch.Indices? value)
	{
		Instance.Indices = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Whether to expand wildcard expression to concrete indices that are open, closed or both.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Cluster.HealthRequestDescriptor ExpandWildcards(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? value)
	{
		Instance.ExpandWildcards = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Whether to expand wildcard expression to concrete indices that are open, closed or both.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Cluster.HealthRequestDescriptor ExpandWildcards(params Elastic.Clients.Elasticsearch.ExpandWildcard[] values)
	{
		Instance.ExpandWildcards = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// Can be one of cluster, indices or shards. Controls the details level of the health information returned.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Cluster.HealthRequestDescriptor Level(Elastic.Clients.Elasticsearch.Level? value)
	{
		Instance.Level = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If true, the request retrieves information from the local node only. Defaults to false, which means information is retrieved from the master node.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Cluster.HealthRequestDescriptor Local(bool? value = true)
	{
		Instance.Local = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Period to wait for a connection to the master node. If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Cluster.HealthRequestDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.MasterTimeout = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Period to wait for a response. If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Cluster.HealthRequestDescriptor Timeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.Timeout = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A number controlling to how many active shards to wait for, all to wait for all shards in the cluster to be active, or 0 to not wait.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Cluster.HealthRequestDescriptor WaitForActiveShards(Elastic.Clients.Elasticsearch.WaitForActiveShards? value)
	{
		Instance.WaitForActiveShards = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Can be one of immediate, urgent, high, normal, low, languid. Wait until all currently queued events with the given priority are processed.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Cluster.HealthRequestDescriptor WaitForEvents(Elastic.Clients.Elasticsearch.WaitForEvents? value)
	{
		Instance.WaitForEvents = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The request waits until the specified number N of nodes is available. It also accepts >=N, &lt;=N, >N and &lt;N. Alternatively, it is possible to use ge(N), le(N), gt(N) and lt(N) notation.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Cluster.HealthRequestDescriptor WaitForNodes(Elastic.Clients.Elasticsearch.Cluster.WaitForNodes? value)
	{
		Instance.WaitForNodes = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A boolean value which controls whether to wait (until the timeout provided) for the cluster to have no shard initializations. Defaults to false, which means it will not wait for initializing shards.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Cluster.HealthRequestDescriptor WaitForNoInitializingShards(bool? value = true)
	{
		Instance.WaitForNoInitializingShards = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A boolean value which controls whether to wait (until the timeout provided) for the cluster to have no shard relocations. Defaults to false, which means it will not wait for relocating shards.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Cluster.HealthRequestDescriptor WaitForNoRelocatingShards(bool? value = true)
	{
		Instance.WaitForNoRelocatingShards = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// One of green, yellow or red. Will wait (until the timeout provided) until the status of the cluster changes to the one provided or better, i.e. green > yellow > red. By default, will not wait for any status.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Cluster.HealthRequestDescriptor WaitForStatus(Elastic.Clients.Elasticsearch.HealthStatus? value)
	{
		Instance.WaitForStatus = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Cluster.HealthRequest Build(System.Action<Elastic.Clients.Elasticsearch.Cluster.HealthRequestDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Cluster.HealthRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Cluster.HealthRequestDescriptor(new Elastic.Clients.Elasticsearch.Cluster.HealthRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Cluster.HealthRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.HealthRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.HealthRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.HealthRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.HealthRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.HealthRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.HealthRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}

/// <summary>
/// <para>
/// Get the cluster health status.
/// </para>
/// <para>
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
public readonly partial struct HealthRequestDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.Cluster.HealthRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public HealthRequestDescriptor(Elastic.Clients.Elasticsearch.Cluster.HealthRequest instance)
	{
		Instance = instance;
	}

	public HealthRequestDescriptor(Elastic.Clients.Elasticsearch.Indices? indices)
	{
		Instance = new Elastic.Clients.Elasticsearch.Cluster.HealthRequest(indices);
	}

	public HealthRequestDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Cluster.HealthRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Cluster.HealthRequestDescriptor<TDocument>(Elastic.Clients.Elasticsearch.Cluster.HealthRequest instance) => new Elastic.Clients.Elasticsearch.Cluster.HealthRequestDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Cluster.HealthRequest(Elastic.Clients.Elasticsearch.Cluster.HealthRequestDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Comma-separated list of data streams, indices, and index aliases used to limit the request. Wildcard expressions (<c>*</c>) are supported. To target all data streams and indices in a cluster, omit this parameter or use _all or <c>*</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Cluster.HealthRequestDescriptor<TDocument> Indices(Elastic.Clients.Elasticsearch.Indices? value)
	{
		Instance.Indices = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Whether to expand wildcard expression to concrete indices that are open, closed or both.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Cluster.HealthRequestDescriptor<TDocument> ExpandWildcards(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? value)
	{
		Instance.ExpandWildcards = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Whether to expand wildcard expression to concrete indices that are open, closed or both.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Cluster.HealthRequestDescriptor<TDocument> ExpandWildcards(params Elastic.Clients.Elasticsearch.ExpandWildcard[] values)
	{
		Instance.ExpandWildcards = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// Can be one of cluster, indices or shards. Controls the details level of the health information returned.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Cluster.HealthRequestDescriptor<TDocument> Level(Elastic.Clients.Elasticsearch.Level? value)
	{
		Instance.Level = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If true, the request retrieves information from the local node only. Defaults to false, which means information is retrieved from the master node.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Cluster.HealthRequestDescriptor<TDocument> Local(bool? value = true)
	{
		Instance.Local = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Period to wait for a connection to the master node. If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Cluster.HealthRequestDescriptor<TDocument> MasterTimeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.MasterTimeout = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Period to wait for a response. If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Cluster.HealthRequestDescriptor<TDocument> Timeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.Timeout = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A number controlling to how many active shards to wait for, all to wait for all shards in the cluster to be active, or 0 to not wait.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Cluster.HealthRequestDescriptor<TDocument> WaitForActiveShards(Elastic.Clients.Elasticsearch.WaitForActiveShards? value)
	{
		Instance.WaitForActiveShards = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Can be one of immediate, urgent, high, normal, low, languid. Wait until all currently queued events with the given priority are processed.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Cluster.HealthRequestDescriptor<TDocument> WaitForEvents(Elastic.Clients.Elasticsearch.WaitForEvents? value)
	{
		Instance.WaitForEvents = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The request waits until the specified number N of nodes is available. It also accepts >=N, &lt;=N, >N and &lt;N. Alternatively, it is possible to use ge(N), le(N), gt(N) and lt(N) notation.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Cluster.HealthRequestDescriptor<TDocument> WaitForNodes(Elastic.Clients.Elasticsearch.Cluster.WaitForNodes? value)
	{
		Instance.WaitForNodes = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A boolean value which controls whether to wait (until the timeout provided) for the cluster to have no shard initializations. Defaults to false, which means it will not wait for initializing shards.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Cluster.HealthRequestDescriptor<TDocument> WaitForNoInitializingShards(bool? value = true)
	{
		Instance.WaitForNoInitializingShards = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A boolean value which controls whether to wait (until the timeout provided) for the cluster to have no shard relocations. Defaults to false, which means it will not wait for relocating shards.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Cluster.HealthRequestDescriptor<TDocument> WaitForNoRelocatingShards(bool? value = true)
	{
		Instance.WaitForNoRelocatingShards = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// One of green, yellow or red. Will wait (until the timeout provided) until the status of the cluster changes to the one provided or better, i.e. green > yellow > red. By default, will not wait for any status.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Cluster.HealthRequestDescriptor<TDocument> WaitForStatus(Elastic.Clients.Elasticsearch.HealthStatus? value)
	{
		Instance.WaitForStatus = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Cluster.HealthRequest Build(System.Action<Elastic.Clients.Elasticsearch.Cluster.HealthRequestDescriptor<TDocument>>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Cluster.HealthRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Cluster.HealthRequestDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.Cluster.HealthRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Cluster.HealthRequestDescriptor<TDocument> ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.HealthRequestDescriptor<TDocument> FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.HealthRequestDescriptor<TDocument> Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.HealthRequestDescriptor<TDocument> Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.HealthRequestDescriptor<TDocument> SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.HealthRequestDescriptor<TDocument> RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.HealthRequestDescriptor<TDocument> RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}