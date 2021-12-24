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
namespace Elastic.Clients.Elasticsearch.Cluster.Stats
{
	public partial class IndexingPressureMemorySummary
	{
		[JsonInclude]
		[JsonPropertyName("all_in_bytes")]
		public long AllInBytes { get; init; }

		[JsonInclude]
		[JsonPropertyName("combined_coordinating_and_primary_in_bytes")]
		public long CombinedCoordinatingAndPrimaryInBytes { get; init; }

		[JsonInclude]
		[JsonPropertyName("coordinating_in_bytes")]
		public long CoordinatingInBytes { get; init; }

		[JsonInclude]
		[JsonPropertyName("coordinating_rejections")]
		public long? CoordinatingRejections { get; init; }

		[JsonInclude]
		[JsonPropertyName("primary_in_bytes")]
		public long PrimaryInBytes { get; init; }

		[JsonInclude]
		[JsonPropertyName("primary_rejections")]
		public long? PrimaryRejections { get; init; }

		[JsonInclude]
		[JsonPropertyName("replica_in_bytes")]
		public long ReplicaInBytes { get; init; }

		[JsonInclude]
		[JsonPropertyName("replica_rejections")]
		public long? ReplicaRejections { get; init; }
	}
}