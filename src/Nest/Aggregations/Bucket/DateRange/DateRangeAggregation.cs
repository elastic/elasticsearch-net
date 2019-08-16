using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(AggregationJsonConverter<DateRangeAggregation>))]
	public interface IDateRangeAggregation : IBucketAggregation
	{
		[JsonProperty("field")]
		Field Field { get; set; }

		[JsonProperty("format")]
		string Format { get; set; }

		[JsonProperty("missing")]
		object Missing { get; set; }

		[JsonProperty("ranges")]
		IEnumerable<IDateRangeExpression> Ranges { get; set; }

		[JsonProperty("time_zone")]
		string TimeZone { get; set; }
	}

	public class DateRangeAggregation : BucketAggregationBase, IDateRangeAggregation
	{
		internal DateRangeAggregation() { }

		public DateRangeAggregation(string name) : base(name) { }

		public Field Field { get; set; }
		public string Format { get; set; }
		public object Missing { get; set; }
		public IEnumerable<IDateRangeExpression> Ranges { get; set; }
		public string TimeZone { get; set; }

		internal override void WrapInContainer(AggregationContainer c) => c.DateRange = this;
	}

	public class DateRangeAggregationDescriptor<T>
		: BucketAggregationDescriptorBase<DateRangeAggregationDescriptor<T>, IDateRangeAggregation, T>
			, IDateRangeAggregation
		where T : class
	{
		Field IDateRangeAggregation.Field { get; set; }

		string IDateRangeAggregation.Format { get; set; }

		object IDateRangeAggregation.Missing { get; set; }

		IEnumerable<IDateRangeExpression> IDateRangeAggregation.Ranges { get; set; }

		string IDateRangeAggregation.TimeZone { get; set; }

		public DateRangeAggregationDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		public DateRangeAggregationDescriptor<T> Field(Expression<Func<T, object>> field) => Assign(field, (a, v) => a.Field = v);

		public DateRangeAggregationDescriptor<T> Format(string format) => Assign(format, (a, v) => a.Format = v);

		public DateRangeAggregationDescriptor<T> Missing(object missing) => Assign(missing, (a, v) => a.Missing = v);

		public DateRangeAggregationDescriptor<T> Ranges(params IDateRangeExpression[] ranges) =>
			Assign(ranges.ToListOrNullIfEmpty(), (a, v) => a.Ranges = v);

		public DateRangeAggregationDescriptor<T> TimeZone(string timeZone) => Assign(timeZone, (a, v) => a.TimeZone = v);

		public DateRangeAggregationDescriptor<T> Ranges(params Func<DateRangeExpressionDescriptor, IDateRangeExpression>[] ranges) =>
			Assign(ranges?.Select(r => r(new DateRangeExpressionDescriptor())).ToListOrNullIfEmpty(), (a, v) => a.Ranges = v);

		public DateRangeAggregationDescriptor<T> Ranges(IEnumerable<Func<DateRangeExpressionDescriptor, IDateRangeExpression>> ranges) =>
			Assign(ranges?.Select(r => r(new DateRangeExpressionDescriptor())).ToListOrNullIfEmpty(), (a, v) => a.Ranges = v);
	}
}
