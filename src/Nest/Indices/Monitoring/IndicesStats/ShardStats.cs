using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class ShardStats
	{
		[JsonProperty("routing")]
		public ShardRouting Routing { get; set; }

		[JsonProperty("docs")]
		public ShardDocs Documents { get; set; }

		[JsonProperty("store")]
		public ShardStatsStore Store { get; set; }

		[JsonProperty("indexing")]
		public ShardIndexing Indexing { get; set; }

		[JsonProperty("get")]
		public ShardGet Get { get; set; }

		[JsonProperty("search")]
		public ShardSearch Search { get; set; }

		[JsonProperty("merges")]
		public ShardMerges Merges { get; set; }

		[JsonProperty("refresh")]
		public ShardRefresh Refresh { get; set; }

		[JsonProperty("flush")]
		public ShardFlush Flush { get; set; }

		[JsonProperty("warmer")]
		public ShardWarmer Warmer { get; set; }

		[JsonProperty("query_cache")]
		public ShardQueryCache QueryCache { get; set; }

		[JsonProperty("fielddata")]
		public ShardFieldData FieldData { get; set; }

		[JsonProperty("completion")]
		public ShardCompletion Completion { get; set; }

		[JsonProperty("segments")]
		public ShardSegments Segments { get; set; }

		[JsonProperty("translog")]
		public ShardTransactionLog TransactionLog { get; set; }

		[JsonProperty("request_cache")]
		public ShardRequestCache RequestCache { get; set; }

		[JsonProperty("recovery")]
		public ShardStatsRecovery Recovery { get; set; }

		[JsonProperty("commit")]
		public ShardCommit Commit { get; set; }

		[JsonProperty("seq_no")]
		public ShardSequenceNumber SequenceNumber { get; set; }

		[JsonProperty("shard_path")]
		public ShardPath Path { get; set; }
	}
}