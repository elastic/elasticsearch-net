// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using Nest.Utf8Json;

namespace Nest
{
	[JsonFormatter(typeof(VerbatimDictionaryKeysFormatter<Aliases, IAliases, IndexName, IAlias>))]
	public interface IAliases : IIsADictionary<IndexName, IAlias> { }

	public class Aliases : IsADictionaryBase<IndexName, IAlias>, IAliases
	{
		public Aliases() { }

		public Aliases(IDictionary<IndexName, IAlias> container) : base(container) { }

		public Aliases(Dictionary<IndexName, IAlias> container) : base(container) { }

		/// <summary>
		/// Add any setting to the index
		/// </summary>
		public void Add(IndexName index, IAlias alias) => BackingDictionary.Add(index, alias);
	}

	public class AliasesDescriptor : IsADictionaryDescriptorBase<AliasesDescriptor, IAliases, IndexName, IAlias>
	{
		public AliasesDescriptor() : base(new Aliases()) { }

		public AliasesDescriptor Alias(string alias, Func<AliasDescriptor, IAlias> selector = null) =>
			Assign(alias, selector.InvokeOrDefault(new AliasDescriptor()));
	}
}
