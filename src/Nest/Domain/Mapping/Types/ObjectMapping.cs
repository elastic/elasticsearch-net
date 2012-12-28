using System.Collections.Generic;
using Newtonsoft.Json;
using System;

namespace Nest
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false)]
	public class ObjectMapping : Attribute, IElasticType
	{
		[JsonIgnore]
		public string Name { get; set; }

		[JsonProperty("type")]
		public virtual string Type { get { return "object"; } }

		//TODO START: Still expose this on (Root)ObjectMappingDescriptor

		[JsonProperty("dynamic")]
		public bool? Dynamic { get; set; }

		[JsonProperty("enabled")]
		public bool? Enabled { get; set; }

		[JsonProperty("include_in_all")]
		public bool? IncludeInAll { get; set; }

		[JsonProperty("path")]
		public string Path { get; set; }

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
		public ParentTypeMapping Parent { get; set; }

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
		public IDictionary<string, IElasticType> Properties { get; set; }


		public ObjectMapping()
		{
			this.Properties = new Dictionary<string, IElasticType>();
		}
	}
}