using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IClusterStateResponse : IResponse
	{
		[JsonProperty("cluster_name")]
		string ClusterName { get; }

		[JsonProperty("master_node")]
		string MasterNode { get; }

		[JsonProperty("state_uuid")]
		string StateUUID { get; }

		[JsonProperty("version")]
		long Version { get; }

		[JsonProperty("nodes")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter))]
		Dictionary<string, NodeState> Nodes { get; }

		[JsonProperty("metadata")]
		MetadataState Metadata { get; }

		[JsonProperty("routing_table")]
		RoutingTableState RoutingTable { get; }

		[JsonProperty("routing_nodes")]
		RoutingNodesState RoutingNodes { get; }

		[JsonProperty("blocks")]
		BlockState Blocks { get; }
	}

	public class ClusterStateResponse : ResponseBase, IClusterStateResponse
	{
		public string ClusterName { get; internal set; }

		public string MasterNode { get; internal set; }

		public string StateUUID { get; internal set; }

		public long Version { get; internal set; }

		public Dictionary<string, NodeState> Nodes { get; internal set; }

		public MetadataState Metadata { get; internal set; }

		public RoutingTableState RoutingTable { get; internal set; }

		public RoutingNodesState RoutingNodes { get; internal set; }

		public BlockState Blocks { get; internal set; }
	}
}
