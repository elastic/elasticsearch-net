using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject]
	public class ClusterRerouteNode
	{
		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("transport_address")]
		public string TransportAddress { get; set; }

		[JsonProperty("attributes")]
		public object Attributes { get; set; }
	}
}
