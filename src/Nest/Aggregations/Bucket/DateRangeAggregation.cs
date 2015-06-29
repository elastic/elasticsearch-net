using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<DateRangeAggregator>))]
	public interface IDateRangeAggregator : IBucketAggregator
	{
		[JsonProperty("field")]
		PropertyPathMarker Field { get; set; }

		[JsonProperty("format")]
		string Format { get; set; }

		[JsonProperty(PropertyName = "ranges")]
		IEnumerable<DateExpressionRange> Ranges { get; set; }
	}

	public class DateRangeAggregator : BucketAggregator, IDateRangeAggregator
	{
		public PropertyPathMarker Field { get; set; }
		public string Format { get; set; }
		public IEnumerable<DateExpressionRange> Ranges { get; set; }
	}

	public class DateRangeAggregationDescriptor<T> : BucketAggregationBaseDescriptor<DateRangeAggregationDescriptor<T>, T>, IDateRangeAggregator where T : class
	{
		private IDateRangeAggregator Self => this;

		[JsonProperty("field")]
		PropertyPathMarker IDateRangeAggregator.Field { get; set; }
		
		[JsonProperty("format")]
		string IDateRangeAggregator.Format { get; set; }

		[JsonProperty(PropertyName = "ranges")]
		IEnumerable<DateExpressionRange> IDateRangeAggregator.Ranges { get; set; }
		
		public DateRangeAggregationDescriptor<T> Field(string field)
		{
			Self.Field = field;
			return this;
		}

		public DateRangeAggregationDescriptor<T> Field(Expression<Func<T, object>> field)
		{
			Self.Field = field;
			return this;
		}

		public DateRangeAggregationDescriptor<T> Format(string format)
		{
			Self.Format = format;
			return this;
		}

		public DateRangeAggregationDescriptor<T> Ranges(params Func<DateExpressionRange, DateExpressionRange>[] ranges)
		{
			var newRanges = from range in ranges let r = new DateExpressionRange() select range(r);
			Self.Ranges = newRanges;
			return this;
		}
	}
}