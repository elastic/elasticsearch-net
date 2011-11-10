using Newtonsoft.Json;

namespace ElasticSearch.Client
{
    public abstract class FacetItem
    {
        [JsonProperty("count")]
        public virtual int Count { get; internal set; }
    }
}