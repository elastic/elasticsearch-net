using Newtonsoft.Json;

namespace Nest
{
	public class AliasRemoveIndexOperation
	{
		[JsonProperty("index")]
		public IndexName Index { get; set; }
	}
}
