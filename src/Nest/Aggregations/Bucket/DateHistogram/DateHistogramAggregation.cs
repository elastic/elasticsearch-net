using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(AggregationJsonConverter<DateHistogramAggregation>))]
	public interface IDateHistogramAggregation : IBucketAggregation
	{
		[JsonProperty("field")]
		Field Field { get; set; }

		[JsonProperty("script")]
		IScript Script { get; set; }

		[JsonProperty("params")]
		IDictionary<string, object> Params { get; set; }

		[JsonProperty("interval")]
		Union<DateInterval, Time> Interval { get; set; }

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

		[JsonProperty("missing")]
		DateTime? Missing { get; set; }
    }

	public class DateHistogramAggregation : BucketAggregationBase, IDateHistogramAggregation
	{
		private string _format;
		public Field Field { get; set; }
		public IScript Script { get; set; }
		public IDictionary<string, object> Params { get; set; }
		public Union<DateInterval, Time> Interval { get; set; }

		public string Format
		{
			get
			{
				return !string.IsNullOrEmpty(_format) &&
					!_format.Contains("date_optional_time") &&
					ExtendedBounds != null
					? _format + "||date_optional_time"
					: _format;
			}
			set { _format = value; }
		}

		public int? MinimumDocumentCount { get; set; }
		public string TimeZone { get; set; }
		public int? Factor { get; set; }
		public string Offset { get; set; }
		public HistogramOrder Order { get; set; }
		public ExtendedBounds<DateTime> ExtendedBounds { get; set; }
		public DateTime? Missing { get; set; }

		internal DateHistogramAggregation() { }

		public DateHistogramAggregation(string name) : base(name) { }

		internal override void WrapInContainer(AggregationContainer c) => c.DateHistogram = this;
	}

	public class DateHistogramAggregationDescriptor<T>
		: BucketAggregationDescriptorBase<DateHistogramAggregationDescriptor<T>, IDateHistogramAggregation, T>
			, IDateHistogramAggregation
		where T : class
	{
		private string _format;
		Field IDateHistogramAggregation.Field { get; set; }

		IScript IDateHistogramAggregation.Script { get; set; }

		IDictionary<string, object> IDateHistogramAggregation.Params { get; set; }

		Union<DateInterval, Time> IDateHistogramAggregation.Interval { get; set; }

		string IDateHistogramAggregation.Format
		{
			get
			{
				return !string.IsNullOrEmpty(_format) &&
					!_format.Contains("date_optional_time") &&
					Self.ExtendedBounds != null
					? _format + "||date_optional_time"
					: _format;
			}
			set { _format = value; }
		}

		int? IDateHistogramAggregation.MinimumDocumentCount { get; set; }

		string IDateHistogramAggregation.TimeZone { get; set; }

		int? IDateHistogramAggregation.Factor { get; set; }

		string IDateHistogramAggregation.Offset { get; set; }

		HistogramOrder IDateHistogramAggregation.Order { get; set; }

		ExtendedBounds<DateTime> IDateHistogramAggregation.ExtendedBounds { get; set; }

		DateTime? IDateHistogramAggregation.Missing { get; set; }

		public DateHistogramAggregationDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		public DateHistogramAggregationDescriptor<T> Field(Expression<Func<T, object>> field) => Assign(a => a.Field = field);

		public DateHistogramAggregationDescriptor<T> Script(string script) => Assign(a => a.Script = (InlineScript)script);

		public DateHistogramAggregationDescriptor<T> Script(Func<ScriptDescriptor, IScript> scriptSelector) =>
			Assign(a => a.Script = scriptSelector?.Invoke(new ScriptDescriptor()));

		public DateHistogramAggregationDescriptor<T> Interval(Time interval) => Assign(a => a.Interval = interval);

		public DateHistogramAggregationDescriptor<T> Interval(DateInterval interval) =>
			Assign(a => a.Interval = interval);

		public DateHistogramAggregationDescriptor<T> Format(string format) => Assign(a => a.Format = format);

		public DateHistogramAggregationDescriptor<T> MinimumDocumentCount(int minimumDocumentCount) =>
			Assign(a => a.MinimumDocumentCount = minimumDocumentCount);

		public DateHistogramAggregationDescriptor<T> TimeZone(string timeZone) => Assign(a => a.TimeZone = timeZone);

		public DateHistogramAggregationDescriptor<T> Interval(int factor) => Assign(a => a.Factor = factor);

		public DateHistogramAggregationDescriptor<T> Offset(string offset) => Assign(a => a.Offset = offset);

		public DateHistogramAggregationDescriptor<T> Order(HistogramOrder order) => Assign(a => a.Order = order);

		public DateHistogramAggregationDescriptor<T> OrderAscending(string key) =>
			Assign(a => a.Order = new HistogramOrder { Key = key, Order = SortOrder.Descending });

		public DateHistogramAggregationDescriptor<T> OrderDescending(string key) =>
			Assign(a => a.Order = new HistogramOrder { Key = key, Order = SortOrder.Descending });

		public DateHistogramAggregationDescriptor<T> ExtendedBounds(DateTime min, DateTime max) =>
			Assign(a=>a.ExtendedBounds = new ExtendedBounds<DateTime> { Minimum = min, Maximum = max });

		public DateHistogramAggregationDescriptor<T> Missing(DateTime missing) => Assign(a => a.Missing = missing);
	}
}
