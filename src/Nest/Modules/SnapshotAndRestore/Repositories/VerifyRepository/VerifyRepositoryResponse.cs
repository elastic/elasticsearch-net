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
		Dictionary<string, CompactNodeInfo> Nodes { get; set; }
	}

	[JsonObject]
	public class VerifyRepositoryResponse : ResponseBase, IVerifyRepositoryResponse
	{

		/// <summary>
		///  A dictionary of nodeId => nodeinfo of nodes that verified the repository
		/// </summary>
		public Dictionary<string, CompactNodeInfo> Nodes { get; set; }
	}
}
