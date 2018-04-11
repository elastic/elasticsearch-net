using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class ShardStats
	{
		[JsonProperty("routing")]
		public ShardRouting Routing { get; set; }

		[JsonProperty("store")]
		public ShardStatsStore Store { get; set; }

		[JsonProperty("commit")]
		public ShardCommit Commit { get; set; }

		[JsonProperty("shard_path")]
		public ShardPath Path { get; set; }
	}
}
