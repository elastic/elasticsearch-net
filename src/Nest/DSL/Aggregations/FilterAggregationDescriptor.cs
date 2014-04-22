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
		internal BaseFilterDescriptor FilterDescriptor { get; set; }

		public FilterAggregationDescriptor<T> Filter(Func<FilterDescriptorDescriptor<T>, BaseFilterDescriptor> selector)
		{
			this.FilterDescriptor = selector(new FilterDescriptorDescriptor<T>());
			return this;
		}


		object ICustomJson.GetCustomJson()
		{
			return FilterDescriptor;
		}
	}
}