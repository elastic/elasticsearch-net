using System;
using System.Collections.Generic;
using System.Linq;
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
}
