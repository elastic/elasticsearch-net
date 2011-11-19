using System.Collections.Generic;
using Newtonsoft.Json;

namespace ElasticSearch.Client
{
    [JsonObject]
    public class HistogramFacet : Facet, IFacet<HistogramItem>
    {
        [JsonProperty("entries")]
        public IEnumerable<HistogramItem> Items { get; internal set; }
    }
    public class HistogramItem : FacetItem
    {
        [JsonProperty("key")]
        public double Key { get; set; }
    }

}