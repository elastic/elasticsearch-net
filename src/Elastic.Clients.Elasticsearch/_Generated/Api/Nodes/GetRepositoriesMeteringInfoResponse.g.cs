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
using Elastic.Transport.Products.Elasticsearch;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Nodes;

public sealed partial class GetRepositoriesMeteringInfoResponse : ElasticsearchResponse
{
	/// <summary>
	/// <para>Name of the cluster. Based on the [Cluster name setting](https://www.elastic.co/guide/en/elasticsearch/reference/current/important-settings.html#cluster-name).</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("cluster_name")]
	public string ClusterName { get; init; }

	/// <summary>
	/// <para>Contains repositories metering information for the nodes selected by the request.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("nodes")]
	public IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Nodes.RepositoryMeteringInformation> Nodes { get; init; }

	/// <summary>
	/// <para>Contains statistics about the number of nodes selected by the request’s node filters.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("_nodes")]
	public Elastic.Clients.Elasticsearch.NodeStatistics? NodeStats { get; init; }
}