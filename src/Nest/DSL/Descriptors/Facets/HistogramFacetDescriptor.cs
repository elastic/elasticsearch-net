using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Linq.Expressions;

namespace Nest.DSL
{
  [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
  public class HistogramFacetDescriptor<T> : BaseFacetDescriptor<T> where T : class
  {
    [JsonProperty(PropertyName = "field")]
    internal string _Field { get; set; }

    [JsonProperty(PropertyName = "key_field")]
    internal string _KeyField { get; set; }

    [JsonProperty(PropertyName = "value_field")]
    internal string _ValueField { get; set; }

    [JsonProperty(PropertyName = "key_script")]
    internal string _KeyScript { get; set; }

    [JsonProperty(PropertyName = "value_script")]
    internal string _ValueScript { get; set; }

    [JsonProperty(PropertyName = "interval")]
    internal int? _Interval { get; set; }

    [JsonProperty(PropertyName = "time_interval")]
    internal string _TimeInterval { get; set; }

    [JsonProperty(PropertyName = "params")]
    internal Dictionary<string,object> _Params { get; set; }

    public HistogramFacetDescriptor<T> OnField(string field)
    {
      field.ThrowIfNullOrEmpty("field");
      this._Field = field;
      return this;
    }
    public HistogramFacetDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
    {
      objectPath.ThrowIfNull("objectPath");
      var fieldName = ElasticClient.PropertyNameResolver.ResolveToSort(objectPath);
      return this.OnField(fieldName);
    }
    public HistogramFacetDescriptor<T> Interval(int interval)
    {
      this._Interval = interval;
      return this;
    }
    public HistogramFacetDescriptor<T> TimeInterval(string timeInterval)
    {
      timeInterval.ThrowIfNullOrEmpty("timeInterval");
      this._TimeInterval = timeInterval;
      return this;
    }
    public HistogramFacetDescriptor<T> TimeInterval(TimeSpan timespanInterval)
    {
 #if !DEBUG
    throw new NotImplementedException("serializes TimeSpan.FromHours(1.5) to '01:30:00'");
 #endif
      //TODO: now serializes TimeSpan.FromHours(1.5) to '01:30:00' 
      //Probably needs to be 1.5h but integration tests will show if this is correct 
      //or not
      this._TimeInterval = timespanInterval.ToString();
      return this;
    }
    public HistogramFacetDescriptor<T> KeyField(Expression<Func<T, object>> objectPath)
    {
      objectPath.ThrowIfNull("objectPath");
      var fieldName = ElasticClient.PropertyNameResolver.ResolveToSort(objectPath);
      return this.KeyField(fieldName);
    }
    public HistogramFacetDescriptor<T> KeyField(string keyField)
    {
      keyField.ThrowIfNull("keyField");
      this._KeyField = keyField;
      return this;
    }
    public HistogramFacetDescriptor<T> KeyScript(string keyScript)
    {
      keyScript.ThrowIfNull("keyScript");
      this._KeyScript = keyScript;
      return this;
    }
    public HistogramFacetDescriptor<T> ValueField(Expression<Func<T, object>> objectPath)
    {
      objectPath.ThrowIfNull("objectPath");
      var fieldName = ElasticClient.PropertyNameResolver.ResolveToSort(objectPath);
      return this.ValueField(fieldName);
    }
    public HistogramFacetDescriptor<T> ValueField(string valueField)
    {
      valueField.ThrowIfNull("valueField");
      this._ValueField = valueField;
      return this;
    }
    public HistogramFacetDescriptor<T> ValueScript(string valueScript)
    {
      valueScript.ThrowIfNull("valueScript");
      this._ValueScript = valueScript;
      return this;
    }
    public HistogramFacetDescriptor<T> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramDictionary)
    {
      paramDictionary.ThrowIfNull("paramDictionary");
      this._Params = paramDictionary(new FluentDictionary<string,object>());
      return this;
    }



    public new HistogramFacetDescriptor<T> Global()
    {
      this._IsGlobal = true;
      return this;
    }
		public new HistogramFacetDescriptor<T> FacetFilter(
      Func<FilterDescriptor<T>, FilterDescriptor<T>> facetFilter
    )
    {
      var filter = facetFilter(new FilterDescriptor<T>());
      this._FacetFilter = filter;
      return this;
    }
    public new HistogramFacetDescriptor<T> Nested(string nested)
    {
      this._Nested = nested;
      return this;
    }
    public new HistogramFacetDescriptor<T> Scope(string scope)
    {
      this._Scope = scope;
      return this;
    }
  }
}
