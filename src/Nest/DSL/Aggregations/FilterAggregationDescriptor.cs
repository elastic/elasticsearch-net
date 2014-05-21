using System;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(CustomJsonConverter))]
	public class FilterAggregationDescriptor<T> 
		: BucketAggregationBaseDescriptor<FilterAggregationDescriptor<T>, T>
		, ICustomJson
		where T : class
	{
		internal FilterContainer FilterDescriptor { get; set; }

		public FilterAggregationDescriptor<T> Filter(Func<FilterDescriptor<T>, FilterContainer> selector)
		{
			this.FilterDescriptor = selector(new FilterDescriptor<T>());
			return this;
		}


		object ICustomJson.GetCustomJson()
		{
			return FilterDescriptor;
		}
	}
}