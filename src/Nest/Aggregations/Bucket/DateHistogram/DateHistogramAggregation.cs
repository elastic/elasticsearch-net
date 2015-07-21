using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<DateHistogramAggregator>))]
	public interface IDateHistogramAggregator : IBucketAggregator
	{
		[JsonProperty("field")]
		FieldName Field { get; set; }

		[JsonProperty("script")]
		string Script { get; set; }

		[JsonProperty("params")]
		IDictionary<string, object> Params { get; set; }

		[JsonProperty("interval")]
		Union<DateInterval, TimeUnitExpression> Interval { get; set; }

		[JsonProperty("format")]
		string Format { get; set; }

		[JsonProperty("min_doc_count")]
		int? MinimumDocumentCount { get; set; }

		[JsonProperty("time_zone")]
		string TimeZone { get; set; }

		[JsonProperty("factor")]
		int? Factor { get; set; }

		[JsonProperty("offset")]
		string Offset { get; set; }

		[JsonProperty("order")]
		HistogramOrder Order { get; set; }

		[JsonProperty("extended_bounds")]
		ExtendedBounds<DateTime> ExtendedBounds { get; set; }
	}

	public class DateHistogramAggregator : BucketAggregator, IDateHistogramAggregator
	{
		public FieldName Field { get; set; }
		public string Script { get; set; }
		public IDictionary<string, object> Params { get; set; }
		public Union<DateInterval, TimeUnitExpression>  Interval { get; set; }
		public string Format { get; set; }
		public int? MinimumDocumentCount { get; set; }
		public string TimeZone { get; set; }
		public int? Factor { get; set; }
		public string Offset { get; set; }
		public HistogramOrder Order { get; set; }
		public ExtendedBounds<DateTime> ExtendedBounds { get; set; }
	}

	public class DateHistogramAgg : BucketAgg, IDateHistogramAggregator
	{
		public FieldName Field { get; set; }
		public string Script { get; set; }
		public IDictionary<string, object> Params { get; set; }
		public Union<DateInterval, TimeUnitExpression> Interval { get; set; }
		public string Format { get; set; }
		public int? MinimumDocumentCount { get; set; }
		public string TimeZone { get; set; }
		public int? Factor { get; set; }
		public string Offset { get; set; }
		public HistogramOrder Order { get; set; }
		public ExtendedBounds<DateTime> ExtendedBounds { get; set; }

		public DateHistogramAgg(string name) : base(name) { }

		internal override void WrapInContainer(AggregationContainer c) => c.DateHistogram = this;
	}

	public class DateHistogramAggregatorDescriptor<T>
		: BucketAggregatorBaseDescriptor<DateHistogramAggregatorDescriptor<T>, IDateHistogramAggregator, T>
			, IDateHistogramAggregator
		where T : class
	{
		FieldName IDateHistogramAggregator.Field { get; set; }

		string IDateHistogramAggregator.Script { get; set; }

		IDictionary<string, object> IDateHistogramAggregator.Params { get; set; }

		Union<DateInterval, TimeUnitExpression> IDateHistogramAggregator.Interval { get; set; }

		string IDateHistogramAggregator.Format { get; set; }

		int? IDateHistogramAggregator.MinimumDocumentCount { get; set; }

		string IDateHistogramAggregator.TimeZone { get; set; }

		int? IDateHistogramAggregator.Factor { get; set; }

		string IDateHistogramAggregator.Offset { get; set; }

		HistogramOrder IDateHistogramAggregator.Order { get; set; }

		ExtendedBounds<DateTime> IDateHistogramAggregator.ExtendedBounds { get; set; }

		public DateHistogramAggregatorDescriptor<T> Field(string field) => Assign(a => a.Field = field);

		public DateHistogramAggregatorDescriptor<T> Field(Expression<Func<T, object>> field) => Assign(a => a.Field = field);

		public DateHistogramAggregatorDescriptor<T> Script(string script) => Assign(a => a.Script = script);

		public DateHistogramAggregatorDescriptor<T> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramSelector) =>
			Assign(a => a.Params = paramSelector?.Invoke(new FluentDictionary<string, object>()).NullIfNoKeys());

		public DateHistogramAggregatorDescriptor<T> Interval(TimeUnitExpression interval) => Assign(a => a.Interval = interval);

		public DateHistogramAggregatorDescriptor<T> Interval(DateInterval interval) =>
			Assign(a => a.Interval = interval);

		public DateHistogramAggregatorDescriptor<T> Format(string format) => Assign(a => a.Format = format);

		public DateHistogramAggregatorDescriptor<T> MinimumDocumentCount(int minimumDocumentCount) =>
			Assign(a => a.MinimumDocumentCount = minimumDocumentCount);

		public DateHistogramAggregatorDescriptor<T> TimeZone(string timeZone) => Assign(a => a.TimeZone = timeZone);

		public DateHistogramAggregatorDescriptor<T> Interval(int factor) => Assign(a => a.Factor = factor);

		public DateHistogramAggregatorDescriptor<T> Offset(string offset) => Assign(a => a.Offset = offset);

		public DateHistogramAggregatorDescriptor<T> Order(HistogramOrder order) => Assign(a => a.Order = order);

		public DateHistogramAggregatorDescriptor<T> OrderAscending(string key) =>
			Assign(a => a.Order = new HistogramOrder { Key = key, Order = SortOrder.Descending });

		public DateHistogramAggregatorDescriptor<T> OrderDescending(string key) =>
			Assign(a => a.Order = new HistogramOrder { Key = key, Order = SortOrder.Descending });

		public DateHistogramAggregatorDescriptor<T> ExtendedBounds(DateTime min, DateTime max) =>
			Assign(a=>a.ExtendedBounds = new ExtendedBounds<DateTime> { Minimum = min, Maximum = max });

	}
}