using Newtonsoft.Json;

namespace Nest
{
	public class AliasRemoveOperation
	{
		[JsonProperty("alias")]
		public string Alias { get; set; }

		[JsonProperty("index")]
		public IndexName Index { get; set; }
	}
}
