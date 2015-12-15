using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class ClusterRerouteState
	{
		[JsonProperty("version")]
		public int Version { get; internal set; }

		[JsonProperty("master_node")]
		public string MasterNode { get; internal set; }

		[JsonProperty("blocks")]
		public BlockState Blocks { get; internal set; }

		[JsonProperty("nodes")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter))]
		public Dictionary<string, NodeState> Nodes { get; internal set; }

		[JsonProperty("routing_table")]
		public RoutingTableState RoutingTable { get; internal set; }

		[JsonProperty("routing_nodes")]
		public RoutingNodesState RoutingNodes { get; internal set; }
	}
}
