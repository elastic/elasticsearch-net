using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace Nest
{
  public class UpdateDescriptor<T>
  {
    [JsonProperty(PropertyName = "script")]
    internal string _Script { get; set; }

    [JsonProperty(PropertyName = "params")]
    internal Dictionary<string, object> _Params { get; set; }

    internal int? _RetriesOnConflict { get; set; }
    internal bool? _Refresh { get; set; }
    internal string _Percolate { get; set; }
    internal Consistency? _Consistency { get; set; }
    internal Nest.Replication? _Replication { get; set; }
    internal int? _Timeout { get; set; }
    internal string _Parent { get; set; }
    internal string _Routing { get; set; }
    internal string _Index { get; set; }
    internal string _Type { get; set; }
    internal string _Id { get; set; }
    internal T _Object { get; set; }

    public UpdateDescriptor<T> Script(string script)
    {
      script.ThrowIfNull("script");
      this._Script = script;
      return this;
    }

    public UpdateDescriptor<T> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramDictionary)
    {
      paramDictionary.ThrowIfNull("paramDictionary");
      this._Params = paramDictionary(new FluentDictionary<string, object>());
      return this;
    }
    public UpdateDescriptor<T> Index(string index)
    {
      index.ThrowIfNullOrEmpty("indices");
      this._Index = index;
      return this;
    }
    public UpdateDescriptor<T> Type(string type)
    {
      type.ThrowIfNullOrEmpty("type");
      this._Type = type;
      return this;
    }
    public UpdateDescriptor<T> Type(Type type)
    {
      type.ThrowIfNull("type");
      return this.Type(ElasticClient.GetTypeNameFor(type));
    }
    public UpdateDescriptor<T> Routing(string routing)
    {
      routing.ThrowIfNullOrEmpty("routing");
      this._Routing = routing;
      return this;
    }
    public UpdateDescriptor<T> Parent(string parent)
    {
      parent.ThrowIfNullOrEmpty("parent");
      this._Parent = parent;
      return this;
    }
    public UpdateDescriptor<T> Timeout(int timeout)
    {
      this._Timeout = timeout;
      return this;
    }
    public UpdateDescriptor<T> Replication(Replication replication)
    {
      this._Replication = replication;
      return this;
    }
    public UpdateDescriptor<T> Concistency(Consistency consistency)
    {
      this._Consistency = consistency;
      return this;
    }
    public UpdateDescriptor<T> Percolate(string percolation)
    {
      this._Percolate = percolation;
      return this;
    }
    public UpdateDescriptor<T> Refresh(bool refresh = true)
    {
      this._Refresh = refresh;
      return this;
    }
    public UpdateDescriptor<T> RetriesOnConflict(int retriesOnConflict)
    {
      this._RetriesOnConflict = retriesOnConflict;
      return this;
    }
    public UpdateDescriptor<T> Id(int id)
    {
      return this.Id(id.ToString());
    }
    public UpdateDescriptor<T> Id(string id)
    {
      this._Id = id;
      return this;
    }
    public UpdateDescriptor<T> Object(T @object)
    {
      this._Object = @object;
      return this;
    }

  }
}
