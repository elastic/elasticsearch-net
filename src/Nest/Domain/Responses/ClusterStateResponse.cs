using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
    public interface IClusterStateResponse : IResponse
    {
        string ClusterName { get; }
        string MasterNode { get; }
        Dictionary<string, NodeState> Nodes { get; }
        MetadataState Metadata { get; }
        RoutingTableState RoutingTable { get; }
        RoutingNodesState RoutingNodes { get; }
    }

    [JsonObject]
    public class ClusterStateResponse : BaseResponse, IClusterStateResponse
    {
        public ClusterStateResponse()
        {
            this.IsValid = true;
        }
        [JsonProperty("cluster_name")]
        public string ClusterName { get; internal set; }
        [JsonProperty("master_node")]
        public string MasterNode { get; internal set; }

        [JsonProperty("nodes")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		public Dictionary<string, NodeState> Nodes { get; internal set; }

        [JsonProperty("metadata")]
        public MetadataState Metadata { get; internal set; }

        [JsonProperty("routing_table")]
        public RoutingTableState RoutingTable { get; internal set; }
        
        [JsonProperty("routing_nodes")]
        public RoutingNodesState RoutingNodes { get; internal set; }
    }
}
