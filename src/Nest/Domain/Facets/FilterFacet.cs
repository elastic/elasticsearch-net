using Newtonsoft.Json;

namespace Nest
{
    public class FilterFacet : Facet
    {
        [JsonProperty(PropertyName = "count")]
        public int Count { get; internal set; }
    }
}
