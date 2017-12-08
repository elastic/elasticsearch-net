using Newtonsoft.Json;

namespace Nest
{
	public interface IClusterStatsResponse : INodesResponse
	{
		[JsonProperty("cluster_name")]
		string ClusterName { get; }

		[JsonProperty("timestamp")]
		long Timestamp { get; }

		[JsonProperty("status")]
		ClusterStatus Status { get; }

		[JsonProperty("indices")]
		ClusterIndicesStats Indices { get; }

		[JsonProperty("nodes")]
		ClusterNodesStats Nodes { get; }
	}

	public class ClusterStatsResponse : NodesResponseBase, IClusterStatsResponse
	{
		public string ClusterName { get; internal set; }

		public long Timestamp { get; internal set; }

		public ClusterStatus Status { get; internal set; }

		public ClusterIndicesStats Indices { get; internal set; }

		public ClusterNodesStats Nodes { get; internal set; }
	}
}
