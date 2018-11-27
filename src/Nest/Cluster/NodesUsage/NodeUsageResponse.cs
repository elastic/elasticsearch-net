using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public interface INodesUsageResponse : INodesResponse
	{
		string ClusterName { get; }

		IReadOnlyDictionary<string, NodeUsageInformation> Nodes { get; }
	}

	public class NodesUsageResponse : NodesResponseBase, INodesUsageResponse
	{
		[DataMember(Name ="cluster_name")]
		public string ClusterName { get; internal set; }

		[DataMember(Name ="nodes")]
		public IReadOnlyDictionary<string, NodeUsageInformation> Nodes { get; internal set; } =
			EmptyReadOnly<string, NodeUsageInformation>.Dictionary;
	}
}
