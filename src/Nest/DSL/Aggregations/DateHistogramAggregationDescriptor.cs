using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Elasticsearch.Net;
using Nest.Resolvers;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	public class DateHistogramAggregationDescriptor<T> : BucketAggregationBaseDescriptor<DateHistogramAggregationDescriptor<T>, T>
		where T : class
	{
		[JsonProperty("field")]
		internal PropertyPathMarker _Field { get; set; }

		public DateHistogramAggregationDescriptor()
		{
			this.Format("yyyy-MM-dd");
		}


		public DateHistogramAggregationDescriptor<T> Field(string field)
		{
			this._Field = field;
			return this;
		}

		public DateHistogramAggregationDescriptor<T> Field(Expression<Func<T, object>> field)
		{
			this._Field = field;
			return this;
		}

		[JsonProperty("script")]
		internal string _Script { get; set; }

		public DateHistogramAggregationDescriptor<T> Script(string script)
		{
			this._Script = script;
			return this;
		}

		[JsonProperty("params")]
		internal FluentDictionary<string, object> _Params { get; set; }

		public DateHistogramAggregationDescriptor<T> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramSelector)
		{
			this._Params = paramSelector(new FluentDictionary<string, object>());
			return this;
		}

		[JsonProperty("interval")]
		internal string _Interval { get; set; }

		public DateHistogramAggregationDescriptor<T> Interval(string interval)
		{
			this._Interval = interval;
			return this;
		}
	
		[JsonProperty("format")]
		internal string _Format { get; set; }

		public DateHistogramAggregationDescriptor<T> Format(string format)
		{
			if (format.IsNullOrEmpty())
				return this;
			this._Format = format;
			return this;
		}
			
		[JsonProperty("min_doc_count")]
		internal int? _MinimumDocumentCount { get; set; }

		public DateHistogramAggregationDescriptor<T> MinimumDocumentCount(int minimumDocumentCount)
		{
			this._MinimumDocumentCount = minimumDocumentCount;
			return this;
		}
		
		[JsonProperty("pre_zone")]
		internal string _PreZone { get; set; }

		public DateHistogramAggregationDescriptor<T> PreZone(string preZone)
		{
			this._PreZone = preZone;
			return this;
		}

		[JsonProperty("post_zone")]
		internal string _PostZone { get; set; }

		public DateHistogramAggregationDescriptor<T> PostZone(string postZone)
		{
			this._PostZone = postZone;
			return this;
		}

		[JsonProperty("time_zone")]
		internal string _TimeZone { get; set; }

		public DateHistogramAggregationDescriptor<T> TimeZone(string timeZone)
		{
			this._TimeZone = timeZone;
			return this;
		}
		[JsonProperty("pre_zone_adjust_large_interval")]
		internal bool? _PreZoneAdjustLargeInterval { get; set; }

		public DateHistogramAggregationDescriptor<T> PreZoneAdjustLargeInterval(bool adjustLargeInterval = true)
		{
			this._PreZoneAdjustLargeInterval = adjustLargeInterval;
			return this;
		}
		[JsonProperty("factor")]
		internal int? _Factor { get; set; }

		public DateHistogramAggregationDescriptor<T> Interval(int factor)
		{
			this._Factor = factor;
			return this;
		}

		[JsonProperty("pre_offset")]
		internal string _PreOffset { get; set; }

		public DateHistogramAggregationDescriptor<T> PreOffset(string preOffset)
		{
			this._PreOffset = preOffset;
			return this;
		}
		[JsonProperty("post_offset")]
		internal string _PostOffset { get; set; }

		public DateHistogramAggregationDescriptor<T> PostOffset(string postOffset)
		{
			this._PostOffset = postOffset;
			return this;
		}
	
		[JsonProperty("order")]
		internal IDictionary<string, string> _Order { get; set; }

		public DateHistogramAggregationDescriptor<T> OrderAscending(string key)
		{
			this._Order = new Dictionary<string, string> { {key, "asc"}};
			return this;
		}
	
		public DateHistogramAggregationDescriptor<T> OrderDescending(string key)
		{
			this._Order = new Dictionary<string, string> { {key, "asc"}};
			return this;
		}

		[JsonProperty("extended_bounds")]
		internal IDictionary<string, object> _ExtendedBounds { get; set; }

		public DateHistogramAggregationDescriptor<T> ExtendedBounds(string min, string max)
		{
			this._ExtendedBounds = new Dictionary<string, object> { { "min", min }, { "max", max } };
			return this;
		}

	}
}