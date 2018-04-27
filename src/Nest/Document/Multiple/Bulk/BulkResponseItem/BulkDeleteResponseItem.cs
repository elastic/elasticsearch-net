using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	[JsonConverter(typeof(BulkResponseItemJsonConverter))]
	public class BulkDeleteResponseItem : BulkResponseItemBase
	{
		public override string Operation { get; internal set; }

		[JsonProperty("found")]
		[Obsolete("Removed in 6.0.")]
		public bool Found { get; internal set; }
	}
}
