// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[DataContract]
	public class IndexHealthStats
	{
		[DataMember(Name = "active_primary_shards")]
		public int ActivePrimaryShards { get; internal set; }

		[DataMember(Name = "active_shards")]
		public int ActiveShards { get; internal set; }

		[DataMember(Name = "initializing_shards")]
		public int InitializingShards { get; internal set; }

		[DataMember(Name = "number_of_replicas")]
		public int NumberOfReplicas { get; internal set; }

		[DataMember(Name = "number_of_shards")]
		public int NumberOfShards { get; internal set; }

		[DataMember(Name = "relocating_shards")]
		public int RelocatingShards { get; internal set; }

		[DataMember(Name = "shards")]
		[JsonFormatter(typeof(VerbatimInterfaceReadOnlyDictionaryKeysFormatter<string, ShardHealthStats>))]
		public IReadOnlyDictionary<string, ShardHealthStats> Shards { get; internal set; } = EmptyReadOnly<string, ShardHealthStats>.Dictionary;

		[DataMember(Name = "status")]
		public Health Status { get; internal set; }

		[DataMember(Name = "unassigned_shards")]
		public int UnassignedShards { get; internal set; }
	}
}
