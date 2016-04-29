using Newtonsoft.Json;

namespace Nest
{
	public class ShieldNode
	{
		[JsonProperty("name")]
		public string Name { get; set; }
		[JsonProperty("status")]
		public ShieldNodeStatus Status { get; set; }
	}
}