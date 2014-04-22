using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.Resolvers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Linq.Expressions;

namespace Nest
{
  public interface IFacetDescriptor<out T> : IFacetDescriptor
  {

  }
  public interface IFacetDescriptor
  {
	  bool? IsGlobal { get; set; }
	  BaseFilterDescriptor FacetFilterDescriptor { get; set; }
	  string Scope { get; set; }
	  PropertyPathMarker Nested { get; set; }
  }
}
