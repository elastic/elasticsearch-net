using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class CatAllocationRecord : ICatRecord
	{
		[JsonProperty("shards")]
		public string Shards { get; set; }

		[JsonProperty("diskUsed")]
		public string DiskUsed { get; set; }

		[JsonProperty("diskAvail")]
		public string DiskAvailable { get; set; }

		[JsonProperty("diskRatio")]
		public string DiskRatio { get; set; }

		[JsonProperty("ip")]
		public string Ip { get; set; }

		[JsonProperty("node")]
		public string Node { get; set; }
	}
}