using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	public interface IAliases : IHasADictionary { }
	public class Aliases : IsADictionary<IndexName, IAlias>, IAliases
	{
		public Aliases() : base() { }
		public Aliases(IDictionary<IndexName, IAlias> container) : base(container) { }
		public Aliases(Dictionary<IndexName, IAlias> container)
			: base(container.Select(kv => kv).ToDictionary(kv => kv.Key, kv => kv.Value))
		{ }

		/// <summary>
		/// Add any setting to the index
		/// </summary>
		public void Add(IndexName index, IAlias alias) => BackingDictionary.Add(index, alias);

	}
	
	public class AliasesDescriptor : HasADictionary<AliasesDescriptor, IndexName, IAlias>, IAliases
	{
		public AliasesDescriptor Alias<T>(Func<BulkAliasDescriptor, ITypeMapping> selector) where T : class
		{
			this.BackingDictionary.Add(typeof(T), selector?.Invoke(new TypeMappingDescriptor<T>()));
			return this;
		}
	}

}