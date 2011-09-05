using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace ElasticSearch.Client
{
	[JsonObject]
	public class HistogramFacet : Facet
	{
		[JsonProperty(PropertyName = "key")]
		public new string Key { get; internal set; }
	}
}
