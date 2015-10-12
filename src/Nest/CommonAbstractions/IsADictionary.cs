using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace Nest
{

	public abstract class IsADictionary<TKey, TValue> : IDictionary<TKey, TValue>, IDictionary, IHasADictionary<TKey, TValue>
	{
		protected IHasADictionary Self => this;
		protected Dictionary<TKey, TValue> BackingDictionary { get; set; }
		IDictionary IHasADictionary.Dictionary => this.BackingDictionary;
		IDictionary<TKey, TValue> IHasADictionary<TKey, TValue>.Dictionary => this.BackingDictionary;

		protected IsADictionary() { this.BackingDictionary = new Dictionary<TKey, TValue>(); }
		protected IsADictionary(Dictionary<TKey, TValue> backingDictionary) { this.BackingDictionary = backingDictionary; }
		protected IsADictionary(IDictionary<TKey, TValue> backingDictionary)
		{
			this.BackingDictionary = backingDictionary != null 
				? new Dictionary<TKey, TValue>(backingDictionary) 
				: new Dictionary<TKey, TValue>();
		}

		void IDictionary.Clear() => this.BackingDictionary.Clear();

		IDictionaryEnumerator IDictionary.GetEnumerator() => Self.Dictionary?.GetEnumerator();

		void IDictionary.Remove(object key) => Self.Dictionary?.Remove(key);

		object IDictionary.this[object key]
		{
			get { return Self.Dictionary?[key]; }
			set { Self.Dictionary[key] = value; }
		}

		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() =>
			this.BackingDictionary.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => this.BackingDictionary.GetEnumerator();

		[EditorBrowsable(EditorBrowsableState.Never)]
		bool IDictionary.Contains(object key) => (Self.Dictionary?.Contains(key)).GetValueOrDefault(false);

		void IDictionary.Add(object key, object value) => Self.Dictionary?.Add(key, value);

		void ICollection<KeyValuePair<TKey, TValue>>.Clear() =>
			this.BackingDictionary.Clear();

		[EditorBrowsable(EditorBrowsableState.Never)]
		bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item) =>
			Self.Dictionary.Contains(item);

		void ICollection<KeyValuePair<TKey, TValue>>.CopyTo( KeyValuePair<TKey, TValue>[] array, int arrayIndex) =>
			Self.Dictionary.CopyTo(array, arrayIndex);

		void ICollection.CopyTo(Array array, int index) => Self.Dictionary?.CopyTo(array, index);

		int ICollection.Count => (Self.Dictionary?.Count).GetValueOrDefault(0);

		object ICollection.SyncRoot => Self.Dictionary?.SyncRoot;

		bool ICollection.IsSynchronized => (Self.Dictionary?.IsSynchronized).GetValueOrDefault(false);

		int ICollection<KeyValuePair<TKey, TValue>>.Count => this.BackingDictionary.Count;

		ICollection IDictionary.Values => Self.Dictionary?.Values;

		bool IDictionary.IsReadOnly=> (Self.Dictionary?.IsReadOnly).GetValueOrDefault(false);

		bool IDictionary.IsFixedSize=> (Self.Dictionary?.IsFixedSize).GetValueOrDefault(false);

		bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => Self.Dictionary.IsReadOnly;

		[EditorBrowsable(EditorBrowsableState.Never)]
		bool IDictionary<TKey, TValue>.ContainsKey(TKey key) => this.BackingDictionary.ContainsKey(key);

		void IDictionary<TKey, TValue>.Add(TKey key, TValue value) =>
			this.BackingDictionary.Add(key, value);

		bool IDictionary<TKey, TValue>.Remove(TKey key) =>
			this.BackingDictionary.Remove(key);

		bool IDictionary<TKey, TValue>.TryGetValue(TKey key, out TValue value) =>
			this.BackingDictionary.TryGetValue(key, out value);

		bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item) =>
			((ICollection<KeyValuePair<TKey, TValue>>)this.BackingDictionary).Remove(item);

		void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)=>
			((ICollection<KeyValuePair<TKey, TValue>>)this.BackingDictionary).Add(item);

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

		ICollection<TKey> IDictionary<TKey, TValue>.Keys => this.BackingDictionary.Keys;

		ICollection IDictionary.Keys => Self.Dictionary?.Keys;

		ICollection<TValue> IDictionary<TKey, TValue>.Values => this.BackingDictionary.Values;

	}


}