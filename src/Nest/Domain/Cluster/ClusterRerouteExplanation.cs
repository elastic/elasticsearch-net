using Newtonsoft.Json;
using System.Collections.Generic;

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
