using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest
{
    public interface INodeInfoResponse : IResponse
    {
        bool OK { get; }
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
        [JsonProperty("ok")]
        public bool OK { get; internal set; }

        [JsonProperty(PropertyName = "cluster_name")]
        public string ClusterName { get; internal set; }

        [JsonProperty(PropertyName = "nodes")]
        public Dictionary<string, NodeInfo> Nodes { get; set; }
    }
}
