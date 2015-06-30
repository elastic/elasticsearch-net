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

	public class DateRangeAggregatorDescriptor<T> 
		: BucketAggregatorBaseDescriptor<DateRangeAggregatorDescriptor<T>, IDateRangeAggregator, T>
			, IDateRangeAggregator 
		where T : class
	{
		PropertyPathMarker IDateRangeAggregator.Field { get; set; }
		
		string IDateRangeAggregator.Format { get; set; }

		IEnumerable<DateExpressionRange> IDateRangeAggregator.Ranges { get; set; }

		public DateRangeAggregatorDescriptor<T> Field(string field) => Assign(a => a.Field = field);

		public DateRangeAggregatorDescriptor<T> Field(Expression<Func<T, object>> field) => Assign(a => a.Field = field);

		public DateRangeAggregatorDescriptor<T> Format(string format) => Assign(a => a.Format = format);

		public DateRangeAggregatorDescriptor<T> Ranges(params Func<DateExpressionRange, DateExpressionRange>[] ranges) =>
			Assign(a=>a.Ranges = (from range in ranges let r = new DateExpressionRange() select range(r)).ToListOrNullIfEmpty());

	}
}