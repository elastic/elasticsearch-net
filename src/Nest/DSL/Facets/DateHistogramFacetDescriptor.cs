using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Linq.Expressions;
using Elasticsearch.Net;
using Nest.Resolvers;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<DateHistogramFacetRequest>))]
	public interface IDateHistogramFacetRequest : IFacetRequest
	{
		[JsonProperty(PropertyName = "field")]
		PropertyPathMarker Field { get; set; }

		[JsonProperty(PropertyName = "key_field")]
		PropertyPathMarker KeyField { get; set; }

		[JsonProperty(PropertyName = "value_field")]
		PropertyPathMarker ValueField { get; set; }

		[JsonProperty(PropertyName = "key_script")]
		string KeyScript { get; set; }

		[JsonProperty(PropertyName = "value_script")]
		string ValueScript { get; set; }

		[JsonProperty(PropertyName = "interval")]
		string Interval { get; set; }

		[JsonProperty(PropertyName = "time_zone")]
		string TimeZone { get; set; }

		[JsonProperty(PropertyName = "pre_zone")]
		string PreZone { get; set; }

		[JsonProperty(PropertyName = "post_zone")]
		string PostZone { get; set; }

		[JsonProperty(PropertyName = "factor")]
		int? Factor { get; set; }

		[JsonProperty(PropertyName = "pre_offset")]
		string PreOffset { get; set; }

		[JsonProperty(PropertyName = "post_offset")]
		string PostOffset { get; set; }

		[JsonProperty(PropertyName = "params")]
		[JsonConverter(typeof (DictionaryKeysAreNotPropertyNamesJsonConverter))]
		Dictionary<string, object> Params { get; set; }
	}
	
	public class DateHistogramFacetRequest : FacetRequest, IDateHistogramFacetRequest
	{
		public PropertyPathMarker Field { get; set; }
		public PropertyPathMarker KeyField { get; set; }
		public PropertyPathMarker ValueField { get; set; }
		public string KeyScript { get; set; }
		public string ValueScript { get; set; }
		public string Interval { get; set; }
		public string TimeZone { get; set; }
		public string PreZone { get; set; }
		public string PostZone { get; set; }
		public int? Factor { get; set; }
		public string PreOffset { get; set; }
		public string PostOffset { get; set; }
		public Dictionary<string, object> Params { get; set; }
	}

	public class DateHistogramFacetDescriptor<T> : BaseFacetDescriptor<DateHistogramFacetDescriptor<T>,T>, IDateHistogramFacetRequest where T : class
	{
		protected IDateHistogramFacetRequest Self { get { return this;  } }

		PropertyPathMarker IDateHistogramFacetRequest.Field { get; set; }

		PropertyPathMarker IDateHistogramFacetRequest.KeyField { get; set; }

		PropertyPathMarker IDateHistogramFacetRequest.ValueField { get; set; }

		string IDateHistogramFacetRequest.KeyScript { get; set; }

		string IDateHistogramFacetRequest.ValueScript { get; set; }

		string IDateHistogramFacetRequest.Interval { get; set; }

		string IDateHistogramFacetRequest.TimeZone { get; set; }

		string IDateHistogramFacetRequest.PreZone { get; set; }

		string IDateHistogramFacetRequest.PostZone { get; set; }

		int? IDateHistogramFacetRequest.Factor { get; set; }

		string IDateHistogramFacetRequest.PreOffset { get; set; }

		string IDateHistogramFacetRequest.PostOffset { get; set; }

		Dictionary<string, object> IDateHistogramFacetRequest.Params { get; set; }

		public DateHistogramFacetDescriptor<T> OnField(string field)
		{
			field.ThrowIfNullOrEmpty("field");
			Self.Field = field;
			return this;
		}
		public DateHistogramFacetDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			objectPath.ThrowIfNull("objectPath");
			Self.Field = objectPath;
			return this;
		}
        /// <summary>
        /// Added to support custom date intervals on date histogram facet. Eg: "5s" for 5 seconds interval.
        /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-facets-date-histogram-facet.html#_interval
        /// </summary>
        /// <param name="interval">Custom interval string</param>
        /// <returns></returns>
        public DateHistogramFacetDescriptor<T> Interval(string interval)
        {
            var intervalString = interval.ToLowerInvariant();
            Self.Interval = intervalString;
            return this;
        }
        /// <summary>
        /// Added to support custom date intervals on date histogram facet. Eg: "5s" for 5 seconds interval.
        /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-facets-date-histogram-facet.html#_interval
        /// </summary>
        /// <param name="interval">Custom interval string</param>
        /// <param name="dateRounding">Date rounding type</param>
        /// <returns></returns>
        public DateHistogramFacetDescriptor<T> Interval(string interval, DateRounding dateRounding)
        {
            var intervalString = interval.ToLowerInvariant();
            var roundingString = Enum.GetName(typeof(DateRounding), dateRounding).ToLowerInvariant();
            Self.Interval = intervalString + ":" + roundingString;
            return this;
        }
        public DateHistogramFacetDescriptor<T> Interval(DateInterval interval)
		{
			var intervalString = Enum.GetName(typeof(DateInterval), interval).ToLowerInvariant();
			Self.Interval = intervalString;
			return this;
		}
		public DateHistogramFacetDescriptor<T> Interval(DateInterval interval, DateRounding dateRounding)
		{
			var intervalString = Enum.GetName(typeof(DateInterval), interval).ToLowerInvariant();
			var roundingString = Enum.GetName(typeof(DateRounding), dateRounding).ToLowerInvariant();
			Self.Interval = intervalString + ":" + roundingString;
			return this;
		}
		public DateHistogramFacetDescriptor<T> Factor(int factor)
		{
			Self.Factor = factor;
			return this;
		}
		public DateHistogramFacetDescriptor<T> Offset(string Pre = null, string Post = null)
		{
			Self.PreOffset = Pre;
			Self.PostOffset = Post;
			return this;
		}
		public DateHistogramFacetDescriptor<T> TimeZone(string timeZone)
		{
			Self.TimeZone = timeZone;
			return this;
		}
		public DateHistogramFacetDescriptor<T> TimeZones(string Pre = null, string Post = null)
		{
			//elasticsearch actually sets timezone in pre_zone so reset timezone in case its set
			if (!string.IsNullOrEmpty(Pre))
				Self.TimeZone = null;
			Self.PreZone = Pre;
			Self.PostZone = Post;
			return this;
		}
		public DateHistogramFacetDescriptor<T> KeyField(Expression<Func<T, object>> objectPath)
		{
			objectPath.ThrowIfNull("objectPath");
			Self.KeyField = objectPath;
			return this;
		}
		public DateHistogramFacetDescriptor<T> KeyField(string keyField)
		{
			keyField.ThrowIfNull("keyField");
			Self.KeyField = keyField;
			return this;
		}
		public DateHistogramFacetDescriptor<T> KeyScript(string keyScript)
		{
			keyScript.ThrowIfNull("keyScript");
			Self.KeyScript = keyScript;
			return this;
		}
		public DateHistogramFacetDescriptor<T> ValueField(Expression<Func<T, object>> objectPath)
		{
			objectPath.ThrowIfNull("objectPath");
			Self.ValueField = objectPath;
			return this;
		}
		public DateHistogramFacetDescriptor<T> ValueField(string valueField)
		{
			valueField.ThrowIfNull("valueField");
			Self.ValueField = valueField;
			return this;
		}
		public DateHistogramFacetDescriptor<T> ValueScript(string valueScript)
		{
			valueScript.ThrowIfNull("valueScript");
			Self.ValueScript = valueScript;
			return this;
		}
		public DateHistogramFacetDescriptor<T> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramDictionary)
		{
			paramDictionary.ThrowIfNull("paramDictionary");
			Self.Params = paramDictionary(new FluentDictionary<string, object>());
			return this;
		}

	}
}
