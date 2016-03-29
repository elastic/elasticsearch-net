using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace Nest
{
	public abstract class IsADictionaryBase<TKey, TValue> : IIsADictionary<TKey, TValue>
	{
		protected Dictionary<TKey, TValue> BackingDictionary { get; }
		private IDictionary Self => BackingDictionary;

		protected IsADictionaryBase() { this.BackingDictionary = new Dictionary<TKey, TValue>(); }
		//protected IsADictionaryBase(Dictionary<TKey, TValue> backingDictionary) { this.BackingDictionary = backingDictionary; }
		protected IsADictionaryBase(IDictionary<TKey, TValue> backingDictionary)
		{
			this.BackingDictionary = backingDictionary != null 
				? new Dictionary<TKey, TValue>(backingDictionary) 
				: new Dictionary<TKey, TValue>();
		}

		void IDictionary.Clear() => this.BackingDictionary.Clear();
		IDictionaryEnumerator IDictionary.GetEnumerator() => Self?.GetEnumerator();
		void IDictionary.Remove(object key) => Self?.Remove(key);
		object IDictionary.this[object key]
		{
			get { return Self?[key]; }
			set { Self[key] = value; }
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		bool IDictionary.Contains(object key) => (Self?.Contains(key)).GetValueOrDefault(false);
		void IDictionary.Add(object key, object value) => Self?.Add(key, value);
		bool IDictionary.IsReadOnly=> (Self?.IsReadOnly).GetValueOrDefault(false);
		bool IDictionary.IsFixedSize=> (Self?.IsFixedSize).GetValueOrDefault(false);
		ICollection IDictionary.Keys => Self?.Keys;

		IEnumerator IEnumerable.GetEnumerator() => this.BackingDictionary.GetEnumerator();

		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() => this.BackingDictionary.GetEnumerator();

		//public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => this.BackingDictionary.GetEnumerator();

		void ICollection<KeyValuePair<TKey, TValue>>.Clear() => this.BackingDictionary.Clear();
		[EditorBrowsable(EditorBrowsableState.Never)]
		bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item) => Self?.Contains(item) ?? false;
		void ICollection<KeyValuePair<TKey, TValue>>.CopyTo( KeyValuePair<TKey, TValue>[] array, int arrayIndex) => Self?.CopyTo(array, arrayIndex);
		void ICollection.CopyTo(Array array, int index) => Self?.CopyTo(array, index);
		int ICollection<KeyValuePair<TKey, TValue>>.Count => this.BackingDictionary.Count;
		bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => Self?.IsReadOnly ?? false;
		bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item) => ((ICollection<KeyValuePair<TKey, TValue>>)this.BackingDictionary).Remove(item);
		void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)=> ((ICollection<KeyValuePair<TKey, TValue>>)this.BackingDictionary).Add(item);

		int ICollection.Count => (Self?.Count).GetValueOrDefault(0);
		object ICollection.SyncRoot => Self?.SyncRoot;
		bool ICollection.IsSynchronized => (Self?.IsSynchronized).GetValueOrDefault(false);
		ICollection IDictionary.Values => Self?.Values;

		[EditorBrowsable(EditorBrowsableState.Never)]
		bool IDictionary<TKey, TValue>.ContainsKey(TKey key) => this.BackingDictionary.ContainsKey(key);
		void IDictionary<TKey, TValue>.Add(TKey key, TValue value) => this.BackingDictionary.Add(key, value);
		bool IDictionary<TKey, TValue>.Remove(TKey key) => this.BackingDictionary.Remove(key);
		bool IDictionary<TKey, TValue>.TryGetValue(TKey key, out TValue value) => this.BackingDictionary.TryGetValue(key, out value);
		ICollection<TKey> IDictionary<TKey, TValue>.Keys => this.BackingDictionary.Keys;
		ICollection<TValue> IDictionary<TKey, TValue>.Values => this.BackingDictionary.Values;

		TValue IDictionary<TKey, TValue>.this[TKey key]
		{
			get { return this.BackingDictionary[key]; }
			set { this.BackingDictionary[key] = value; }
		}

		public TValue this[TKey key]
		{
			get { return this.BackingDictionary[key]; }
			set { this.BackingDictionary[key] = value; }
		}


	}


}