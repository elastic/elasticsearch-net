using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace Nest.DSL.Descriptors
{
  public class SortScriptDescriptor<T>
  {
    internal string _Field { get; set; }

    [JsonProperty("missing")]
    internal string _Missing { get; set; }

    [JsonProperty("order")]
    internal string _Order { get; set; }

    [JsonProperty(PropertyName = "type")]
    internal string _Type { get; set; }

    [JsonProperty(PropertyName = "script")]
    internal string _Script { get; set; }

    [JsonProperty(PropertyName = "params")]
    internal Dictionary<string, object> _Params { get; set; }

    public SortScriptDescriptor<T> Script(string script)
    {
      script.ThrowIfNull("script");
      this._Script = script;
      return this;
    }

    public SortScriptDescriptor<T> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramDictionary)
    {
      paramDictionary.ThrowIfNull("paramDictionary");
      this._Params = paramDictionary(new FluentDictionary<string, object>());
      return this;
    }

    public virtual SortScriptDescriptor<T> OnField(string field)
    {
      this._Field = field;
      return this;
    }
    public virtual SortScriptDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
    {
      var fieldName = ElasticClient.PropertyNameResolver.Resolve(objectPath);
      return this.OnField(fieldName);
    }

    public virtual SortScriptDescriptor<T> MissingLast()
    {
      this._Missing = "_last";
      return this;
    }
    public virtual SortScriptDescriptor<T> MissingFirst()
    {
      this._Missing = "_first";
      return this;
    }
    public virtual SortScriptDescriptor<T> Type(string type)
    {
      this._Type = type;
      return this;
    }
    public virtual SortScriptDescriptor<T> MissingValue(string value)
    {
      this._Missing = value;
      return this;
    }
    public virtual SortScriptDescriptor<T> Ascending()
    {
      this._Order = "asc";
      return this;
    }
    public virtual SortScriptDescriptor<T> Descending()
    {
      this._Order = "desc";
      return this;
    }
    public SortScriptDescriptor<T> ToggleSort(bool ascending)
    {
      this._Order = ascending ? "asc" : "desc";
      return this;
    }
  }
}
