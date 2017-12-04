using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	// TODO: ReindexOnServer and UpdateByQuery aggregate failures under a single failures property
	// So the shape is a bit odd
	// https://github.com/elastic/elasticsearch/issues/17539
	// We could come up with abstractions and normalization here but we should fix this at the root for 5.0

	[JsonObject]
	public class BulkIndexByScrollFailure
	{
		[JsonProperty("index")]
		public string Index { get; set; }

		[JsonProperty("type")]
		public string Type { get; internal set; }

		[JsonProperty("id")]
		public string Id { get; internal set; }

		[JsonProperty("cause")]
		public BulkIndexFailureCause Cause { get; set; }

		[JsonProperty("status")]
		public int Status { get; set; }
	}

	[JsonObject]
	public class BulkIndexFailureCause : Error
	{
		[JsonProperty("index_uuid")]
		public string IndexUniqueId { get; internal set; }

		[JsonProperty("shard")]
		public string Shard { get; internal set; }

		[JsonProperty("index")]
		public string Index { get; internal set; }
	}
}
