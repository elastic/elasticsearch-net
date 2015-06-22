using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject]
	public class ClusterRerouteExplanation
	{
		[JsonProperty("command")]
		public string Command { get; set; }
		
		[JsonProperty("parameters")]
		public ClusterRerouteParameters Parameters { get; set; }

		[JsonProperty("decisions")]
		public IEnumerable<ClusterRerouteDecision> Decisions { get; set; }
	}
}
