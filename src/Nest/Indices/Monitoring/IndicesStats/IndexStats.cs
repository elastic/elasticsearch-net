using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class IndexStats
	{
		[JsonProperty(PropertyName = "docs")]
		public DocStats Documents { get; set; }

		[JsonProperty(PropertyName = "store")]
		public StoreStats Store { get; set; }

		[JsonProperty(PropertyName = "indexing")]
		public IndexingStats Indexing { get; set; }

		[JsonProperty(PropertyName = "get")]
		public GetStats Get { get; set; }

		[JsonProperty(PropertyName = "search")]
		public SearchStats Search { get; set; }

		[JsonProperty(PropertyName = "merges")]
		public MergesStats Merges { get; set; }

		[JsonProperty(PropertyName = "refresh")]
		public RefreshStats Refresh { get; set; }

		[JsonProperty(PropertyName = "flush")]
		public FlushStats Flush { get; set; }

		[JsonProperty(PropertyName = "warmer")]
		public WarmerStats Warmer { get; set; }

		[JsonProperty(PropertyName = "query_cache")]
		public QueryCacheStats QueryCache { get; set; }

		[JsonProperty(PropertyName = "fielddata")]
		public FielddataStats Fielddata { get; set; }

		[JsonProperty(PropertyName = "percolate")]
		public PercolateStats Percolate { get; set; }

		[JsonProperty(PropertyName = "completion")]
		public CompletionStats Completion { get; set; }

		[JsonProperty(PropertyName = "segments")]
		public SegmentsStats Segments { get; set; }

		[JsonProperty(PropertyName = "translog")]
		public TranslogStats Translog { get; set; }

		[JsonProperty(PropertyName = "suggest")]
		public SuggestStats Suggest { get; set; }

		[JsonProperty(PropertyName = "request_cache")]
		public RequestCacheStats RequestCache { get; set; }

		[JsonProperty(PropertyName = "recovery")]
		public RecoveryStats Recovery { get; set; }
	}
}
