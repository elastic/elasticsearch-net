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

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.Cluster
{
	public partial class NodeAllocationExplanation
	{
		[JsonInclude]
		[JsonPropertyName("deciders")]
		public IReadOnlyCollection<Elastic.Clients.Elasticsearch.Cluster.AllocationDecision> Deciders { get; init; }

		[JsonInclude]
		[JsonPropertyName("node_attributes")]
		public Dictionary<string, string> NodeAttributes { get; init; }

		[JsonInclude]
		[JsonPropertyName("node_decision")]
		public Elastic.Clients.Elasticsearch.Cluster.Decision NodeDecision { get; init; }

		[JsonInclude]
		[JsonPropertyName("node_id")]
		public string NodeId { get; init; }

		[JsonInclude]
		[JsonPropertyName("node_name")]
		public string NodeName { get; init; }

		[JsonInclude]
		[JsonPropertyName("store")]
		public Elastic.Clients.Elasticsearch.Cluster.AllocationStore? Store { get; init; }

		[JsonInclude]
		[JsonPropertyName("transport_address")]
		public string TransportAddress { get; init; }

		[JsonInclude]
		[JsonPropertyName("weight_ranking")]
		public int WeightRanking { get; init; }
	}
}