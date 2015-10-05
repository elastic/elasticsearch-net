using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	public interface INodesStatsResponse : IResponse
	{
		string ClusterName { get; }
		Dictionary<string, NodeStats> Nodes { get; }
	}

	[JsonObject]
	public class NodesStatsRsponse : BaseResponse, INodesStatsResponse
	{
		[JsonProperty(PropertyName = "cluster_name")]
		public string ClusterName { get; internal set; }
		[JsonProperty(PropertyName = "nodes")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter))]
		public Dictionary<string, NodeStats> Nodes { get; set; }
	}
}
