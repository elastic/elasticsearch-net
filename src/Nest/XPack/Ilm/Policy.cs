using Newtonsoft.Json;

namespace Nest
{
	public class Policy
	{
		[JsonProperty("phases")]
		public Phases Phases { get; set; }
	}
}
