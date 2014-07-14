using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<HistogramFacetRequest>))]
	public interface IHistogramFacetRequest : IFacetRequest
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
		int? Interval { get; set; }

		[JsonProperty(PropertyName = "time_interval")]
		string TimeInterval { get; set; }

		[JsonProperty(PropertyName = "params")]
		[JsonConverter(typeof (DictionaryKeysAreNotPropertyNamesJsonConverter))]
		Dictionary<string, object> Params { get; set; }
	}
	
	public class HistogramFacetRequest : FacetRequest, IHistogramFacetRequest
	{
		public PropertyPathMarker Field { get; set; }
		public PropertyPathMarker KeyField { get; set; }
		public PropertyPathMarker ValueField { get; set; }
		public string KeyScript { get; set; }
		public string ValueScript { get; set; }
		public int? Interval { get; set; }
		public string TimeInterval { get; set; }
		public Dictionary<string, object> Params { get; set; }
	}

	public class HistogramFacetDescriptor<T> : BaseFacetDescriptor<HistogramFacetDescriptor<T>, T>, 
		IHistogramFacetRequest where T : class
	{
		protected IHistogramFacetRequest Self { get { return this; } }

		PropertyPathMarker IHistogramFacetRequest.Field { get; set; }

		PropertyPathMarker IHistogramFacetRequest.KeyField { get; set; }

		PropertyPathMarker IHistogramFacetRequest.ValueField { get; set; }

		string IHistogramFacetRequest.KeyScript { get; set; }

		string IHistogramFacetRequest.ValueScript { get; set; }

		int? IHistogramFacetRequest.Interval { get; set; }

		string IHistogramFacetRequest.TimeInterval { get; set; }

		Dictionary<string, object> IHistogramFacetRequest.Params { get; set; }

		public HistogramFacetDescriptor<T> OnField(string field)
		{
			field.ThrowIfNullOrEmpty("field");
			Self.Field = field;
			return this;
		}
		public HistogramFacetDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			objectPath.ThrowIfNull("objectPath");
			Self.Field = objectPath;
			return this;
		}
		public HistogramFacetDescriptor<T> Interval(int interval)
		{
			Self.Interval = interval;
			return this;
		}
		public HistogramFacetDescriptor<T> TimeInterval(string timeInterval)
		{
			timeInterval.ThrowIfNullOrEmpty("timeInterval");
			Self.TimeInterval = timeInterval;
			return this;
		}
		public HistogramFacetDescriptor<T> TimeInterval(TimeSpan timespanInterval)
		{
			//now serializes TimeSpan.FromHours(1.5) to '01:30:00' 
			//TODO check with integration test if this produces the correct result
			Self.TimeInterval = timespanInterval.ToString();
			return this;
		}
		public HistogramFacetDescriptor<T> KeyField(Expression<Func<T, object>> objectPath)
		{
			objectPath.ThrowIfNull("objectPath");
			Self.KeyField = objectPath;
			return this;
		}
		public HistogramFacetDescriptor<T> KeyField(string keyField)
		{
			keyField.ThrowIfNull("keyField");
			Self.KeyField = keyField;
			return this;
		}
		public HistogramFacetDescriptor<T> KeyScript(string keyScript)
		{
			keyScript.ThrowIfNull("keyScript");
			Self.KeyScript = keyScript;
			return this;
		}
		public HistogramFacetDescriptor<T> ValueField(Expression<Func<T, object>> objectPath)
		{
			objectPath.ThrowIfNull("objectPath");
			Self.ValueField = objectPath;
			return this;
		}
		public HistogramFacetDescriptor<T> ValueField(string valueField)
		{
			valueField.ThrowIfNull("valueField");
			Self.ValueField = valueField;
			return this;
		}
		public HistogramFacetDescriptor<T> ValueScript(string valueScript)
		{
			valueScript.ThrowIfNull("valueScript");
			Self.ValueScript = valueScript;
			return this;
		}
		public HistogramFacetDescriptor<T> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramDictionary)
		{
			paramDictionary.ThrowIfNull("paramDictionary");
			Self.Params = paramDictionary(new FluentDictionary<string, object>());
			return this;
		}
	}
}
