using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class CatCountRecord : ICatRecord
	{
		[JsonProperty("epoch")]
		public string Epoch { get; set; }

		[JsonProperty("timestamp")]
		public string Timestamp { get; set; }

		[JsonProperty("count")]
		public string Count { get; set; }
	}
}