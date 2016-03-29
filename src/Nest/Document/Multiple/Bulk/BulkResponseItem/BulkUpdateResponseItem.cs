using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	[JsonConverter(typeof(BulkResponseItemJsonConverter))]
	public class BulkUpdateResponseItem : BulkResponseItemBase
	{
		public override string Operation { get; internal set; }
	}
}