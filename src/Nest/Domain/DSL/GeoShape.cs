using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// An object to describe a geoshape
	/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/query-dsl-geo-shape-filter.html
	/// </summary>
	public class GeoShape
	{
		[JsonProperty("type")]
		public string Type { get; set; }

		[JsonProperty("coordinates")]
		public object Coordinates { get; set; }

		[JsonProperty("radius")]
		public string Radius { get; set; }
	}
}
