using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace ElasticSearch.Client
{
	[JsonObject]
	public class Facet
	{
		public string Key { get; internal set; }

		public bool Global { get; internal set; }
		[JsonProperty(PropertyName = "count")]
		public int Count { get; internal set; }
	}
}
