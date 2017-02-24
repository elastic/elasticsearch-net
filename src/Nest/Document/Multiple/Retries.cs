using Newtonsoft.Json;

namespace Nest_5_2_0
{
	public class Retries
	{
		[JsonProperty("bulk")]
		public long Bulk { get; internal set; }

		[JsonProperty("search")]
		public long Search { get; internal set; }
	}
}