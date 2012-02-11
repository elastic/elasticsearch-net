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
  public class FacetDescriptorsBucket<T> where T : class
  {
    [JsonProperty(PropertyName = "terms")]
    public TermFacetDescriptor<T> Terms { get; set; }

    [JsonProperty(PropertyName = "range")]
    public IFacetDescriptor Range { get; set; }
  }
}
