using System;
using System.Collections.Generic;
using Nest.Resolvers;
using Newtonsoft.Json;
using Nest.Resolvers.Writers;

namespace Nest
{
    public class RootObjectMappingDescriptor<T> where T : class
    {
		private readonly IConnectionSettings _connectionSettings;

		internal RootObjectMapping _Mapping { get; set; }
		internal TypeNameMarker _TypeName { get; set; }
		internal string _IndexName { get; set; }
		internal bool _IgnoreConflicts { get; set; }

		public RootObjectMappingDescriptor(IConnectionSettings connectionSettings)
		{
			this._connectionSettings = connectionSettings;
			this._TypeName = TypeNameMarker.Create<T>();
			this._Mapping = new RootObjectMapping() { TypeNameMarker = this._TypeName };
		}

		/// <summary>
		/// Convenience method to map from most of the object from the attributes/properties.
		/// Later calls on the fluent interface can override whatever is set is by this call. 
		/// This helps mapping all the ints as ints, floats as floats etcetera withouth having to be overly verbose in your fluent mapping
		/// </summary>
		/// <returns></returns>
		public RootObjectMappingDescriptor<T> MapFromAttributes(int maxRecursion = 0)
		{
			var writer = new TypeMappingWriter(typeof(T), this._TypeName, this._connectionSettings, maxRecursion);
			var mapping = writer.RootObjectMappingFromAttributes();
			if (mapping == null)
				return this;
			var properties = mapping.Properties;
			if (this._Mapping.Properties == null)
				this._Mapping.Properties = new Dictionary<string, IElasticType>();

			foreach (var p in properties)
			{
				this._Mapping.Properties[p.Key] = p.Value;
			}
			return this;
		}

		/// <summary>
		/// Explicitly set the typename otherwise it will infer the typename on its own (lowercase and pluralized).
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public RootObjectMappingDescriptor<T> TypeName(string name)
		{
			this._TypeName = name;
			this._Mapping.TypeNameMarker = name;
			return this;
		}
		/// <summary>
		/// Explicitly set the index name otherwise it will infer the indexname based on the type
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public RootObjectMappingDescriptor<T> IndexName(string name)
		{
			this._IndexName = name;
			return this;
		}
		/// <summary>
		/// Explicitly set the index names otherwise it will infer the indexname based on the type
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public RootObjectMappingDescriptor<T> IndexNames(params string[] names)
		{
			this._IndexName = string.Join(",", names);
			return this;
		}
		/// <summary>
		/// Explicitly set the index names otherwise it will infer the indexname based on the type
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public RootObjectMappingDescriptor<T> IndexNames(IEnumerable<string> names)
		{
			this._IndexName = string.Join(",", names);
			return this;
		}
		/// <summary>
		/// When an existing mapping already exists under the given type, the two mapping definitions, the one already defined, and the new ones are merged. 
		/// The ignore_conflicts parameters can be used to control if conflicts should be ignored or not, by default, it is set to false which means conflicts are not ignored.
		/// The definition of conflict is really dependent on the type merged, but in general, if a different core type is defined, it is considered as a conflict. 
		/// New mapping definitions can be added to object types, and core type mapping can be upgraded to multi_field type.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public RootObjectMappingDescriptor<T> IgnoreConflicts(bool ignore = true)
		{
			this._IgnoreConflicts = ignore;
			return this;
		}

		public RootObjectMappingDescriptor<T> Dynamic(bool dynamic = true)
		{
			this._Mapping.Dynamic = dynamic;
			return this;
		}
		public RootObjectMappingDescriptor<T> Enabled(bool enabled = true)
		{
			this._Mapping.Enabled = enabled;
			return this;
		}
		public RootObjectMappingDescriptor<T> IncludeInAll(bool includeInAll = true)
		{
			this._Mapping.IncludeInAll = includeInAll;
			return this;
		}
		public RootObjectMappingDescriptor<T> Path(string path)
		{
			this._Mapping.Path = path;
			return this;
		}


		public RootObjectMappingDescriptor<T> SetParent(string parentType)
		{
			this._Mapping.Parent = new ParentTypeMapping() { Type = parentType };
			return this;
		}
		public RootObjectMappingDescriptor<T> SetParent<K>() where K : class
		{
			var parentType = TypeNameMarker.Create<K>();
			this._Mapping.Parent = new ParentTypeMapping() { Type = parentType };
			return this;
		}

		public RootObjectMappingDescriptor<T> DisableAllField(bool disabled = true)
		{
			this._Mapping.AllFieldMapping = new AllFieldMapping().SetDisabled(disabled);
			return this;
		}

		public RootObjectMappingDescriptor<T> DisableSizeField(bool disabled = true)
		{
			this._Mapping.SizeFieldMapping = new SizeFieldMapping().SetDisabled(disabled);
			return this;
		}

		public RootObjectMappingDescriptor<T> DisableIndexField(bool disabled = true)
		{
			this._Mapping.IndexFieldMapping = new IndexFieldMapping().SetDisabled(disabled);
			return this;
		}

		public RootObjectMappingDescriptor<T> IndexAnalyzer(string indexAnalyzer)
		{
			this._Mapping.IndexAnalyzer = indexAnalyzer;
			return this;
		}

		public RootObjectMappingDescriptor<T> SearchAnalyzer(string searchAnalyzer)
		{
			this._Mapping.SearchAnalyzer = searchAnalyzer;
			return this;
		}
		public RootObjectMappingDescriptor<T> DynamicDateFormats(IEnumerable<string> dateFormats)
		{
			this._Mapping.DynamicDateFormats = dateFormats;
			return this;
		}
		public RootObjectMappingDescriptor<T> DateDetection(bool detect = true)
		{
			this._Mapping.DateDetection = detect;
			return this;
		}
		public RootObjectMappingDescriptor<T> NumericDetection(bool detect = true)
		{
			this._Mapping.NumericDetection = detect;
			return this;
		}
		public RootObjectMappingDescriptor<T> IdField(Func<IdFieldMapping, IdFieldMapping> idMapper)
		{
			idMapper.ThrowIfNull("idMapper");
			this._Mapping.IdFieldMapping = idMapper(new IdFieldMapping());
			return this;
		}

		public RootObjectMappingDescriptor<T> TypeField(Func<TypeFieldMapping, TypeFieldMapping> typeMapper)
		{
			typeMapper.ThrowIfNull("typeMapper");
			this._Mapping.TypeFieldMapping = typeMapper(new TypeFieldMapping());
			return this;
		}
		public RootObjectMappingDescriptor<T> SourceField(Func<SourceFieldMapping, SourceFieldMapping> sourceMapper)
		{
			sourceMapper.ThrowIfNull("sourceMapper");
			this._Mapping.SourceFieldMapping = sourceMapper(new SourceFieldMapping());
			return this;
		}
		
		public RootObjectMappingDescriptor<T> AnalyzerField(Func<AnalyzerFieldMapping<T>, AnalyzerFieldMapping> analyzeMapper)
		{
			analyzeMapper.ThrowIfNull("analyzeMapper");
			this._Mapping.AnalyzerFieldMapping = analyzeMapper(new AnalyzerFieldMapping<T>());
			return this;
		}
		public RootObjectMappingDescriptor<T> BoostField(Func<BoostFieldMapping<T>, BoostFieldMapping> boostMapper)
		{
			boostMapper.ThrowIfNull("boostMapper");
			this._Mapping.BoostFieldMapping = boostMapper(new BoostFieldMapping<T>());
			return this;
		}
		public RootObjectMappingDescriptor<T> RoutingField(Func<RoutingFieldMapping<T>, RoutingFieldMapping> routingMapper)
		{
			routingMapper.ThrowIfNull("routingMapper");
			this._Mapping.RoutingFieldMapping = routingMapper(new RoutingFieldMapping<T>());
			return this;
		}
		public RootObjectMappingDescriptor<T> TimestampField(Func<TimestampFieldMapping<T>, TimestampFieldMapping> timestampMapper)
		{
			timestampMapper.ThrowIfNull("timestampMapper");
			this._Mapping.TimestampFieldMapping = timestampMapper(new TimestampFieldMapping<T>());
			return this;
		}
		public RootObjectMappingDescriptor<T> TtlField(Func<TtlFieldMapping, TtlFieldMapping> ttlFieldMapper)
		{
			ttlFieldMapper.ThrowIfNull("ttlFieldMapper");
			this._Mapping.TtlFieldMapping = ttlFieldMapper(new TtlFieldMapping());
			return this;
		}
		public RootObjectMappingDescriptor<T> Properties(Func<PropertiesDescriptor<T>, PropertiesDescriptor<T>> propertiesSelector)
		{
			propertiesSelector.ThrowIfNull("propertiesSelector");
			var properties = propertiesSelector(new PropertiesDescriptor<T>(this._connectionSettings));
			if (this._Mapping.Properties == null)
				this._Mapping.Properties = new Dictionary<string, IElasticType>();

			foreach (var t in properties._Deletes)
			{
				_Mapping.Properties.Remove(t);
			}
			foreach (var p in properties.Properties)
			{
				_Mapping.Properties[p.Key] = p.Value;
			}
			return this;
		}
		public RootObjectMappingDescriptor<T> Meta(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> metaSelector)
		{
			metaSelector.ThrowIfNull("metaSelector");
			this._Mapping.Meta = metaSelector(new FluentDictionary<string, object>());
			return this;
		}
		public RootObjectMappingDescriptor<T> DynamicTemplates(Func<DynamicTemplatesDescriptor<T>, DynamicTemplatesDescriptor<T>> dynamicTemplatesSelector)
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
    }
}