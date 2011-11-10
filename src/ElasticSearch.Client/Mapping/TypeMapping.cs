using System.Collections.Generic;
using Newtonsoft.Json;

namespace ElasticSearch.Client.Mapping
{
    public class TypeMapping
    {
        public TypeMapping(string name)
        {
            this.Name = name;
            this.Properties = new Dictionary<string, TypeMappingProperty>();
            this.Source = new TypeMappingSource();
        }

        [JsonIgnore]
        public string Name { get; set; }

        [JsonProperty("_source")]
        public TypeMappingSource Source { get; set; }

        [JsonProperty("properties")]
        public IDictionary<string, TypeMappingProperty> Properties { get; set; }
    }
}