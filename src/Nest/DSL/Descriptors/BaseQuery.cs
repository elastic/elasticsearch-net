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
  public class BaseQuery
  {
    [JsonProperty(PropertyName = "bool")]
    internal object BoolQueryDescriptor { get; set; }

    public static BaseQuery operator &(BaseQuery lbq, BaseQuery rbq)
    {
      var q = new BaseQuery();
      var bq = new BoolBaseQueryDescriptor();
      bq._MustQueries = new[] { lbq, rbq };
      q.BoolQueryDescriptor = bq;
      return q;
    }

    public static BaseQuery operator |(BaseQuery lbq, BaseQuery rbq)
    {
      var q = new BaseQuery();
      var bq = new BoolBaseQueryDescriptor();
      bq._ShouldQueries = new[] { lbq, rbq };
      q.BoolQueryDescriptor = bq;
      return q;
    }
  }
}
