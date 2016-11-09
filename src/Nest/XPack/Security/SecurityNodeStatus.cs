using Newtonsoft.Json;

namespace Nest
{
	public class SecurityNodeStatus : Throwable
	{
		[JsonProperty("success")]
		public bool Success { get; set; }
	}
}
