using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace ElasticSearch.Client
{
	[JsonObject]
	public class DateHistogramFacet : Facet
	{
		[JsonProperty(PropertyName = "time")]
		public DateTime Time { get; internal set; }
	}
}
