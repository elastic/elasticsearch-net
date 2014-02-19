using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
		public NodeInfoResponse()
		{
			this.IsValid = true;
		}

		[JsonProperty(PropertyName = "cluster_name")]
		public string ClusterName { get; internal set; }

		[JsonProperty(PropertyName = "nodes")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		public Dictionary<string, NodeInfo> Nodes { get; set; }
	}
}
