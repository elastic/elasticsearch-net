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

		//TODO START: Still expose this on TypeMappingDescriptor

		[JsonProperty("dynamic")]
		public bool? Dynamic { get; set; }

		[JsonProperty("dynamic")]
		public bool? Enabled { get; set; }

		[JsonProperty("date_detection")]
		public bool? DateDetection { get; set; }

		[JsonProperty("numeric_detection")]
		public bool? NumericDetection { get; set; }

		[JsonProperty("include_in_all")]
		public bool? IncludeInAll { get; set; }

		[JsonProperty("index_analyzer")]
		public string IndexAnalyzer { get; set; }

		[JsonProperty("search_analyzer")]
		public string SearchAnalyzer { get; set; }

		[JsonProperty("path")]
		public string Path { get; set; }

		[JsonProperty("dynamic_date_formats")]
		public IEnumerable<string> DynamicDateFormats { get; set; }

		//NESTED TODO also map dynamic_templates

		//TODO END


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

        [JsonProperty("_parent")]
        public TypeMappingParent Parent { get; set; }

		[JsonProperty("_routing")]
		public RoutingFieldMapping RoutingFieldMapping { get; set; }

		[JsonProperty("_index")]
		public IndexFieldMapping IndexFieldMapping { get; set; }

		[JsonProperty("_size")]
		public SizeFieldMapping SizeFieldMapping { get; set; }

		[JsonProperty("_timestamp")]
		public TimestampFieldMapping TimestampFieldMapping { get; set; }

		[JsonProperty("_ttl")]
		public TtlFieldMapping TtlFieldMapping { get; set; }

		[JsonProperty("properties")]
		public IDictionary<string, TypeMappingProperty> Properties { get; set; }
		
    }
}