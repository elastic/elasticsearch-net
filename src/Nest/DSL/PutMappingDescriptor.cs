using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Nest.Resolvers;
using Newtonsoft.Json;
using Nest.Resolvers.Writers;

namespace Nest
{
	[DescriptorFor("IndicesPutMapping")]
	public partial class PutMappingDescriptor<T> 
		: IndicesTypePathDescriptor<PutMappingDescriptor<T>, PutMappingRequestParameters, T>
		, IPathInfo<PutMappingRequestParameters> 
		where T : class
	{
		private readonly IConnectionSettingsValues _connectionSettings;
		internal RootObjectMapping _Mapping { get; set; }
		public ElasticInferrer Infer { get; set; }

		public PutMappingDescriptor(IConnectionSettingsValues connectionSettings)
		{
			this._connectionSettings = connectionSettings;
			this._Mapping = new RootObjectMapping() {  };
			this.Infer = new ElasticInferrer(this._connectionSettings);
		}


		public PutMappingDescriptor<T> InitializeUsing(RootObjectMapping rootObjectMapping)
		{
			if (rootObjectMapping == null)
				return this;

			this._Mapping = rootObjectMapping;
			return this;
		}

		/// <summary>
		/// Convenience method to map from most of the object from the attributes/properties.
		/// Later calls on the fluent interface can override whatever is set is by this call. 
		/// This helps mapping all the ints as ints, floats as floats etcetera withouth having to be overly verbose in your fluent mapping
		/// </summary>
		/// <returns></returns>
		public PutMappingDescriptor<T> MapFromAttributes(int maxRecursion = 0)
		{
			var writer = new TypeMappingWriter(typeof(T), this._Type, this._connectionSettings, maxRecursion);
			var mapping = writer.RootObjectMappingFromAttributes();
			if (mapping == null)
				return this;
			var properties = mapping.Properties;
			if (this._Mapping.Properties == null)
				this._Mapping.Properties = new Dictionary<PropertyNameMarker, IElasticType>();

			foreach (var p in properties)
			{
				this._Mapping.Properties[p.Key] = p.Value;
			}
			return this;
		}
		

		public PutMappingDescriptor<T> Dynamic(DynamicMappingOption dynamic)
		{
			this._Mapping.Dynamic = dynamic;
			return this;
		}
		public PutMappingDescriptor<T> Dynamic(bool dynamic = true)
		{
			return this.Dynamic(dynamic ? DynamicMappingOption.allow : DynamicMappingOption.ignore);
		}
		public PutMappingDescriptor<T> Enabled(bool enabled = true)
		{
			this._Mapping.Enabled = enabled;
			return this;
		}
		public PutMappingDescriptor<T> IncludeInAll(bool includeInAll = true)
		{
			this._Mapping.IncludeInAll = includeInAll;
			return this;
		}
		public PutMappingDescriptor<T> Path(string path)
		{
			this._Mapping.Path = path;
			return this;
		}

		public PutMappingDescriptor<T> SetParent(string parentType)
		{
			this._Mapping.Parent = new ParentTypeMapping() { Type = parentType };
			return this;
		}
		public PutMappingDescriptor<T> SetParent<K>() where K : class
		{
			var parentType = TypeNameMarker.Create<K>();
			this._Mapping.Parent = new ParentTypeMapping() { Type = parentType };
			return this;
		}

		public PutMappingDescriptor<T> DisableAllField(bool disabled = true)
		{
			this._Mapping.AllFieldMapping = new AllFieldMapping().SetDisabled(disabled);
			return this;
		}

		public PutMappingDescriptor<T> DisableSizeField(bool disabled = true)
		{
			this._Mapping.SizeFieldMapping = new SizeFieldMapping().SetDisabled(disabled);
			return this;
		}

		public PutMappingDescriptor<T> DisableIndexField(bool disabled = true)
		{
			this._Mapping.IndexFieldMapping = new IndexFieldMapping().SetDisabled(disabled);
			return this;
		}

		public PutMappingDescriptor<T> IndexAnalyzer(string indexAnalyzer)
		{
			this._Mapping.IndexAnalyzer = indexAnalyzer;
			return this;
		}

		public PutMappingDescriptor<T> SearchAnalyzer(string searchAnalyzer)
		{
			this._Mapping.SearchAnalyzer = searchAnalyzer;
			return this;
		}
		public PutMappingDescriptor<T> DynamicDateFormats(IEnumerable<string> dateFormats)
		{
			this._Mapping.DynamicDateFormats = dateFormats;
			return this;
		}
		public PutMappingDescriptor<T> DateDetection(bool detect = true)
		{
			this._Mapping.DateDetection = detect;
			return this;
		}
		public PutMappingDescriptor<T> NumericDetection(bool detect = true)
		{
			this._Mapping.NumericDetection = detect;
			return this;
		}
		public PutMappingDescriptor<T> IdField(Func<IdFieldMapping, IdFieldMapping> idMapper)
		{
			idMapper.ThrowIfNull("idMapper");
			this._Mapping.IdFieldMapping = idMapper(new IdFieldMapping());
			return this;
		}

		public PutMappingDescriptor<T> TypeField(Func<TypeFieldMapping, TypeFieldMapping> typeMapper)
		{
			typeMapper.ThrowIfNull("typeMapper");
			this._Mapping.TypeFieldMapping = typeMapper(new TypeFieldMapping());
			return this;
		}
		public PutMappingDescriptor<T> SourceField(Func<SourceFieldMapping, SourceFieldMapping> sourceMapper)
		{
			sourceMapper.ThrowIfNull("sourceMapper");
			this._Mapping.SourceFieldMapping = sourceMapper(new SourceFieldMapping());
			return this;
		}

		public PutMappingDescriptor<T> AnalyzerField(Func<AnalyzerFieldMapping<T>, AnalyzerFieldMapping> analyzeMapper)
		{
			analyzeMapper.ThrowIfNull("analyzeMapper");
			this._Mapping.AnalyzerFieldMapping = analyzeMapper(new AnalyzerFieldMapping<T>());
			return this;
		}
		public PutMappingDescriptor<T> BoostField(Func<BoostFieldMapping<T>, BoostFieldMapping> boostMapper)
		{
			boostMapper.ThrowIfNull("boostMapper");
			this._Mapping.BoostFieldMapping = boostMapper(new BoostFieldMapping<T>());
			return this;
		}
		public PutMappingDescriptor<T> RoutingField(Func<RoutingFieldMapping<T>, RoutingFieldMapping> routingMapper)
		{
			routingMapper.ThrowIfNull("routingMapper");
			this._Mapping.RoutingFieldMapping = routingMapper(new RoutingFieldMapping<T>());
			return this;
		}
		public PutMappingDescriptor<T> TimestampField(Func<TimestampFieldMapping<T>, TimestampFieldMapping> timestampMapper)
		{
			timestampMapper.ThrowIfNull("timestampMapper");
			this._Mapping.TimestampFieldMapping = timestampMapper(new TimestampFieldMapping<T>());
			return this;
		}
		public PutMappingDescriptor<T> TtlField(Func<TtlFieldMapping, TtlFieldMapping> ttlFieldMapper)
		{
			ttlFieldMapper.ThrowIfNull("ttlFieldMapper");
			this._Mapping.TtlFieldMapping = ttlFieldMapper(new TtlFieldMapping());
			return this;
		}
		public PutMappingDescriptor<T> Properties(Func<PropertiesDescriptor<T>, PropertiesDescriptor<T>> propertiesSelector)
		{
			propertiesSelector.ThrowIfNull("propertiesSelector");
			var properties = propertiesSelector(new PropertiesDescriptor<T>(this._connectionSettings));
			if (this._Mapping.Properties == null)
				this._Mapping.Properties = new Dictionary<PropertyNameMarker, IElasticType>();

			foreach (var t in properties._Deletes)
			{
				_Mapping.Properties.Remove(t);
			}
			foreach (var p in properties.Properties)
			{
				var key = this.Infer.PropertyName(p.Key);
				_Mapping.Properties[key] = p.Value;
			}
			return this;
		}
		public PutMappingDescriptor<T> Meta(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> metaSelector)
		{
			metaSelector.ThrowIfNull("metaSelector");
			this._Mapping.Meta = metaSelector(new FluentDictionary<string, object>());
			return this;
		}
		public PutMappingDescriptor<T> DynamicTemplates(Func<DynamicTemplatesDescriptor<T>, DynamicTemplatesDescriptor<T>> dynamicTemplatesSelector)
		{
			dynamicTemplatesSelector.ThrowIfNull("dynamicTemplatesSelector");
			var templates = dynamicTemplatesSelector(new DynamicTemplatesDescriptor<T>(this._connectionSettings));
			if (this._Mapping.DynamicTemplates == null)
				this._Mapping.DynamicTemplates = new Dictionary<string, DynamicTemplate>();

			foreach (var t in templates._Deletes)
			{
				_Mapping.DynamicTemplates.Remove(t);
			}
			foreach (var t in templates.Templates)
			{
				_Mapping.DynamicTemplates[t.Key] = t.Value;
			}
			return this;
		}

		ElasticsearchPathInfo<PutMappingRequestParameters> IPathInfo<PutMappingRequestParameters>.ToPathInfo(IConnectionSettingsValues settings)
		{
			var pathInfo = base.ToPathInfo<PutMappingRequestParameters>(settings, this._QueryString);
			pathInfo.HttpMethod = PathInfoHttpMethod.PUT;

			return pathInfo;
		}
	}
}