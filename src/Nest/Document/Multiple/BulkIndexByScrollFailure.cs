using Newtonsoft.Json;

namespace Nest
{
	// ReindexOnServer and UpdateByQuery aggregate failures under a single failures property
	// So the shape is a bit odd
	// https://github.com/elastic/elasticsearch/issues/17539
	// We could come up with abstractions and normalization here but we should fix this at the root for 5.0

	[JsonObject]
	public class BulkIndexByScrollFailure
	{
		[JsonProperty("index")]
		public string Index { get; internal set; }

		[JsonProperty("type")]
		public string Type { get; internal set; }

		[JsonProperty("id")]
		public string Id { get; internal set; }

		[JsonProperty("node")]
		public string Node { get; internal set; }

		[JsonProperty("shard")]
		public int Shard { get; internal set; }

		[JsonProperty("status")]
		public int Status { get; internal set; }

		[JsonProperty("cause")]
		public Throwable Cause { get; internal set; }

		[JsonProperty("reason")]
		public Throwable Reason { get; internal set; }


	}
}
