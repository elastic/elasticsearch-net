/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
using System.Collections.Generic;
using Elasticsearch.Net.Utf8Json;

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
