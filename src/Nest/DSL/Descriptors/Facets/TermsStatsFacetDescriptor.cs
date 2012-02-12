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
  public class TermsStatsFacetDescriptor<T> : BaseFacetDescriptor<T> where T : class
  {
    [JsonProperty(PropertyName = "fields")]
    internal IEnumerable<string> _Fields { get; set; }

    [JsonProperty(PropertyName = "key_field")]
    internal string _KeyField { get; set; }

    [JsonProperty(PropertyName = "value_field")]
    internal string _ValueField { get; set; }

    [JsonProperty(PropertyName = "key_script")]
    internal string _KeyScript { get; set; }

    [JsonProperty(PropertyName = "value_script")]
    internal string _ValueScript { get; set; }

    [JsonProperty(PropertyName = "order")]
    [JsonConverter(typeof(StringEnumConverter))]
    internal TermsStatsOrder? _Order { get; set; }

    [JsonProperty(PropertyName = "lang")]
    internal string _Lang { get; set; }

    [JsonProperty(PropertyName = "params")]
    internal Dictionary<string,object> _Params { get; set; }

    public TermsStatsFacetDescriptor<T> KeyField(Expression<Func<T, object>> objectPath)
    {
      objectPath.ThrowIfNull("objectPath");
      var fieldName = ElasticClient.PropertyNameResolver.ResolveToSort(objectPath);
      return this.KeyField(fieldName);
    }
    public TermsStatsFacetDescriptor<T> KeyField(string keyField)
    {
      keyField.ThrowIfNull("keyField");
      this._KeyField = keyField;
      return this;
    }
    public TermsStatsFacetDescriptor<T> KeyScript(string keyScript)
    {
      keyScript.ThrowIfNull("keyScript");
      this._KeyScript = keyScript;
      return this;
    }
    public TermsStatsFacetDescriptor<T> ValueField(Expression<Func<T, object>> objectPath)
    {
      objectPath.ThrowIfNull("objectPath");
      var fieldName = ElasticClient.PropertyNameResolver.ResolveToSort(objectPath);
      return this.ValueField(fieldName);
    }
    public TermsStatsFacetDescriptor<T> ValueField(string valueField)
    {
      valueField.ThrowIfNull("valueField");
      this._ValueField = valueField;
      return this;
    }
    public TermsStatsFacetDescriptor<T> ValueScript(string valueScript)
    {
      valueScript.ThrowIfNull("valueScript");
      this._ValueScript = valueScript;
      return this;
    }
    public TermsStatsFacetDescriptor<T> Language(string language)
    {
      language.ThrowIfNull("language");
      this._Lang = language;
      return this;
    }
    public TermsStatsFacetDescriptor<T> Order(TermsStatsOrder order)
    {
      this._Order = order;
      return this;
    }

    public TermsStatsFacetDescriptor<T> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramDictionary)
    {
      paramDictionary.ThrowIfNull("paramDictionary");
      this._Params = paramDictionary(new FluentDictionary<string,object>());
      return this;
    }



    public new TermsStatsFacetDescriptor<T> Global()
    {
      this._IsGlobal = true;
      return this;
    }
    public TermsStatsFacetDescriptor<T> FacetFilter(
      Func<FilterDescriptor<T>, FilterDescriptor<T>> facetFilter
    )
    {
      var filter = facetFilter(new FilterDescriptor<T>());
      this._FacetFilter = filter;
      return this;
    }
  }
}
