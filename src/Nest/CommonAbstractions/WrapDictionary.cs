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
}