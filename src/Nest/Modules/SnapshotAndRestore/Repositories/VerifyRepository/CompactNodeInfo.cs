using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class CompactNodeInfo
	{
		[JsonProperty("name")]
		public string Name { get; internal set; }
	}
}
