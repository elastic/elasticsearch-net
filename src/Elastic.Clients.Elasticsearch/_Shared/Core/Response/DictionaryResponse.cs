// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;

using Elastic.Transport.Products.Elasticsearch;

namespace Elastic.Clients.Elasticsearch;

public abstract class DictionaryResponse<TKey, TValue> : ElasticsearchResponse
{
	internal DictionaryResponse(IReadOnlyDictionary<TKey, TValue> dictionary)
	{
		if (dictionary is null)
			throw new ArgumentNullException(nameof(dictionary));

		BackingDictionary = dictionary;
	}

	internal DictionaryResponse() => BackingDictionary = EmptyReadOnly<TKey, TValue>.Dictionary;

	protected IReadOnlyDictionary<TKey, TValue> BackingDictionary { get; }

	// TODO: Remove after exposing the values in the derived responses
	[Obsolete("Temporary workaround. This field will be removed in the future and replaced by custom accessors in the derived classes.")]
	public IReadOnlyDictionary<TKey, TValue> Values => BackingDictionary;
}
