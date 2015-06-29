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
		PropertyPathMarker Field { get; set; }

		[JsonProperty("script")]
		string Script { get; set; }

		[JsonProperty("params")]
		IDictionary<string, object> Params { get; set; }

		[JsonProperty("interval")]
		string Interval { get; set; }

		[JsonProperty("format")]
		string Format { get; set; }

		[JsonProperty("min_doc_count")]
		int? MinimumDocumentCount { get; set; }

		[JsonProperty("pre_zone")]
		string PreZone { get; set; }

		[JsonProperty("post_zone")]
		string PostZone { get; set; }

		[JsonProperty("time_zone")]
		string TimeZone { get; set; }

		[JsonProperty("pre_zone_adjust_large_interval")]
		bool? PreZoneAdjustLargeInterval { get; set; }

		[JsonProperty("factor")]
		int? Factor { get; set; }

		[JsonProperty("pre_offset")]
		string PreOffset { get; set; }

		[JsonProperty("post_offset")]
		string PostOffset { get; set; }

		[JsonProperty("order")]
		IDictionary<string, string> Order { get; set; }

		[JsonProperty("extended_bounds")]
		IDictionary<string, object> ExtendedBounds { get; set; }
	}

	public class DateHistogramAggregator : BucketAggregator, IDateHistogramAggregator
	{
		public PropertyPathMarker Field { get; set; }
		public string Script { get; set; }
		public IDictionary<string, object> Params { get; set; }
		public string Interval { get; set; }
		public string Format { get; set; }
		public int? MinimumDocumentCount { get; set; }
		public string PreZone { get; set; }
		public string PostZone { get; set; }
		public string TimeZone { get; set; }
		public bool? PreZoneAdjustLargeInterval { get; set; }
		public int? Factor { get; set; }
		public string PreOffset { get; set; }
		public string PostOffset { get; set; }
		public IDictionary<string, string> Order { get; set; }
		public IDictionary<string, object> ExtendedBounds { get; set; }
	}

	public class DateHistogramAggregationDescriptor<T>
		: BucketAggregationBaseDescriptor<DateHistogramAggregationDescriptor<T>, IDateHistogramAggregator, T>
			, IDateHistogramAggregator
		where T : class
	{
		PropertyPathMarker IDateHistogramAggregator.Field { get; set; }

		string IDateHistogramAggregator.Script { get; set; }

		IDictionary<string, object> IDateHistogramAggregator.Params { get; set; }

		string IDateHistogramAggregator.Interval { get; set; }

		//TODO is this default necessary?
		string IDateHistogramAggregator.Format { get; set; } = "date_optional_time";

		int? IDateHistogramAggregator.MinimumDocumentCount { get; set; }

		string IDateHistogramAggregator.PreZone { get; set; }

		string IDateHistogramAggregator.PostZone { get; set; }

		string IDateHistogramAggregator.TimeZone { get; set; }

		bool? IDateHistogramAggregator.PreZoneAdjustLargeInterval { get; set; }

		int? IDateHistogramAggregator.Factor { get; set; }

		string IDateHistogramAggregator.PreOffset { get; set; }

		string IDateHistogramAggregator.PostOffset { get; set; }

		IDictionary<string, string> IDateHistogramAggregator.Order { get; set; }

		IDictionary<string, object> IDateHistogramAggregator.ExtendedBounds { get; set; }

		public DateHistogramAggregationDescriptor<T> Field(string field) => Assign(a => a.Field = field);

		public DateHistogramAggregationDescriptor<T> Field(Expression<Func<T, object>> field) => Assign(a => a.Field = field);

		public DateHistogramAggregationDescriptor<T> Script(string script) => Assign(a => a.Script = script);

		public DateHistogramAggregationDescriptor<T> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramSelector) =>
			Assign(a => a.Params = paramSelector?.Invoke(new FluentDictionary<string, object>()).NullIfNoKeys());

		public DateHistogramAggregationDescriptor<T> Interval(string interval) => Assign(a => a.Interval = interval);

		public DateHistogramAggregationDescriptor<T> Interval(DateInterval interval) =>
			Assign(a => a.Interval = interval.GetStringValue());

		public DateHistogramAggregationDescriptor<T> Format(string format) => Assign(a => a.Format = format);

		public DateHistogramAggregationDescriptor<T> MinimumDocumentCount(int minimumDocumentCount) =>
			Assign(a => a.MinimumDocumentCount = minimumDocumentCount);

		public DateHistogramAggregationDescriptor<T> PreZone(string preZone) => Assign(a => a.PreZone = preZone);

		public DateHistogramAggregationDescriptor<T> PostZone(string postZone) => Assign(a => a.PostZone = postZone);

		public DateHistogramAggregationDescriptor<T> TimeZone(string timeZone) => Assign(a => a.TimeZone = timeZone);

		public DateHistogramAggregationDescriptor<T> PreZoneAdjustLargeInterval(bool adjustLargeInterval = true) =>
			Assign(a => a.PreZoneAdjustLargeInterval = adjustLargeInterval);

		public DateHistogramAggregationDescriptor<T> Interval(int factor) => Assign(a => a.Factor = factor);

		public DateHistogramAggregationDescriptor<T> PreOffset(string preOffset) => Assign(a => a.PreOffset = preOffset);

		public DateHistogramAggregationDescriptor<T> PostOffset(string postOffset) => Assign(a => a.PostOffset = postOffset);

		public DateHistogramAggregationDescriptor<T> OrderAscending(string key) =>
			Assign(a => a.Order = new Dictionary<string, string> { { key, "asc" } });

		public DateHistogramAggregationDescriptor<T> OrderDescending(string key) =>
			Assign(a => a.Order = new Dictionary<string, string> { { key, "desc" } });


		public DateHistogramAggregationDescriptor<T> ExtendedBounds(string min, string max) =>
			Assign(a=>a.ExtendedBounds = new Dictionary<string, object> { { "min", min }, { "max", max } });

	}
}