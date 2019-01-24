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
		[JsonProperty("extended_bounds")]
		ExtendedBounds<DateMath> ExtendedBounds { get; set; }

		[JsonProperty("field")]
		Field Field { get; set; }

		[JsonProperty("format")]
		string Format { get; set; }

		[JsonProperty("interval")]
		Union<DateInterval, Time> Interval { get; set; }

		[JsonProperty("min_doc_count")]
		int? MinimumDocumentCount { get; set; }

		[JsonProperty("missing")]
		DateTime? Missing { get; set; }

		[JsonProperty("offset")]
		string Offset { get; set; }

		[JsonProperty("order")]
		HistogramOrder Order { get; set; }

		[JsonProperty("params")]
		IDictionary<string, object> Params { get; set; }

		[JsonProperty("script")]
		IScript Script { get; set; }

		[JsonProperty("time_zone")]
		string TimeZone { get; set; }
	}

	public class DateHistogramAggregation : BucketAggregationBase, IDateHistogramAggregation
	{
		private string _format;

		internal DateHistogramAggregation() { }

		public DateHistogramAggregation(string name) : base(name) { }

		public ExtendedBounds<DateMath> ExtendedBounds { get; set; }
		public Field Field { get; set; }

		public string Format
		{
			get => !string.IsNullOrEmpty(_format) &&
				!_format.Contains("date_optional_time") &&
				(ExtendedBounds != null || Missing.HasValue)
					? _format + "||date_optional_time"
					: _format;
			set => _format = value;
		}

		public Union<DateInterval, Time> Interval { get; set; }

		public int? MinimumDocumentCount { get; set; }
		public DateTime? Missing { get; set; }
		public string Offset { get; set; }
		public HistogramOrder Order { get; set; }
		public IDictionary<string, object> Params { get; set; }
		public IScript Script { get; set; }
		public string TimeZone { get; set; }

		internal override void WrapInContainer(AggregationContainer c) => c.DateHistogram = this;
	}

	public class DateHistogramAggregationDescriptor<T>
		: BucketAggregationDescriptorBase<DateHistogramAggregationDescriptor<T>, IDateHistogramAggregation, T>
			, IDateHistogramAggregation
		where T : class
	{
		private string _format;

		ExtendedBounds<DateMath> IDateHistogramAggregation.ExtendedBounds { get; set; }
		Field IDateHistogramAggregation.Field { get; set; }

		//see: https://github.com/elastic/elasticsearch/issues/9725
		string IDateHistogramAggregation.Format
		{
			get => !string.IsNullOrEmpty(_format) &&
				!_format.Contains("date_optional_time") &&
				(Self.ExtendedBounds != null || Self.Missing.HasValue)
					? _format + "||date_optional_time"
					: _format;
			set => _format = value;
		}

		Union<DateInterval, Time> IDateHistogramAggregation.Interval { get; set; }

		int? IDateHistogramAggregation.MinimumDocumentCount { get; set; }

		DateTime? IDateHistogramAggregation.Missing { get; set; }

		string IDateHistogramAggregation.Offset { get; set; }

		HistogramOrder IDateHistogramAggregation.Order { get; set; }

		IDictionary<string, object> IDateHistogramAggregation.Params { get; set; }

		IScript IDateHistogramAggregation.Script { get; set; }

		string IDateHistogramAggregation.TimeZone { get; set; }

		public DateHistogramAggregationDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		public DateHistogramAggregationDescriptor<T> Field(Expression<Func<T, object>> field) => Assign(a => a.Field = field);

		public DateHistogramAggregationDescriptor<T> Script(string script) => Assign(a => a.Script = (InlineScript)script);

		public DateHistogramAggregationDescriptor<T> Script(Func<ScriptDescriptor, IScript> scriptSelector) =>
			Assign(a => a.Script = scriptSelector?.Invoke(new ScriptDescriptor()));

		public DateHistogramAggregationDescriptor<T> Interval(Time interval) => Assign(a => a.Interval = interval);

		public DateHistogramAggregationDescriptor<T> Interval(DateInterval interval) =>
			Assign(a => a.Interval = interval);

		public DateHistogramAggregationDescriptor<T> Format(string format) => Assign(a => a.Format = format);

		public DateHistogramAggregationDescriptor<T> MinimumDocumentCount(int? minimumDocumentCount) =>
			Assign(a => a.MinimumDocumentCount = minimumDocumentCount);

		public DateHistogramAggregationDescriptor<T> TimeZone(string timeZone) => Assign(a => a.TimeZone = timeZone);

		public DateHistogramAggregationDescriptor<T> Offset(string offset) => Assign(a => a.Offset = offset);

		public DateHistogramAggregationDescriptor<T> Order(HistogramOrder order) => Assign(a => a.Order = order);

		public DateHistogramAggregationDescriptor<T> OrderAscending(string key) =>
			Assign(a => a.Order = new HistogramOrder { Key = key, Order = SortOrder.Descending });

		public DateHistogramAggregationDescriptor<T> OrderDescending(string key) =>
			Assign(a => a.Order = new HistogramOrder { Key = key, Order = SortOrder.Descending });

		public DateHistogramAggregationDescriptor<T> ExtendedBounds(DateMath min, DateMath max) =>
			Assign(a => a.ExtendedBounds = new ExtendedBounds<DateMath> { Minimum = min, Maximum = max });

		public DateHistogramAggregationDescriptor<T> Missing(DateTime? missing) => Assign(a => a.Missing = missing);
	}
}
