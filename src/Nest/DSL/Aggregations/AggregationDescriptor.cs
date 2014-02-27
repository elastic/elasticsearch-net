using System;
using System.Collections.Generic;

namespace Nest.DSL.Aggregations
{
	public class AggregationDescriptor<T>
		where T : class
	{
		private readonly IDictionary<string, IAggregationDescriptor> _aggregations =
			new Dictionary<string, IAggregationDescriptor>();

		public AggregationDescriptor<T> Average(
			Func<AverageAggregationDescriptor<T>, AverageAggregationDescriptor<T>> selector)
		{
			var agg = selector(new AverageAggregationDescriptor<T>());
			if (agg == null) return this;
			this._aggregations.Add("avg", agg);
			return this;
		}
	}
}