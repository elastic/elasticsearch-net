using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<Mappings, TypeName, ITypeMapping>))]
	public interface IMappings : IIsADictionary<TypeName, ITypeMapping>
	{
	}

	public class Mappings : IsADictionary<TypeName, ITypeMapping>, IMappings
	{
		public Mappings() : base() { }
		public Mappings(IDictionary<TypeName, ITypeMapping> container) : base(container) { }
		public Mappings(Dictionary<TypeName, ITypeMapping> container)
			: base(container.Select(kv => kv).ToDictionary(kv => kv.Key, kv => kv.Value))
		{ }

		/// <summary>
		/// Add any setting to the index
		/// </summary>
		public void Add(TypeName type, ITypeMapping mapping) => BackingDictionary.Add(type, mapping);
	}
	
	public class MappingsDescriptor : IsADictionaryDescriptor<MappingsDescriptor,IMappings, TypeName, ITypeMapping>, IMappings
	{
		public MappingsDescriptor Map<T>(Func<TypeMappingDescriptor<T>, ITypeMapping> selector) where T : class
		{
			this.BackingDictionary.Add(typeof(T), selector?.Invoke(new TypeMappingDescriptor<T>()));
			return this;
		}
		public MappingsDescriptor Map<T>(TypeName name, Func<TypeMappingDescriptor<T>, ITypeMapping> selector) where T : class
		{
			this.BackingDictionary.Add(name, selector?.Invoke(new TypeMappingDescriptor<T>()));
			return this;
		}
		public MappingsDescriptor Map(TypeName name, Func<TypeMappingDescriptor<object>, ITypeMapping> selector) 
		{
			this.BackingDictionary.Add(name, selector?.Invoke(new TypeMappingDescriptor<object>()));
			return this;
		}
	}

}