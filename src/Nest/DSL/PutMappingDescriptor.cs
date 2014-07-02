using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Nest.Resolvers.Writers;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IPutMappingRequest : IRequest<PutMappingRequestParameters>
	{
		RootObjectMapping Mapping { get; set; }
	}

	public interface IPutMappingRequest<T> : IPutMappingRequest where T : class {}

	internal static class PutMappingPathInfo
	{
		public static void Update(ElasticsearchPathInfo<PutMappingRequestParameters> pathInfo, IPutMappingRequest request)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.PUT;
		}
	}

	public partial class PutMappingRequest : IndicesTypePathBase<PutMappingRequestParameters>, IPutMappingRequest
	{
		public RootObjectMapping Mapping { get; set; }
		
		/// <summary>
		/// Calls putmapping on /_all/{type}
		/// </summary>
		public PutMappingRequest(TypeNameMarker type)
		{
			this.Type = type;
			this.AllIndices = true;
		}

		/// <summary>
		/// Calls putmapping on /{indices}/{type}
		/// </summary>
		public PutMappingRequest(TypeNameMarker type, params IndexNameMarker[] indices)
		{
			this.Type = type;
			this.Indices = indices;
		}

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<PutMappingRequestParameters> pathInfo)
		{
			PutMappingPathInfo.Update(pathInfo, this);
		}
	}

	public partial class PutMappingRequest<T> : IndicesTypePathBase<PutMappingRequestParameters, T>, IPutMappingRequest<T>
		where T : class
	{
		public RootObjectMapping Mapping { get; set; }

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
		private IPutMappingRequest Self { get { return this; } }

		RootObjectMapping IPutMappingRequest.Mapping { get; set; }

		private readonly IConnectionSettingsValues _connectionSettings;
		private readonly ElasticInferrer _infer;

		public PutMappingDescriptor(IConnectionSettingsValues connectionSettings)
		{
			this._connectionSettings = connectionSettings;
			this._infer = new ElasticInferrer(this._connectionSettings);
			Self.Mapping = new RootObjectMapping() {  };
		}


		public PutMappingDescriptor<T> InitializeUsing(RootObjectMapping rootObjectMapping)
		{
			if (rootObjectMapping == null)
				return this;

			Self.Mapping = rootObjectMapping;
			return this;
		}

		/// <summary>
		/// Convenience method to map from most of the object from the attributes/properties.
		/// Later calls can override whatever is set is by this call. 
		/// This helps mapping all the ints as ints, floats as floats etcetera withouth having to be overly verbose in your fluent mapping
		/// </summary>
		/// <returns></returns>
		public PutMappingDescriptor<T> MapFromAttributes(int maxRecursion = 0)
		{
			//TODO no longer needed when we have an IPutMappingRequest
			IIndicesTypePath<PutMappingRequestParameters, T> request = this;
			var writer = new TypeMappingWriter(typeof(T), request.Type, this._connectionSettings, maxRecursion);
			var mapping = writer.RootObjectMappingFromAttributes();
			if (mapping == null)
				return this;
			var properties = mapping.Properties;
			if (Self.Mapping.Properties == null)
				Self.Mapping.Properties = new Dictionary<PropertyNameMarker, IElasticType>();

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
			Self.Mapping.Parent = new ParentTypeMapping() { Type = parentType };
			return this;
		}
		public PutMappingDescriptor<T> SetParent<K>() where K : class
		{
			var parentType = TypeNameMarker.Create<K>();
			Self.Mapping.Parent = new ParentTypeMapping() { Type = parentType };
			return this;
		}

		public PutMappingDescriptor<T> AllField(Func<AllFieldMapping, AllFieldMapping> allFieldSelector)
		{
			Self.Mapping.AllFieldMapping = allFieldSelector(new AllFieldMapping());
			return this;
		}

		public PutMappingDescriptor<T> DisableSizeField(bool disabled = true)
		{
			Self.Mapping.SizeFieldMapping = new SizeFieldMapping().SetDisabled(disabled);
			return this;
		}

		public PutMappingDescriptor<T> DisableIndexField(bool disabled = true)
		{
			Self.Mapping.IndexFieldMapping = new IndexFieldMapping().SetDisabled(disabled);
			return this;
		}

		public PutMappingDescriptor<T> IndexAnalyzer(string indexAnalyzer)
		{
			Self.Mapping.IndexAnalyzer = indexAnalyzer;
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
		public PutMappingDescriptor<T> IdField(Func<IdFieldMapping, IdFieldMapping> idMapper)
		{
			idMapper.ThrowIfNull("idMapper");
			Self.Mapping.IdFieldMapping = idMapper(new IdFieldMapping());
			return this;
		}

		public PutMappingDescriptor<T> TypeField(Func<TypeFieldMapping, TypeFieldMapping> typeMapper)
		{
			typeMapper.ThrowIfNull("typeMapper");
			Self.Mapping.TypeFieldMapping = typeMapper(new TypeFieldMapping());
			return this;
		}
		public PutMappingDescriptor<T> SourceField(Func<SourceFieldMapping, SourceFieldMapping> sourceMapper)
		{
			sourceMapper.ThrowIfNull("sourceMapper");
			Self.Mapping.SourceFieldMapping = sourceMapper(new SourceFieldMapping());
			return this;
		}

		public PutMappingDescriptor<T> AnalyzerField(Func<AnalyzerFieldMapping<T>, AnalyzerFieldMapping> analyzeMapper)
		{
			analyzeMapper.ThrowIfNull("analyzeMapper");
			Self.Mapping.AnalyzerFieldMapping = analyzeMapper(new AnalyzerFieldMapping<T>());
			return this;
		}
		public PutMappingDescriptor<T> BoostField(Func<BoostFieldMapping<T>, BoostFieldMapping> boostMapper)
		{
			boostMapper.ThrowIfNull("boostMapper");
			Self.Mapping.BoostFieldMapping = boostMapper(new BoostFieldMapping<T>());
			return this;
		}
		public PutMappingDescriptor<T> RoutingField(Func<RoutingFieldMapping<T>, RoutingFieldMapping> routingMapper)
		{
			routingMapper.ThrowIfNull("routingMapper");
			Self.Mapping.RoutingFieldMapping = routingMapper(new RoutingFieldMapping<T>());
			return this;
		}
		public PutMappingDescriptor<T> TimestampField(Func<TimestampFieldMapping<T>, TimestampFieldMapping> timestampMapper)
		{
			timestampMapper.ThrowIfNull("timestampMapper");
			Self.Mapping.TimestampFieldMapping = timestampMapper(new TimestampFieldMapping<T>());
			return this;
		}
		public PutMappingDescriptor<T> TtlField(Func<TtlFieldMapping, TtlFieldMapping> ttlFieldMapper)
		{
			ttlFieldMapper.ThrowIfNull("ttlFieldMapper");
			Self.Mapping.TtlFieldMapping = ttlFieldMapper(new TtlFieldMapping());
			return this;
		}
		public PutMappingDescriptor<T> Properties(Func<PropertiesDescriptor<T>, PropertiesDescriptor<T>> propertiesSelector)
		{
			propertiesSelector.ThrowIfNull("propertiesSelector");
			var properties = propertiesSelector(new PropertiesDescriptor<T>(this._connectionSettings));
			if (Self.Mapping.Properties == null)
				Self.Mapping.Properties = new Dictionary<PropertyNameMarker, IElasticType>();

			foreach (var t in properties._Deletes)
			{
				Self.Mapping.Properties.Remove(t);
			}
			foreach (var p in properties.Properties)
			{
				var key = this._infer.PropertyName(p.Key);
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