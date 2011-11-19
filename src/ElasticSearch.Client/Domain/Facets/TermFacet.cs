using System.Collections.Generic;
using Newtonsoft.Json;

namespace ElasticSearch.Client
{
    [JsonObject]
    public class TermFacet : Facet, IFacet<TermItem>
    {
        [JsonProperty("missing")]
        public int Missing { get; internal set; }

        [JsonProperty("other")]
        public int Other { get; internal set; }

        [JsonProperty("total")]
        public int Total { get; internal set; }

        [JsonProperty("terms")]
        public IEnumerable<TermItem> Items { get; internal set; }
    }
    public class TermItem : FacetItem
    {
        [JsonProperty(PropertyName = "term")]
        public string Term { get; internal set; }
    }
}