using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
    public class TypeMapping
    {
        public TypeMapping(string name)
        {
            this.Name = name;
            this.Properties = new Dictionary<string, TypeMappingProperty>();
            this.SourceMapping = new SourceMapping();
            this.Parent = null;
        }

        [JsonIgnore]
        public string Name { get; set; }

        [JsonProperty("_source")]
        public SourceMapping SourceMapping { get; set; }

		[JsonProperty("_id")]
		public IdMapping IdMapping { get; set; }

        [JsonProperty("properties")]
        public IDictionary<string, TypeMappingProperty> Properties { get; set; }

        [JsonProperty("_parent")]
        public TypeMappingParent Parent { get; set; }
    }
}