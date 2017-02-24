using Newtonsoft.Json;

namespace Nest_5_2_0
{
	[JsonObject]
	public class IndicesStats
	{
		[JsonProperty(PropertyName = "primaries")]
		public IndexStats Primaries { get; set; }
		[JsonProperty(PropertyName = "total")]
		public IndexStats Total { get; set; }
	}
}
