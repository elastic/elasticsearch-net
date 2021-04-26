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
using System.Linq;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[JsonFormatter(typeof(VerbatimDictionaryKeysFormatter<NamedFiltersContainer, INamedFiltersContainer, string, IQueryContainer>))]
	public interface INamedFiltersContainer : IIsADictionary<string, IQueryContainer> { }

	public class NamedFiltersContainer : IsADictionaryBase<string, IQueryContainer>, INamedFiltersContainer
	{
		public NamedFiltersContainer() { }

		public NamedFiltersContainer(IDictionary<string, IQueryContainer> container) : base(container) { }

		public NamedFiltersContainer(Dictionary<string, QueryContainer> container)
			: base(container.Select(kv => kv).ToDictionary(kv => kv.Key, kv => (IQueryContainer)kv.Value)) { }

		public void Add(string name, IQueryContainer filter) => BackingDictionary.Add(name, filter);

		public void Add(string name, QueryContainer filter) => BackingDictionary.Add(name, filter);
	}

	public class NamedFiltersContainerDescriptor<T>
		: IsADictionaryDescriptorBase<NamedFiltersContainerDescriptor<T>, INamedFiltersContainer, string, IQueryContainer>
		where T : class
	{
		public NamedFiltersContainerDescriptor() : base(new NamedFiltersContainer()) { }

		public NamedFiltersContainerDescriptor<T> Filter(string name, IQueryContainer filter) => Assign(name, filter);

		public NamedFiltersContainerDescriptor<T> Filter(string name, Func<QueryContainerDescriptor<T>, QueryContainer> selector) =>
			Assign(name, selector?.Invoke(new QueryContainerDescriptor<T>()));

		public NamedFiltersContainerDescriptor<T> Filter<TOther>(string name, Func<QueryContainerDescriptor<TOther>, QueryContainer> selector)
			where TOther : class =>
			Assign(name, selector?.Invoke(new QueryContainerDescriptor<TOther>()));
	}
}
