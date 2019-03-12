using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class CatThreadPoolRecord : ICatRecord
	{
		[JsonProperty("active")]
		public int Active { get; set; }

		[JsonProperty("completed")]
		public long Completed { get; set; }

		[JsonProperty("ephemeral_node_id")]
		public string EphemeralNodeId { get; set; }

		[JsonProperty("host")]
		public string Host { get; set; }

		[JsonProperty("ip")]
		public string Ip { get; set; }

		[JsonProperty("keep_alive")]
		public Time KeepAlive { get; set; }

		[JsonProperty("largest")]
		public int Largest { get; set; }

		//TODO: This is now often reported back as null since 7.x (investigate)
		[JsonProperty("max")]
		public int? Maximum { get; set; }

		//TODO: this appears to no longer be reported
		[JsonProperty("min")]
		public int? Minimum { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("node_id")]
		public string NodeId { get; set; }

		[JsonProperty("node_name")]
		public string NodeName { get; set; }

		[JsonProperty("port")]
		public int Port { get; set; }

		[JsonProperty("pid")]
		public int ProcessId { get; set; }

		[JsonProperty("queue")]
		public int Queue { get; set; }

		[JsonProperty("queue_size")]
		public int? QueueSize { get; set; }

		[JsonProperty("rejected")]
		public long Rejected { get; set; }

		[JsonProperty("size")]
		public int Size { get; set; }

		[JsonProperty("type")]
		public string Type { get; set; }
	}
}
