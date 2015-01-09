using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class IndicesStats
	{
		[JsonProperty("count")]
		public long Count { get; internal set; }

		[JsonProperty("shards")]
		public IndicesShardsStats Shards { get; internal set; }

		[JsonProperty("docs")]
		public DocStats Docs { get; internal set; }

		[JsonProperty("store")]
		public StoreStats Store { get; internal set; }

		[JsonProperty("fielddata")]
		public FieldDataStats FieldData { get; internal set; }

		[JsonProperty("filter_cache")]
		public FilterCacheStats FilterCache { get; internal set; }

		[JsonProperty("id_cache")]
		public IdCacheStats IdCache { get; internal set; }

		[JsonProperty("completion")]
		public CompletionStats Completion { get; internal set; }

		[JsonProperty("segments")]
		public SegmentsStats Segments { get; internal set; }

		[JsonProperty("percolate")]
		public PercolateStats Percolate { get; internal set; }
	}

	[JsonObject]
	public class IndicesShardsStats
	{
		[JsonProperty("total")]
		public double Total { get; internal set; }

		[JsonProperty("primaries")]
		public double Primaries { get; internal set; }

		[JsonProperty("replication")]
		public double Replication { get; internal set; }

		[JsonProperty("index")]
		public IndicesShardsIndexStats Index { get; internal set; }
	}

	[JsonObject]
	public class IndicesShardsIndexStats
	{
		[JsonProperty("shards")]
		public IndicesShardsIndexStatsMetrics Shards { get; internal set; }

		[JsonProperty("primaries")]
		public IndicesShardsIndexStatsMetrics Primaries { get; internal set; }

		[JsonProperty("replication")]
		public IndicesShardsIndexStatsMetrics Replication { get; internal set; }
	}

	public class IndicesShardsIndexStatsMetrics
	{
		[JsonProperty("min")]
		public double Min { get; internal set; }

		[JsonProperty("max")]
		public double Max { get; internal set; }

		[JsonProperty("avg")]
		public double Avg { get; internal set; }
	}
}
