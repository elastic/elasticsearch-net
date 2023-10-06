// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using Elastic.Transport.Products.Elasticsearch;

#if ELASTICSEARCH_SERVERLESS
namespace Elastic.Clients.Elasticsearch.Serverless;
#else
namespace Elastic.Clients.Elasticsearch;
#endif

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
}
