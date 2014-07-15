using System.Collections.Generic;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public class RootObjectMapping : ObjectMapping
	{
		[JsonProperty("type")]
		public override TypeNameMarker Type { get { return null; } }

		[JsonProperty("index_analyzer")]
		public string IndexAnalyzer { get; set; }

		[JsonProperty("search_analyzer")]
		public string SearchAnalyzer { get; set; }

		[JsonProperty("dynamic_date_formats")]
		public IEnumerable<string> DynamicDateFormats { get; set; }

		[JsonProperty("date_detection")]
		public bool? DateDetection { get; set; }

		[JsonProperty("numeric_detection")]
		public bool? NumericDetection { get; set; }


		//Special fields (From the documentation i cannot see that these also would apply to the base object mapping class

		[JsonProperty("_id")]
		public IdFieldMapping IdFieldMapping { get; set; }

		[JsonProperty("_source")]
		public SourceFieldMapping SourceFieldMapping { get; set; }

		[JsonProperty("_type")]
		public TypeFieldMapping TypeFieldMapping { get; set; }

		[JsonProperty("_all")]
		public IAllFieldMapping AllFieldMapping { get; set; }

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

		[JsonProperty("_meta")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		public FluentDictionary<string, object> Meta { get; set; }

		[JsonProperty("dynamic_templates", TypeNameHandling = TypeNameHandling.None)]
		[JsonConverter(typeof(DynamicTemplatesConverter))]
		public IDictionary<string, DynamicTemplate> DynamicTemplates { get; set; }

	}
}