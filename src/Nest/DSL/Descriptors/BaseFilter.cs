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
      bq._MustFilters = new[] { lbq, rbq };
      q.BoolBaseFilterDescriptor = bq;
      return q;
    }
  }
}
