using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	public interface IFacetRequest
	{
		bool? Global { get; set; }
		[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeConverter<FilterContainer>, CustomJsonConverter>))]
		IFilterContainer FacetFilter { get; set; }
		PropertyPathMarker Nested { get; set; }
	}
	public abstract class FacetRequest : IFacetRequest
	{
		public bool? Global { get; set; }
		[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeConverter<FilterContainer>, CustomJsonConverter>))]
		public IFilterContainer FacetFilter { get; set; }
		public PropertyPathMarker Nested { get; set; }
	}
}