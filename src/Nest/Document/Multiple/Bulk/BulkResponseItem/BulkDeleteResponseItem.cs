using Newtonsoft.Json;

namespace Nest_5_2_0
{
	[JsonObject]
	[JsonConverter(typeof(BulkResponseItemJsonConverter))]
	public class BulkDeleteResponseItem : BulkResponseItemBase
	{
		public override string Operation { get; internal set; }
		[JsonProperty("found")]
		public bool Found { get; internal set; }
	}
}