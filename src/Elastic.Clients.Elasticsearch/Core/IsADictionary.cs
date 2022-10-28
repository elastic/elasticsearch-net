// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace Elastic.Clients.Elasticsearch;

public interface IIsADictionary { }

public interface IIsADictionary<TKey, TValue> : IDictionary<TKey, TValue>, IIsADictionary { }

/// <summary>
/// A base dictionary type for internally tagged unions.
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TValue"></typeparam>

public abstract class IsADictionary<TKey, TValue> : IIsADictionary<TKey, TValue>
{
	internal IsADictionary() => BackingDictionary = new Dictionary<TKey, TValue>();

	internal IsADictionary(IDictionary<TKey, TValue> backingDictionary)
	{
		if (backingDictionary != null)
			foreach (var key in backingDictionary.Keys)
				ValidateKey(Sanitize(key));

		BackingDictionary = backingDictionary != null
			? new Dictionary<TKey, TValue>(backingDictionary)
			: new Dictionary<TKey, TValue>();
	}

	public TValue this[TKey key]
	{
		get => BackingDictionary[Sanitize(key)];
		set => BackingDictionary[ValidateKey(Sanitize(key))] = value;
	}

	internal Dictionary<TKey, TValue> BackingDictionary { get; }
	int ICollection<KeyValuePair<TKey, TValue>>.Count => BackingDictionary.Count;
	bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => Self.IsReadOnly;

	TValue IDictionary<TKey, TValue>.this[TKey key]
	{
		get => BackingDictionary[Sanitize(key)];
		set => BackingDictionary[ValidateKey(Sanitize(key))] = value;
	}

	ICollection<TKey> IDictionary<TKey, TValue>.Keys => BackingDictionary.Keys;
	private ICollection<KeyValuePair<TKey, TValue>> Self => BackingDictionary;
	ICollection<TValue> IDictionary<TKey, TValue>.Values => BackingDictionary.Values;

	void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
	{
		ValidateKey(Sanitize(item.Key));
		Self.Add(item);
	}

	void ICollection<KeyValuePair<TKey, TValue>>.Clear() => BackingDictionary.Clear();

	[EditorBrowsable(EditorBrowsableState.Never)]
	bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item) => Self.Contains(item);

	void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => Self.CopyTo(array, arrayIndex);

	bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item) => Self.Remove(item);

	void IDictionary<TKey, TValue>.Add(TKey key, TValue value) => BackingDictionary.Add(ValidateKey(Sanitize(key)), value);

	[EditorBrowsable(EditorBrowsableState.Never)]
	bool IDictionary<TKey, TValue>.ContainsKey(TKey key) => BackingDictionary.ContainsKey(Sanitize(key));

	bool IDictionary<TKey, TValue>.Remove(TKey key) => BackingDictionary.Remove(Sanitize(key));

	bool IDictionary<TKey, TValue>.TryGetValue(TKey key, out TValue value) => BackingDictionary.TryGetValue(Sanitize(key), out value);

	IEnumerator IEnumerable.GetEnumerator() => BackingDictionary.GetEnumerator();

	IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() => BackingDictionary.GetEnumerator();

	protected virtual TKey ValidateKey(TKey key) => key;

	protected virtual TKey Sanitize(TKey key) => key;
}
