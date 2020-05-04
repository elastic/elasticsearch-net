// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using Elasticsearch.Net;
using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class ShardHealthStats
	{
		[DataMember(Name ="active_shards")]
		public int ActiveShards { get; internal set; }

		[DataMember(Name ="initializing_shards")]
		public int InitializingShards { get; internal set; }

		[DataMember(Name ="primary_active")]
		public bool PrimaryActive { get; internal set; }

		[DataMember(Name ="relocating_shards")]
		public int RelocatingShards { get; internal set; }

		[DataMember(Name ="status")]
		public Health Status { get; internal set; }

		[DataMember(Name ="unassigned_shards")]
		public int UnassignedShards { get; internal set; }
	}
}
