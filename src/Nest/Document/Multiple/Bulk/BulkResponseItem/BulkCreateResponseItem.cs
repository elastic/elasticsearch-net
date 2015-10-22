using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	[JsonConverter(typeof(BulkResponseItemJsonConverter))]
	public class BulkCreateResponseItem : BulkResponseItem
	{
		public override string Operation { get; internal set; }
	}
}