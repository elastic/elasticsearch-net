using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class ShardSequenceNumber
	{
		[JsonProperty("max_seq_no")]
		public long MaximumSequenceNumber { get; internal set; }
		[JsonProperty("local_checkpoint")]
		public long LocalCheckpoint { get; internal set; }
		[JsonProperty("global_checkpoint")]
		public long GlobalCheckpoint { get; internal set; }
	}
}
