using System.Collections.Generic;
using Newtonsoft.Json;

namespace ElasticSearch.Client
{
    [JsonObject]
    public class TermStatsFacet : Facet, IFacet<TermStats>
    {
        [JsonProperty("missing")]
        public int Missing { get; internal set; }

        [JsonProperty("terms")]
        public IEnumerable<TermStats> Items { get; internal set; }

    }
    public class TermStats : TermItem
    {
        [JsonProperty(PropertyName = "min")]
        public double Min { get; internal set; }

        [JsonProperty(PropertyName = "max")]
        public double Max { get; internal set; }

        [JsonProperty(PropertyName = "total")]
        public double Total { get; internal set; }

        [JsonProperty(PropertyName = "mean")]
        public double Mean { get; internal set; }

        [JsonProperty("total_count")]
        public int TotalCount { get; set; }
    }

}