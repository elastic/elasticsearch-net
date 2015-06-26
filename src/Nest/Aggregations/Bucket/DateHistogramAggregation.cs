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

	public class DateHistogramAggregationDescriptor<T> : BucketAggregationBaseDescriptor<DateHistogramAggregationDescriptor<T>, T>, IDateHistogramAggregator where T : class
	{
		private IDateHistogramAggregator Self { get { return this; } }

		PropertyPathMarker IDateHistogramAggregator.Field { get; set; }

		string IDateHistogramAggregator.Script { get; set; }
		
		IDictionary<string, object> IDateHistogramAggregator.Params { get; set; }

		string IDateHistogramAggregator.Interval { get; set; }

		string IDateHistogramAggregator.Format { get; set; }

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

		public DateHistogramAggregationDescriptor()
		{
			this.Self.Format = "date_optional_time";
		}

		public DateHistogramAggregationDescriptor<T> Field(string field)
		{
			Self.Field = field;
			return this;
		}

		public DateHistogramAggregationDescriptor<T> Field(Expression<Func<T, object>> field)
		{
			Self.Field = field;
			return this;
		}

		public DateHistogramAggregationDescriptor<T> Script(string script)
		{
			Self.Script = script;
			return this;
		}

		public DateHistogramAggregationDescriptor<T> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramSelector)
		{
			Self.Params = paramSelector(new FluentDictionary<string, object>());
			return this;
		}

		public DateHistogramAggregationDescriptor<T> Interval(string interval)
		{
			Self.Interval = interval;
			return this;
		}

		public DateHistogramAggregationDescriptor<T> Interval(DateInterval interval)
		{
			var intervalString = interval.GetStringValue();
			Self.Interval = intervalString;
			return this;
		}
	
		public DateHistogramAggregationDescriptor<T> Format(string format)
		{
			if (format.IsNullOrEmpty())
				return this;
			Self.Format = format;
			return this;
		}
			
		public DateHistogramAggregationDescriptor<T> MinimumDocumentCount(int minimumDocumentCount)
		{
			Self.MinimumDocumentCount = minimumDocumentCount;
			return this;
		}
		
		public DateHistogramAggregationDescriptor<T> PreZone(string preZone)
		{
			Self.PreZone = preZone;
			return this;
		}

		public DateHistogramAggregationDescriptor<T> PostZone(string postZone)
		{
			Self.PostZone = postZone;
			return this;
		}

		public DateHistogramAggregationDescriptor<T> TimeZone(string timeZone)
		{
			Self.TimeZone = timeZone;
			return this;
		}
		
		public DateHistogramAggregationDescriptor<T> PreZoneAdjustLargeInterval(bool adjustLargeInterval = true)
		{
			Self.PreZoneAdjustLargeInterval = adjustLargeInterval;
			return this;
		}
		
		public DateHistogramAggregationDescriptor<T> Interval(int factor)
		{
			Self.Factor = factor;
			return this;
		}

		public DateHistogramAggregationDescriptor<T> PreOffset(string preOffset)
		{
			Self.PreOffset = preOffset;
			return this;
		}
		public DateHistogramAggregationDescriptor<T> PostOffset(string postOffset)
		{
			Self.PostOffset = postOffset;
			return this;
		}
	
		public DateHistogramAggregationDescriptor<T> OrderAscending(string key)
		{
			Self.Order = new Dictionary<string, string> { {key, "asc"}};
			return this;
		}
	
		public DateHistogramAggregationDescriptor<T> OrderDescending(string key)
		{
			Self.Order = new Dictionary<string, string> { {key, "desc"}};
			return this;
		}


		public DateHistogramAggregationDescriptor<T> ExtendedBounds(string min, string max)
		{
			Self.ExtendedBounds = new Dictionary<string, object> { { "min", min }, { "max", max } };
			return this;
		}

	}
}