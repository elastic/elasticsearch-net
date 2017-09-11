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
		public Mappings() {}
		public Mappings(IDictionary<TypeName, ITypeMapping> container) : base(container) { }
		public Mappings(Dictionary<TypeName, ITypeMapping> container)
			: base(container.Select(kv => kv).ToDictionary(kv => kv.Key, kv => kv.Value))
		{ }

		public void Add(TypeName type, ITypeMapping mapping) => BackingDictionary.Add(type, mapping);
	}

	public class MappingsDescriptor : IsADictionaryDescriptorBase<MappingsDescriptor,IMappings, TypeName, ITypeMapping>
	{
		public MappingsDescriptor() : base(new Mappings()) { }
		internal MappingsDescriptor(IMappings mappings) : base(mappings) { }

		protected MultipleMappingsDescriptor AssignMap(IPromise<IMappings> d)  =>
			new MultipleMappingsDescriptor(d.Value);

		public virtual MultipleMappingsDescriptor Map<T>(Func<TypeMappingDescriptor<T>, ITypeMapping> selector) where T : class =>
			AssignMap(Assign(typeof (T), selector?.Invoke(new TypeMappingDescriptor<T>())));

		public virtual MultipleMappingsDescriptor Map<T>(TypeName name, Func<TypeMappingDescriptor<T>, ITypeMapping> selector) where T : class =>
			AssignMap(Assign(name, selector?.Invoke(new TypeMappingDescriptor<T>())));

		public virtual MultipleMappingsDescriptor Map(TypeName name, Func<TypeMappingDescriptor<object>, ITypeMapping> selector) =>
			AssignMap(Assign(name, selector?.Invoke(new TypeMappingDescriptor<object>())));
	}

	public class MultipleMappingsDescriptor : MappingsDescriptor
	{
		internal MultipleMappingsDescriptor(IMappings mappings) : base(mappings) { }

		[Obsolete("Mapping multiple types is no longer supported on indices created in Elasticsearch 6.x and up")]
		public override MultipleMappingsDescriptor Map<T>(Func<TypeMappingDescriptor<T>, ITypeMapping> selector) =>
			AssignMap(Assign(typeof(T), selector?.Invoke(new TypeMappingDescriptor<T>())));

		[Obsolete("Mapping multiple types is no longer supported on indices created in Elasticsearch 6.x and up")]
		public override MultipleMappingsDescriptor Map<T>(TypeName name, Func<TypeMappingDescriptor<T>, ITypeMapping> selector) =>
			AssignMap(Assign(name, selector?.Invoke(new TypeMappingDescriptor<T>())));

		[Obsolete("Mapping multiple types is no longer supported on indices created in Elasticsearch 6.x and up")]
		public override MultipleMappingsDescriptor Map(TypeName name, Func<TypeMappingDescriptor<object>, ITypeMapping> selector) =>
			AssignMap(Assign(name, selector?.Invoke(new TypeMappingDescriptor<object>())));

	}

}
