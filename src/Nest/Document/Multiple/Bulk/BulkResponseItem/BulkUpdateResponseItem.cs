using Newtonsoft.Json;

namespace Nest_5_2_0
{
	[JsonObject]
	[JsonConverter(typeof(BulkResponseItemJsonConverter))]
	public class BulkUpdateResponseItem : BulkResponseItemBase
	{
		public override string Operation { get; internal set; }
	}
}