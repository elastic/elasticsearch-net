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

		[JsonProperty(PropertyName = "ranges")]
		IEnumerable<IDateRangeExpression> Ranges { get; set; }
	}

	public class DateRangeAggregation : BucketAggregationBase, IDateRangeAggregation
	{
		public Field Field { get; set; }
		public string Format { get; set; }
		public IEnumerable<IDateRangeExpression> Ranges { get; set; }

		internal DateRangeAggregation() { }

		public DateRangeAggregation(string name) : base(name) { }

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

		public DateRangeAggregationDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		public DateRangeAggregationDescriptor<T> Field(Expression<Func<T, object>> field) => Assign(a => a.Field = field);

		public DateRangeAggregationDescriptor<T> Format(string format) => Assign(a => a.Format = format);

		public DateRangeAggregationDescriptor<T> Ranges(params IDateRangeExpression[] ranges) =>
			Assign(a=>a.Ranges = ranges.ToListOrNullIfEmpty());

		public DateRangeAggregationDescriptor<T> Ranges(params Func<DateRangeExpressionDescriptor, IDateRangeExpression>[] ranges) =>
			Assign(a=>a.Ranges = ranges?.Select(r=>r(new DateRangeExpressionDescriptor())).ToListOrNullIfEmpty());

		public DateRangeAggregationDescriptor<T> Ranges(IEnumerable<Func<DateRangeExpressionDescriptor, IDateRangeExpression>> ranges) =>
			Assign(a=>a.Ranges = ranges?.Select(r=>r(new DateRangeExpressionDescriptor())).ToListOrNullIfEmpty());
	}
}
