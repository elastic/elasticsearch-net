using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public interface INodesInfoResponse : INodesResponse
	{
		string ClusterName { get; }
		IReadOnlyDictionary<string, NodeInfo> Nodes { get; }
	}

	[DataContract]
	public class NodesInfoResponse : NodesResponseBase, INodesInfoResponse
	{
		[DataMember(Name ="cluster_name")]
		public string ClusterName { get; internal set; }

		[DataMember(Name ="nodes")]
		[JsonFormatter(typeof(VerbatimDictionaryKeysFormatter<string, NodeInfo>))]
		public IReadOnlyDictionary<string, NodeInfo> Nodes { get; internal set; } = EmptyReadOnly<string, NodeInfo>.Dictionary;
	}
}
