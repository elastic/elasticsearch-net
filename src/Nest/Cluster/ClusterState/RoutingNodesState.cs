using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public class RoutingNodesState
	{
		[JsonProperty("nodes")]
		public IReadOnlyDictionary<string, List<RoutingShard>> Nodes { get; internal set; }

		[JsonProperty("unassigned")]
		public IReadOnlyCollection<RoutingShard> Unassigned { get; internal set; }
	}
}
