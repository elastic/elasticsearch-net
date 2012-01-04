using Newtonsoft.Json;

namespace Nest
{
    public abstract class FacetItem
    {
        [JsonProperty("count")]
        public virtual int Count { get; internal set; }
    }
}