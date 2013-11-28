using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.Resolvers;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// An object to describe a geoshape vetor
	/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/query-dsl-geo-shape-filter.html
	/// </summary>
	public class GeoShapeVector
	{
		[JsonProperty("type")]
		public string Type { get; set; }

		[JsonProperty("coordinates")]
		public IEnumerable<IEnumerable<double>> Coordinates { get; set; }
	}


	/// <summary>
	/// An object to describe a geoshape vetor
	/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/query-dsl-geo-shape-filter.html
	/// </summary>
	public class GeoIndexedShapeVector
	{
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("type")]
		public TypeNameMarker Type { get; set; }

		[JsonProperty("index")]
		public IndexNameMarker Index { get; set; }

		[JsonProperty("shape_field_name")]
		public string Field { get; set; }
	}
}
