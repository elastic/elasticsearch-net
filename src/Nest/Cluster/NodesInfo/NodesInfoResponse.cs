using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface INodesInfoResponse : IResponse
	{
		string ClusterName { get; }
		Dictionary<string, NodeInfo> Nodes { get; }
	}

	[JsonObject]
	public class NodesInfoResponse : ResponseBase, INodesInfoResponse
	{
		[JsonProperty(PropertyName = "cluster_name")]
		public string ClusterName { get; internal set; }

		[JsonProperty(PropertyName = "nodes")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter))]
		public Dictionary<string, NodeInfo> Nodes { get; set; }
	}
}
