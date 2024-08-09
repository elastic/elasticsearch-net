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

namespace Elastic.Clients.Elasticsearch.Cluster;

/// <summary>
/// <para>
/// Contains statistics about shards assigned to selected nodes.
/// </para>
/// </summary>
public sealed partial class ClusterIndicesShards
{
	/// <summary>
	/// <para>
	/// Contains statistics about shards assigned to selected nodes.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("index")]
	public Elastic.Clients.Elasticsearch.Cluster.ClusterIndicesShardsIndex? Index { get; init; }

	/// <summary>
	/// <para>
	/// Number of primary shards assigned to selected nodes.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("primaries")]
	public double? Primaries { get; init; }

	/// <summary>
	/// <para>
	/// Ratio of replica shards to primary shards across all selected nodes.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("replication")]
	public double? Replication { get; init; }

	/// <summary>
	/// <para>
	/// Total number of shards assigned to selected nodes.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("total")]
	public double? Total { get; init; }
}