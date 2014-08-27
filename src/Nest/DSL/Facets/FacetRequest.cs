using System;

namespace Nest
{
	[Obsolete("Facets are deprecated and will be removed in a future release. You are encouraged to migrate to aggregations instead.")]
	public interface IFacetRequest
	{
		bool? Global { get; set; }
		IFilterContainer FacetFilter { get; set; }
		PropertyPathMarker Nested { get; set; }
	}
	[Obsolete("Facets are deprecated and will be removed in a future release. You are encouraged to migrate to aggregations instead.")]
	public abstract class FacetRequest : IFacetRequest
	{
		public bool? Global { get; set; }
		public IFilterContainer FacetFilter { get; set; }
		public PropertyPathMarker Nested { get; set; }
	}
}