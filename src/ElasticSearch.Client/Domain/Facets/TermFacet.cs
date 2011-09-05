using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace ElasticSearch.Client
{
	[JsonObject]
	public class TermFacet : Facet
	{
		[JsonProperty(PropertyName = "term")]
		public string Term { get; internal set; }
	}
}
