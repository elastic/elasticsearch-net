using Newtonsoft.Json;

namespace Nest
{
	public class FilterFacet : Facet
	{
		[JsonProperty(PropertyName = "count")]
		public long Count { get; internal set; }
	}
}
