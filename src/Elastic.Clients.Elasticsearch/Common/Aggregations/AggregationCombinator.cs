using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elastic.Clients.Elasticsearch.Aggregations;

namespace Elastic.Clients.Elasticsearch.Common.Aggregations
{
	/// <summary>
	/// Combines aggregations into a single list of aggregations
	/// </summary>
	//internal class AggregationCombinator : AggregationBase, Aggregation
	//{
	//	public AggregationCombinator(string name, AggregationBase left, AggregationBase right) : base(name)
	//	{
	//		AddAggregation(left);
	//		AddAggregation(right);
	//	}

	//	internal List<AggregationBase> Aggregations { get; } = new List<AggregationBase>();

	//	internal override void WrapInContainer(AggregationContainer container) { }

	//	private void AddAggregation(AggregationBase agg)
	//	{
	//		switch (agg)
	//		{
	//			case null:
	//				return;
	//			case AggregationCombinator combinator when combinator.Aggregations.Any():
	//				Aggregations.AddRange(combinator.Aggregations);
	//				break;
	//			default:
	//				Aggregations.Add(agg);
	//				break;
	//		}
	//	}
	//}
}
