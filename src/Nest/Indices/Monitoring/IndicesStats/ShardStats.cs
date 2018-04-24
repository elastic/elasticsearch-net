using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class ShardStats
	{
		[JsonProperty("routing")]
		public ShardRouting Routing { get; internal set; }

		[JsonProperty("store")]
		public ShardStatsStore Store { get; internal set; }

		[JsonProperty("commit")]
		public ShardCommit Commit { get; internal set; }

		[JsonProperty("shard_path")]
		public ShardPath Path { get; internal set; }
	}
}
