using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<DateRangeAggregator>))]
	public interface IDateRangeAggregator : IBucketAggregator
	{
		[JsonProperty("field")]
		Field Field { get; set; }

		[JsonProperty("format")]
		string Format { get; set; }

		[JsonProperty(PropertyName = "ranges")]
		IEnumerable<IDateRangeExpression> Ranges { get; set; }
	}

	public class DateRangeAggregator : BucketAggregator, IDateRangeAggregator
	{
		public Field Field { get; set; }
		public string Format { get; set; }
		public IEnumerable<IDateRangeExpression> Ranges { get; set; }
	}

	public class DateRangeAgg : BucketAgg, IDateRangeAggregator
	{
		public Field Field { get; set; }
		public string Format { get; set; }
		public IEnumerable<IDateRangeExpression> Ranges { get; set; }

		public DateRangeAgg(string name) : base(name) { }

		internal override void WrapInContainer(AggregationContainer c) => c.DateRange = this;
	}

	public class DateRangeAggregatorDescriptor<T> 
		: BucketAggregatorBaseDescriptor<DateRangeAggregatorDescriptor<T>, IDateRangeAggregator, T>
			, IDateRangeAggregator 
		where T : class
	{
		Field IDateRangeAggregator.Field { get; set; }
		
		string IDateRangeAggregator.Format { get; set; }

		IEnumerable<IDateRangeExpression> IDateRangeAggregator.Ranges { get; set; }

		public DateRangeAggregatorDescriptor<T> Field(string field) => Assign(a => a.Field = field);

		public DateRangeAggregatorDescriptor<T> Field(Expression<Func<T, object>> field) => Assign(a => a.Field = field);

		public DateRangeAggregatorDescriptor<T> Format(string format) => Assign(a => a.Format = format);

		public DateRangeAggregatorDescriptor<T> Ranges(params IDateRangeExpression[] ranges) =>
			Assign(a=>a.Ranges = ranges.ToListOrNullIfEmpty());

		public DateRangeAggregatorDescriptor<T> Ranges(params Func<DateRangeExpressionDescriptor, IDateRangeExpression>[] ranges) =>
			Assign(a=>a.Ranges = ranges?.Select(r=>r(new DateRangeExpressionDescriptor())).ToListOrNullIfEmpty());

		public DateRangeAggregatorDescriptor<T> Ranges(IEnumerable<Func<DateRangeExpressionDescriptor, IDateRangeExpression>> ranges) =>
			Assign(a=>a.Ranges = ranges?.Select(r=>r(new DateRangeExpressionDescriptor())).ToListOrNullIfEmpty());

	}
}