using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Linq.Expressions;

namespace Nest.DSL
{
  public partial class BaseFacetDescriptor<T> : IFacetDescriptor where T : class 
  {
    internal bool? _IsGlobal { get; set; }
    public BaseFacetDescriptor<T> Global() 
    {
      throw new NotImplementedException("Cannot call Base directly");
    }

    internal FilterDescriptor<T> _FacetFilter { get; set; }
    public virtual BaseFacetDescriptor<T> FacetFilter(
      Func<FilterDescriptor<T>, FilterDescriptor<T>> facetFilter
    )
    {
      throw new NotImplementedException("Cannot call Base directly");
    }

    internal string _Scope { get; set;}
		public virtual BaseFacetDescriptor<T> Scope(string scope)
    {
      throw new NotImplementedException("Cannot call Base directly");
    }
    internal string _Nested { get; set; }
		public virtual BaseFacetDescriptor<T> Nested(string nested)
    {
      throw new NotImplementedException("Cannot call Base directly");
    }
  }
}
