using Newtonsoft.Json;

namespace Nest
{
		public class QueryFacet : Facet
    {
        [JsonProperty(PropertyName = "count")]
        public int Count { get; internal set; }
    }
}
