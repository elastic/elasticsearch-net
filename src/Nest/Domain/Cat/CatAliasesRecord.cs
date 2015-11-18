using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class CatAliasesRecord : ICatRecord
	{
		[JsonProperty("alias")]
		public string Alias { get; set; }

		[JsonProperty("index")]
		public string Index { get; set; }

		[JsonProperty("filter")]
		public string Filter { get; set; }

		[JsonProperty("routing.index")]
		public string IndexRouting { get; set; }

		[JsonProperty("routing.search")]
		public string SearchRouting { get; set; }
	}
}