using Newtonsoft.Json;

namespace Nest
{
	public class RecoveryBytes
	{
		[JsonProperty("percent")]
		public string Percent { get; internal set; }

		[JsonProperty("recovered")]
		public long Recovered { get; internal set; }

		[JsonProperty("reused")]
		public long Reused { get; internal set; }

		[JsonProperty("total")]
		public long Total { get; internal set; }
	}
}
