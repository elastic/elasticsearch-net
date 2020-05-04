// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class ClusterIndicesStats
	{
		[DataMember(Name ="completion")]
		public CompletionStats Completion { get; internal set; }

		[DataMember(Name ="count")]
		public long Count { get; internal set; }

		[DataMember(Name ="docs")]
		public DocStats Documents { get; internal set; }

		[DataMember(Name ="fielddata")]
		public FielddataStats Fielddata { get; internal set; }

		[DataMember(Name ="query_cache")]
		public QueryCacheStats QueryCache { get; internal set; }

		[DataMember(Name ="segments")]
		public SegmentsStats Segments { get; internal set; }

		[DataMember(Name ="shards")]
		public ClusterIndicesShardsStats Shards { get; internal set; }

		[DataMember(Name ="store")]
		public StoreStats Store { get; internal set; }
	}

	[DataContract]
	public class ClusterIndicesShardsStats
	{
		[DataMember(Name ="index")]
		public ClusterIndicesShardsIndexStats Index { get; internal set; }

		[DataMember(Name ="primaries")]
		public double Primaries { get; internal set; }

		[DataMember(Name ="replication")]
		public double Replication { get; internal set; }

		[DataMember(Name ="total")]
		public double Total { get; internal set; }
	}

	[DataContract]
	public class ClusterIndicesShardsIndexStats
	{
		[DataMember(Name ="primaries")]
		public ClusterShardMetrics Primaries { get; internal set; }

		[DataMember(Name ="replication")]
		public ClusterShardMetrics Replication { get; internal set; }

		[DataMember(Name ="shards")]
		public ClusterShardMetrics Shards { get; internal set; }
	}

	[DataContract]
	public class ClusterShardMetrics
	{
		[DataMember(Name ="avg")]
		public double Avg { get; internal set; }

		[DataMember(Name ="max")]
		public double Max { get; internal set; }

		[DataMember(Name ="min")]
		public double Min { get; internal set; }
	}
}
