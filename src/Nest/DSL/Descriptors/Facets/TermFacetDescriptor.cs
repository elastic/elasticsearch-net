using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest.DSL
{
  public interface IFacetDescriptor
  {

  }
  
  [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
  public class FacetDescriptorsBucket
  {
    [JsonProperty(PropertyName = "terms")]
    public TermFacetDescriptor Terms { get; set; }
  }

  [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
  public class TermFacetDescriptor : IFacetDescriptor
  {
    [JsonProperty(PropertyName = "field")]
    internal string _Field { get; set; }
    [JsonProperty(PropertyName = "size")]
    internal int _Size { get; set; }

    public TermFacetDescriptor OnField(string field)
    {
      this._Field = field;
      return this;
    }
    public TermFacetDescriptor Size(int size)
    {
      this._Size = size;
      return this;
    }
  }
}
