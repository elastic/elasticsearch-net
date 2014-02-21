using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Linq.Expressions;
using Elasticsearch.Net;
using Nest.Resolvers;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class DateHistogramFacetDescriptor<T> : BaseFacetDescriptor<DateHistogramFacetDescriptor<T>,T> 
		where T : class
	{
		[JsonProperty(PropertyName = "field")]
		internal PropertyPathMarker _Field { get; set; }

		[JsonProperty(PropertyName = "key_field")]
		internal PropertyPathMarker _KeyField { get; set; }

		[JsonProperty(PropertyName = "value_field")]
		internal PropertyPathMarker _ValueField { get; set; }

		[JsonProperty(PropertyName = "key_script")]
		internal string _KeyScript { get; set; }

		[JsonProperty(PropertyName = "value_script")]
		internal string _ValueScript { get; set; }

		[JsonProperty(PropertyName = "interval")]
		internal string _Interval { get; set; }

		[JsonProperty(PropertyName = "time_zone")]
		internal string _TimeZone { get; set; }

		[JsonProperty(PropertyName = "pre_zone")]
		internal string _PreZone { get; set; }
		[JsonProperty(PropertyName = "post_zone")]
		internal string _PostZone { get; set; }

		[JsonProperty(PropertyName = "factor")]
		internal int? _Factor { get; set; }

		[JsonProperty(PropertyName = "pre_offset")]
		internal string _PreOffset { get; set; }
		[JsonProperty(PropertyName = "post_offset")]
		internal string _PostOffset { get; set; }

		[JsonProperty(PropertyName = "params")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		internal Dictionary<string, object> _Params { get; set; }

		public DateHistogramFacetDescriptor<T> OnField(string field)
		{
			field.ThrowIfNullOrEmpty("field");
			this._Field = field;
			return this;
		}
		public DateHistogramFacetDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			objectPath.ThrowIfNull("objectPath");
			this._Field = objectPath;
			return this;
		}
		public DateHistogramFacetDescriptor<T> Interval(DateInterval interval)
		{
			var intervalString = Enum.GetName(typeof(DateInterval), interval).ToLower();
			this._Interval = intervalString;
			return this;
		}
		public DateHistogramFacetDescriptor<T> Interval(DateInterval interval, DateRounding dateRounding)
		{
			var intervalString = Enum.GetName(typeof(DateInterval), interval).ToLower();
			var roundingString = Enum.GetName(typeof(DateRounding), dateRounding).ToLower();
			this._Interval = intervalString + ":" + roundingString;
			return this;
		}
		public DateHistogramFacetDescriptor<T> Factor(int factor)
		{
			this._Factor = factor;
			return this;
		}
		public DateHistogramFacetDescriptor<T> Offset(string Pre = null, string Post = null)
		{
			this._PreOffset = Pre;
			this._PostOffset = Post;
			return this;
		}
		public DateHistogramFacetDescriptor<T> TimeZone(string timeZone)
		{
			this._TimeZone = timeZone;
			return this;
		}
		public DateHistogramFacetDescriptor<T> TimeZones(string Pre = null, string Post = null)
		{
			//elasticsearch actually sets timezone in pre_zone so reset timezone in case its set
			if (!string.IsNullOrEmpty(Pre))
				this._TimeZone = null;
			this._PreZone = Pre;
			this._PostZone = Post;
			return this;
		}
		public DateHistogramFacetDescriptor<T> KeyField(Expression<Func<T, object>> objectPath)
		{
			objectPath.ThrowIfNull("objectPath");
			this._KeyField = objectPath;
			return this;
		}
		public DateHistogramFacetDescriptor<T> KeyField(string keyField)
		{
			keyField.ThrowIfNull("keyField");
			this._KeyField = keyField;
			return this;
		}
		public DateHistogramFacetDescriptor<T> KeyScript(string keyScript)
		{
			keyScript.ThrowIfNull("keyScript");
			this._KeyScript = keyScript;
			return this;
		}
		public DateHistogramFacetDescriptor<T> ValueField(Expression<Func<T, object>> objectPath)
		{
			objectPath.ThrowIfNull("objectPath");
			this._ValueField = objectPath;
			return this;
		}
		public DateHistogramFacetDescriptor<T> ValueField(string valueField)
		{
			valueField.ThrowIfNull("valueField");
			this._ValueField = valueField;
			return this;
		}
		public DateHistogramFacetDescriptor<T> ValueScript(string valueScript)
		{
			valueScript.ThrowIfNull("valueScript");
			this._ValueScript = valueScript;
			return this;
		}
		public DateHistogramFacetDescriptor<T> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramDictionary)
		{
			paramDictionary.ThrowIfNull("paramDictionary");
			this._Params = paramDictionary(new FluentDictionary<string, object>());
			return this;
		}

	}
}
