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
	[JsonFormatter(typeof(DynamicTemplatesInterfaceFormatter))]
	public interface IDynamicTemplateContainer : IIsADictionary<string, IDynamicTemplate> { }

	[JsonFormatter(typeof(DynamicTemplatesFormatter))]
	public class DynamicTemplateContainer : IsADictionaryBase<string, IDynamicTemplate>, IDynamicTemplateContainer
	{
		public DynamicTemplateContainer() { }

		public DynamicTemplateContainer(IDictionary<string, IDynamicTemplate> container) : base(container) { }

		public DynamicTemplateContainer(Dictionary<string, IDynamicTemplate> container) : base(container) { }

		/// <summary>
		/// Add any setting to the index
		/// </summary>
		public void Add(string name, IDynamicTemplate dynamicTemplate) => BackingDictionary.Add(name, dynamicTemplate);
	}

	public class DynamicTemplateContainerDescriptor<T>
		: IsADictionaryDescriptorBase<DynamicTemplateContainerDescriptor<T>, IDynamicTemplateContainer, string, IDynamicTemplate>
		where T : class
	{
		public DynamicTemplateContainerDescriptor() : base(new DynamicTemplateContainer()) { }

		public DynamicTemplateContainerDescriptor<T> DynamicTemplate(string name, Func<DynamicTemplateDescriptor<T>, IDynamicTemplate> selector) =>
			Assign(name, selector?.Invoke(new DynamicTemplateDescriptor<T>()));
	}
}
