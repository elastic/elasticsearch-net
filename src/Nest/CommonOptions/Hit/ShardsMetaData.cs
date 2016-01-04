using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
    [JsonObject]
    public class ShardsMetaData
    {
        [JsonProperty]
        public int Total { get; internal set; }

        [JsonProperty]
        public int Successful { get; internal set; }

        [JsonProperty]
        public int Failed { get; internal set; }

        [JsonProperty("failures")]
        public IEnumerable<ShardFailure> Failures { get; internal set; }
    }
}