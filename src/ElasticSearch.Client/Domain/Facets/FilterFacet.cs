using Newtonsoft.Json;

namespace ElasticSearch.Client
{
    public class FilterFacet : Facet
    {
        [JsonProperty(PropertyName = "count")]
        public int Count { get; internal set; }
    }
}
