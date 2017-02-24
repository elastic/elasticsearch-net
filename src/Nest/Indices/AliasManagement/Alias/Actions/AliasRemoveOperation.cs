using Newtonsoft.Json;

namespace Nest_5_2_0
{
	public class AliasRemoveOperation
	{
		[JsonProperty("index")]
		public IndexName Index { get; set; }
		[JsonProperty("alias")]
		public string Alias { get; set; }
	}
}