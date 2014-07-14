using Nest.Resolvers;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// An object to describe an indexed geoshape
	/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/query-dsl-geo-shape-filter.html
	/// </summary>
	public class GeoIndexedShape
	{
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("type")]
		public TypeNameMarker Type { get; set; }

		[JsonProperty("index")]
		public IndexNameMarker Index { get; set; }

		[JsonProperty("path")]
		public PropertyPathMarker Field { get; set; }
	}
}