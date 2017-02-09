using Newtonsoft.Json;

namespace Nest
{
	public class SecurityNode
	{
		[JsonProperty("name")]
		public string Name { get; set; }
	}
}