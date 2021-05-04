// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections;
using System.Collections.Generic;
using Elasticsearch.Net;

namespace Nest
{
	public abstract class IsAReadOnlyDictionaryBase<TKey, TValue> : IIsAReadOnlyDictionary<TKey, TValue>
	{
		protected IsAReadOnlyDictionaryBase(IReadOnlyDictionary<TKey, TValue> backingDictionary)
		{
			if (backingDictionary == null) return;

			var dictionary = new Dictionary<TKey, TValue>(backingDictionary.Count);
			foreach (var key in backingDictionary.Keys)
				// ReSharper disable once VirtualMemberCallInConstructor
				// expect all implementations of Sanitize to be pure
				dictionary[Sanitize(key)] = backingDictionary[key];

			BackingDictionary = dictionary;
		}

		public int Count => BackingDictionary.Count;

		public TValue this[TKey key] => BackingDictionary[key];

		public IEnumerable<TKey> Keys => BackingDictionary.Keys;

		public IEnumerable<TValue> Values => BackingDictionary.Values;
		protected internal IReadOnlyDictionary<TKey, TValue> BackingDictionary { get; } = EmptyReadOnly<TKey, TValue>.Dictionary;

		IEnumerator IEnumerable.GetEnumerator() => BackingDictionary.GetEnumerator();

		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() =>
			BackingDictionary.GetEnumerator();

		public bool ContainsKey(TKey key) => BackingDictionary.ContainsKey(key);

		public bool TryGetValue(TKey key, out TValue value) =>
			BackingDictionary.TryGetValue(key, out value);

		protected virtual TKey Sanitize(TKey key) => key;
	}
}
