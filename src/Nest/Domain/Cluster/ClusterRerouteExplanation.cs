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
		public string Comand { get; set; }
		
		[JsonProperty("parameters")]
		public ClusterRerouteParameters Parameters { get; set; }

		[JsonProperty("decisions")]
		public IEnumerable<ClusterRerouteDecision> Descisions { get; set; }
	}
}
