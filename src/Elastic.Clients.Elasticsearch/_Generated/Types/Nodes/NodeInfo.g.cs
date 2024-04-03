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
using Elastic.Clients.Elasticsearch.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Nodes;

public sealed partial class NodeInfo
{
	[JsonInclude, JsonPropertyName("aggregations")]
	public IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Nodes.NodeInfoAggregation>? Aggregations { get; init; }
	[JsonInclude, JsonPropertyName("attributes")]
	public IReadOnlyDictionary<string, string> Attributes { get; init; }
	[JsonInclude, JsonPropertyName("build_flavor")]
	public string BuildFlavor { get; init; }

	/// <summary>
	/// <para>Short hash of the last git commit in this release.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("build_hash")]
	public string BuildHash { get; init; }
	[JsonInclude, JsonPropertyName("build_type")]
	public string BuildType { get; init; }

	/// <summary>
	/// <para>The node’s host name.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("host")]
	public string Host { get; init; }
	[JsonInclude, JsonPropertyName("http")]
	public Elastic.Clients.Elasticsearch.Nodes.NodeInfoHttp? Http { get; init; }
	[JsonInclude, JsonPropertyName("ingest")]
	public Elastic.Clients.Elasticsearch.Nodes.NodeInfoIngest? Ingest { get; init; }

	/// <summary>
	/// <para>The node’s IP address.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("ip")]
	public string Ip { get; init; }
	[JsonInclude, JsonPropertyName("jvm")]
	public Elastic.Clients.Elasticsearch.Nodes.NodeJvmInfo? Jvm { get; init; }
	[JsonInclude, JsonPropertyName("modules")]
	public IReadOnlyCollection<Elastic.Clients.Elasticsearch.PluginStats>? Modules { get; init; }

	/// <summary>
	/// <para>The node's name</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("name")]
	public string Name { get; init; }
	[JsonInclude, JsonPropertyName("network")]
	public Elastic.Clients.Elasticsearch.Nodes.NodeInfoNetwork? Network { get; init; }
	[JsonInclude, JsonPropertyName("os")]
	public Elastic.Clients.Elasticsearch.Nodes.NodeOperatingSystemInfo? Os { get; init; }
	[JsonInclude, JsonPropertyName("plugins")]
	public IReadOnlyCollection<Elastic.Clients.Elasticsearch.PluginStats>? Plugins { get; init; }
	[JsonInclude, JsonPropertyName("process")]
	public Elastic.Clients.Elasticsearch.Nodes.NodeProcessInfo? Process { get; init; }
	[JsonInclude, JsonPropertyName("roles")]
	public IReadOnlyCollection<Elastic.Clients.Elasticsearch.NodeRole> Roles { get; init; }
	[JsonInclude, JsonPropertyName("settings")]
	public Elastic.Clients.Elasticsearch.Nodes.NodeInfoSettings? Settings { get; init; }
	[JsonInclude, JsonPropertyName("thread_pool")]
	public IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Nodes.NodeThreadPoolInfo>? ThreadPool { get; init; }

	/// <summary>
	/// <para>Total heap allowed to be used to hold recently indexed documents before they must be written to disk. This size is a shared pool across all shards on this node, and is controlled by Indexing Buffer settings.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("total_indexing_buffer")]
	public long? TotalIndexingBuffer { get; init; }

	/// <summary>
	/// <para>Same as total_indexing_buffer, but expressed in bytes.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("total_indexing_buffer_in_bytes")]
	public Elastic.Clients.Elasticsearch.ByteSize? TotalIndexingBufferInBytes { get; init; }
	[JsonInclude, JsonPropertyName("transport")]
	public Elastic.Clients.Elasticsearch.Nodes.NodeInfoTransport? Transport { get; init; }

	/// <summary>
	/// <para>Host and port where transport HTTP connections are accepted.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("transport_address")]
	public string TransportAddress { get; init; }

	/// <summary>
	/// <para>Elasticsearch version running on this node.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("version")]
	public string Version { get; init; }
}