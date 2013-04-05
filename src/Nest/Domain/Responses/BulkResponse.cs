using Newtonsoft.Json;
using System.Collections.Generic;
using Nest.Resolvers.Converters;

namespace Nest
{
	[JsonObject]
	public class BulkResponse : BaseResponse, IBulkResponse
	{
		[JsonProperty("took")]
		public int Took { get; internal set; }

		[JsonProperty("items")]
		public IEnumerable<BulkOperationResponseItem> Items { get; internal set; }
	}
}
