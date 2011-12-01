using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace ElasticSearch.Client.Domain
{
	[JsonObject]
	public class Stats
	{
		[JsonProperty(PropertyName = "primaries")]
		public StatsContainer Primaries { get; set; }
		[JsonProperty(PropertyName = "total")]
		public StatsContainer Total { get; set; }
	}
}
