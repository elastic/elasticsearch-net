using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	public class ClusterStateResponse : ResponseBase
	{
		[DataMember(Name = "blocks")]
		public BlockState Blocks { get; internal set; }

		[DataMember(Name = "cluster_name")]
		public string ClusterName { get; internal set; }

		/// <summary>The Universally Unique Identifier for the cluster.</summary>
		/// <remarks>While the cluster is still forming, it is possible for the `cluster_uuid` to be `_na_`.</remarks>
		[DataMember(Name = "cluster_uuid")]
		public string ClusterUUID { get; internal set; }

		[DataMember(Name = "master_node")]
		public string MasterNode { get; internal set; }

		[DataMember(Name = "metadata")]
		public MetadataState Metadata { get; internal set; }

		[DataMember(Name = "nodes")]
		[JsonFormatter(typeof(VerbatimInterfaceReadOnlyDictionaryKeysFormatter<string, NodeState>))]
		public IReadOnlyDictionary<string, NodeState> Nodes { get; internal set; }
			= EmptyReadOnly<string, NodeState>.Dictionary;

		[DataMember(Name = "routing_nodes")]
		public RoutingNodesState RoutingNodes { get; internal set; }

		[DataMember(Name = "routing_table")]
		public RoutingTableState RoutingTable { get; internal set; }

		[DataMember(Name = "state_uuid")]
		public string StateUUID { get; internal set; }

		[DataMember(Name = "version")]
		public long Version { get; internal set; }
	}
}
