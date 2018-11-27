using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class ClusterRerouteState
	{
		[DataMember(Name ="blocks")]
		public BlockState Blocks { get; internal set; }

		[DataMember(Name ="master_node")]
		public string MasterNode { get; internal set; }

		[DataMember(Name ="nodes")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<string, NodeState>))]
		public IReadOnlyDictionary<string, NodeState> Nodes { get; internal set; }

		[DataMember(Name ="routing_nodes")]
		public RoutingNodesState RoutingNodes { get; internal set; }

		[DataMember(Name ="routing_table")]
		public RoutingTableState RoutingTable { get; internal set; }

		[DataMember(Name ="version")]
		public int Version { get; internal set; }
	}
}
