using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
    [JsonObject]
    public class MultiHit<T> where T : class
    {
        [JsonProperty("docs")]
        public IEnumerable<Hit<T>> Hits { get; internal set; }
    }
}
