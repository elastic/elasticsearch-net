using Newtonsoft.Json;

namespace Nest
{
	public class AliasRemoveOperation
	{
		[JsonProperty("index")]
		public IndexName Index { get; set; }
		[JsonProperty("alias")]
		public string Alias { get; set; }
	}
}