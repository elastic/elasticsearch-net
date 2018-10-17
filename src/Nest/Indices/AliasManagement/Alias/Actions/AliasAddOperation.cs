using Newtonsoft.Json;

namespace Nest
{
	public class AliasAddOperation
	{
		[JsonProperty("index")]
		public IndexName Index { get; set; }

		[JsonProperty("alias")]
		public string Alias { get; set; }

		[JsonProperty("filter")]
		public QueryContainer Filter { get; set; }

		[JsonProperty("routing")]
		public string Routing { get; set; }

		[JsonProperty("index_routing")]
		public string IndexRouting { get; set; }

		[JsonProperty("search_routing")]
		public string SearchRouting { get; set; }

		/// <summary>
		/// If an alias points to multiple indices elasticsearch will reject the write operations
		/// unless one is explicitly marked with as the write alias using this property.
		/// </summary>
		[JsonProperty("is_write_index")]
		public bool? IsWriteIndex { get; set; }
	}
}
