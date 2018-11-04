using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class CatSnapshotsRecord : ICatRecord
	{
		[JsonProperty("duration")]
		public Time Duration { get; set; }

		[JsonProperty("end_epoch")]
		public long EndEpoch { get; set; }

		[JsonProperty("end_time")]
		public string EndTime { get; set; }

		[JsonProperty("failed_shards")]
		public long FailedShards { get; set; }

		// duration indices successful_shards failed_shards total_shards
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("indices")]
		public long Indices { get; set; }

		[JsonProperty("start_epoch")]
		public long StartEpoch { get; set; }

		[JsonProperty("start_time")]
		public string StartTime { get; set; }

		[JsonProperty("status")]
		public string Status { get; set; }

		[JsonProperty("succesful_shards")]
		public long SuccesfulShards { get; set; }

		[JsonProperty("total_shards")]
		public long TotalShards { get; set; }
	}
}
