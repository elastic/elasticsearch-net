using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Nest.Resolvers;
using Newtonsoft.Json;

namespace Nest.DSL.Aggregations
{
	public class DateRangeAggregationDescriptor<T> : BucketAggregationBaseDescriptor<DateRangeAggregationDescriptor<T>, T>
	{
		[JsonProperty("field")]
		internal PropertyPathMarker _Field { get; set; }
		
		public DateRangeAggregationDescriptor<T> Field(string field)
		{
			this._Field = field;
			return this;
		}

		public DateRangeAggregationDescriptor<T> Field(Expression<Func<T, object>> field)
		{
			this._Field = field;
			return this;
		}

		[JsonProperty("format")]
		internal string _Format { get; set; }

		public DateRangeAggregationDescriptor<T> Format(string format)
		{
			this._Format = format;
			return this;
		}

		[JsonProperty(PropertyName = "ranges")]
		internal IEnumerable<DateExpressionRange> _Ranges { get; set; }
		
		public DateRangeAggregationDescriptor<T> Ranges(params Func<DateExpressionRange, DateExpressionRange>[] ranges)
		{
			var newRanges = from range in ranges let r = new DateExpressionRange() select range(r);
			this._Ranges = newRanges;
			return this;
		}
	}
}