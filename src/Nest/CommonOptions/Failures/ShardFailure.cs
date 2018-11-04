using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class ShardFailure
	{
		[JsonProperty("index")]
		public string Index { get; internal set; }

		[JsonProperty("node")]
		public string Node { get; internal set; }

		[JsonProperty("reason")]
		public ShardFailureReason Reason { get; internal set; }

		[JsonProperty("shard")]
		public int Shard { get; internal set; }
	}

	[JsonObject]
	public interface IFailureReason
	{
		[JsonProperty("reason")]
		string Reason { get; }

		[JsonProperty("type")]
		string Type { get; }
	}

	public class ShardFailureReason : IFailureReason
	{
		[JsonProperty("caused_by")]
		public CausedBy CausedBy { get; internal set; }

		public string Reason { get; internal set; }
		public string Type { get; internal set; }
	}

	public class CausedBy : IFailureReason
	{
		[JsonProperty("caused_by")]
		public CausedBy InnerCausedBy { get; internal set; }

		public string Reason { get; internal set; }
		public string Type { get; internal set; }

		public override string ToString()
		{
			var innerCause = InnerCausedBy != null ? $" CausedBy:\n{InnerCausedBy}" : string.Empty;

			return $"Type: {Type} Reason: \"{Reason}\"{innerCause}";
		}
	}
}
