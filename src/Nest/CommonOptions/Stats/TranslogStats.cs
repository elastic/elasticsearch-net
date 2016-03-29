using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class TranslogStats
	{
		[JsonProperty("operations")]
		public long Operations  { get; set; }

		[JsonProperty("size")]
		public string Size { get; set; }

		[JsonProperty("size_in_bytes")]
		public long SizeInBytes  { get; set; }

	}
}