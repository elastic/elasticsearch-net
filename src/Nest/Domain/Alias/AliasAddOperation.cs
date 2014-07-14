using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// 
	/// </summary>
	public class AliasAddOperation
	{
		[JsonProperty("index")]
		public IndexNameMarker Index { get; set; }
		[JsonProperty("alias")]
		public string Alias { get; set; }
		[JsonProperty("filter")]
		public FilterContainer FilterDescriptor { get; set; }
		[JsonProperty("routing")]
		public string Routing { get; set; }
		[JsonProperty("index_routing")]
		public string IndexRouting { get; set; }
		[JsonProperty("search_routing")]
		public string SearchRouting { get; set; }
	}
}