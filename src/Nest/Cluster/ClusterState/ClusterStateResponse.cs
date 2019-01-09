using System.Collections.Generic;
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	public interface IClusterStateResponse : IResponse
	{
		[DataMember(Name = "blocks")]
		BlockState Blocks { get; }

		[DataMember(Name = "cluster_name")]
		string ClusterName { get; }

		/// <summary>The Universally Unique Identifier for the cluster.</summary>
		/// <remarks>While the cluster is still forming, it is possible for the `cluster_uuid` to be `_na_`.</remarks>
		[DataMember(Name = "cluster_uuid")]
		string ClusterUUID { get; }

		[DataMember(Name = "master_node")]
		string MasterNode { get; }

		[DataMember(Name = "metadata")]
		MetadataState Metadata { get; }

		[DataMember(Name = "nodes")]
		[JsonFormatter(typeof(VerbatimDictionaryInterfaceKeysFormatter<string, NodeState>))]
		IReadOnlyDictionary<string, NodeState> Nodes { get; }

		[DataMember(Name = "routing_nodes")]
		RoutingNodesState RoutingNodes { get; }

		[DataMember(Name = "routing_table")]
		RoutingTableState RoutingTable { get; }

		[DataMember(Name = "state_uuid")]
		string StateUUID { get; }

		[DataMember(Name = "version")]
		long Version { get; }
	}

	public class ClusterStateResponse : ResponseBase, IClusterStateResponse
	{
		public BlockState Blocks { get; internal set; }
		public string ClusterName { get; internal set; }

		/// <inheritdoc cref="IClusterStateResponse.ClusterUUID" />
		public string ClusterUUID { get; internal set; }

		public string MasterNode { get; internal set; }

		public MetadataState Metadata { get; internal set; }

		public IReadOnlyDictionary<string, NodeState> Nodes { get; internal set; } = EmptyReadOnly<string, NodeState>.Dictionary;

		public RoutingNodesState RoutingNodes { get; internal set; }

		public RoutingTableState RoutingTable { get; internal set; }

		public string StateUUID { get; internal set; }

		public long Version { get; internal set; }
	}
}
