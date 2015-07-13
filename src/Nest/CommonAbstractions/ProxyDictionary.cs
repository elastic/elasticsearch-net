using System;
using System.Collections;
using System.Collections.Generic;

namespace Nest
{
	public abstract class ProxyDictionary<TKey, TValue> : IDictionary<TKey, TValue>, IDictionary
	{
		protected readonly IDictionary<TKey, TValue> _backingDictionary;
		private IDictionary ObsoleteDict => _backingDictionary as IDictionary;

		protected ProxyDictionary() { this._backingDictionary = new Dictionary<TKey, TValue>(); }
		protected ProxyDictionary(IDictionary<TKey, TValue> backingDictionary) { this._backingDictionary = backingDictionary; }
		protected ProxyDictionary(Dictionary<TKey, TValue> backingDictionary) { this._backingDictionary = backingDictionary; }

		void IDictionary.Clear() => _backingDictionary.Clear();

		IDictionaryEnumerator IDictionary.GetEnumerator() => ObsoleteDict?.GetEnumerator();

		void IDictionary.Remove(object key) => ObsoleteDict?.Remove(key);

		object IDictionary.this[object key]
		{
			get { return ObsoleteDict?[key]; }
			set { ObsoleteDict[key] = value; }
		}

		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.  GetEnumerator() =>
			_backingDictionary.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => _backingDictionary.GetEnumerator();

		void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item) =>
			_backingDictionary.Add(item);

		bool IDictionary.Contains(object key) => (ObsoleteDict?.Contains(key)).GetValueOrDefault(false);

		void IDictionary.Add(object key, object value) => ObsoleteDict?.Add(key, value);

		void ICollection<KeyValuePair<TKey, TValue>>.Clear() =>
			_backingDictionary.Clear();

		bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item) =>
			_backingDictionary.Contains(item);

		void ICollection<KeyValuePair<TKey, TValue>>.CopyTo( KeyValuePair<TKey, TValue>[] array, int arrayIndex) =>
			_backingDictionary.CopyTo(array, arrayIndex);

		bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item) =>
			_backingDictionary.Remove(item);

		void ICollection.CopyTo(Array array, int index) => ObsoleteDict?.CopyTo(array, index);

		int ICollection.Count => (ObsoleteDict?.Count).GetValueOrDefault(0);

		object ICollection.SyncRoot => ObsoleteDict?.SyncRoot;

		bool ICollection.IsSynchronized => (ObsoleteDict?.IsSynchronized).GetValueOrDefault(false);

		int ICollection<KeyValuePair<TKey, TValue>>.Count => _backingDictionary.Count;

		ICollection IDictionary.Values => ObsoleteDict?.Values;

		bool IDictionary.IsReadOnly=> (ObsoleteDict?.IsReadOnly).GetValueOrDefault(false);

		bool IDictionary.IsFixedSize=> (ObsoleteDict?.IsFixedSize).GetValueOrDefault(false);

		bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => _backingDictionary.IsReadOnly;

		bool IDictionary<TKey, TValue>.ContainsKey(TKey key) => _backingDictionary.ContainsKey(key);

		void IDictionary<TKey, TValue>.Add(TKey key, TValue value) =>
			_backingDictionary.Add(key, value);

		bool IDictionary<TKey, TValue>.Remove(TKey key) =>
			_backingDictionary.Remove(key);

		bool IDictionary<TKey, TValue>.TryGetValue(TKey key, out TValue value) =>
			_backingDictionary.TryGetValue(key, out value);

		TValue IDictionary<TKey, TValue>.this[TKey key]
		{
			get { return _backingDictionary[key]; }
			set { _backingDictionary[key] = value; }
		}

		public TValue this[TKey key]
		{
			get { return _backingDictionary[key]; }
			set { _backingDictionary[key] = value; }
		}

		ICollection<TKey> IDictionary<TKey, TValue>.Keys => _backingDictionary.Keys;

		ICollection IDictionary.Keys => ObsoleteDict?.Keys;

		ICollection<TValue> IDictionary<TKey, TValue>.Values => _backingDictionary.Values;

	}
}