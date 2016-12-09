using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class IndexActionResultIndexResponse
	{
		[JsonProperty("index")]
		public IndexName Index { get; set; }

		[JsonProperty("type")]
		public TypeName Type { get; set; }

		[JsonProperty("version")]
		public int Version { get; set; }

		[JsonProperty("created")]
		public bool? Created { get; set; }

		[JsonProperty("result")]
		public Result Result { get; set; }

		[JsonProperty("id")]
		public string Id { get; set; }
	}
}
