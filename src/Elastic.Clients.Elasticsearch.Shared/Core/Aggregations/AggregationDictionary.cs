// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;

#if ELASTICSEARCH_SERVERLESS
namespace Elastic.Clients.Elasticsearch.Serverless.Aggregations;
#else
namespace Elastic.Clients.Elasticsearch.Aggregations;
#endif

/// <summary>
/// Describes aggregations to execute as part of a search.
/// </summary>
public sealed class AggregationDictionary : IsADictionary<string, Aggregation>
{
	public AggregationDictionary() { }

	public AggregationDictionary(IDictionary<string, Aggregation> dictionary)
		: base(dictionary.ToDictionary(kv => kv.Key, kv => kv.Value)) { } // Copy the existing dictionary rather then using the existing reference

	public AggregationDictionary(Dictionary<string, Aggregation> dictionary)
		: base(dictionary.ToDictionary(kv => kv.Key, kv => kv.Value)) { } // Copy the existing dictionary rather then using the existing reference

	public static implicit operator AggregationDictionary(Dictionary<string, Aggregation> dictionary) =>
		new(dictionary);

	public static implicit operator AggregationDictionary(SearchAggregation aggregator)
	{
		SearchAggregation b;
		if (aggregator is AggregationCombinator combinator)
		{
			var dict = new AggregationDictionary();
			foreach (var agg in combinator.Aggregations)
			{
				b = agg;
				if (b.Name.IsNullOrEmpty())
					throw new ArgumentException($"{aggregator.GetType().Name}.Name is not set!");

				dict.Add(agg);
			}
			return dict;
		}

		b = aggregator;

		if (b.Name.IsNullOrEmpty())
			throw new ArgumentException($"{aggregator.GetType().Name}.Name is not set!");

		return new AggregationDictionary { { aggregator } };
	}

	public void Add(string key, Aggregation value) => BackingDictionary.Add(ValidateKey(key), value);

	public void Add(Aggregation value)
	{
		if (value.Variant.Name.IsNullOrEmpty())
			throw new ArgumentException($"{value.GetType().Name}.Name is not set!");

		BackingDictionary.Add(ValidateKey(value.Variant.Name), value);
	}
}
