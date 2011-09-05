using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace ElasticSearch.Client
{
	[JsonObject]
	public class FacetMetaData
	{
		[JsonProperty(PropertyName = "_type")]
		public string Type { get; internal set; }
		[JsonProperty(PropertyName = "missing")]
		public int Missing { get; internal set; }

		public string Field { get; internal set; }

		public List<Facet> Facets { get; internal set; }
	}

}
