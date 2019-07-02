using System.Runtime.Serialization;

namespace Nest
{
	public class ClusterStatsResponse : NodesResponseBase
	{
		[DataMember(Name ="cluster_name")]
		public string ClusterName { get; internal set; }

		[DataMember(Name ="cluster_uuid")]
		public string ClusterUUID { get; internal set; }

		[DataMember(Name ="indices")]
		public ClusterIndicesStats Indices { get; internal set; }

		[DataMember(Name ="nodes")]
		public ClusterNodesStats Nodes { get; internal set; }

		[DataMember(Name ="status")]
		public ClusterStatus Status { get; internal set; }

		[DataMember(Name ="timestamp")]
		public long Timestamp { get; internal set; }
	}
}
