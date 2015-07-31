using System.Collections.Generic;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public class RootObjectMapping : ObjectType
	{
		[JsonProperty("type")]
		public override TypeName Type { get { return null; } }

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

		[JsonProperty("transform")]
		[JsonConverter(typeof(MappingTransformConverter))]
		public IList<MappingTransform> Transform { get; set; }

		//Special fields (From the documentation i cannot see that these also would apply to the base object mapping class

		[JsonProperty("_id")]
		public IIdField IdFieldMappingDescriptor { get; set; }

		[JsonProperty("_source")]
		public ISourceField SourceFieldMappingDescriptor { get; set; }

		[JsonProperty("_type")]
		public ITypeField TypeFieldMappingDescriptor { get; set; }

		[JsonProperty("_all")]
		public IAllField AllFieldMapping { get; set; }

		[JsonProperty("_analyzer")]
		public IAnalyzerField AnalyzerFieldMapping { get; set; }

		[JsonProperty("_boost")]
		public IBoostField BoostFieldMapping { get; set; }

		[JsonProperty("_parent")]
		public ParentField Parent { get; set; }

		[JsonProperty("_routing")]
		public IRoutingField RoutingFieldMapping { get; set; }

		[JsonProperty("_index")]
		public IIndexField IndexFieldMapping { get; set; }

		[JsonProperty("_size")]
		public ISizeField SizeFieldMapping { get; set; }

		[JsonProperty("_timestamp")]
		public ITimestampField TimestampFieldMapping { get; set; }

		[JsonProperty("_field_names")]
		public IFieldNamesField FieldNamesFieldMapping { get; set; }

		[JsonProperty("_ttl")]
		public ITtlField TtlFieldMappingDescriptor { get; set; }

		[JsonProperty("_meta")]
		[JsonConverter(typeof(DictionaryKeysAreNotFieldNamesJsonConverter))]
		public FluentDictionary<string, object> Meta { get; set; }

		[JsonProperty("dynamic_templates", TypeNameHandling = TypeNameHandling.None)]
		[JsonConverter(typeof(DynamicTemplatesConverter))]
		public IDictionary<string, DynamicTemplate> DynamicTemplates { get; set; }

	}
}