using Newtonsoft.Json;
using System.Collections.Generic;

namespace ElasticSearch.Client
{
    [JsonObject]
    public class IndexSegment
    {
        [JsonProperty(PropertyName="shards")]
        public Dictionary<string, ShardsSegment> Shards { get; internal set; }
    }
}