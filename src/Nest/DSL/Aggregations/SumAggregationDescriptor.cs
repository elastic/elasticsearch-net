using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Nest.Resolvers;
using Newtonsoft.Json;

namespace Nest.DSL.Aggregations
{
	public class SumAggregationDescriptor<T> : MetricAggregationBaseDescriptor<SumAggregationDescriptor<T>, T>
		where T : class
	{
		
	}
}
