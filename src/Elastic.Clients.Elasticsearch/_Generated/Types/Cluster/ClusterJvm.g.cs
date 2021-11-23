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
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.Cluster.Stats
{
	public partial class ClusterJvm
	{
		[JsonInclude]
		[JsonPropertyName("max_uptime_in_millis")]
		public long MaxUptimeInMillis { get; init; }

		[JsonInclude]
		[JsonPropertyName("mem")]
		public Elastic.Clients.Elasticsearch.Cluster.Stats.ClusterJvmMemory Mem { get; init; }

		[JsonInclude]
		[JsonPropertyName("threads")]
		public long Threads { get; init; }

		[JsonInclude]
		[JsonPropertyName("versions")]
		public IReadOnlyCollection<Elastic.Clients.Elasticsearch.Cluster.Stats.ClusterJvmVersion> Versions { get; init; }
	}
}