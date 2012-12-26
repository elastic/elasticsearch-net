using System;
using System.Collections.Generic;
using Nest.Resolvers;
using Newtonsoft.Json;
using Nest.Resolvers.Writers;

namespace Nest
{
    public class TypeMappingDescriptor<T> where T : class
    {
		internal TypeMapping _TypeMapping { get; set; }
		internal string _Name { get; set; }

		public TypeMappingDescriptor()
		{
			this._Name = new TypeNameResolver().GetTypeNameFor<T>();
			this._TypeMapping = new TypeMapping(this._Name);
        }

		/// <summary>
		/// Convenience method to map from most of the object from the attributes/properties.
		/// Later calls on the fluent interface can override whatever is set is by this call. 
		/// This helps mapping all the ints as ints, floats as floats etcetera withouth having to be overly verbose in your fluent mapping
		/// </summary>
		/// <returns></returns>
		public TypeMappingDescriptor<T> MapFromAttributes(int maxRecursion = 0)
		{
			var writer = new TypeMappingWriter(typeof(T), this._Name, maxRecursion);
			var json = writer.MapFromAttributes();
			this._TypeMapping = JsonConvert.DeserializeObject<TypeMapping>(json);
			return this;
		}

		/// <summary>
		/// Explicitly set the typename otherwise it will infer the typename on its own (lowercase and pluralized).
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public TypeMappingDescriptor<T> TypeName(string name)
		{
			this._Name = name;
			this._TypeMapping.Name = name;
			return this;
		}
		public TypeMappingDescriptor<T> SetParent(string parentType)
		{
			this._TypeMapping.Parent = new TypeMappingParent() { Type = parentType };
			return this;
		}
		public TypeMappingDescriptor<T> SetParent<K>() where K : class
		{
			var parentType = new TypeNameResolver().GetTypeNameFor<K>();
			this._TypeMapping.Parent = new TypeMappingParent() { Type = parentType };
			return this;
		}

		public TypeMappingDescriptor<T> DisableAllField(bool disabled = true)
		{
			this._TypeMapping.AllFieldMapping = new AllFieldMapping().SetDisabled(disabled);
			return this;
		}

		public TypeMappingDescriptor<T> DisableSizeField(bool disabled = true)
		{
			this._TypeMapping.SizeFieldMapping = new SizeFieldMapping().SetDisabled(disabled);
			return this;
		}

		public TypeMappingDescriptor<T> DisableIndexField(bool disabled = true)
		{
			this._TypeMapping.IndexFieldMapping = new IndexFieldMapping().SetDisabled(disabled);
			return this;
		}

		public TypeMappingDescriptor<T> IdField(Func<IdFieldMapping, IdFieldMapping> idMapper)
		{
			idMapper.ThrowIfNull("idMapper");
			this._TypeMapping.IdFieldMapping = idMapper(new IdFieldMapping());
			return this;
		}

		public TypeMappingDescriptor<T> TypeField(Func<TypeFieldMapping, TypeFieldMapping> typeMapper)
		{
			typeMapper.ThrowIfNull("typeMapper");
			this._TypeMapping.TypeFieldMapping = typeMapper(new TypeFieldMapping());
			return this;
		}
		public TypeMappingDescriptor<T> SourceField(Func<SourceFieldMapping, SourceFieldMapping> sourceMapper)
		{
			sourceMapper.ThrowIfNull("sourceMapper");
			this._TypeMapping.SourceFieldMapping = sourceMapper(new SourceFieldMapping());
			return this;
		}
		
		public TypeMappingDescriptor<T> AnalyzerField(Func<AnalyzerFieldMapping<T>, AnalyzerFieldMapping> analyzeMapper)
		{
			analyzeMapper.ThrowIfNull("analyzeMapper");
			this._TypeMapping.AnalyzerFieldMapping = analyzeMapper(new AnalyzerFieldMapping<T>());
			return this;
		}
		public TypeMappingDescriptor<T> BoostField(Func<BoostFieldMapping<T>, BoostFieldMapping> boostMapper)
		{
			boostMapper.ThrowIfNull("boostMapper");
			this._TypeMapping.BoostFieldMapping = boostMapper(new BoostFieldMapping<T>());
			return this;
		}
		public TypeMappingDescriptor<T> RoutingField(Func<RoutingFieldMapping<T>, RoutingFieldMapping> routingMapper)
		{
			routingMapper.ThrowIfNull("routingMapper");
			this._TypeMapping.RoutingFieldMapping = routingMapper(new RoutingFieldMapping<T>());
			return this;
		}
		public TypeMappingDescriptor<T> TimestampField(Func<TimestampFieldMapping<T>, TimestampFieldMapping> timestampMapper)
		{
			timestampMapper.ThrowIfNull("timestampMapper");
			this._TypeMapping.TimestampFieldMapping = timestampMapper(new TimestampFieldMapping<T>());
			return this;
		}
		public TypeMappingDescriptor<T> TtlField(Func<TtlFieldMapping, TtlFieldMapping> ttlFieldMapper)
		{
			ttlFieldMapper.ThrowIfNull("ttlFieldMapper");
			this._TypeMapping.TtlFieldMapping = ttlFieldMapper(new TtlFieldMapping());
			return this;
		}
		
    }
}