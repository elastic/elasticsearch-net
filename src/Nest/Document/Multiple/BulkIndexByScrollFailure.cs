using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
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
	[ContractJsonConverter(typeof(ErrorCauseJsonConverter<BulkIndexFailureCause>))]
	public class BulkIndexFailureCause : Error
	{
		public string IndexUniqueId => this.Metadata?.IndexUUID;
		public int? Shard => this.Metadata?.Shard;
		public string Index => this.Metadata?.Index;
	}
}
