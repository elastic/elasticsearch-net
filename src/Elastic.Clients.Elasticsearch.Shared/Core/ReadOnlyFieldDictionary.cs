// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Collections;
using System.Collections.Generic;
using Elastic.Transport;

#if ELASTICSEARCH_SERVERLESS
namespace Elastic.Clients.Elasticsearch.Serverless;
#else
namespace Elastic.Clients.Elasticsearch;
#endif

/// <summary>
/// A specialised readonly dictionary for <typeparamref name="TValue"/> data, keyed by <see cref="IndexName"/>.
/// <para>This supports inferrence enabled lookups by ensuring keys are sanitized when storing the values and when performing lookups.</para>
/// </summary>
/// <typeparam name="TValue"></typeparam>
internal readonly struct ReadOnlyFieldDictionary<TValue> : IReadOnlyDictionary<Field, TValue>
{
	private readonly Dictionary<Field, TValue> _backingDictionary;
	private readonly IElasticsearchClientSettings? _settings;

	public ReadOnlyFieldDictionary()
	{
		_backingDictionary = new Dictionary<Field, TValue>(0);
		_settings = null;
	}

	internal ReadOnlyFieldDictionary(Dictionary<Field, TValue> source, IElasticsearchClientSettings settings)
	{
		_settings = settings;

		// This is an "optimised version" which doesn't cause a second dictionary to be allocated.
		// Since we expect this to be used only for deserialisation, the keys received will already have been strings,
		// so no further sanitisation is required.

		if (source == null)
		{
			_backingDictionary = new Dictionary<Field, TValue>(0);
			return;
		}

		_backingDictionary = source;
	}

	private string Sanitize(Field key) => _settings is not null ? (key as IUrlParameter)?.GetString(_settings) : string.Empty;

	public TValue this[Field key] => _backingDictionary.TryGetValue(Sanitize(key), out var v) ? v : default;
	public TValue this[string key] => _backingDictionary.TryGetValue(key, out var v) ? v : default;

	public IEnumerable<Field> Keys => _backingDictionary.Keys;
	public IEnumerable<TValue> Values => _backingDictionary.Values;
	public int Count => _backingDictionary.Count;
	public bool ContainsKey(Field key) => _backingDictionary.ContainsKey(Sanitize(key));
	public IEnumerator<KeyValuePair<Field, TValue>> GetEnumerator() => _backingDictionary.GetEnumerator();
	public bool TryGetValue(Field key, out TValue value) => _backingDictionary.TryGetValue(Sanitize(key), out value);
	IEnumerator IEnumerable.GetEnumerator() => _backingDictionary.GetEnumerator();
}
