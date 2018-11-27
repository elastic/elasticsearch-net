using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(DateRangeAggregation))]
	public interface IDateRangeAggregation : IBucketAggregation
	{
		[DataMember(Name ="field")]
		Field Field { get; set; }

		[DataMember(Name ="format")]
		string Format { get; set; }

		[DataMember(Name ="ranges")]
		IEnumerable<IDateRangeExpression> Ranges { get; set; }

		[DataMember(Name ="time_zone")]
		string TimeZone { get; set; }
	}

	public class DateRangeAggregation : BucketAggregationBase, IDateRangeAggregation
	{
		internal DateRangeAggregation() { }

		public DateRangeAggregation(string name) : base(name) { }

		public Field Field { get; set; }
		public string Format { get; set; }
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

		IEnumerable<IDateRangeExpression> IDateRangeAggregation.Ranges { get; set; }

		string IDateRangeAggregation.TimeZone { get; set; }

		public DateRangeAggregationDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		public DateRangeAggregationDescriptor<T> Field(Expression<Func<T, object>> field) => Assign(a => a.Field = field);

		public DateRangeAggregationDescriptor<T> Format(string format) => Assign(a => a.Format = format);

		public DateRangeAggregationDescriptor<T> Ranges(params IDateRangeExpression[] ranges) =>
			Assign(a => a.Ranges = ranges.ToListOrNullIfEmpty());

		public DateRangeAggregationDescriptor<T> TimeZone(string timeZone) => Assign(a => a.TimeZone = timeZone);

		public DateRangeAggregationDescriptor<T> Ranges(params Func<DateRangeExpressionDescriptor, IDateRangeExpression>[] ranges) =>
			Assign(a => a.Ranges = ranges?.Select(r => r(new DateRangeExpressionDescriptor())).ToListOrNullIfEmpty());

		public DateRangeAggregationDescriptor<T> Ranges(IEnumerable<Func<DateRangeExpressionDescriptor, IDateRangeExpression>> ranges) =>
			Assign(a => a.Ranges = ranges?.Select(r => r(new DateRangeExpressionDescriptor())).ToListOrNullIfEmpty());
	}
}
