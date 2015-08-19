using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	public interface INodeStatsResponse : IResponse
	{
		string ClusterName { get; }
		Dictionary<string, NodeStats> Nodes { get; }
	}

	[JsonObject]
	public class NodeStatsResponse : BaseResponse, INodeStatsResponse
	{
		[JsonProperty(PropertyName = "cluster_name")]
		public string ClusterName { get; internal set; }
		[JsonProperty(PropertyName = "nodes")]
		[JsonConverter(typeof(DictionaryKeysAreNotFieldNamesJsonConverter))]
		public Dictionary<string, NodeStats> Nodes { get; set; }
	}
}
