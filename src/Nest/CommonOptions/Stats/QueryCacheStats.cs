// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class QueryCacheStats
	{
		[DataMember(Name ="cache_count")]
		public long CacheCount { get; set; }

		[DataMember(Name ="cache_size")]
		public long CacheSize { get; set; }

		[DataMember(Name ="evictions")]
		public long Evictions { get; set; }

		[DataMember(Name ="hit_count")]
		public long HitCount { get; set; }

		[DataMember(Name ="memory_size_in_bytes")]
		public long MemorySizeInBytes { get; set; }

		[DataMember(Name ="miss_count")]
		public long MissCount { get; set; }

		[DataMember(Name ="total_count")]
		public long TotalCount { get; set; }
	}
}
