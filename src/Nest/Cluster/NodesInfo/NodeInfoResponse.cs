using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	public interface INodeInfoResponse : IResponse
	{
		string ClusterName { get; }
		Dictionary<string, NodeInfo> Nodes { get; }
	}

	[JsonObject]
	public class NodeInfoResponse : BaseResponse, INodeInfoResponse
	{
		[JsonProperty(PropertyName = "cluster_name")]
		public string ClusterName { get; internal set; }

		[JsonProperty(PropertyName = "nodes")]
		[JsonConverter(typeof(DictionaryKeysAreNotFieldNamesJsonConverter))]
		public Dictionary<string, NodeInfo> Nodes { get; set; }
	}
}
