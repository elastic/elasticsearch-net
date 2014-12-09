using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class CompactNodeInfo
	{
		[JsonProperty(PropertyName = "name")]
		public string Name { get; internal set; }
	}
}