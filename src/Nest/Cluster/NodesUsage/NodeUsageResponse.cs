using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public class NodesUsageResponse : NodesResponseBase
	{
		[DataMember(Name ="cluster_name")]
		public string ClusterName { get; internal set; }

		[DataMember(Name ="nodes")]
		public IReadOnlyDictionary<string, NodeUsageInformation> Nodes { get; internal set; } =
			EmptyReadOnly<string, NodeUsageInformation>.Dictionary;
	}
}
