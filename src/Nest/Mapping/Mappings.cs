using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	public interface IMappings : IWrapDictionary { }
	public class Mappings : ProxyDictionary<TypeName, ITypeMapping>, IMappings
	{
		public Mappings() : base() { }
		public Mappings(IDictionary<TypeName, ITypeMapping> container) : base(container) { }
		public Mappings(Dictionary<TypeName, ITypeMapping> container)
			: base(container.Select(kv => kv).ToDictionary(kv => kv.Key, kv => kv.Value))
		{ }

		/// <summary>
		/// Add any setting to the index
		/// </summary>
		public void Add(TypeName type, ITypeMapping mapping) => _backingDictionary.Add(type, mapping);

	}
	
	public class MappingsDescriptor : WrapDictionaryDescriptor<MappingsDescriptor, TypeName, ITypeMapping>, IMappings
	{
		public MappingsDescriptor Map<T>(Func<TypeMappingDescriptor<T>, ITypeMapping> selector) where T : class
		{
			_backingDictionary.Add(typeof(T), selector?.Invoke(new TypeMappingDescriptor<T>()));
			return this;
		}
	}

}