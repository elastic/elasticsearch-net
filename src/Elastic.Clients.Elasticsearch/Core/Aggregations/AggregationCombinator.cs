// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Linq;

namespace Elastic.Clients.Elasticsearch.Aggregations;

/// <summary>
/// Combines aggregations into a single list of aggregations.
/// </summary>
internal class AggregationCombinator : Aggregation
{
	public AggregationCombinator(string name, Aggregation left, Aggregation right)
	{
		AddAggregation(left);
		AddAggregation(right);
		Name = name;
	}

	public override string? Name { get; internal set; }

	internal List<Aggregation> Aggregations { get; } = new List<Aggregation>();

	private void AddAggregation(Aggregation agg)
	{
		switch (agg)
		{
			case null:
				return;
			case AggregationCombinator combinator when combinator.Aggregations.Any():
				Aggregations.AddRange(combinator.Aggregations);
				break;
			default:
				Aggregations.Add(agg);
				break;
		}
	}
}
