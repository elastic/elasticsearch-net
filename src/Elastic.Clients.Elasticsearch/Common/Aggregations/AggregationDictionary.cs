// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Elastic.Clients.Elasticsearch.Aggregations;

public class AggregationDictionary : IsADictionaryBase<string, AggregationContainer>
{
	public AggregationDictionary() { }

	public AggregationDictionary(IDictionary<string, AggregationContainer> container) : base(container) { }

	public AggregationDictionary(Dictionary<string, AggregationContainer> container)
		: base(container.ToDictionary(kv => kv.Key, kv => kv.Value)) { }

	public static implicit operator AggregationDictionary(Dictionary<string, AggregationContainer> container) =>
		new(container);

	public static implicit operator AggregationDictionary(AggregationBase aggregator)
	{
		AggregationBase b;
		if (aggregator is AggregationCombinator combinator)
		{
			var dict = new AggregationDictionary();
			foreach (var agg in combinator.Aggregations)
			{
				b = agg;
				if (b.Name.IsNullOrEmpty())
					throw new ArgumentException($"{aggregator.GetType().Name}.Name is not set!");

				dict.Add(b.Name, agg);
			}
			return dict;
		}

		b = aggregator;

		if (b.Name.IsNullOrEmpty())
			throw new ArgumentException($"{aggregator.GetType().Name}.Name is not set!");

		return new AggregationDictionary { { b.Name, aggregator } };
	}

	public void Add(string key, AggregationContainer value) => BackingDictionary.Add(ValidateKey(key), value);

	protected override string ValidateKey(string key) => key; // TODO!
															  //{
															  //if (AggregateFormatter.AllReservedAggregationNames.Contains(key))
															  //	throw new ArgumentException(
															  //		string.Format(AggregateFormatter.UsingReservedAggNameFormat, key), nameof(key));
															  //return key;
															  //}
}
