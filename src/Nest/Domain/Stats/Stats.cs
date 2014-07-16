using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
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
