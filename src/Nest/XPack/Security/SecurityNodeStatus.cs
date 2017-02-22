using Newtonsoft.Json;

namespace Nest_5_2_0
{
	public class SecurityNodeStatus : Throwable
	{
		[JsonProperty("success")]
		public bool Success { get; set; }
	}
}
