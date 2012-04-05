using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest.DSL.Descriptors
{
  public class SortDescriptor<T> where T : class
  {
    internal string _Field { get; set; }

    [JsonProperty("missing")]
    internal string _Missing { get; set; }

    [JsonProperty("order")]
    internal string _Order { get; set; }

    public virtual SortDescriptor<T> OnField(string field)
    {
      this._Field = field;
      return this;
    }
    public virtual SortDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
    {
      var fieldName = ElasticClient.PropertyNameResolver.Resolve(objectPath);
      return this.OnField(fieldName);
    }

    public virtual SortDescriptor<T> MissingLast()
    {
      this._Missing = "_last";
      return this;
    }
    public virtual SortDescriptor<T> MissingFirst()
    {
      this._Missing = "_first";
      return this;
    }
    public virtual SortDescriptor<T> MissingValue(string value)
    {
      this._Missing = value;
      return this;
    }
    public virtual SortDescriptor<T> Ascending()
    {
      this._Order = "asc";
      return this;
    }
    public virtual SortDescriptor<T> Descending()
    {
      this._Order = "desc";
      return this;
    }
  }
}
