using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace Nest
{
	public interface IWrapDictionary
	{
		IDictionary BackingDictionary { get; }
	}

	public abstract class WrapDictionary<TKey, TValue> : IWrapDictionary
	{
		protected readonly IDictionary<TKey, TValue> _backingDictionary;
		IDictionary IWrapDictionary.BackingDictionary => _backingDictionary as IDictionary;

		protected WrapDictionary() { this._backingDictionary = new Dictionary<TKey, TValue>(); }
		protected WrapDictionary(IDictionary<TKey, TValue> backingDictionary)
		{
			this._backingDictionary = new Dictionary<TKey, TValue>(backingDictionary);
		}
		protected WrapDictionary(Dictionary<TKey, TValue> backingDictionary) { this._backingDictionary = backingDictionary; }
	}

	public abstract class WrapDictionaryDescriptor<TDescriptor, TKey, TValue> 
		: WrapDictionaryDescriptor<TDescriptor, TDescriptor, TKey, TValue>
		where TDescriptor : WrapDictionaryDescriptor<TDescriptor, TKey, TValue> { }

	public abstract class WrapDictionaryDescriptor<TDescriptor, TInterface, TKey, TValue> 
		: DescriptorBase<TDescriptor, TInterface>, IWrapDictionary
		where TDescriptor : WrapDictionaryDescriptor<TDescriptor, TInterface, TKey, TValue>, TInterface
		where TInterface : class
	{
		protected readonly IDictionary<TKey, TValue> _backingDictionary;
		IDictionary IWrapDictionary.BackingDictionary => _backingDictionary as IDictionary;

		protected WrapDictionaryDescriptor() { this._backingDictionary = new Dictionary<TKey, TValue>(); }
	}
}