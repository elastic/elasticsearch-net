using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class ShardSequenceNumber
	{   
		[JsonProperty("max_seq_no")]
		public long MaximumSequenceNumber { get; set; }
		[JsonProperty("local_checkpoint")]
		public long LocalCheckpoint { get; set; }
		[JsonProperty("global_checkpoint")]
		public long GlobalCheckpoint { get; set; }
	}
}