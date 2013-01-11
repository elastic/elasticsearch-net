using Newtonsoft.Json;

namespace Nest
{
    public class ParentTypeMapping
    {
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
