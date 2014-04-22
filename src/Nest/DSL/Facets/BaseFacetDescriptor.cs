using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.Resolvers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Linq.Expressions;
using Elasticsearch.Net;

namespace Nest
{
	public class BaseFacetDescriptor<TFacetDescriptor, T> : IFacetDescriptor<T> 
		where TFacetDescriptor : BaseFacetDescriptor<TFacetDescriptor, T> 
		where T : class 
	{
		bool? IFacetDescriptor.IsGlobal { get; set; }
		public TFacetDescriptor Global(bool global = true)
		{
			((IFacetDescriptor)this).IsGlobal = global;
			return (TFacetDescriptor) this;
		}

		BaseFilterDescriptor IFacetDescriptor.FacetFilterDescriptor { get; set; }
		public TFacetDescriptor FacetFilter(Func<FilterDescriptorDescriptor<T>, BaseFilterDescriptor> facetFilter)
		{
			facetFilter.ThrowIfNull("facetFilter");
			var filter = facetFilter(new FilterDescriptorDescriptor<T>());
			if (filter.IsConditionless)
				filter = null;
			((IFacetDescriptor) this).FacetFilterDescriptor = filter;
			return (TFacetDescriptor) this;
		}

		string IFacetDescriptor.Scope { get; set;}
		public TFacetDescriptor Scope(string scope)
		{
			((IFacetDescriptor)this).Scope = scope;
			return (TFacetDescriptor) this;
		}
		
		PropertyPathMarker IFacetDescriptor.Nested { get; set; }
		public TFacetDescriptor Nested(string nested)
		{
			((IFacetDescriptor)this).Nested = nested;
			return (TFacetDescriptor) this;
		}
		public TFacetDescriptor Nested(Expression<Func<T, object>> objectPath)
		{
			((IFacetDescriptor)this).Nested = objectPath;
			return (TFacetDescriptor) this;
		}
	}
}
