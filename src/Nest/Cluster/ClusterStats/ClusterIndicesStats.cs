
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class ClusterIndicesStats
	{
		[JsonProperty("completion")]
		public CompletionStats Completion { get; internal set; }

		[JsonProperty("count")]
		public long Count { get; internal set; }

		[JsonProperty("docs")]
		public DocStats Documents { get; internal set; }

		[JsonProperty("fielddata")]
		public FielddataStats Fielddata { get; internal set; }

		[JsonProperty("percolate")]
		public PercolateStats Percolate { get; internal set; }

		[JsonProperty("query_cache")]
		public QueryCacheStats QueryCache { get; internal set; }

		[JsonProperty("segments")]
		public SegmentsStats Segments { get; internal set; }

		[JsonProperty("shards")]
		public ClusterIndicesShardsStats Shards { get; internal set; }

		[JsonProperty("store")]
		public StoreStats Store { get; internal set; }

	}

	[JsonObject]
	public class ClusterIndicesShardsStats
	{
		[JsonProperty("total")]
		public double Total { get; internal set; }

		[JsonProperty("primaries")]
		public double Primaries { get; internal set; }

		[JsonProperty("replication")]
		public double Replication { get; internal set; }

		[JsonProperty("index")]
		public ClusterIndicesShardsIndexStats Index { get; internal set; }
	}

	[JsonObject]
	public class ClusterIndicesShardsIndexStats
	{
		[JsonProperty("shards")]
		public ClusterShardMetrics Shards { get; internal set; }

		[JsonProperty("primaries")]
		public ClusterShardMetrics Primaries { get; internal set; }

		[JsonProperty("replication")]
		public ClusterShardMetrics Replication { get; internal set; }
	}

	[JsonObject]
	public class ClusterShardMetrics
	{
		[JsonProperty("min")]
		public double Min { get; internal set; }

		[JsonProperty("max")]
		public double Max { get; internal set; }

		[JsonProperty("avg")]
		public double Avg { get; internal set; }
	}
}
