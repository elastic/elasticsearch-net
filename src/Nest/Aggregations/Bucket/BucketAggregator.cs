using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{

	public interface IBucketAggregator
	{
		IDictionary<string, IAggregationContainer> Aggregations { get; set; }
	}

	public abstract class BucketAggregator : IBucketAggregator
	{
		IDictionary<string, IAggregationContainer> IBucketAggregator.Aggregations { get; set; }
	}

}