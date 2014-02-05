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
	//TODO curiously inject this
	public partial class BaseFacetDescriptor<T> 
		: IFacetDescriptor<T> 
		where T : class 
	{
		internal bool? _IsGlobal { get; set; }
		public BaseFacetDescriptor<T> Global() 
		{
			throw new NotImplementedException("Cannot call Base directly");
		}

		internal BaseFilter _FacetFilter { get; set; }
		public BaseFacetDescriptor<T> FacetFilter(Func<FilterDescriptor<T>, FilterDescriptor<T>> facetFilter)
		{
			throw new NotImplementedException("Cannot call Base directly");
		}

		internal string _Scope { get; set;}
		public BaseFacetDescriptor<T> Scope(string scope)
		{
			throw new NotImplementedException("Cannot call Base directly");
		}
		internal PropertyPathMarker _Nested { get; set; }
		public BaseFacetDescriptor<T> Nested(string nested)
		{
			throw new NotImplementedException("Cannot call Base directly");
		}
	}
}
