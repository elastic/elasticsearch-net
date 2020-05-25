// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class RequestCacheStats
	{
		[DataMember(Name ="evictions")]
		public long Evictions { get; set; }

		[DataMember(Name ="hit_count")]
		public long HitCount { get; set; }

		[DataMember(Name ="memory_size")]
		public string MemorySize { get; set; }

		[DataMember(Name ="memory_size_in_bytes")]
		public long MemorySizeInBytes { get; set; }

		[DataMember(Name ="miss_count")]
		public long MissCount { get; set; }
	}
}
