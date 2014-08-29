using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject]
	public class ClusterRerouteState
	{
		[JsonProperty("version")]
		public int Version { get; set; }

		[JsonProperty("master_node")]
		public string MasterNode { get; set; }

		[JsonProperty("nodes")]
		public IDictionary<string, ClusterRerouteNode> Nodes { get; set; }
	}
}
