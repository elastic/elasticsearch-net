using System.Collections.Generic;
using Newtonsoft.Json;

namespace ElasticSearch.Client.Mapping
{
    public class TypeMapping
    {
        public TypeMapping()
        {
            this.Properties = new Dictionary<string, TypeMappingProperty>();
        }

        [JsonProperty("properties")]
        public IDictionary<string, TypeMappingProperty> Properties { get; set; }
    }
}
