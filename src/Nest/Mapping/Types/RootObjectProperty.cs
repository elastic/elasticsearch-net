using System.Collections.Generic;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public class RootObjectProperty : ObjectProperty
	{
		[JsonProperty("type")]
		public override TypeName Type { get { return null; } }

		[JsonProperty("analyzer")]
		public string Analyzer { get; set; }

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

		[JsonProperty("_id")]
		public IIdField IdField { get; set; }

		[JsonProperty("_source")]
		public ISourceField SourceField { get; set; }

		[JsonProperty("_type")]
		public ITypeField TypeField { get; set; }

		[JsonProperty("_all")]
		public IAllField AllField { get; set; }

		[JsonProperty("_analyzer")]
		public IAnalyzerField AnalyzerField { get; set; }

		[JsonProperty("_boost")]
		public IBoostField BoostField { get; set; }

		[JsonProperty("_parent")]
		public IParentField ParentField { get; set; }

		[JsonProperty("_routing")]
		public IRoutingField RoutingField { get; set; }

		[JsonProperty("_index")]
		public IIndexField IndexField { get; set; }

		[JsonProperty("_size")]
		public ISizeField SizeField { get; set; }

		[JsonProperty("_timestamp")]
		public ITimestampField TimestampField { get; set; }

		[JsonProperty("_field_names")]
		public IFieldNamesField FieldNamesField { get; set; }

		[JsonProperty("_ttl")]
		public ITtlField TtlField { get; set; }

		[JsonProperty("_meta")]
		[JsonConverter(typeof(DictionaryKeysAreNotFieldNamesJsonConverter))]
		public FluentDictionary<string, object> Meta { get; set; }

		[JsonProperty("dynamic_templates", TypeNameHandling = TypeNameHandling.None)]
		[JsonConverter(typeof(DynamicTemplatesConverter))]
		public IDictionary<string, DynamicTemplate> DynamicTemplates { get; set; }

	}
}