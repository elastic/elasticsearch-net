using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[DataContract]
	public class NodesInfoResponse : NodesResponseBase
	{
		[DataMember(Name ="cluster_name")]
		public string ClusterName { get; internal set; }

		[DataMember(Name ="nodes")]
		[JsonFormatter(typeof(VerbatimInterfaceReadOnlyDictionaryKeysFormatter<string, NodeInfo>))]
		public IReadOnlyDictionary<string, NodeInfo> Nodes { get; internal set; } = EmptyReadOnly<string, NodeInfo>.Dictionary;
	}
}
