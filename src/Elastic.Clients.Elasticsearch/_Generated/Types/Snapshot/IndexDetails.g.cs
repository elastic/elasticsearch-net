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
namespace Elastic.Clients.Elasticsearch.Snapshot
{
	public partial class IndexDetails
	{
		[JsonInclude]
		[JsonPropertyName("max_segments_per_shard")]
		public long MaxSegmentsPerShard { get; init; }

		[JsonInclude]
		[JsonPropertyName("shard_count")]
		public int ShardCount { get; init; }

		[JsonInclude]
		[JsonPropertyName("size")]
		public Elastic.Clients.Elasticsearch.ByteSize? Size { get; init; }

		[JsonInclude]
		[JsonPropertyName("size_in_bytes")]
		public long SizeInBytes { get; init; }
	}
}