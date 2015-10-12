using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;

namespace Nest
{
	public interface IHasADictionary
	{
		[JsonIgnore]
		IDictionary Dictionary { get; }
	}
	public interface IHasADictionary<TKey, TValue> : IHasADictionary
	{
		[JsonIgnore]
		new IDictionary<TKey, TValue> Dictionary { get; }

		TValue this[TKey key] { get; set; }

	}

	public abstract class HasADictionary<TKey, TValue> : IHasADictionary<TKey, TValue>
	{
		protected Dictionary<TKey, TValue> BackingDictionary { get; set; }
		IDictionary IHasADictionary.Dictionary => this.BackingDictionary as IDictionary;
		IDictionary<TKey, TValue> IHasADictionary<TKey, TValue>.Dictionary => this.BackingDictionary;

		protected HasADictionary() { this.BackingDictionary = new Dictionary<TKey, TValue>(); }
		protected HasADictionary(Dictionary<TKey, TValue> backingDictionary) { this.BackingDictionary = backingDictionary; }
		protected HasADictionary(IDictionary<TKey, TValue> backingDictionary)
		{
			this.BackingDictionary = new Dictionary<TKey, TValue>(backingDictionary);
		}
		public TValue this[TKey key]
		{
			get { return this.BackingDictionary[key]; }
			set { this.BackingDictionary[key] = value; }
		}
	}

	public abstract class HasADictionary<TDescriptor, TKey, TValue> 
		: HasADictionaryDescriptor<TDescriptor, TDescriptor, TKey, TValue>
		where TDescriptor : HasADictionary<TDescriptor, TKey, TValue> { }

	public abstract class HasADictionaryDescriptor<TDescriptor, TInterface, TKey, TValue> 
		: DescriptorBase<TDescriptor, TInterface>, IHasADictionary<TKey, TValue>
		where TDescriptor : HasADictionaryDescriptor<TDescriptor, TInterface, TKey, TValue>, TInterface
		where TInterface : class
	{
		protected Dictionary<TKey, TValue> BackingDictionary { get; set; }
		IDictionary IHasADictionary.Dictionary => this.BackingDictionary;
		IDictionary<TKey, TValue> IHasADictionary<TKey, TValue>.Dictionary => this.BackingDictionary;

		protected TDescriptor Assign(TKey key, TValue value) =>
			Fluent.Assign<TDescriptor, TDescriptor>((TDescriptor)this, (a) => BackingDictionary.Add(key, value));

		protected HasADictionaryDescriptor() { this.BackingDictionary = new Dictionary<TKey, TValue>(); }
		protected HasADictionaryDescriptor(Dictionary<TKey, TValue> backingDictionary) { this.BackingDictionary = backingDictionary; }
		protected HasADictionaryDescriptor(IDictionary<TKey, TValue> backingDictionary)
		{
			this.BackingDictionary = backingDictionary != null 
				? new Dictionary<TKey, TValue>(backingDictionary) 
				: new Dictionary<TKey, TValue>();
		}

		TValue IHasADictionary<TKey, TValue>.this[TKey key]
		{
			get { return this.BackingDictionary[key]; }
			set { this.BackingDictionary[key] = value; }
		}
	}
}