using Newtonsoft.Json;

namespace Nest
{
	public class MoveClusterRerouteCommand : IClusterRerouteCommand
	{
		[JsonIgnore]
		public string Name { get { return "move";  } }

		[JsonProperty("index")]
		public string Index { get; set; }

		[JsonProperty("shard")]
		public int Shard { get; set; }

		[JsonProperty("from_node")]
		public string FromNode { get; set; }

		[JsonProperty("to_node")]
		public string ToNode { get; set; }
	}
}
