using Newtonsoft.Json;

namespace Nest
{
		public class QueryFacet : Facet
    {
        [JsonProperty(PropertyName = "count")]
        public long Count { get; internal set; }
    }
}
