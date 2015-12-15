using System.Collections;
using System.Collections.Generic;

namespace Nest
{
	public interface IIsADictionary { }

	public interface IIsADictionary<TKey, TValue> : IDictionary<TKey, TValue>, IDictionary, IIsADictionary
	{
		
	}

	public abstract class IsADictionaryDescriptor<TDescriptor, TInterface, TKey, TValue> 
		: DescriptorPromiseBase<TDescriptor, TInterface>
		where TDescriptor : IsADictionaryDescriptor<TDescriptor, TInterface, TKey, TValue>
		where TInterface : class, IIsADictionary<TKey, TValue>
	{

		protected IsADictionaryDescriptor(TInterface instance) : base(instance) {}

		protected TDescriptor Assign(TKey key, TValue value) => Assign(a => a.Add(key, value));

	}
}