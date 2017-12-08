using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface INodesUsageResponse : INodesResponse
	{
		string ClusterName { get; }

		IReadOnlyDictionary<string, NodeUsageInformation> Nodes { get; }
	}

	public class NodesUsageResponse : NodesResponseBase, INodesUsageResponse
	{
		[JsonProperty("cluster_name")]
		public string ClusterName { get; internal set; }

		[JsonProperty("nodes")]
		public IReadOnlyDictionary<string, NodeUsageInformation> Nodes { get; internal set; } = EmptyReadOnly<string, NodeUsageInformation>.Dictionary;
	}
}
