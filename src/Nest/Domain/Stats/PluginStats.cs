using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject]
	public class PluginStats
	{
		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("version")]
		public string Version { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }

		[JsonProperty("url")]
		public string Url { get; set; }

		[JsonProperty("jvm")]
		public bool Jvm { get; set; }

		[JsonProperty("site")]
		public bool Site { get; set; }
	}
}
