using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(PutMappingRequest.Json))]
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

		internal class Json : JsonConverter
		{
			public override bool CanConvert(Type objectType) => true;

			public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
			{
				reader.Read();
				var type = reader.Value as string;
				reader.Read();
				var mapping = serializer.Deserialize<TypeMapping>(reader);
				var request = new PutMappingRequest(type)
				{
					DynamicDateFormats = mapping.DynamicDateFormats,
					DateDetection = mapping.DateDetection,
					NumericDetection = mapping.NumericDetection,
					Transform = mapping.Transform,
					Analyzer = mapping.Analyzer,
					SearchAnalyzer = mapping.SearchAnalyzer,
					IdField = mapping.IdField,
					SourceField = mapping.SourceField,
					TypeField = mapping.TypeField,
					AllField = mapping.AllField,
					BoostField = mapping.BoostField,
					ParentField = mapping.ParentField,
					RoutingField = mapping.RoutingField,
					IndexField = mapping.IndexField,
					SizeField = mapping.SizeField,
					TimestampField = mapping.TimestampField,
					TtlField = mapping.TtlField,
					Meta = mapping.Meta,
					DynamicTemplates = mapping.DynamicTemplates,
					Dynamic = mapping.Dynamic,
					Properties = mapping.Properties
				};
				return request;
			}

			public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
			{
				var request = value as IPutMappingRequest;
				if (request != null)
				{
					serializer.Serialize(writer,
						new Dictionary<TypeName, IPutMappingRequest>
						{
							{ ((ITypeMapping)request).Type, request}
						}
					);
				}
			}
		}
	}

	public partial class PutMappingRequest<T> : IndicesTypePathBase<PutMappingRequestParameters, T>, IPutMappingRequest<T>
		where T : class
	{
		public PutMappingRequest()
		{
			this.Type = typeof(T);
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

	[DescriptorFor("IndicesPutMapping")]
	public partial class PutMappingDescriptor<T> :
		IndicesTypePathDescriptor<PutMappingDescriptor<T>, PutMappingRequestParameters, T>, IPutMappingRequest<T>
		where T : class
	{
		private IPutMappingRequest Self => this;
		private ITypeMapping Mapping => this;

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
		TypeName ITypeMapping.Type { get; set; }

		public PutMappingDescriptor()
		{
			((IIndicesTypePath<PutMappingRequestParameters>)this).Type = typeof(T);
			Mapping.Type = typeof(T);
		}

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
			Mapping.Dynamic = dynamic;
			return this;
		}

		public PutMappingDescriptor<T> Dynamic(bool dynamic = true)
		{
			return this.Dynamic(dynamic ? DynamicMapping.Allow : DynamicMapping.Ignore);
		}

		public PutMappingDescriptor<T> SetParent(string parentType)
		{
			Mapping.ParentField = new ParentField { Type = parentType };
			return this;
		}

		public PutMappingDescriptor<T> SetParent<K>() where K : class
		{
			var parentType = TypeName.Create<K>();
			Mapping.ParentField = new ParentField { Type = parentType };
			return this;
		}

		public PutMappingDescriptor<T> Analyzer(string analyzer)
		{
			Mapping.Analyzer = analyzer;
			return this;
		}

		public PutMappingDescriptor<T> SearchAnalyzer(string searchAnalyzer)
		{
			Mapping.SearchAnalyzer = searchAnalyzer;
			return this;
		}

		public PutMappingDescriptor<T> AllField(Func<AllFieldDescriptor, AllFieldDescriptor> allFieldSelector)
		{
			Mapping.AllField = allFieldSelector(new AllFieldDescriptor());
			return this;
		}

		public PutMappingDescriptor<T> IndexField(Func<IndexFieldDescriptor, IndexFieldDescriptor> indexFieldSelector)
		{
			Mapping.IndexField = indexFieldSelector(new IndexFieldDescriptor());
			return this;
		}

		public PutMappingDescriptor<T> SizeField(Func<SizeFieldDescriptor, SizeFieldDescriptor> sizeFieldSelector)
		{
			Mapping.SizeField = sizeFieldSelector(new SizeFieldDescriptor());
			return this;
		}

		public PutMappingDescriptor<T> DisableSizeField(bool disabled = true)
		{
			Mapping.SizeField = new SizeField { Enabled = !disabled };
			return this;
		}

		public PutMappingDescriptor<T> DisableIndexField(bool disabled = true)
		{
			Mapping.IndexField = new IndexField { Enabled = !disabled };
			return this;
		}

		public PutMappingDescriptor<T> DynamicDateFormats(IEnumerable<string> dateFormats)
		{
			Mapping.DynamicDateFormats = dateFormats;
			return this;
		}

		public PutMappingDescriptor<T> DateDetection(bool detect = true)
		{
			Mapping.DateDetection = detect;
			return this;
		}

		public PutMappingDescriptor<T> NumericDetection(bool detect = true)
		{
			Mapping.NumericDetection = detect;
			return this;
		}

		public PutMappingDescriptor<T> Transform(Func<MappingTransformDescriptor, MappingTransformDescriptor> mappingTransformSelector)
		{
			mappingTransformSelector.ThrowIfNull("mappingTransformSelector");
			var transformDescriptor = mappingTransformSelector(new MappingTransformDescriptor());
			if (Mapping.Transform == null)
				Mapping.Transform = new List<MappingTransform>();
			Mapping.Transform.Add(transformDescriptor._mappingTransform);
			return this;
		}

		public PutMappingDescriptor<T> IdField(Func<IdFieldDescriptor, IIdField> idMapper)
		{
			idMapper.ThrowIfNull("idMapper");
			Mapping.IdField = idMapper(new IdFieldDescriptor());
			return this;
		}

		public PutMappingDescriptor<T> TypeField(Func<TypeFieldDescriptor, ITypeField> typeMapper)
		{
			typeMapper.ThrowIfNull("typeMapper");
			Mapping.TypeField = typeMapper(new TypeFieldDescriptor());
			return this;
		}

		public PutMappingDescriptor<T> SourceField(Func<SourceFieldDescriptor, ISourceField> sourceMapper)
		{
			sourceMapper.ThrowIfNull("sourceMapper");
			Mapping.SourceField = sourceMapper(new SourceFieldDescriptor());
			return this;
		}

		public PutMappingDescriptor<T> BoostField(Func<BoostFieldDescriptor<T>, IBoostField> boostMapper)
		{
			boostMapper.ThrowIfNull("boostMapper");
			Mapping.BoostField = boostMapper(new BoostFieldDescriptor<T>());
			return this;
		}

		public PutMappingDescriptor<T> RoutingField(Func<RoutingFieldDescriptor<T>, IRoutingField> routingMapper)
		{
			routingMapper.ThrowIfNull("routingMapper");
			Mapping.RoutingField = routingMapper(new RoutingFieldDescriptor<T>());
			return this;
		}

		public PutMappingDescriptor<T> TimestampField(Func<TimestampFieldDescriptor<T>, ITimestampField> timestampMapper)
		{
			timestampMapper.ThrowIfNull("timestampMapper");
			Mapping.TimestampField = timestampMapper(new TimestampFieldDescriptor<T>());
			return this;
		}

		public PutMappingDescriptor<T> FieldNamesField(Func<FieldNamesFieldDescriptor<T>, IFieldNamesField> fieldNamesMapper)
		{
			Mapping.FieldNamesField = fieldNamesMapper == null ? null : fieldNamesMapper(new FieldNamesFieldDescriptor<T>());
			return this;
		}

		public PutMappingDescriptor<T> TtlField(Func<TtlFieldDescriptor, ITtlField> ttlFieldMapper)
		{
			ttlFieldMapper.ThrowIfNull("ttlFieldMapper");
			Mapping.TtlField = ttlFieldMapper(new TtlFieldDescriptor());
			return this;
		}

		public PutMappingDescriptor<T> Properties(Func<PropertiesDescriptor<T>, PropertiesDescriptor<T>> propertiesSelector)
		{
			propertiesSelector.ThrowIfNull("propertiesSelector");
			var properties = propertiesSelector(new PropertiesDescriptor<T>());
			if (Mapping.Properties == null)
				Mapping.Properties = new Dictionary<FieldName, IElasticsearchProperty>();
			foreach (var p in properties.Properties)
				Mapping.Properties[p.Key] = p.Value;
			return this;
		}

		public PutMappingDescriptor<T> Meta(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> metaSelector)
		{
			metaSelector.ThrowIfNull("metaSelector");
			Mapping.Meta = metaSelector(new FluentDictionary<string, object>());
			return this;
		}

		public PutMappingDescriptor<T> DynamicTemplates(Func<DynamicTemplatesDescriptor<T>, DynamicTemplatesDescriptor<T>> dynamicTemplatesSelector)
		{
			dynamicTemplatesSelector.ThrowIfNull("dynamicTemplatesSelector");
			var templates = dynamicTemplatesSelector(new DynamicTemplatesDescriptor<T>());
			if (Mapping.DynamicTemplates == null)
				Mapping.DynamicTemplates = new Dictionary<string, DynamicTemplate>();
			foreach (var t in templates._Deletes)
				Mapping.DynamicTemplates.Remove(t);
			foreach (var t in templates.Templates)
				Mapping.DynamicTemplates[t.Key] = t.Value;
			return this;
		}

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<PutMappingRequestParameters> pathInfo)
		{
			PutMappingPathInfo.Update(pathInfo, this);
		}
	}
}