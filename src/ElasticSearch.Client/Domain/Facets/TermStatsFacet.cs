using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace ElasticSearch.Client
{
	[JsonObject]
	public class TermStatsFacet : Facet
	{
		[JsonProperty(PropertyName = "term")]
		public string Term { get; internal set; }
		[JsonProperty(PropertyName = "min")]
		public float? Min { get; internal set; }
		[JsonProperty(PropertyName = "max")]
		public float? Max { get; internal set; }
		[JsonProperty(PropertyName = "total")]
		public float Total { get; internal set; }
		[JsonProperty(PropertyName = "mean")]
		public float? Mean { get; internal set; }
	}

}
