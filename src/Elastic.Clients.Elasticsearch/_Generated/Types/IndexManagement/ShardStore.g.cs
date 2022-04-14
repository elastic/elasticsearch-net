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
namespace Elastic.Clients.Elasticsearch.IndexManagement
{
	public partial class ShardStore
	{
		[JsonInclude]
		[JsonPropertyName("allocation")]
		public Elastic.Clients.Elasticsearch.IndexManagement.ShardStoreAllocation Allocation { get; init; }

		[JsonInclude]
		[JsonPropertyName("allocation_id")]
		public string AllocationId { get; init; }

		[JsonInclude]
		[JsonPropertyName("attributes")]
		public Dictionary<string, object> Attributes { get; init; }

		[JsonInclude]
		[JsonPropertyName("id")]
		public string Id { get; init; }

		[JsonInclude]
		[JsonPropertyName("legacy_version")]
		public long LegacyVersion { get; init; }

		[JsonInclude]
		[JsonPropertyName("name")]
		public string Name { get; init; }

		[JsonInclude]
		[JsonPropertyName("store_exception")]
		public Elastic.Clients.Elasticsearch.IndexManagement.ShardStoreException StoreException { get; init; }

		[JsonInclude]
		[JsonPropertyName("transport_address")]
		public string TransportAddress { get; init; }
	}
}