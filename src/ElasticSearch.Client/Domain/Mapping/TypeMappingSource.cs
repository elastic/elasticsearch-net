using Newtonsoft.Json;

namespace ElasticSearch.Client.Mapping
{
    public class TypeMappingSource
    {
        public TypeMappingSource()
        {
            this.Enabled = true;
        }

        [JsonProperty("enabled")]
        public bool Enabled { get; set; }
    }
}