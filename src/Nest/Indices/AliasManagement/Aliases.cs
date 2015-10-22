using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter))]
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
	
	public class AliasesDescriptor : IsADictionaryDescriptor<AliasesDescriptor, IAliases, IndexName, IAlias>, IAliases
	{
		public AliasesDescriptor Alias(string alias, Func<AliasDescriptor, IAlias> selector = null)
		{
			this.BackingDictionary.Add(alias, selector.InvokeOrDefault(new AliasDescriptor()));
			return this;
		}
	}

}