// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
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
