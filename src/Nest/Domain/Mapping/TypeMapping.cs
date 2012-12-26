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
            this.SourceFieldMapping = new SourceFieldMapping();
			this.IdFieldMapping = new IdFieldMapping();
			this.TypeFieldMapping = new TypeFieldMapping();
            this.Parent = null;
        }

        [JsonIgnore]
        public string Name { get; set; }

		[JsonProperty("_id")]
		public IdFieldMapping IdFieldMapping { get; set; }

        [JsonProperty("_source")]
        public SourceFieldMapping SourceFieldMapping { get; set; }

		[JsonProperty("_type")]
		public TypeFieldMapping TypeFieldMapping { get; set; }

        [JsonProperty("properties")]
        public IDictionary<string, TypeMappingProperty> Properties { get; set; }

        [JsonProperty("_parent")]
        public TypeMappingParent Parent { get; set; }
    }
}