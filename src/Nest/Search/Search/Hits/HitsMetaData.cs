using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
    [JsonObject]
    public class HitsMetaData<T> where T : class
    {
        [JsonProperty("total")]
        public long Total { get; internal set; }

        [JsonProperty("max_score")]
        public double MaxScore { get; internal set; }

        [JsonProperty("hits")]
        public List<IHit<T>> Hits { get; internal set; }


    }
}
