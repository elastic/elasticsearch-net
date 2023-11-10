// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Linq;

#if ELASTICSEARCH_SERVERLESS
namespace Elastic.Clients.Elasticsearch.Serverless.Aggregations;
#else
namespace Elastic.Clients.Elasticsearch.Aggregations;
#endif

/// <summary>
/// Combines aggregations into a single list of aggregations.
/// </summary>
internal class AggregationCombinator : SearchAggregation
{
	public AggregationCombinator(string name, SearchAggregation left, SearchAggregation right)
	{
		AddAggregation(left);
		AddAggregation(right);
		Name = name;
	}

	public override string? Name { get; internal set; }

	internal List<SearchAggregation> Aggregations { get; } = new List<SearchAggregation>();

	private void AddAggregation(SearchAggregation agg)
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
