using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;

namespace Nest
{
	public interface IIsADictionary<TKey, TValue> : IDictionary<TKey, TValue>, IDictionary, IHasADictionary
	{
		
	}

	public abstract class IsADictionaryDescriptor<TDescriptor, TInterface, TKey, TValue> 
		: DescriptorBase<TDescriptor, TInterface>, IIsADictionary<TKey, TValue> 
		where TDescriptor : IsADictionaryDescriptor<TDescriptor, TInterface, TKey, TValue>, TInterface
		where TInterface : class
	{

		protected TDescriptor Assign(TKey key, TValue value) =>
			Fluent.Assign<TDescriptor, TDescriptor>((TDescriptor)this, (a) => BackingDictionary.Add(key, value));

		private IDictionary OldSchoolDictionary => ((IHasADictionary)this).Dictionary;
		protected Dictionary<TKey, TValue> BackingDictionary { get; set; }

		IDictionary IHasADictionary.Dictionary => this.BackingDictionary;

		protected IsADictionaryDescriptor() { this.BackingDictionary = new Dictionary<TKey, TValue>(); }
		protected IsADictionaryDescriptor(Dictionary<TKey, TValue> backingDictionary) { this.BackingDictionary = backingDictionary; }
		protected IsADictionaryDescriptor(IDictionary<TKey, TValue> backingDictionary)
		{
			this.BackingDictionary = backingDictionary != null 
				? new Dictionary<TKey, TValue>(backingDictionary) 
				: new Dictionary<TKey, TValue>();
		}

		void IDictionary.Clear() => this.BackingDictionary.Clear();

		IDictionaryEnumerator IDictionary.GetEnumerator() => this.OldSchoolDictionary?.GetEnumerator();

		void IDictionary.Remove(object key) => this.OldSchoolDictionary?.Remove(key);

		object IDictionary.this[object key]
		{
			get { return this.OldSchoolDictionary?[key]; }
			set { this.OldSchoolDictionary[key] = value; }
		}

		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() =>
			this.BackingDictionary.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => this.BackingDictionary.GetEnumerator();

		[EditorBrowsable(EditorBrowsableState.Never)]
		bool IDictionary.Contains(object key) => (this.OldSchoolDictionary?.Contains(key)).GetValueOrDefault(false);

		void IDictionary.Add(object key, object value) => this.OldSchoolDictionary?.Add(key, value);

		void ICollection<KeyValuePair<TKey, TValue>>.Clear() =>
			this.BackingDictionary.Clear();

		[EditorBrowsable(EditorBrowsableState.Never)]
		bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item) =>
			this.OldSchoolDictionary.Contains(item);

		void ICollection<KeyValuePair<TKey, TValue>>.CopyTo( KeyValuePair<TKey, TValue>[] array, int arrayIndex) =>
			this.OldSchoolDictionary.CopyTo(array, arrayIndex);

		void ICollection.CopyTo(Array array, int index) => this.OldSchoolDictionary?.CopyTo(array, index);

		int ICollection.Count => (this.OldSchoolDictionary?.Count).GetValueOrDefault(0);

		object ICollection.SyncRoot => this.OldSchoolDictionary?.SyncRoot;

		bool ICollection.IsSynchronized => (this.OldSchoolDictionary?.IsSynchronized).GetValueOrDefault(false);

		int ICollection<KeyValuePair<TKey, TValue>>.Count => this.BackingDictionary.Count;

		ICollection IDictionary.Values => this.OldSchoolDictionary?.Values;

		bool IDictionary.IsReadOnly=> (this.OldSchoolDictionary?.IsReadOnly).GetValueOrDefault(false);

		bool IDictionary.IsFixedSize=> (this.OldSchoolDictionary?.IsFixedSize).GetValueOrDefault(false);

		bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => this.OldSchoolDictionary.IsReadOnly;

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

		ICollection IDictionary.Keys => this.OldSchoolDictionary?.Keys;

		ICollection<TValue> IDictionary<TKey, TValue>.Values => this.BackingDictionary.Values;

	}
}