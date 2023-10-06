// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Elastic.Transport;
using System.Collections;
using System.Collections.Generic;

namespace Elastic.Clients.Elasticsearch;

/// <summary>
/// A proxy dictionary that is settings-aware to correctly handle IUrlParameter-based keys such as IndexName.
/// </summary>
public sealed class ResolvableDictionaryProxy<TKey, TValue> : IIsAReadOnlyDictionary<TKey, TValue>
		where TKey : IUrlParameter
{
	private readonly IElasticsearchClientSettings _elasticsearchClientSettings;

	internal ResolvableDictionaryProxy(IElasticsearchClientSettings elasticsearchClientSettings, IReadOnlyDictionary<TKey, TValue> backingDictionary)
	{
		_elasticsearchClientSettings = elasticsearchClientSettings;

		if (backingDictionary == null)
			return;

		Original = backingDictionary;

		var dictionary = new Dictionary<string, TValue>(backingDictionary.Count);

		foreach (var key in backingDictionary.Keys)
			dictionary[Sanitize(key)] = backingDictionary[key];

		BackingDictionary = dictionary;
	}

	public int Count => BackingDictionary.Count;

	public TValue this[TKey key] => BackingDictionary.TryGetValue(Sanitize(key), out var v) ? v : default;
	public TValue this[string key] => BackingDictionary.TryGetValue(key, out var v) ? v : default;

	public IEnumerable<TKey> Keys => Original.Keys;
	public IEnumerable<string> ResolvedKeys => BackingDictionary.Keys;

	public IEnumerable<TValue> Values => BackingDictionary.Values;
	internal IReadOnlyDictionary<string, TValue> BackingDictionary { get; } = EmptyReadOnly<string, TValue>.Dictionary;
	private IReadOnlyDictionary<TKey, TValue> Original { get; } = EmptyReadOnly<TKey, TValue>.Dictionary;

	IEnumerator IEnumerable.GetEnumerator() => Original.GetEnumerator();

	IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() =>
		Original.GetEnumerator();

	public bool ContainsKey(TKey key) => BackingDictionary.ContainsKey(Sanitize(key));

	public bool TryGetValue(TKey key, out TValue value) =>
		BackingDictionary.TryGetValue(Sanitize(key), out value);

	private string Sanitize(TKey key) => key?.GetString(_elasticsearchClientSettings);
}
