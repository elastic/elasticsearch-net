using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject]
	public class ClusterRerouteDecision
	{
		[JsonProperty("decider")]
		public string Decider { get; set; }

		[JsonProperty("decision")]
		public string Decision { get; set; }

		[JsonProperty("explanation")]
		public string Explanation { get; set; }
	}
}
