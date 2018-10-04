using Newtonsoft.Json;

namespace Nest
{
	public class RollupJobInformation
	{
		[JsonProperty("config")]
		public RollupJobConfiguration Config { get; internal set; }

		[JsonProperty("status")]
		public RollupJobStatus Status { get; internal set; }

		[JsonProperty("stats")]
		public RollupJobStats Stats { get; internal set; }

	}
}
