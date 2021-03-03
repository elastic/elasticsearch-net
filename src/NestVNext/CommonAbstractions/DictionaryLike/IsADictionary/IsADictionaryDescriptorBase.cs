// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	public abstract class IsADictionaryDescriptorBase<TDescriptor, TInterface, TKey, TValue>
		: DescriptorPromiseBase<TDescriptor, TInterface>
		where TDescriptor : IsADictionaryDescriptorBase<TDescriptor, TInterface, TKey, TValue>
		where TInterface : class, IIsADictionary<TKey, TValue>
	{
		protected IsADictionaryDescriptorBase(TInterface instance) : base(instance) { }

		protected TDescriptor Assign(TKey key, TValue value)
		{
			PromisedValue.Add(key, value);
			return Self;
		}
	}
}
