using Newtonsoft.Json;

namespace ElasticSearch.Client.Mapping
{
    public class TypeMappingParent
    {
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
