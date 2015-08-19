using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	[JsonConverter(typeof(BulkResponseItemJsonConverter))]
	public class BulkUpdateResponseItem : BulkResponseItem
	{
		public override string Operation { get; internal set; }
		[JsonProperty("_index")]
		public override string Index { get; internal set; }
		[JsonProperty("_type")]
		public override string Type { get; internal set; }
		[JsonProperty("_id")]
		public override string Id { get; internal set; }
		[JsonProperty("_version")]
		public override long Version { get; internal set; }
		[JsonProperty("status")]
		public override int Status { get; internal set; }
		[JsonProperty("error")]
		public override string Error { get; internal set; }
	}
}