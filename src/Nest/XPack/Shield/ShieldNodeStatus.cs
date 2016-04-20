using Newtonsoft.Json;

namespace Nest
{
	public class ShieldNodeStatus : Throwable
	{
		[JsonProperty("success")]
		public bool Success { get; set; }
	}
}