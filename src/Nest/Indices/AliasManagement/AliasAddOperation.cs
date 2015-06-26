using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	public class AliasAddOperation
	{
		[JsonProperty("index")]
		public IndexNameMarker Index { get; set; }

		[JsonProperty("alias")]
		public string Alias { get; set; }

		[JsonProperty("filter")]
		[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeConverter<QueryContainer>, CustomJsonConverter>))]
		public QueryContainer QueryDescriptor { get; set; }

		[JsonProperty("routing")]
		public string Routing { get; set; }

		[JsonProperty("index_routing")]
		public string IndexRouting { get; set; }

		[JsonProperty("search_routing")]
		public string SearchRouting { get; set; }
	}
}