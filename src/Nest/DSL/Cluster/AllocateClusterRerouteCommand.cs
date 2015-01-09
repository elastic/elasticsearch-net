using Newtonsoft.Json;

namespace Nest
{
	public class AllocateClusteRerouteCommand : IClusterRerouteCommand
	{
		[JsonIgnore]
		public string Name { get { return "allocate"; } }

		[JsonProperty("index")]
		public string Index { get; set; }

		[JsonProperty("shard")]
		public int Shard { get; set; }

		[JsonProperty("node")]
		public string Node { get; set; }

		[JsonProperty("allow_primary")]
		public bool? AllowPrimary { get; set; }
	}
}
