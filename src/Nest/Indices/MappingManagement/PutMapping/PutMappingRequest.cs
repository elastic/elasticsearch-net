using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<PutMappingRequest>))]
	public interface IPutMappingRequest : IIndicesTypePath<PutMappingRequestParameters>, ITypeMapping
	{
	}

	public interface IPutMappingRequest<T> : IPutMappingRequest where T : class {}

	internal static class PutMappingPathInfo
	{
		public static void Update(ElasticsearchPathInfo<PutMappingRequestParameters> pathInfo, IPutMappingRequest request)
		{
			pathInfo.HttpMethod = HttpMethod.PUT;
		}
	}

	public partial class PutMappingRequest : IndicesTypePathBase<PutMappingRequestParameters>, IPutMappingRequest
	{
		public PutMappingRequest()
		{
		}

		/// <summary>
		/// Calls putmapping on /_all/{type}
		/// </summary>
		public PutMappingRequest(TypeName type)
		{
			this.Type = type;
			this.AllIndices = true;
		}

		/// <summary>
		/// Calls putmapping on /{indices}/{type}
		/// </summary>
		public PutMappingRequest(IEnumerable<IndexName> indices, TypeName type)
		{
			this.Type = type;
			this.Indices = indices;
		}

		/// <summary>
		/// Calls putmapping on /{index}/{type}
		/// </summary>
		public PutMappingRequest(IndexName index, TypeName type)
		{
			this.Type = type;
			this.Indices = new [] { index };
		}

		public IAllField AllField { get; set; }
		public IBoostField BoostField { get; set; }
		public bool? DateDetection { get; set; }
		public IEnumerable<string> DynamicDateFormats { get; set; }
		public IDictionary<string, DynamicTemplate> DynamicTemplates { get; set; }
		public DynamicMapping? Dynamic { get; set; }
		public string Analyzer { get; set; }
		public string SearchAnalyzer { get; set; }
		public IFieldNamesField FieldNamesField { get; set; }
		public IIdField IdField { get; set; }
		public IIndexField IndexField { get; set; }
		public FluentDictionary<string, object> Meta { get; set; }
		public bool? NumericDetection { get; set; }
		public IParentField ParentField { get; set; }
		public IDictionary<FieldName, IElasticsearchProperty> Properties { get; set; }
		public IRoutingField RoutingField { get; set; }
		public ISizeField SizeField { get; set; }
		public ISourceField SourceField { get; set; }
		public ITimestampField TimestampField { get; set; }
		public IList<MappingTransform> Transform { get; set; }
		public ITtlField TtlField { get; set; }
		public ITypeField TypeField { get; set; }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<PutMappingRequestParameters> pathInfo)
		{
			PutMappingPathInfo.Update(pathInfo, this);
		}
	}

	public partial class PutMappingRequest<T> : IndicesTypePathBase<PutMappingRequestParameters, T>, IPutMappingRequest<T>
		where T : class
	{
		public IAllField AllField { get; set; }
		public IBoostField BoostField { get; set; }
		public bool? DateDetection { get; set; }
		public IEnumerable<string> DynamicDateFormats { get; set; }
		public IDictionary<string, DynamicTemplate> DynamicTemplates { get; set; }
		public DynamicMapping? Dynamic { get; set; }
		public string Analyzer { get; set; }
		public string SearchAnalyzer { get; set; }
		public IFieldNamesField FieldNamesField { get; set; }
		public IIdField IdField { get; set; }
		public IIndexField IndexField { get; set; }
		public FluentDictionary<string, object> Meta { get; set; }
		public bool? NumericDetection { get; set; }
		public IParentField ParentField { get; set; }
		public IDictionary<FieldName, IElasticsearchProperty> Properties { get; set; }
		public IRoutingField RoutingField { get; set; }
		public ISizeField SizeField { get; set; }
		public ISourceField SourceField { get; set; }
		public ITimestampField TimestampField { get; set; }
		public IList<MappingTransform> Transform { get; set; }
		public ITtlField TtlField { get; set; }
		public ITypeField TypeField { get; set; }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<PutMappingRequestParameters> pathInfo)
		{
			PutMappingPathInfo.Update(pathInfo, this);
		}
	}

	[DescriptorFor("IndicesPutMapping")]
	public partial class PutMappingDescriptor<T> :
		IndicesTypePathDescriptor<PutMappingDescriptor<T>, PutMappingRequestParameters, T>, IPutMappingRequest<T>
		where T : class
	{
		private ITypeMapping Self => this;

		IAllField ITypeMapping.AllField { get; set; }
		IBoostField ITypeMapping.BoostField { get; set; }
		bool? ITypeMapping.DateDetection { get; set; }
		IEnumerable<string> ITypeMapping.DynamicDateFormats { get; set; }
		string ITypeMapping.Analyzer { get; set; }
		string ITypeMapping.SearchAnalyzer { get; set; }
		IDictionary<string, DynamicTemplate> ITypeMapping.DynamicTemplates { get; set; }
		DynamicMapping? ITypeMapping.Dynamic { get; set; }
		IFieldNamesField ITypeMapping.FieldNamesField { get; set; }
		IIdField ITypeMapping.IdField { get; set; }
		IIndexField ITypeMapping.IndexField { get; set; }
		FluentDictionary<string, object> ITypeMapping.Meta { get; set; }
		bool? ITypeMapping.NumericDetection { get; set; }
		IParentField ITypeMapping.ParentField { get; set; }
		IDictionary<FieldName, IElasticsearchProperty> ITypeMapping.Properties { get; set; }
		IRoutingField ITypeMapping.RoutingField { get; set; }
		ISizeField ITypeMapping.SizeField { get; set; }
		ISourceField ITypeMapping.SourceField { get; set; }
		ITimestampField ITypeMapping.TimestampField { get; set; }
		IList<MappingTransform> ITypeMapping.Transform { get; set; }
		ITtlField ITypeMapping.TtlField { get; set; }
		ITypeField ITypeMapping.TypeField { get; set; }

		/// <summary>
		/// Convenience method to map as much as it can based on ElasticType attributes set on the type.
		/// <pre>This method also automatically sets up mappings for known values types (int, long, double, datetime, etcetera)</pre>
		/// <pre>Class types default to object and Enums to int</pre>
		/// <pre>Later calls can override whatever is set is by this call.</pre>
		/// </summary>
		public PutMappingDescriptor<T> AutoMap(IPropertyVisitor visitor = null)
		{
			var walker = new PropertyWalker(typeof(T), visitor);
			Self.Properties = walker.GetProperties();
			return this;
		}
		
		public PutMappingDescriptor<T> Dynamic(DynamicMapping dynamic)
		{
			Self.Dynamic = dynamic;
			return this;
		}

		public PutMappingDescriptor<T> Dynamic(bool dynamic = true)
		{
			return this.Dynamic(dynamic ? DynamicMapping.Allow : DynamicMapping.Ignore);
		}

		public PutMappingDescriptor<T> SetParent(string parentType)
		{
			Self.ParentField = new ParentField { Type = parentType };
			return this;
		}

		public PutMappingDescriptor<T> SetParent<K>() where K : class
		{
			var parentType = TypeName.Create<K>();
			Self.ParentField = new ParentField { Type = parentType };
			return this;
		}

		public PutMappingDescriptor<T> Analyzer(string analyzer)
		{
			Self.Analyzer = analyzer;
			return this;
		}

		public PutMappingDescriptor<T> SearchAnalyzer(string searchAnalyzer)
		{
			Self.SearchAnalyzer = searchAnalyzer;
			return this;
		}

		public PutMappingDescriptor<T> AllField(Func<AllFieldDescriptor, AllFieldDescriptor> allFieldSelector)
		{
			Self.AllField = allFieldSelector(new AllFieldDescriptor());
			return this;
		}

		public PutMappingDescriptor<T> IndexField(Func<IndexFieldDescriptor, IndexFieldDescriptor> indexFieldSelector)
		{
			Self.IndexField = indexFieldSelector(new IndexFieldDescriptor());
			return this;
		}

		public PutMappingDescriptor<T> SizeField(Func<SizeFieldDescriptor, SizeFieldDescriptor> sizeFieldSelector)
		{
			Self.SizeField = sizeFieldSelector(new SizeFieldDescriptor());
			return this;
		}

		public PutMappingDescriptor<T> DisableSizeField(bool disabled = true)
		{
			Self.SizeField = new SizeField { Enabled = !disabled };
			return this;
		}

		public PutMappingDescriptor<T> DisableIndexField(bool disabled = true)
		{
			Self.IndexField = new IndexField { Enabled = !disabled };
			return this;
		}

		public PutMappingDescriptor<T> DynamicDateFormats(IEnumerable<string> dateFormats)
		{
			Self.DynamicDateFormats = dateFormats;
			return this;
		}

		public PutMappingDescriptor<T> DateDetection(bool detect = true)
		{
			Self.DateDetection = detect;
			return this;
		}

		public PutMappingDescriptor<T> NumericDetection(bool detect = true)
		{
			Self.NumericDetection = detect;
			return this;
		}

		public PutMappingDescriptor<T> Transform(Func<MappingTransformDescriptor, MappingTransformDescriptor> mappingTransformSelector)
		{
			mappingTransformSelector.ThrowIfNull("mappingTransformSelector");
			var transformDescriptor = mappingTransformSelector(new MappingTransformDescriptor());
			if (Self.Transform == null)
				Self.Transform = new List<MappingTransform>();
			Self.Transform.Add(transformDescriptor._mappingTransform);
			return this;
		}

		public PutMappingDescriptor<T> IdField(Func<IdFieldDescriptor, IIdField> idMapper)
		{
			idMapper.ThrowIfNull("idMapper");
			Self.IdField = idMapper(new IdFieldDescriptor());
			return this;
		}

		public PutMappingDescriptor<T> TypeField(Func<TypeFieldDescriptor, ITypeField> typeMapper)
		{
			typeMapper.ThrowIfNull("typeMapper");
			Self.TypeField = typeMapper(new TypeFieldDescriptor());
			return this;
		}

		public PutMappingDescriptor<T> SourceField(Func<SourceFieldDescriptor, ISourceField> sourceMapper)
		{
			sourceMapper.ThrowIfNull("sourceMapper");
			Self.SourceField = sourceMapper(new SourceFieldDescriptor());
			return this;
		}

		public PutMappingDescriptor<T> BoostField(Func<BoostFieldDescriptor<T>, IBoostField> boostMapper)
		{
			boostMapper.ThrowIfNull("boostMapper");
			Self.BoostField = boostMapper(new BoostFieldDescriptor<T>());
			return this;
		}

		public PutMappingDescriptor<T> RoutingField(Func<RoutingFieldDescriptor<T>, IRoutingField> routingMapper)
		{
			routingMapper.ThrowIfNull("routingMapper");
			Self.RoutingField = routingMapper(new RoutingFieldDescriptor<T>());
			return this;
		}

		public PutMappingDescriptor<T> TimestampField(Func<TimestampFieldDescriptor<T>, ITimestampField> timestampMapper)
		{
			timestampMapper.ThrowIfNull("timestampMapper");
			Self.TimestampField = timestampMapper(new TimestampFieldDescriptor<T>());
			return this;
		}

		public PutMappingDescriptor<T> FieldNamesField(Func<FieldNamesFieldDescriptor<T>, IFieldNamesField> fieldNamesMapper)
		{
			Self.FieldNamesField = fieldNamesMapper == null ? null : fieldNamesMapper(new FieldNamesFieldDescriptor<T>());
			return this;
		}

		public PutMappingDescriptor<T> TtlField(Func<TtlFieldDescriptor, ITtlField> ttlFieldMapper)
		{
			ttlFieldMapper.ThrowIfNull("ttlFieldMapper");
			Self.TtlField = ttlFieldMapper(new TtlFieldDescriptor());
			return this;
		}

		public PutMappingDescriptor<T> Properties(Func<PropertiesDescriptor<T>, PropertiesDescriptor<T>> propertiesSelector)
		{
			propertiesSelector.ThrowIfNull("propertiesSelector");
			var properties = propertiesSelector(new PropertiesDescriptor<T>());
			if (Self.Properties == null)
				Self.Properties = new Dictionary<FieldName, IElasticsearchProperty>();
			foreach (var p in properties.Properties)
				Self.Properties[p.Key] = p.Value;
			return this;
		}

		public PutMappingDescriptor<T> Meta(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> metaSelector)
		{
			metaSelector.ThrowIfNull("metaSelector");
			Self.Meta = metaSelector(new FluentDictionary<string, object>());
			return this;
		}

		public PutMappingDescriptor<T> DynamicTemplates(Func<DynamicTemplatesDescriptor<T>, DynamicTemplatesDescriptor<T>> dynamicTemplatesSelector)
		{
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

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<PutMappingRequestParameters> pathInfo)
		{
			PutMappingPathInfo.Update(pathInfo, this);
		}
	}
}