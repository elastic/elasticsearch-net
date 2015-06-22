using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class CatHealthRecord : ICatRecord
	{
		[JsonProperty("cluster")]
		public string Cluster { get; set; }

		[JsonProperty("epoch")]
		public string Epoch { get; set; }

		[JsonProperty("node.data")]
		public string NodeData { get; set; }

		[JsonProperty("node.total")]
		public string NodeTotal { get; set; }

		[JsonProperty("pri")]
		public string Primary { get; set; }

		[JsonProperty("relo")]
		public string Relocating { get; set; }

		[JsonProperty("init")]
		public string Initializing { get; set; }

		[JsonProperty("shards")]
		public string Shards { get; set; }

		[JsonProperty("status")]
		public string Status { get; set; }

		[JsonProperty("timestamp")]
		public string Timestamp { get; set; }

		[JsonProperty("unassign")]
		public string Unassigned { get; set; }

		[JsonProperty("pending_tasks")]
		public string PendingTasks { get; set; }
	}
}