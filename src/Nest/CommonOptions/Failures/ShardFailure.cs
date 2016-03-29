using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class ShardFailure
	{
		[JsonProperty("index")]
		public string Index { get; internal set; }

		[JsonProperty("shard")]
		public int Shard { get; internal set; }

		[JsonProperty("node")]
		public string Node { get; internal set; }

		[JsonProperty("reason")]
		public ShardFailureReason Reason { get; internal set; }
	}

	[JsonObject]
	public interface IFailureReason
	{
		[JsonProperty("type")]
		string Type { get; }

		[JsonProperty("reason")]
		string Reason { get; }
	}

	public class ShardFailureReason : IFailureReason
	{
		public string Type { get; internal set; }

		public string Reason { get; internal set; }

		[JsonProperty("caused_by")]
		public CausedBy CausedBy { get; internal set; }
	}

	public class CausedBy : IFailureReason
	{
		public string Type { get; internal set; }
		public string Reason { get; internal set; }
	}
}