using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface INodesInfoResponse : IResponse
	{
		string ClusterName { get; }
		IReadOnlyDictionary<string, NodeInfo> Nodes { get; }
	}

	[JsonObject]
	public class NodesInfoResponse : ResponseBase, INodesInfoResponse
	{
		[JsonProperty("cluster_name")]
		public string ClusterName { get; internal set; }

		[JsonProperty("nodes")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<string, NodeInfo>))]
		public IReadOnlyDictionary<string, NodeInfo> Nodes { get; internal set; } = EmptyReadOnly<string, NodeInfo>.Dictionary;
	}
}
