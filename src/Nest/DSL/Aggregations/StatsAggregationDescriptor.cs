using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using Nest.Resolvers;

namespace Nest.DSL.Aggregations
{
	public class StatsAggregationDescriptor<T> : MetricAggregationBaseDescriptor<StatsAggregationDescriptor<T>, T>
		where T : class
	{
		
	}
	
	public class ExtendedStatsAggregationDescriptor<T> : MetricAggregationBaseDescriptor<ExtendedStatsAggregationDescriptor<T>, T>
		where T : class
	{
		
	}
	
	public class ValueCountAggregationDescriptor<T> : MetricAggregationBaseDescriptor<ValueCountAggregationDescriptor<T>, T>
		where T : class
	{
		
	}
}
