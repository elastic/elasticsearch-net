using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Nest
{
	public abstract class IsADictionaryBase<TKey, TValue> : IIsADictionary<TKey, TValue>
	{
		protected Dictionary<TKey, TValue> BackingDictionary { get; }
		private ICollection<KeyValuePair<TKey, TValue>> Self => BackingDictionary;

		protected IsADictionaryBase() => this.BackingDictionary = new Dictionary<TKey, TValue>();

		protected IsADictionaryBase(IDictionary<TKey, TValue> backingDictionary)
		{
			if (backingDictionary != null)
				foreach (var key in backingDictionary.Keys) ValidateKey(Sanitize(key));

			this.BackingDictionary = backingDictionary != null
				? new Dictionary<TKey, TValue>(backingDictionary)
				: new Dictionary<TKey, TValue>();
		}

		IEnumerator IEnumerable.GetEnumerator() => this.BackingDictionary.GetEnumerator();

		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() => this.BackingDictionary.GetEnumerator();

		void ICollection<KeyValuePair<TKey, TValue>>.Clear() => this.BackingDictionary.Clear();
		[EditorBrowsable(EditorBrowsableState.Never)]
		bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item) => Self.Contains(item);
		void ICollection<KeyValuePair<TKey, TValue>>.CopyTo( KeyValuePair<TKey, TValue>[] array, int arrayIndex) => Self.CopyTo(array, arrayIndex);
		int ICollection<KeyValuePair<TKey, TValue>>.Count => this.BackingDictionary.Count;
		bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => Self.IsReadOnly;
		bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item) => Self.Remove(item);
		void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
		{
			ValidateKey(Sanitize(item.Key));
			Self.Add(item);
		}

		ICollection<TKey> IDictionary<TKey, TValue>.Keys => this.BackingDictionary.Keys;
		ICollection<TValue> IDictionary<TKey, TValue>.Values => this.BackingDictionary.Values;

		[EditorBrowsable(EditorBrowsableState.Never)]
		bool IDictionary<TKey, TValue>.ContainsKey(TKey key) => this.BackingDictionary.ContainsKey(Sanitize(key));
		void IDictionary<TKey, TValue>.Add(TKey key, TValue value) => this.BackingDictionary.Add(ValidateKey(Sanitize(key)), value);
		bool IDictionary<TKey, TValue>.Remove(TKey key) => this.BackingDictionary.Remove(Sanitize(key));
		bool IDictionary<TKey, TValue>.TryGetValue(TKey key, out TValue value) => this.BackingDictionary.TryGetValue(Sanitize(key), out value);

		protected virtual TKey ValidateKey(TKey key) => key;
		protected virtual TKey Sanitize(TKey key) => key;

		TValue IDictionary<TKey, TValue>.this[TKey key]
		{
			get => this.BackingDictionary[Sanitize(key)];
			set => this.BackingDictionary[ValidateKey(Sanitize(key))] = value;
		}

		public TValue this[TKey key]
		{
			get => this.BackingDictionary[Sanitize(key)];
			set => this.BackingDictionary[ValidateKey(Sanitize(key))] = value;
		}
	}
}
