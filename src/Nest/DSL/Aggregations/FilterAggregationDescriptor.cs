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
		internal BaseFilter _Filter { get; set; }

		public FilterAggregationDescriptor<T> Filter(Func<FilterDescriptor<T>, BaseFilter> selector)
		{
			this._Filter = selector(new FilterDescriptor<T>());
			return this;
		}


		object ICustomJson.GetCustomJson()
		{
			return _Filter;
		}
	}
}