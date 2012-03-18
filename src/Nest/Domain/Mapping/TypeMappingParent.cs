using Newtonsoft.Json;

namespace Nest
{
    public class TypeMappingParent
    {
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
