using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public class RoutingNodesState
	{
		[DataMember(Name ="nodes")]
		public IReadOnlyDictionary<string, List<RoutingShard>> Nodes { get; internal set; }

		[DataMember(Name ="unassigned")]
		public IReadOnlyCollection<RoutingShard> Unassigned { get; internal set; }
	}
}
