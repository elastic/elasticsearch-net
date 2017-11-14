using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class IndicesStats
	{
		[JsonProperty("primaries")]
		public IndexStats Primaries { get; set; }
		[JsonProperty("total")]
		public IndexStats Total { get; set; }
	}
}
