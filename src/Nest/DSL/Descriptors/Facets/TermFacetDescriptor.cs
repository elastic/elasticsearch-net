using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest.DSL
{
  public interface IFacetDescriptor
  {

  }
  public enum FacetOrder
  {
    count = 0,
    term,
    reverse_count,
    reverse_term
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
    [JsonConverter(typeof(StringEnumConverter))]
    [JsonProperty(PropertyName = "order")]
    internal FacetOrder? _FacetOrder { get; set; }
    [JsonProperty(PropertyName = "all_terms")]
    internal bool? _AllTerms { get; set; }
    [JsonProperty(PropertyName = "exclude")]
    internal IEnumerable<string> _Exclude { get; set; }


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
    public TermFacetDescriptor Order(FacetOrder order)
    {
      this._FacetOrder = order;
      return this;
    }
    public TermFacetDescriptor Exclude(params string[] args)
    {
      this._Exclude = args;
      return this;
    }
    public TermFacetDescriptor AllTerms()
    {
      this._AllTerms = true;
      return this;
    }
  }
}
