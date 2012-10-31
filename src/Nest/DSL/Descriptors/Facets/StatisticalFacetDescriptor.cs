using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Linq.Expressions;
using Nest.Resolvers;

namespace Nest
{
  [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
  public class StatisticalFacetDescriptor<T> : BaseFacetDescriptor<T> where T : class
  {
    [JsonProperty(PropertyName = "field")]
    internal string _Field { get; set; }

    [JsonProperty(PropertyName = "fields")]
    internal IEnumerable<string> _Fields { get; set; }

    [JsonProperty(PropertyName = "script")]
    internal string _Script { get; set; }

    [JsonProperty(PropertyName = "params")]
    internal Dictionary<string, object> _Params { get; set; }

    public StatisticalFacetDescriptor<T> OnField(string field)
    {
      field.ThrowIfNullOrEmpty("field");
      if (this._Fields != null)
        this._Fields = null;
      this._Field = field;
      return this;
    }
    public StatisticalFacetDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
    {
      objectPath.ThrowIfNull("objectPath");
      var fieldName = new PropertyNameResolver().Resolve(objectPath);
      return this.OnField(fieldName);
    }
    public StatisticalFacetDescriptor<T> OnFields(params string[] fields)
    {
      if (this._Field != null)
        this._Field = null;
      this._Fields = fields;
      return this;
    }
    public StatisticalFacetDescriptor<T> OnFields(params Expression<Func<T, object>>[] objectPaths)
    {
      var fieldNames = objectPaths.Select(o => new PropertyNameResolver().Resolve(o))
        .ToArray();

      return this.OnFields(fieldNames);
    }
    public StatisticalFacetDescriptor<T> Script(string script)
    {
      script.ThrowIfNull("script");
      this._Script = script;
      return this;
    }

    public StatisticalFacetDescriptor<T> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramDictionary)
    {
      paramDictionary.ThrowIfNull("paramDictionary");
      this._Params = paramDictionary(new FluentDictionary<string, object>());
      return this;
    }

    public new StatisticalFacetDescriptor<T> Global()
    {
      this._IsGlobal = true;
      return this;
    }
    public new StatisticalFacetDescriptor<T> FacetFilter(
      Func<FilterDescriptor<T>, BaseFilter> facetFilter
    )
    {
      var filter = facetFilter(new FilterDescriptor<T>());
      this._FacetFilter = filter;
      return this;
    }
    public new StatisticalFacetDescriptor<T> Nested(string nested)
    {
      this._Nested = nested;
      return this;
    }
    public new StatisticalFacetDescriptor<T> Scope(string scope)
    {
      this._Scope = scope;
      return this;
    }
  }
}
