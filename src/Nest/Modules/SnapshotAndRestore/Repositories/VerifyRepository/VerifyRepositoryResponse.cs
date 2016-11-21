using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IVerifyRepositoryResponse : IResponse
	{
		/// <summary>
		///  A dictionary of nodeId => nodeinfo of nodes that verified the repository
		/// </summary>
		[JsonProperty(PropertyName = "nodes")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter))]
		IReadOnlyDictionary<string, CompactNodeInfo> Nodes { get; }
	}

	[JsonObject]
	public class VerifyRepositoryResponse : ResponseBase, IVerifyRepositoryResponse
	{

		/// <summary>
		///  A dictionary of nodeId => nodeinfo of nodes that verified the repository
		/// </summary>
		public IReadOnlyDictionary<string, CompactNodeInfo> Nodes { get; internal set; } = EmptyReadOnly<string, CompactNodeInfo>.Dictionary;
	}
}
