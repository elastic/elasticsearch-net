using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest_5_2_0
{
	public class RoutingNodesState
	{
		[JsonProperty("unassigned")]
		public IReadOnlyCollection<RoutingShard> Unassigned { get; internal set; }

		[JsonProperty("nodes")]
		public IReadOnlyDictionary<string, List<RoutingShard>> Nodes { get; internal set; }
	}
}
