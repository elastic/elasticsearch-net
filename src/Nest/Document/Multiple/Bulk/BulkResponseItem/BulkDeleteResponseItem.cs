using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	[JsonConverter(typeof(BulkResponseItemJsonConverter))]
	public class BulkDeleteResponseItem : BulkResponseItem
	{
		public override string Operation { get; internal set; }
		[JsonProperty("found")]
		public bool Found { get; internal set; }
	}
}