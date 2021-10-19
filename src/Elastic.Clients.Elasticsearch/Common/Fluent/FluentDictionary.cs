// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;

namespace Elastic.Clients.Elasticsearch;

/// <summary>
/// Used in the "fluent" syntax to support chained configuration of dictionary entries.
/// </summary>
/// <typeparam name="TKey">The type for the keys of the <see cref="Dictionary{TKey, TValue}"/>.</typeparam>
/// <typeparam name="TValue">The type for the values of the <see cref="Dictionary{TKey, TValue}"/>.</typeparam>
public class FluentDictionary<TKey, TValue> : Dictionary<TKey, TValue>
{
	public FluentDictionary() { }

	/// <summary>
	/// Creates a new <see cref="FluentDictionary{TKey, TValue}"/> by copying the entries from the <paramref name="source"/>.
	/// </summary>
	/// <param name="source">The source <see cref="IDictionary{TKey, TValue}"/> from which entries will be copied.</param>
	public FluentDictionary(IDictionary<TKey, TValue> source)
	{
		if (source == null)
			return;

		foreach (var kv in source)
			this[kv.Key] = kv.Value;
	}

	/// <inheritdoc cref="IDictionary{TKey, TValue}.Add(TKey, TValue)" />
	public new FluentDictionary<TKey, TValue> Add(TKey key, TValue value)
	{
		base.Add(key, value);
		return this;
	}

	/// <inheritdoc cref="IDictionary{TKey, TValue}.Remove(TKey)" />
	public new FluentDictionary<TKey, TValue> Remove(TKey key)
	{
		base.Remove(key);
		return this;
	}
}
