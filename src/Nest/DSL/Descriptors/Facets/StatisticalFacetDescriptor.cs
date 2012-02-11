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
  public class StatisticalFacetDescriptor<T> : IFacetDescriptor where T : class
  {
    [JsonProperty(PropertyName = "field")]
    internal string _Field { get; set; }

    [JsonProperty(PropertyName = "fields")]
    internal IEnumerable<string> _Fields { get; set; }

    [JsonProperty(PropertyName = "script")]
    internal string _Script { get; set; }

    [JsonProperty(PropertyName = "params")]
    internal Dictionary<string,object> _Params { get; set; }

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
      var fieldName = ElasticClient.PropertyNameResolver.ResolveToSort(objectPath);
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
      var fieldNames = objectPaths.Select(o => ElasticClient.PropertyNameResolver.ResolveToSort(o))
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
      this._Params = paramDictionary(new FluentDictionary<string,object>());
      return this;
    }
  }
}
