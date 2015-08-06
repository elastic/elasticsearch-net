using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Nest.Resolvers.Writers;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IPutMappingRequest : IIndicesTypePath<PutMappingRequestParameters>
	{
		RootObjectType Mapping { get; set; }
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
		public RootObjectType Mapping { get; set; }
		
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
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<PutMappingRequestParameters> pathInfo)
		{
			PutMappingPathInfo.Update(pathInfo, this);
		}
	}

	public partial class PutMappingRequest<T> : IndicesTypePathBase<PutMappingRequestParameters, T>, IPutMappingRequest<T>
		where T : class
	{
		public RootObjectType Mapping { get; set; }

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

		RootObjectType IPutMappingRequest.Mapping { get; set; }

		private readonly IConnectionSettingsValues _connectionSettings;
		private readonly ElasticInferrer _infer;

		public PutMappingDescriptor(IConnectionSettingsValues connectionSettings)
		{
			this._connectionSettings = connectionSettings;
			this._infer = new ElasticInferrer(this._connectionSettings);
			Self.Mapping = new RootObjectType() {  };
		}


		public PutMappingDescriptor<T> InitializeUsing(RootObjectType rootObjectMapping)
		{
			if (rootObjectMapping == null)
				return this;

			Self.Mapping = rootObjectMapping;
			return this;
		}

		/// <summary>
		/// Convenience method to map as much as it can based on ElasticType attributes set on the type.
		/// <pre>This method also automatically sets up mappings for known values types (int, long, double, datetime, etcetera)</pre>
		/// <pre>Class types default to object and Enums to int</pre>
		/// <pre>Later calls can override whatever is set is by this call.</pre>
		/// </summary>
		public PutMappingDescriptor<T> MapFromAttributes(int maxRecursion = 0)
		{
			//TODO no longer needed when we have an IPutMappingRequest
			var writer = new TypeMappingWriter(typeof(T), Self.Type, this._connectionSettings, maxRecursion);
			var mapping = writer.RootObjectMappingFromAttributes();
			if (mapping == null)
				return this;
			var properties = mapping.Properties;
			if (Self.Mapping.Properties == null)
				Self.Mapping.Properties = new Dictionary<FieldName, IElasticType>();

			foreach (var p in properties)
			{
				Self.Mapping.Properties[p.Key] = p.Value;
			}
			return this;
		}
		

		public PutMappingDescriptor<T> Dynamic(DynamicMappingOption dynamic)
		{
			Self.Mapping.Dynamic = dynamic;
			return this;
		}
		public PutMappingDescriptor<T> Dynamic(bool dynamic = true)
		{
			return this.Dynamic(dynamic ? DynamicMappingOption.Allow : DynamicMappingOption.Ignore);
		}
		public PutMappingDescriptor<T> Enabled(bool enabled = true)
		{
			Self.Mapping.Enabled = enabled;
			return this;
		}
		public PutMappingDescriptor<T> IncludeInAll(bool includeInAll = true)
		{
			Self.Mapping.IncludeInAll = includeInAll;
			return this;
		}
		public PutMappingDescriptor<T> Path(string path)
		{
			Self.Mapping.Path = path;
			return this;
		}

		public PutMappingDescriptor<T> SetParent(string parentType)
		{
			Self.Mapping.ParentField = new ParentField { Type = parentType };
			return this;
		}
		public PutMappingDescriptor<T> SetParent<K>() where K : class
		{
			var parentType = TypeName.Create<K>();
			Self.Mapping.ParentField = new ParentField { Type = parentType };
			return this;
		}

		public PutMappingDescriptor<T> AllField(Func<AllFieldDescriptor, AllFieldDescriptor> allFieldSelector)
		{
			Self.Mapping.AllField = allFieldSelector(new AllFieldDescriptor());
			return this;
		}

		public PutMappingDescriptor<T> IndexField(Func<IndexFieldDescriptor, IndexFieldDescriptor> indexFieldSelector)
		{
			Self.Mapping.IndexField = indexFieldSelector(new IndexFieldDescriptor());
			return this;
		}

		public PutMappingDescriptor<T> SizeField(Func<SizeFieldDescriptor, SizeFieldDescriptor> sizeFieldSelector)
		{
			Self.Mapping.SizeField = sizeFieldSelector(new SizeFieldDescriptor());
			return this;
		}

		public PutMappingDescriptor<T> DisableSizeField(bool disabled = true)
		{
			Self.Mapping.SizeField = new SizeField { Enabled = !disabled };
			return this;
		}

		public PutMappingDescriptor<T> DisableIndexField(bool disabled = true)
		{
			Self.Mapping.IndexField = new IndexField { Enabled = !disabled };
			return this;
		}

		public PutMappingDescriptor<T> IndexAnalyzer(string indexAnalyzer)
		{
			Self.Mapping.Analyzer = indexAnalyzer;
			return this;
		}

		public PutMappingDescriptor<T> SearchAnalyzer(string searchAnalyzer)
		{
			Self.Mapping.SearchAnalyzer = searchAnalyzer;
			return this;
		}
		public PutMappingDescriptor<T> DynamicDateFormats(IEnumerable<string> dateFormats)
		{
			Self.Mapping.DynamicDateFormats = dateFormats;
			return this;
		}
		public PutMappingDescriptor<T> DateDetection(bool detect = true)
		{
			Self.Mapping.DateDetection = detect;
			return this;
		}
		public PutMappingDescriptor<T> NumericDetection(bool detect = true)
		{
			Self.Mapping.NumericDetection = detect;
			return this;
		}

		public PutMappingDescriptor<T> Transform(Func<MappingTransformDescriptor, MappingTransformDescriptor> mappingTransformSelector)
		{
			mappingTransformSelector.ThrowIfNull("mappingTransformSelector");
			var transformDescriptor = mappingTransformSelector(new MappingTransformDescriptor());
			if (Self.Mapping.Transform == null)
				Self.Mapping.Transform = new List<MappingTransform>();
			Self.Mapping.Transform.Add(transformDescriptor._mappingTransform);
			return this;
		}

		public PutMappingDescriptor<T> IdField(Func<IdFieldDescriptor, IIdField> idMapper)
		{
			idMapper.ThrowIfNull("idMapper");
			Self.Mapping.IdField = idMapper(new IdFieldDescriptor());
			return this;
		}

		public PutMappingDescriptor<T> TypeField(Func<TypeFieldDescriptor, ITypeField> typeMapper)
		{
			typeMapper.ThrowIfNull("typeMapper");
			Self.Mapping.TypeField = typeMapper(new TypeFieldDescriptor());
			return this;
		}
		public PutMappingDescriptor<T> SourceField(Func<SourceFieldDescriptor, ISourceField> sourceMapper)
		{
			sourceMapper.ThrowIfNull("sourceMapper");
			Self.Mapping.SourceField = sourceMapper(new SourceFieldDescriptor());
			return this;
		}

		public PutMappingDescriptor<T> AnalyzerField(Func<AnalyzerFieldDescriptor<T>, IAnalyzerField> analyzeMapper)
		{
			analyzeMapper.ThrowIfNull("analyzeMapper");
			Self.Mapping.AnalyzerField = analyzeMapper(new AnalyzerFieldDescriptor<T>());
			return this;
		}
		public PutMappingDescriptor<T> BoostField(Func<BoostFieldDescriptor<T>, IBoostField> boostMapper)
		{
			boostMapper.ThrowIfNull("boostMapper");
			Self.Mapping.BoostField = boostMapper(new BoostFieldDescriptor<T>());
			return this;
		}
		public PutMappingDescriptor<T> RoutingField(Func<RoutingFieldDescriptor<T>, IRoutingField> routingMapper)
		{
			routingMapper.ThrowIfNull("routingMapper");
			Self.Mapping.RoutingField = routingMapper(new RoutingFieldDescriptor<T>());
			return this;
		}

		public PutMappingDescriptor<T> TimestampField(Func<TimestampFieldDescriptor<T>, ITimestampField> timestampMapper)
		{
			timestampMapper.ThrowIfNull("timestampMapper");
			Self.Mapping.TimestampField = timestampMapper(new TimestampFieldDescriptor<T>());
			return this;
		}

		public PutMappingDescriptor<T> FieldNamesField(Func<FieldNamesFieldDescriptor<T>, IFieldNamesField> fieldNamesMapper)
		{
			Self.Mapping.FieldNamesField = fieldNamesMapper == null ? null : fieldNamesMapper(new FieldNamesFieldDescriptor<T>());
			return this;
		}

		public PutMappingDescriptor<T> TtlField(Func<TtlFieldDescriptor, ITtlField> ttlFieldMapper)
		{
			ttlFieldMapper.ThrowIfNull("ttlFieldMapper");
			Self.Mapping.TtlField = ttlFieldMapper(new TtlFieldDescriptor());
			return this;
		}
		public PutMappingDescriptor<T> Properties(Func<PropertiesDescriptor<T>, PropertiesDescriptor<T>> propertiesSelector)
		{
			propertiesSelector.ThrowIfNull("propertiesSelector");
			var properties = propertiesSelector(new PropertiesDescriptor<T>(this._connectionSettings));
			if (Self.Mapping.Properties == null)
				Self.Mapping.Properties = new Dictionary<FieldName, IElasticType>();

			foreach (var p in properties.Properties)
			{
				var key = this._infer.FieldName(p.Key);
				Self.Mapping.Properties[key] = p.Value;
			}
			return this;
		}
		public PutMappingDescriptor<T> Meta(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> metaSelector)
		{
			metaSelector.ThrowIfNull("metaSelector");
			Self.Mapping.Meta = metaSelector(new FluentDictionary<string, object>());
			return this;
		}
		public PutMappingDescriptor<T> DynamicTemplates(Func<DynamicTemplatesDescriptor<T>, DynamicTemplatesDescriptor<T>> dynamicTemplatesSelector)
		{
			dynamicTemplatesSelector.ThrowIfNull("dynamicTemplatesSelector");
			var templates = dynamicTemplatesSelector(new DynamicTemplatesDescriptor<T>(this._connectionSettings));
			if (Self.Mapping.DynamicTemplates == null)
				Self.Mapping.DynamicTemplates = new Dictionary<string, DynamicTemplate>();

			foreach (var t in templates._Deletes)
			{
				Self.Mapping.DynamicTemplates.Remove(t);
			}
			foreach (var t in templates.Templates)
			{
				Self.Mapping.DynamicTemplates[t.Key] = t.Value;
			}
			return this;
		}

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<PutMappingRequestParameters> pathInfo)
		{
			PutMappingPathInfo.Update(pathInfo, this);
		}
	}
}