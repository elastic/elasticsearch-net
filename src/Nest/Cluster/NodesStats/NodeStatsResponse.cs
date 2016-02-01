using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface INodesStatsResponse : IResponse
	{
		[JsonProperty(PropertyName = "cluster_name")]
		string ClusterName { get; }

		[JsonProperty(PropertyName = "nodes")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter))]
		Dictionary<string, NodeStats> Nodes { get; }
	}

	public class NodesStatsResponse : ResponseBase, INodesStatsResponse
	{
		public string ClusterName { get; internal set; }

		public Dictionary<string, NodeStats> Nodes { get; set; }
	}
}
