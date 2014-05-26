using Nest.Resolvers;

namespace Nest
{
	public interface IFacetRequest
	{
		bool? Global { get; set; }
		FilterContainer FacetFilter { get; set; }
		string Scope { get; set; }
		PropertyPathMarker Nested { get; set; }
	}
	public abstract class FacetRequest : IFacetRequest
	{
		public bool? Global { get; set; }
		public FilterContainer FacetFilter { get; set; }
		public string Scope { get; set; }
		public PropertyPathMarker Nested { get; set; }
	}
}