using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface ITypeMapping
	{
		TypeName Type { get; set; }

		[JsonProperty("dynamic_date_formats")]
		IEnumerable<string> DynamicDateFormats { get; set; }

		[JsonProperty("date_detection")]
		bool? DateDetection { get; set; }

		[JsonProperty("numeric_detection")]
		bool? NumericDetection { get; set; }

		[JsonProperty("transform")]
		[JsonConverter(typeof(MappingTransformJsonConverter))]
		IList<MappingTransform> Transform { get; set; }

		[JsonProperty("analyzer")]
		string Analyzer { get; set; }

		[JsonProperty("search_analyzer")]
		string SearchAnalyzer { get; set; }

		[JsonProperty("_id")]
		IIdField IdField { get; set; }

		[JsonProperty("_source")]
		ISourceField SourceField { get; set; }

		[JsonProperty("_type")]
		ITypeField TypeField { get; set; }

		[JsonProperty("_all")]
		IAllField AllField { get; set; }

		[JsonProperty("_boost")]
		IBoostField BoostField { get; set; }

		[JsonProperty("_parent")]
		IParentField ParentField { get; set; }

		[JsonProperty("_routing")]
		IRoutingField RoutingField { get; set; }

		[JsonProperty("_index")]
		IIndexField IndexField { get; set; }

		[JsonProperty("_size")]
		ISizeField SizeField { get; set; }

		[JsonProperty("_timestamp")]
		ITimestampField TimestampField { get; set; }

		[JsonProperty("_field_names")]
		IFieldNamesField FieldNamesField { get; set; }

		[JsonProperty("_ttl")]
		ITtlField TtlField { get; set; }

		[JsonProperty("_meta")]
		[JsonConverter(typeof(DictionaryKeysAreNotFieldNamesJsonConverter))]
		FluentDictionary<string, object> Meta { get; set; }

		[JsonProperty("dynamic_templates", TypeNameHandling = TypeNameHandling.None)]
		[JsonConverter(typeof(DynamicTemplatesJsonConverter))]
		IDictionary<string, DynamicTemplate> DynamicTemplates { get; set; }

		[JsonProperty("dynamic")]
		DynamicMapping? Dynamic { get; set; }

		[JsonProperty("properties", TypeNameHandling = TypeNameHandling.None)]
		[JsonConverter(typeof(PropertiesJsonConverter))]
		IDictionary<FieldName, IElasticsearchProperty> Properties { get; set; }
	}

	public class TypeMapping : ITypeMapping
	{
		public TypeName Type { get; set; }
		public IAllField AllField { get; set; }
		public string Analyzer { get; set; }
		public IBoostField BoostField { get; set; }
		public bool? DateDetection { get; set; }
		public DynamicMapping? Dynamic { get; set; }
		public IEnumerable<string> DynamicDateFormats { get; set; }
		public IDictionary<string, DynamicTemplate> DynamicTemplates { get; set; }
		public IFieldNamesField FieldNamesField { get; set; }
		public IIdField IdField { get; set; }
		public IIndexField IndexField { get; set; }
		public FluentDictionary<string, object> Meta { get; set; }
		public bool? NumericDetection { get; set; }
		public IParentField ParentField { get; set; }
		public IDictionary<FieldName, IElasticsearchProperty> Properties { get; set; }
		public IRoutingField RoutingField { get; set; }
		public string SearchAnalyzer { get; set; }
		public ISizeField SizeField { get; set; }
		public ISourceField SourceField { get; set; }
		public ITimestampField TimestampField { get; set; }
		public IList<MappingTransform> Transform { get; set; }
		public ITtlField TtlField { get; set; }
		public ITypeField TypeField { get; set; }
	}
}
