using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest
{
    public class NodeState
    {
        [JsonProperty("name")]
        public string Name { get; internal set; }

        [JsonProperty("transport_address")]
        public string TransportAddress { get; internal set; }

        [JsonProperty("attributes")]
        public Dictionary<string, string> Attributes { get; internal set; }
    }

    public class RoutingTableState
    {
        [JsonProperty("indices")]
        public Dictionary<string, IndexRoutingTable> Indices { get; internal set; }
    }

    public class IndexRoutingTable
    {
        [JsonProperty("shards")]
        public Dictionary<string, List<RoutingShard>> Shards { get; internal set; }
    }

    public class RoutingShard
    {
        [JsonProperty("state")]
        public string State { get; internal set; }

        [JsonProperty("primary")]
        public bool Primary { get; internal set; }
        
        [JsonProperty("node")]
        public string Node { get; internal set; }

        [JsonProperty("relocating_node")]
        public string RelocatingNode { get; internal set; }

        [JsonProperty("shard")]
        public int Shard { get; internal set; }

        [JsonProperty("index")]
        public string Index { get; internal set; }
    }

    public class RoutingNodesState
    {
        [JsonProperty("unassigned")]
        public List<RoutingShard> Unassigned { get; internal set; }

        [JsonProperty("nodes")]
        public Dictionary<string, List<RoutingShard>> Nodes { get; internal set; }
    }

    public class MetadataState
    {
        //[JsonProperty("templates")]
        //public ?? Templates { get; internal set; }

        [JsonProperty("indices")]
        public Dictionary<string, MetadataIndexState> Indices { get; internal set; }
    }

    public class MetadataIndexState
    {
        [JsonProperty("state")]
        public string State { get; internal set; }

        [JsonProperty("settings")]
        public Dictionary<string, string> Settings { get; internal set; }

        [JsonProperty("mappings")]
        public Dictionary<string, dynamic> Mappings { get; internal set; }

        //[JsonProperty("aliases")]
        //public ?? Aliases { get; internal set; }
    }
}
