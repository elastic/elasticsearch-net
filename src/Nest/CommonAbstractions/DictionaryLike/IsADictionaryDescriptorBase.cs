using System.Collections;
using System.Collections.Generic;

namespace Nest
{
	public interface IIsADictionary { }

	public interface IIsADictionary<TKey, TValue> : IDictionary<TKey, TValue>, IDictionary, IIsADictionary
	{
		
	}

	public abstract class IsADictionaryDescriptorBase<TDescriptor, TInterface, TKey, TValue> 
		: DescriptorPromiseBase<TDescriptor, TInterface>
		where TDescriptor : IsADictionaryDescriptorBase<TDescriptor, TInterface, TKey, TValue>
		where TInterface : class, IIsADictionary<TKey, TValue>
	{

		protected IsADictionaryDescriptorBase(TInterface instance) : base(instance) {}

		protected TDescriptor Assign(TKey key, TValue value) => Assign(a => a.Add(key, value));

	}
}