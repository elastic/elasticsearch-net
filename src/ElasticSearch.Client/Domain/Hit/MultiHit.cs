using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace ElasticSearch.Client
{
    [JsonObject]
    public class MultiHit<T> where T : class
    {
        [JsonProperty("docs")]
        public IEnumerable<Hit<T>> Hits { get; internal set; }
    }
}
