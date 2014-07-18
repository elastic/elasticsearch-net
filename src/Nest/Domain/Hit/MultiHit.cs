using System;
using System.Collections.Generic;
using System.Linq;
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
