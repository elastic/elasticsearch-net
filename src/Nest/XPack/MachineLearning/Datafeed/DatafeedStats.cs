using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class DatafeedStats
	{
		[JsonProperty("datafeed_id")]
		public string DatafeedId { get; internal set; }

		[JsonProperty("state")]
		public DatafeedState State { get; internal set; }

		[JsonProperty("node")]
		public DiscoveryNode Node { get; internal set; }

		[JsonProperty("assignment_explanation")]
		public string AssignmentExplanation { get; internal set; }
	}
}