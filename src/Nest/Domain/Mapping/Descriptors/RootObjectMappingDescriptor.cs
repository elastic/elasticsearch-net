using System;
using System.Collections.Generic;
using Nest.Resolvers;
using Newtonsoft.Json;
using Nest.Resolvers.Writers;

namespace Nest
{
    public class RootObjectMappingDescriptor<T> where T : class
    {
		internal MapRootObject _RootObjectMapping { get; set; }
		internal string _TypeName { get; set; }
		internal string _IndexName { get; set; }
		internal bool _IgnoreConflicts { get; set; }

		public RootObjectMappingDescriptor()
		{
			this._TypeName = new TypeNameResolver().GetTypeNameFor<T>();
			this._RootObjectMapping = new MapRootObject() { Name = this._TypeName };
        }

		/// <summary>
		/// Convenience method to map from most of the object from the attributes/properties.
		/// Later calls on the fluent interface can override whatever is set is by this call. 
		/// This helps mapping all the ints as ints, floats as floats etcetera withouth having to be overly verbose in your fluent mapping
		/// </summary>
		/// <returns></returns>
		public RootObjectMappingDescriptor<T> MapFromAttributes(int maxRecursion = 0)
		{
			var writer = new TypeMappingWriter(typeof(T), this._TypeName, maxRecursion);
			this._RootObjectMapping = writer.TypeMappingFromAttributes();

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
			this._RootObjectMapping.Name = name;
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
		public RootObjectMappingDescriptor<T> SetParent(string parentType)
		{
			this._RootObjectMapping.Parent = new ParentTypeMapping() { Type = parentType };
			return this;
		}
		public RootObjectMappingDescriptor<T> SetParent<K>() where K : class
		{
			var parentType = new TypeNameResolver().GetTypeNameFor<K>();
			this._RootObjectMapping.Parent = new ParentTypeMapping() { Type = parentType };
			return this;
		}

		public RootObjectMappingDescriptor<T> DisableAllField(bool disabled = true)
		{
			this._RootObjectMapping.AllFieldMapping = new AllFieldMapping().SetDisabled(disabled);
			return this;
		}

		public RootObjectMappingDescriptor<T> DisableSizeField(bool disabled = true)
		{
			this._RootObjectMapping.SizeFieldMapping = new SizeFieldMapping().SetDisabled(disabled);
			return this;
		}

		public RootObjectMappingDescriptor<T> DisableIndexField(bool disabled = true)
		{
			this._RootObjectMapping.IndexFieldMapping = new IndexFieldMapping().SetDisabled(disabled);
			return this;
		}

		public RootObjectMappingDescriptor<T> IndexAnalyzer(string indexAnalyzer)
		{
			this._RootObjectMapping.IndexAnalyzer = indexAnalyzer;
			return this;
		}

		public RootObjectMappingDescriptor<T> SearchAnalyzer(string searchAnalyzer)
		{
			this._RootObjectMapping.SearchAnalyzer = searchAnalyzer;
			return this;
		}
		public RootObjectMappingDescriptor<T> DynamicDateFormats(IEnumerable<string> dateFormats)
		{
			this._RootObjectMapping.DynamicDateFormats = dateFormats;
			return this;
		}
		public RootObjectMappingDescriptor<T> DateDetection(bool detect = true)
		{
			this._RootObjectMapping.DateDetection = detect;
			return this;
		}
		public RootObjectMappingDescriptor<T> NumericDetection(bool detect = true)
		{
			this._RootObjectMapping.NumericDetection = detect;
			return this;
		}
		public RootObjectMappingDescriptor<T> IdField(Func<IdFieldMapping, IdFieldMapping> idMapper)
		{
			idMapper.ThrowIfNull("idMapper");
			this._RootObjectMapping.IdFieldMapping = idMapper(new IdFieldMapping());
			return this;
		}

		public RootObjectMappingDescriptor<T> TypeField(Func<TypeFieldMapping, TypeFieldMapping> typeMapper)
		{
			typeMapper.ThrowIfNull("typeMapper");
			this._RootObjectMapping.TypeFieldMapping = typeMapper(new TypeFieldMapping());
			return this;
		}
		public RootObjectMappingDescriptor<T> SourceField(Func<SourceFieldMapping, SourceFieldMapping> sourceMapper)
		{
			sourceMapper.ThrowIfNull("sourceMapper");
			this._RootObjectMapping.SourceFieldMapping = sourceMapper(new SourceFieldMapping());
			return this;
		}
		
		public RootObjectMappingDescriptor<T> AnalyzerField(Func<AnalyzerFieldMapping<T>, AnalyzerFieldMapping> analyzeMapper)
		{
			analyzeMapper.ThrowIfNull("analyzeMapper");
			this._RootObjectMapping.AnalyzerFieldMapping = analyzeMapper(new AnalyzerFieldMapping<T>());
			return this;
		}
		public RootObjectMappingDescriptor<T> BoostField(Func<BoostFieldMapping<T>, BoostFieldMapping> boostMapper)
		{
			boostMapper.ThrowIfNull("boostMapper");
			this._RootObjectMapping.BoostFieldMapping = boostMapper(new BoostFieldMapping<T>());
			return this;
		}
		public RootObjectMappingDescriptor<T> RoutingField(Func<RoutingFieldMapping<T>, RoutingFieldMapping> routingMapper)
		{
			routingMapper.ThrowIfNull("routingMapper");
			this._RootObjectMapping.RoutingFieldMapping = routingMapper(new RoutingFieldMapping<T>());
			return this;
		}
		public RootObjectMappingDescriptor<T> TimestampField(Func<TimestampFieldMapping<T>, TimestampFieldMapping> timestampMapper)
		{
			timestampMapper.ThrowIfNull("timestampMapper");
			this._RootObjectMapping.TimestampFieldMapping = timestampMapper(new TimestampFieldMapping<T>());
			return this;
		}
		public RootObjectMappingDescriptor<T> TtlField(Func<TtlFieldMapping, TtlFieldMapping> ttlFieldMapper)
		{
			ttlFieldMapper.ThrowIfNull("ttlFieldMapper");
			this._RootObjectMapping.TtlFieldMapping = ttlFieldMapper(new TtlFieldMapping());
			return this;
		}
		public RootObjectMappingDescriptor<T> Properties(Func<PropertiesDescriptor<T>, PropertiesDescriptor<T>> propertiesSelector)
		{
			propertiesSelector.ThrowIfNull("propertiesSelector");
			//todo merge with _RootObjectMapping 
			return this;
		}
		
    }
}