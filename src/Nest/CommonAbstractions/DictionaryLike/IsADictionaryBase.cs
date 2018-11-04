using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace Nest
{
	public abstract class IsADictionaryBase<TKey, TValue> : IIsADictionary<TKey, TValue>
	{
		protected IsADictionaryBase() => BackingDictionary = new Dictionary<TKey, TValue>();

		protected IsADictionaryBase(IDictionary<TKey, TValue> backingDictionary)
		{
			if (backingDictionary != null)
				foreach (var key in backingDictionary.Keys)
					ValidateKey(key);

			BackingDictionary = backingDictionary != null
				? new Dictionary<TKey, TValue>(backingDictionary)
				: new Dictionary<TKey, TValue>();
		}

		public TValue this[TKey key]
		{
			get => BackingDictionary[key];
			set => BackingDictionary[ValidateKey(key)] = value;
		}

		protected Dictionary<TKey, TValue> BackingDictionary { get; }
		int ICollection<KeyValuePair<TKey, TValue>>.Count => BackingDictionary.Count;
		bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => Self.IsReadOnly;

		TValue IDictionary<TKey, TValue>.this[TKey key]
		{
			get => BackingDictionary[key];
			set => BackingDictionary[ValidateKey(key)] = value;
		}

		ICollection<TKey> IDictionary<TKey, TValue>.Keys => BackingDictionary.Keys;
		private ICollection<KeyValuePair<TKey, TValue>> Self => BackingDictionary;
		ICollection<TValue> IDictionary<TKey, TValue>.Values => BackingDictionary.Values;

		void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
		{
			ValidateKey(item.Key);
			Self.Add(item);
		}

		void ICollection<KeyValuePair<TKey, TValue>>.Clear() => BackingDictionary.Clear();

		[EditorBrowsable(EditorBrowsableState.Never)]
		bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item) => Self.Contains(item);

		void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => Self.CopyTo(array, arrayIndex);

		bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item) => Self.Remove(item);

		void IDictionary<TKey, TValue>.Add(TKey key, TValue value) => BackingDictionary.Add(ValidateKey(key), value);

		[EditorBrowsable(EditorBrowsableState.Never)]
		bool IDictionary<TKey, TValue>.ContainsKey(TKey key) => BackingDictionary.ContainsKey(key);

		bool IDictionary<TKey, TValue>.Remove(TKey key) => BackingDictionary.Remove(key);

		bool IDictionary<TKey, TValue>.TryGetValue(TKey key, out TValue value) => BackingDictionary.TryGetValue(key, out value);

		IEnumerator IEnumerable.GetEnumerator() => BackingDictionary.GetEnumerator();

		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() => BackingDictionary.GetEnumerator();

		public virtual TKey ValidateKey(TKey key) => key;
	}
}
