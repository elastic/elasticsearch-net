using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
using System.Linq.Expressions;

namespace Nest
{
  public class BaseFilter
  {
    [JsonProperty(PropertyName = "bool")]
    internal object BoolBaseFilterDescriptor { get; set; }
    //TODO prevent unnecessary wrapping of boolqueries.
    public static BaseFilter operator &(BaseFilter lbq, BaseFilter rbq)
    {
      var q = new BaseFilter();
      var bq = new BoolBaseFilterDescriptor();
      bq._MustFilters = new[] { lbq, rbq };
      q.BoolBaseFilterDescriptor = bq;
      return q;
    }

    public static BaseFilter operator |(BaseFilter lbq, BaseFilter rbq)
    {
      var q = new BaseFilter();
      var bq = new BoolBaseFilterDescriptor();
      bq._ShouldFilters = new[] { lbq, rbq };
      q.BoolBaseFilterDescriptor = bq;
      return q;
    }
    public static bool operator false(BaseFilter a)
    {
      return false;
    }

    public static bool operator true(BaseFilter a)
    {
      return false;
    }
  }
}
