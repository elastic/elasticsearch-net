using Newtonsoft.Json;
using System.Collections.Generic;

namespace ElasticSearch.Client
{
    [JsonObject]
    public class ShardSegmentRouting
    {
        [JsonProperty(PropertyName="state")]
        public string State { get; internal set; }

        [JsonProperty(PropertyName = "primary")]
        public bool Primary { get; internal set; }

        [JsonProperty(PropertyName = "node")]
        public string Node { get; internal set; }
    }
}