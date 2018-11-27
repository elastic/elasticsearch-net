using System.Runtime.Serialization;

namespace Nest
{
	public interface IClusterStatsResponse : INodesResponse
	{
		[DataMember(Name ="cluster_name")]
		string ClusterName { get; }

		[DataMember(Name ="indices")]
		ClusterIndicesStats Indices { get; }

		[DataMember(Name ="nodes")]
		ClusterNodesStats Nodes { get; }

		[DataMember(Name ="status")]
		ClusterStatus Status { get; }

		[DataMember(Name ="timestamp")]
		long Timestamp { get; }
	}

	public class ClusterStatsResponse : NodesResponseBase, IClusterStatsResponse
	{
		public string ClusterName { get; internal set; }

		public ClusterIndicesStats Indices { get; internal set; }

		public ClusterNodesStats Nodes { get; internal set; }

		public ClusterStatus Status { get; internal set; }

		public long Timestamp { get; internal set; }
	}
}
