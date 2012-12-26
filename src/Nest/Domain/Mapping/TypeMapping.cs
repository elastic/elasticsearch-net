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
        }

        [JsonIgnore]
        public string Name { get; set; }

		[JsonProperty("_id")]
		public IdFieldMapping IdFieldMapping { get; set; }

        [JsonProperty("_source")]
        public SourceFieldMapping SourceFieldMapping { get; set; }

		[JsonProperty("_type")]
		public TypeFieldMapping TypeFieldMapping { get; set; }

		[JsonProperty("_all")]
		public AllFieldMapping AllFieldMapping { get; set; }

		[JsonProperty("_analyzer")]
		public AnalyzerFieldMapping AnalyzerFieldMapping { get; set; }

		[JsonProperty("_boost")]
		public BoostFieldMapping BoostFieldMapping { get; set; }

        [JsonProperty("properties")]
        public IDictionary<string, TypeMappingProperty> Properties { get; set; }

        [JsonProperty("_parent")]
        public TypeMappingParent Parent { get; set; }

		[JsonProperty("_routing")]
		public RoutingFieldMapping RoutingFieldMapping { get; set; }
    }
}