using Nest.Resolvers;

namespace Nest
{
	public interface IFacetRequest
	{
		bool? Global { get; set; }
		IFilterContainer FacetFilter { get; set; }
		PropertyPathMarker Nested { get; set; }
	}
	public abstract class FacetRequest : IFacetRequest
	{
		public bool? Global { get; set; }
		public IFilterContainer FacetFilter { get; set; }
		public PropertyPathMarker Nested { get; set; }
	}
}