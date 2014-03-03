using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Nest.Resolvers;
using Newtonsoft.Json;

namespace Nest
{
	public class IpRangeAggregationDescriptor<T> : BucketAggregationBaseDescriptor<IpRangeAggregationDescriptor<T>, T>
		where T : class
	{

		[JsonProperty("field")]
		internal PropertyPathMarker _Field { get; set; }

		public IpRangeAggregationDescriptor<T> Field(string field)
		{
			this._Field = field;
			return this;
		}

		public IpRangeAggregationDescriptor<T> Field(Expression<Func<T, object>> field)
		{
			this._Field = field;
			return this;
		}

		[JsonProperty(PropertyName = "ranges")]
		internal IEnumerable<Ip4Range> _Ranges { get; set; }

		public IpRangeAggregationDescriptor<T> Ranges(params string[] ranges)
		{
			var newRanges = from range in ranges let r = new Ip4Range().Mask(range) select r;
			this._Ranges = newRanges;
			return this;
		}
	}
}