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
namespace Elastic.Clients.Elasticsearch.IndexManagement.Recovery
{
	public partial class RecoveryBytes
	{
		[JsonInclude]
		[JsonPropertyName("percent")]
		public Elastic.Clients.Elasticsearch.Percentage Percent { get; init; }

		[JsonInclude]
		[JsonPropertyName("recovered")]
		public Elastic.Clients.Elasticsearch.ByteSize? Recovered { get; init; }

		[JsonInclude]
		[JsonPropertyName("recovered_in_bytes")]
		public Elastic.Clients.Elasticsearch.ByteSize RecoveredInBytes { get; init; }

		[JsonInclude]
		[JsonPropertyName("recovered_from_snapshot")]
		public Elastic.Clients.Elasticsearch.ByteSize? RecoveredFromSnapshot { get; init; }

		[JsonInclude]
		[JsonPropertyName("recovered_from_snapshot_in_bytes")]
		public Elastic.Clients.Elasticsearch.ByteSize? RecoveredFromSnapshotInBytes { get; init; }

		[JsonInclude]
		[JsonPropertyName("reused")]
		public Elastic.Clients.Elasticsearch.ByteSize? Reused { get; init; }

		[JsonInclude]
		[JsonPropertyName("reused_in_bytes")]
		public Elastic.Clients.Elasticsearch.ByteSize ReusedInBytes { get; init; }

		[JsonInclude]
		[JsonPropertyName("total")]
		public Elastic.Clients.Elasticsearch.ByteSize? Total { get; init; }

		[JsonInclude]
		[JsonPropertyName("total_in_bytes")]
		public Elastic.Clients.Elasticsearch.ByteSize TotalInBytes { get; init; }
	}
}