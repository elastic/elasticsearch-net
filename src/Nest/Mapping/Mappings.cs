using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<Mappings, TypeName, ITypeMapping>))]
	public interface IMappings : IIsADictionary<TypeName, ITypeMapping> { }

	public class Mappings : IsADictionaryBase<TypeName, ITypeMapping>, IMappings
	{
		public Mappings() : base() { }
		public Mappings(IDictionary<TypeName, ITypeMapping> container) : base(container) { }
		public Mappings(Dictionary<TypeName, ITypeMapping> container)
			: base(container.Select(kv => kv).ToDictionary(kv => kv.Key, kv => kv.Value))
		{ }

		public void Add(TypeName type, ITypeMapping mapping) => BackingDictionary.Add(type, mapping);
	}
	
	public class MappingsDescriptor : IsADictionaryDescriptorBase<MappingsDescriptor,IMappings, TypeName, ITypeMapping>
	{
		public MappingsDescriptor() : base(new Mappings()) { }

		public MappingsDescriptor Map<T>(Func<TypeMappingDescriptor<T>, ITypeMapping> selector) where T : class =>
			Assign(typeof (T), selector?.Invoke(new TypeMappingDescriptor<T>()));

		public MappingsDescriptor Map<T>(TypeName name, Func<TypeMappingDescriptor<T>, ITypeMapping> selector) where T : class =>
			Assign(name, selector?.Invoke(new TypeMappingDescriptor<T>()));

		public MappingsDescriptor Map(TypeName name, Func<TypeMappingDescriptor<object>, ITypeMapping> selector) =>
			Assign(name, selector?.Invoke(new TypeMappingDescriptor<object>()));

	}

}