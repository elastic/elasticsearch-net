using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Nest
{
	public abstract class IsAReadOnlyDictionaryBase<TKey, TValue> : IIsAReadOnlyDictionary<TKey, TValue>
	{
		protected internal IReadOnlyDictionary<TKey, TValue> BackingDictionary { get; } = EmptyReadOnly<TKey, TValue>.Dictionary;

		protected IsAReadOnlyDictionaryBase(IReadOnlyDictionary<TKey, TValue> backingDictionary) =>
			this.BackingDictionary = backingDictionary ?? EmptyReadOnly<TKey, TValue>.Dictionary;
		protected IsAReadOnlyDictionaryBase(IDictionary<TKey, TValue> backingDictionary)
		{
			if (backingDictionary == null) return;

			foreach (var key in backingDictionary.Keys) ValidateKey(key);

			this.BackingDictionary = new ReadOnlyDictionary<TKey, TValue>(backingDictionary);
		}
		protected virtual TKey ValidateKey(TKey key) => key;

		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() =>
			this.BackingDictionary.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => this.BackingDictionary.GetEnumerator();

		public int Count => this.BackingDictionary.Count;

		public bool ContainsKey(TKey key) => this.BackingDictionary.ContainsKey(key);

		public bool TryGetValue(TKey key, out TValue value) =>
			this.BackingDictionary.TryGetValue(key, out value);

		public TValue this[TKey key] => this.BackingDictionary[key];

		public IEnumerable<TKey> Keys => this.BackingDictionary.Keys;

		public IEnumerable<TValue> Values => this.BackingDictionary.Values;
	}
}
