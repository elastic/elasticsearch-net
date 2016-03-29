using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<Aliases, IndexName, IAlias>))]
	public interface IAliases : IIsADictionary<IndexName, IAlias> { }
	public class Aliases : IsADictionaryBase<IndexName, IAlias>, IAliases
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
	
	public class AliasesDescriptor : IsADictionaryDescriptorBase<AliasesDescriptor, IAliases, IndexName, IAlias>
	{
		public AliasesDescriptor() : base(new Aliases()) { }

		public AliasesDescriptor Alias(string alias, Func<AliasDescriptor, IAlias> selector = null) => Assign(alias, selector.InvokeOrDefault(new AliasDescriptor()));
	}

}