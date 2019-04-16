using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	public interface IVerifyRepositoryResponse : IResponse
	{
		/// <summary>
		///  A dictionary of nodeId => nodeinfo of nodes that verified the repository
		/// </summary>
		[DataMember(Name = "nodes")]
		[JsonFormatter(typeof(VerbatimDictionaryInterfaceKeysFormatter<string, CompactNodeInfo>))]
		IReadOnlyDictionary<string, CompactNodeInfo> Nodes { get; }
	}

	[DataContract]
	public class VerifyRepositoryResponse : ResponseBase, IVerifyRepositoryResponse
	{
		/// <summary>
		///  A dictionary of nodeId => nodeinfo of nodes that verified the repository
		/// </summary>
		public IReadOnlyDictionary<string, CompactNodeInfo> Nodes { get; internal set; } = EmptyReadOnly<string, CompactNodeInfo>.Dictionary;
	}
}
