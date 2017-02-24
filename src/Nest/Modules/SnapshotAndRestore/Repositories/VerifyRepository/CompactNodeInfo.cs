using Newtonsoft.Json;

namespace Nest_5_2_0
{
	[JsonObject]
	public class CompactNodeInfo
	{
		[JsonProperty(PropertyName = "name")]
		public string Name { get; internal set; }
	}
}