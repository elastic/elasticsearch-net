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

		/// <summary>The Universally Unique Identifier for the cluster.</summary>
		/// <remarks>While the cluster is still forming, it is possible for the `cluster_uuid` to be `_na_`.</remarks>
		[JsonProperty("cluster_uuid")]
		string ClusterUUID { get; }

		[JsonProperty("version")]
		long Version { get; }

		[JsonProperty("nodes")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<string, NodeState>))]
		IReadOnlyDictionary<string, NodeState> Nodes { get; }

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

		/// <inheritdoc cref="IClusterStateResponse.ClusterUUID"/>
		public string ClusterUUID { get; internal set; }

		public long Version { get; internal set; }

		public IReadOnlyDictionary<string, NodeState> Nodes { get; internal set; } = EmptyReadOnly<string, NodeState>.Dictionary;

		public MetadataState Metadata { get; internal set; }

		public RoutingTableState RoutingTable { get; internal set; }

		public RoutingNodesState RoutingNodes { get; internal set; }

		public BlockState Blocks { get; internal set; }
	}
}
