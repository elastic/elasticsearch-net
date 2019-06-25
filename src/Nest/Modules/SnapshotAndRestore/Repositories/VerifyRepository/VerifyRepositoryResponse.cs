using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[DataContract]
	public class VerifyRepositoryResponse : ResponseBase
	{
		/// <summary>
		///  A dictionary of nodeId => nodeinfo of nodes that verified the repository
		/// </summary>
		[DataMember(Name = "nodes")]
		[JsonFormatter(typeof(VerbatimDictionaryInterfaceKeysFormatter<string, CompactNodeInfo>))]
		public IReadOnlyDictionary<string, CompactNodeInfo> Nodes { get; internal set; } = EmptyReadOnly<string, CompactNodeInfo>.Dictionary;
	}
}
