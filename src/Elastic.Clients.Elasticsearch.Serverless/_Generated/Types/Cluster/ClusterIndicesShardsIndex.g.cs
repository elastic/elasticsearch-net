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
using Elastic.Clients.Elasticsearch.Serverless.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serverless.Cluster;

public sealed partial class ClusterIndicesShardsIndex
{
	/// <summary>
	/// <para>
	/// Contains statistics about the number of primary shards assigned to selected nodes.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("primaries")]
	public Elastic.Clients.Elasticsearch.Serverless.Cluster.ClusterShardMetrics Primaries { get; init; }

	/// <summary>
	/// <para>
	/// Contains statistics about the number of replication shards assigned to selected nodes.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("replication")]
	public Elastic.Clients.Elasticsearch.Serverless.Cluster.ClusterShardMetrics Replication { get; init; }

	/// <summary>
	/// <para>
	/// Contains statistics about the number of shards assigned to selected nodes.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("shards")]
	public Elastic.Clients.Elasticsearch.Serverless.Cluster.ClusterShardMetrics Shards { get; init; }
}