using System;
using Newtonsoft.Json;

namespace Nest.DSL.Aggregations
{
	public class FilterAggregationDescriptor<T> : BucketAggregationBaseDescriptor<FilterAggregationDescriptor<T>, T>
		where T : class
	{
		[JsonProperty("filter")]
		internal BaseFilter _Filter { get; set; }

		public FilterAggregationDescriptor<T> Filter(Func<FilterDescriptor<T>, BaseFilter> selector)
		{
			this._Filter = selector(new FilterDescriptor<T>());
			return this;
		}
	}
}