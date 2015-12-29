using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public class RoutingShard
	{
		[JsonProperty("allocation_id")]
		public AllocationId AllocationId { get; internal set; }

		[JsonProperty("state")]
		public string State { get; internal set; }

		[JsonProperty("primary")]
		public bool Primary { get; internal set; }

		[JsonProperty("node")]
		public string Node { get; internal set; }

		[JsonProperty("relocating_node")]
		public string RelocatingNode { get; internal set; }

		[JsonProperty("shard")]
		public int Shard { get; internal set; }

		[JsonProperty("version")]
		public long Version { get; internal set; }

		[JsonProperty("index")]
		public string Index { get; internal set; }
	}
}