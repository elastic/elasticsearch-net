using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<TypeMapping>))]
	public interface ITypeMapping
	{
		[JsonProperty("dynamic_date_formats")]
		IEnumerable<string> DynamicDateFormats { get; set; }

		[JsonProperty("date_detection")]
		bool? DateDetection { get; set; }

		[JsonProperty("numeric_detection")]
		bool? NumericDetection { get; set; }

		[JsonProperty("transform")]
		[JsonConverter(typeof(MappingTransformCollectionJsonConverter))]
		IList<IMappingTransform> Transform { get; set; }

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
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter))]
		FluentDictionary<string, object> Meta { get; set; }

		[JsonProperty("dynamic_templates", TypeNameHandling = TypeNameHandling.None)]
		[JsonConverter(typeof(DynamicTemplatesJsonConverter))]
		IDictionary<string, DynamicTemplate> DynamicTemplates { get; set; }

		[JsonProperty("dynamic")]
		DynamicMapping? Dynamic { get; set; }

		[JsonProperty("properties", TypeNameHandling = TypeNameHandling.None)]
		IProperties Properties { get; set; }
	}

	public class TypeMapping : ITypeMapping
	{
		/// <inheritdoc/>
		public IAllField AllField { get; set; }
		/// <inheritdoc/>
		public string Analyzer { get; set; }
		/// <inheritdoc/>
		public IBoostField BoostField { get; set; }
		/// <inheritdoc/>
		public bool? DateDetection { get; set; }
		/// <inheritdoc/>
		public DynamicMapping? Dynamic { get; set; }
		/// <inheritdoc/>
		public IEnumerable<string> DynamicDateFormats { get; set; }
		/// <inheritdoc/>
		public IDictionary<string, DynamicTemplate> DynamicTemplates { get; set; }
		/// <inheritdoc/>
		public IFieldNamesField FieldNamesField { get; set; }
		/// <inheritdoc/>
		public IIdField IdField { get; set; }
		/// <inheritdoc/>
		public IIndexField IndexField { get; set; }
		/// <inheritdoc/>
		public FluentDictionary<string, object> Meta { get; set; }
		/// <inheritdoc/>
		public bool? NumericDetection { get; set; }
		/// <inheritdoc/>
		public IParentField ParentField { get; set; }
		/// <inheritdoc/>
		public IProperties Properties { get; set; }
		/// <inheritdoc/>
		public IRoutingField RoutingField { get; set; }
		/// <inheritdoc/>
		public string SearchAnalyzer { get; set; }
		/// <inheritdoc/>
		public ISizeField SizeField { get; set; }
		/// <inheritdoc/>
		public ISourceField SourceField { get; set; }
		/// <inheritdoc/>
		public ITimestampField TimestampField { get; set; }
		/// <inheritdoc/>
		public IList<IMappingTransform> Transform { get; set; }
		/// <inheritdoc/>
		public ITtlField TtlField { get; set; }
		/// <inheritdoc/>
		public ITypeField TypeField { get; set; }
	}


	public class TypeMappingDescriptor<T> : DescriptorBase<TypeMappingDescriptor<T>, ITypeMapping>, ITypeMapping
		where T : class
	{
		IAllField ITypeMapping.AllField { get; set; }
		string ITypeMapping.Analyzer { get; set; }
		IBoostField ITypeMapping.BoostField { get; set; }
		bool? ITypeMapping.DateDetection { get; set; }
		DynamicMapping? ITypeMapping.Dynamic { get; set; }
		IEnumerable<string> ITypeMapping.DynamicDateFormats { get; set; }
		IDictionary<string, DynamicTemplate> ITypeMapping.DynamicTemplates { get; set; }
		IFieldNamesField ITypeMapping.FieldNamesField { get; set; }
		IIdField ITypeMapping.IdField { get; set; }
		IIndexField ITypeMapping.IndexField { get; set; }
		FluentDictionary<string, object> ITypeMapping.Meta { get; set; }
		bool? ITypeMapping.NumericDetection { get; set; }
		IParentField ITypeMapping.ParentField { get; set; }
		IProperties ITypeMapping.Properties { get; set; }
		IRoutingField ITypeMapping.RoutingField { get; set; }
		string ITypeMapping.SearchAnalyzer { get; set; }
		ISizeField ITypeMapping.SizeField { get; set; }
		ISourceField ITypeMapping.SourceField { get; set; }
		ITimestampField ITypeMapping.TimestampField { get; set; }
		IList<IMappingTransform> ITypeMapping.Transform { get; set; }
		ITtlField ITypeMapping.TtlField { get; set; }
		ITypeField ITypeMapping.TypeField { get; set; }

		/// <summary>
		/// Convenience method to map as much as it can based on ElasticType attributes set on the type.
		/// <pre>This method also automatically sets up mappings for known values types (int, long, double, datetime, etcetera)</pre>
		/// <pre>Class types default to object and Enums to int</pre>
		/// <pre>Later calls can override whatever is set is by this call.</pre>
		/// </summary>
		public TypeMappingDescriptor<T> AutoMap(IPropertyVisitor visitor = null) => Assign(a => a.Properties = new PropertyWalker(typeof(T), visitor).GetProperties());

		/// <inheritdoc/>
		public TypeMappingDescriptor<T> Dynamic(DynamicMapping dynamic) => Assign(a => a.Dynamic = dynamic);

		/// <inheritdoc/>
		public TypeMappingDescriptor<T> Dynamic(bool dynamic = true) => this.Dynamic(dynamic ? DynamicMapping.Allow : DynamicMapping.Ignore);

		/// <inheritdoc/>
		public TypeMappingDescriptor<T> SetParent(TypeName parentType) => Assign(a => a.ParentField = new ParentField { Type = parentType });

		/// <inheritdoc/>
		public TypeMappingDescriptor<T> SetParent<K>() where K : class => Assign(a => a.ParentField = new ParentField { Type = typeof(K) });

		/// <inheritdoc/>
		public TypeMappingDescriptor<T> Analyzer(string analyzer) => Assign(a => a.Analyzer = analyzer);

		/// <inheritdoc/>
		public TypeMappingDescriptor<T> SearchAnalyzer(string searchAnalyzer)=> Assign(a => a.SearchAnalyzer = searchAnalyzer);

		/// <inheritdoc/>
		public TypeMappingDescriptor<T> AllField(Func<AllFieldDescriptor, AllFieldDescriptor> allFieldSelector) => Assign(a => a.AllField = allFieldSelector?.Invoke(new AllFieldDescriptor()));

		/// <inheritdoc/>
		public TypeMappingDescriptor<T> IndexField(Func<IndexFieldDescriptor, IndexFieldDescriptor> indexFieldSelector) => Assign(a => a.IndexField = indexFieldSelector?.Invoke(new IndexFieldDescriptor()));

		/// <inheritdoc/>
		public TypeMappingDescriptor<T> SizeField(Func<SizeFieldDescriptor, SizeFieldDescriptor> sizeFieldSelector) => Assign(a => a.SizeField = sizeFieldSelector?.Invoke(new SizeFieldDescriptor()));

		/// <inheritdoc/>
		public TypeMappingDescriptor<T> DisableSizeField(bool disabled = true) => Assign(a => a.SizeField = new SizeField { Enabled = !disabled });

		/// <inheritdoc/>
		public TypeMappingDescriptor<T> DisableIndexField(bool disabled = true) => Assign(a => a.IndexField = new IndexField { Enabled = !disabled });

		/// <inheritdoc/>
		public TypeMappingDescriptor<T> DynamicDateFormats(IEnumerable<string> dateFormats) => Assign(a => a.DynamicDateFormats = dateFormats);

		/// <inheritdoc/>
		public TypeMappingDescriptor<T> DateDetection(bool detect = true) => Assign(a => a.DateDetection = detect);

		/// <inheritdoc/>
		public TypeMappingDescriptor<T> NumericDetection(bool detect = true) => Assign(a => a.NumericDetection = detect);

		/// <inheritdoc/>
		public TypeMappingDescriptor<T> Transform(Func<MappingTransformDescriptor, IMappingTransform> mappingTransformSelector)
		{
			//TODO MappingTransform needs a descriptor so we no longer make this call mutate state
			var t = mappingTransformSelector?.Invoke(new MappingTransformDescriptor());
			if (t == null) return this;
			if (Self.Transform == null) Self.Transform = new List<IMappingTransform>();
			Self.Transform.Add(t);
			return this;
		}

		/// <inheritdoc/>
		public TypeMappingDescriptor<T> IdField(Func<IdFieldDescriptor, IIdField> idMapper) => Assign(a => a.IdField = idMapper?.Invoke(new IdFieldDescriptor()));

		/// <inheritdoc/>
		public TypeMappingDescriptor<T> TypeField(Func<TypeFieldDescriptor, ITypeField> typeMapper) => Assign(a => a.TypeField = typeMapper?.Invoke(new TypeFieldDescriptor()));

		/// <inheritdoc/>
		public TypeMappingDescriptor<T> SourceField(Func<SourceFieldDescriptor, ISourceField> sourceMapper) => Assign(a => a.SourceField = sourceMapper?.Invoke(new SourceFieldDescriptor()));

		/// <inheritdoc/>
		public TypeMappingDescriptor<T> BoostField(Func<BoostFieldDescriptor<T>, IBoostField> boostMapper) => Assign(a => a.BoostField = boostMapper?.Invoke(new BoostFieldDescriptor<T>()));

		/// <inheritdoc/>
		public TypeMappingDescriptor<T> RoutingField(Func<RoutingFieldDescriptor<T>, IRoutingField> routingMapper) => Assign(a => a.RoutingField = routingMapper?.Invoke(new RoutingFieldDescriptor<T>()));

		/// <inheritdoc/>
		public TypeMappingDescriptor<T> TimestampField(Func<TimestampFieldDescriptor<T>, ITimestampField> timestampMapper) => Assign(a => a.TimestampField = timestampMapper?.Invoke(new TimestampFieldDescriptor<T>()));

		/// <inheritdoc/>
		public TypeMappingDescriptor<T> FieldNamesField(Func<FieldNamesFieldDescriptor<T>, IFieldNamesField> fieldNamesMapper) => Assign(a => a.FieldNamesField = fieldNamesMapper.Invoke(new FieldNamesFieldDescriptor<T>()));

		/// <inheritdoc/>
		public TypeMappingDescriptor<T> TtlField(Func<TtlFieldDescriptor, ITtlField> ttlFieldMapper) => Assign(a => a.TtlField = ttlFieldMapper?.Invoke(new TtlFieldDescriptor()));

		/// <inheritdoc/>
		public TypeMappingDescriptor<T> Meta(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> metaSelector) => Assign(a => a.Meta = metaSelector(new FluentDictionary<string, object>()));

		public TypeMappingDescriptor<T> Properties(Func<PropertiesDescriptor<T>, PropertiesDescriptor<T>> propertiesSelector) => 
			Assign(a => a.Properties = propertiesSelector?.Invoke(new PropertiesDescriptor<T>(Self.Properties))?.PromisedValue);

		/// <inheritdoc/>
		public TypeMappingDescriptor<T> DynamicTemplates(Func<DynamicTemplatesDescriptor<T>, DynamicTemplatesDescriptor<T>> dynamicTemplatesSelector)
		{
			//TODO _DELETES concept is wrong?
			dynamicTemplatesSelector.ThrowIfNull("dynamicTemplatesSelector");
			var templates = dynamicTemplatesSelector(new DynamicTemplatesDescriptor<T>());
			if (Self.DynamicTemplates == null)
				Self.DynamicTemplates = new Dictionary<string, DynamicTemplate>();
			foreach (var t in templates._Deletes)
				Self.DynamicTemplates.Remove(t);
			foreach (var t in templates.Templates)
				Self.DynamicTemplates[t.Key] = t.Value;
			return this;
		}

	}
}
