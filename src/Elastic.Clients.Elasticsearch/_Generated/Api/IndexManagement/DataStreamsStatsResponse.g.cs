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

using Elastic.Transport.Products.Elasticsearch;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.IndexManagement
{
	public partial class DataStreamsStatsResponse : ElasticsearchResponseBase
	{
		[JsonInclude]
		[JsonPropertyName("backing_indices")]
		public int BackingIndices { get; init; }

		[JsonInclude]
		[JsonPropertyName("data_stream_count")]
		public int DataStreamCount { get; init; }

		[JsonInclude]
		[JsonPropertyName("data_streams")]
		public IReadOnlyCollection<Elastic.Clients.Elasticsearch.IndexManagement.DataStreamsStatsItem> DataStreams { get; init; }

		[JsonInclude]
		[JsonPropertyName("_shards")]
		public Elastic.Clients.Elasticsearch.ShardStatistics Shards { get; init; }

		[JsonInclude]
		[JsonPropertyName("total_store_size_bytes")]
		public int TotalStoreSizeBytes { get; init; }

		[JsonInclude]
		[JsonPropertyName("total_store_sizes")]
		public Elastic.Clients.Elasticsearch.ByteSize? TotalStoreSizes { get; init; }
	}
}