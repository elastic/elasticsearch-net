using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	public interface INodesInfoResponse : INodesResponse
	{
		[DataMember(Name ="cluster_name")]
		string ClusterName { get; }

		[DataMember(Name ="nodes")]
		[JsonFormatter(typeof(VerbatimInterfaceReadOnlyDictionaryKeysFormatter<string, NodeInfo>))]
		IReadOnlyDictionary<string, NodeInfo> Nodes { get; }
	}

	[DataContract]
	public class NodesInfoResponse : NodesResponseBase, INodesInfoResponse
	{
		public string ClusterName { get; internal set; }
		public IReadOnlyDictionary<string, NodeInfo> Nodes { get; internal set; } = EmptyReadOnly<string, NodeInfo>.Dictionary;
	}
}
