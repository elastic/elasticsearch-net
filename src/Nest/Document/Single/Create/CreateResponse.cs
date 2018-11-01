using Newtonsoft.Json;

namespace Nest
{
	public interface ICreateResponse : IResponse
	{
		string Id { get; }
		string Index { get; }
		long PrimaryTerm { get; }
		Result Result { get; }
		long SequenceNumber { get; }
		ShardStatistics Shards { get; }
		string Type { get; }
		long Version { get; }
	}

	[JsonObject]
	public class CreateResponse : ResponseBase, ICreateResponse
	{
		[JsonProperty("_id")]
		public string Id { get; internal set; }

		[JsonProperty("_index")]
		public string Index { get; internal set; }

		[JsonProperty("_primary_term")]
		public long PrimaryTerm { get; internal set; }

		[JsonProperty("result")]
		public Result Result { get; internal set; }

		[JsonProperty("_seq_no")]
		public long SequenceNumber { get; internal set; }

		[JsonProperty("_shards")]
		public ShardStatistics Shards { get; internal set; }

		[JsonProperty("_type")]
		public string Type { get; internal set; }

		[JsonProperty("_version")]
		public long Version { get; internal set; }
	}
}
