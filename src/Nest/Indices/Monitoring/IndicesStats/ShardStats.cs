using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class ShardStats
	{
		[JsonProperty("routing")]
		public ShardRouting Routing { get; internal set; }

		[JsonProperty("docs")]
		public ShardDocs Documents { get; internal set; }

		[JsonProperty("store")]
		public ShardStatsStore Store { get; internal set; }

		[JsonProperty("indexing")]
		public ShardIndexing Indexing { get; internal set; }

		[JsonProperty("get")]
		public ShardGet Get { get; internal set; }

		[JsonProperty("search")]
		public ShardSearch Search { get; internal set; }

		[JsonProperty("merges")]
		public ShardMerges Merges { get; internal set; }

		[JsonProperty("refresh")]
		public ShardRefresh Refresh { get; internal set; }

		[JsonProperty("flush")]
		public ShardFlush Flush { get; internal set; }

		[JsonProperty("warmer")]
		public ShardWarmer Warmer { get; internal set; }

		[JsonProperty("query_cache")]
		public ShardQueryCache QueryCache { get; internal set; }

		[JsonProperty("fielddata")]
		public ShardFieldData FieldData { get; internal set; }

		[JsonProperty("completion")]
		public ShardCompletion Completion { get; internal set; }

		[JsonProperty("segments")]
		public ShardSegments Segments { get; internal set; }

		[JsonProperty("translog")]
		public ShardTransactionLog TransactionLog { get; internal set; }

		[JsonProperty("request_cache")]
		public ShardRequestCache RequestCache { get; internal set; }

		[JsonProperty("recovery")]
		public ShardStatsRecovery Recovery { get; internal set; }

		[JsonProperty("commit")]
		public ShardCommit Commit { get; internal set; }

		[JsonProperty("seq_no")]
		public ShardSequenceNumber SequenceNumber { get; internal set; }

		[JsonProperty("shard_path")]
		public ShardPath Path { get; internal set; }
	}
}
